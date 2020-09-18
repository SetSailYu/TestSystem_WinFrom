using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Seri;
using System.Data.SqlClient;
using DataModel;

namespace 考试系统服务端
{
    public partial class ServerForm : Form
    {
        public ServerForm(string uname,string ustate)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            udpClient = new UdpClient(8000);//监听端口

            listView1.View = View.LargeIcon;//LargeIcon模式
            listView1.LargeImageList = this.imageList1;//绑定imageList控件中的图片

            int num;
            if (ustate == "管理员")
                num = 5;
            else
                num = 2;

            listView1.BeginUpdate();//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            for (int i = 0; i < num; i++)
            {
                ListViewItem lvi = new ListViewItem();

                lvi.ImageIndex = i;

                lvi.Text = listFunction[i];

                listView1.Items.Add(lvi);
            }
            listView1.EndUpdate();//结束数据处理，UI界面一次性绘制。

            label1.Text = "欢迎登录考试系统！" + uname + "，您当前的权限为：" + ustate;

            this.label1.BackColor = Color.FromArgb(95, 151, 198);
        }

        private string[] listFunction = new string[] { "考试状态", "改卷", "学生信息", "题库","管理员" };
        

        #region  //开启/关闭 服务器
        UdpClient udpClient = null;//监听UDP连接
        UdpClient udpSend = null;//广播UDP连接
        Thread loginthread = null;//监听线程
        Thread sendthread = null;//广播线程
        ServerIP serverip = null;//服务器IP地址
        Student ks = null;//用于接收传输Student信息
        SqlOperation sqlOperation = null;//用于对数据库的操作

        /// <summary>
        /// 获取本机IPv4地址，返回 String 类型的ipv4地址 和 int型连接端口号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void  IPv4()
        {
            string ip = "127.0.0.1";
            string name = Dns.GetHostName();
            IPAddress[] ips = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ips)
            {
                if (!ipa.IsIPv6SiteLocal)
                {
                    ip = ipa.ToString();
                }
            }
            serverip = new ServerIP(ip,8000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;//关闭 开启服务器 按钮
            button2.Enabled = true;//开启 关闭服务器 按钮
            IPv4();

            sendthread = new Thread(new ThreadStart(SendIpToClient));//初始化广播线程
            sendthread.IsBackground = true;//设置为后台线程
            sendthread.Start();//广播启动

            loginthread = new Thread(new ThreadStart(listenlogin));//初始化监听线程
            loginthread.IsBackground = true;//设置为后台线程
            loginthread.Start();//监听启动
        }
        /// <summary>
        /// 监听过程（8000端口）
        /// </summary>
        private void listenlogin()
        {
            IPEndPoint iep = null;
            while (true)
            {
                byte[] bytes = udpClient.Receive(ref iep);//客服端没有send的时候，服务器端进程就停在这里，直到客服端有执行send操作
                ks = new SeriClass().DeSerializeBinary(new MemoryStream(bytes)) as Student;
                switch (ks.Cmd)
                {
                    #region  //登录处理
                    case Student.Way.LogIn:
                        {
                            try
                            {
                                sqlOperation = new SqlOperation();//开启数据库连接
                                sqlOperation.LoadData("student", "zkz", ks.Zkz);//查询数据库
                                bool login = true;
                                if (sqlOperation.MyDataSet.Tables[0].Rows.Count != 0)//如果存在该考生
                                {
                                    if (sqlOperation.MyDataSet.Tables[0].Rows[0]["state"].ToString() == "未登录")
                                    {
                                        ks.State = "已登录";
                                    }
                                    else//考生已经登录过一次的
                                    {
                                        ks.State = "登入过";
                                        login = false;
                                    }
                                    ks.Xh = sqlOperation.MyDataSet.Tables[0].Rows[0]["xh"].ToString();
                                    ks.Xm = sqlOperation.MyDataSet.Tables[0].Rows[0]["xm"].ToString();
                                    ks.Bj = sqlOperation.MyDataSet.Tables[0].Rows[0]["bj"].ToString();
                                    ks.Ip = iep.Address.ToString();
                                }
                                else//如果找不到该考生
                                {
                                    ks.Zkz = null;
                                    login = false;
                                }
                                if (login)//可以登录
                                {
                                    DataRow[] dr = sqlOperation.MyDataSet.Tables[0].Select("zkz= '" + ks.Zkz + "'");//查询数据
                                    for (int i = 0; i < dr.Length; i++)
                                    {
                                        dr[i]["state"] = ks.State;
                                        dr[i]["ip"] = ks.Ip;
                                    }
                                    sqlOperation.UploadData();//上传编辑后的数据库
                                }

                                byte[] bData = new SeriClass().SerializeBinary(ks).ToArray();
                                udpClient.Send(bData, bData.Length, iep);//发送给客户端
                            }
                            catch
                            {
                                MessageBox.Show("无法访问数据库！");
                            }
                            finally
                            {
                                sqlOperation.OffData();
                            }
                            break;
                        }
                    #endregion   
                    #region  //抽题处理
                    case Student.Way.Extracting:
                        {
                            try
                            {
                                sqlOperation = new SqlOperation();//开启数据库连接
                                sqlOperation.LoadData("tk");//查询数据库tk表

                                List<TK> Tm = new List<TK>();
                                for (int i = 0; i < sqlOperation.MyDataSet.Tables[0].Rows.Count; i++)
                                {
                                    int th = Convert.ToInt32(sqlOperation.MyDataSet.Tables[0].Rows[i]["th"]);
                                    string tmnr = sqlOperation.MyDataSet.Tables[0].Rows[i]["tmnr"].ToString();
                                    string tmzj = sqlOperation.MyDataSet.Tables[0].Rows[i]["tmzj"].ToString();
                                    string answer1 = sqlOperation.MyDataSet.Tables[0].Rows[i]["answer1"].ToString();
                                    string answer2 = sqlOperation.MyDataSet.Tables[0].Rows[i]["answer2"].ToString();
                                    string answer3 = sqlOperation.MyDataSet.Tables[0].Rows[i]["answer3"].ToString();
                                    string answer4 = sqlOperation.MyDataSet.Tables[0].Rows[i]["answer4"].ToString();
                                    string result = sqlOperation.MyDataSet.Tables[0].Rows[i]["result"].ToString();
                                    TK record = new TK(th, tmnr, tmzj, answer1, answer2, answer3, answer4, result);
                                    Tm.Add(record);
                                }

                                byte[] bData = new SeriClass().SerializeBinary(Tm).ToArray();
                                udpClient.Send(bData, bData.Length, iep);//发送给客户端
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("错误：" + ex);
                            }
                            finally
                            {
                                sqlOperation.OffData();
                            }
                            break;
                        }
                    #endregion
                    #region  //交卷处理
                    case Student.Way.PutIn:
                        {
                            try
                            {
                                sqlOperation = new SqlOperation();//开启数据库连接

                                sqlOperation.LoadData("ksresult", "ksresult");//查询数据库ksresult表，并保存在本地
                                for (int i = 0; i < ks.Result.Count; i++)
                                {
                                    DataRow dr = sqlOperation.MyDataSet.Tables[0].NewRow();//根据本地数据库中ksresult表结构，复制一条空行
                                    dr.BeginEdit();//开始编辑行
                                    dr["zkz"] = ks.Result[i].Zkz;
                                    dr["ktxh"] = Convert.ToInt32(ks.Result[i].Ktxh);
                                    dr["tkxh"] = Convert.ToInt32(ks.Result[i].Tkth);
                                    dr["xsanswer"] = ks.Result[i].Xsanswer;
                                    dr["tkanswer"] = ks.Result[i].Tkanswer;
                                    sqlOperation.MyDataSet.Tables[0].Rows.Add(dr); //将编辑的行添加到本地数据库中
                                    dr.EndEdit();//结束编辑行
                                }
                                sqlOperation.UploadData("ksresult");//上传编辑后的数据库

                                sqlOperation.LoadData("student", "student");
                                for (int j = 0; j < sqlOperation.MyDataSet.Tables[1].Rows.Count;j++)
                                {
                                    if (sqlOperation.MyDataSet.Tables[1].Rows[j]["state"].ToString().Trim() == "已登录")
                                    {
                                        sqlOperation.MyDataSet.Tables[1].Rows[j]["state"] = "已交卷";
                                    }
                                }
                                sqlOperation.UploadData("student");//上传编辑后的数据库

                                byte[] bData = Encoding.UTF8.GetBytes("交卷成功！");
                                udpClient.Send(bData, bData.Length, iep);//发送给客户端
                            }
                            catch(Exception ex)
                            {
                                byte[] bData = Encoding.UTF8.GetBytes("交卷失败，请重新提交！");
                                udpClient.Send(bData, bData.Length, iep);
                                MessageBox.Show("提示" + ex);
                            }
                            finally
                            {
                                sqlOperation.Empty();
                                sqlOperation.OffData();
                            }
                            break;
                        }
                    default:
                        break;
                        #endregion
                }
            }
        }
        /// <summary>
        /// 广播过程（10000端口）
        /// </summary>
        private void SendIpToClient()
        {
            byte[] bData = new SeriClass().SerializeBinary(serverip).ToArray();
            udpSend = new UdpClient();
            while (true)
            {
                IPEndPoint iep = null;
                try
                {
                    iep = new IPEndPoint(IPAddress.Broadcast, 10000);
                    udpSend.Send(bData, bData.Length, iep);
                }
                catch
                {
                    break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;//开启 开启服务器 按钮
            button2.Enabled = false;//关闭 关闭服务器 按钮
            if (loginthread != null) loginthread.Abort();
            if (sendthread != null) sendthread.Abort();
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //终止线程
            if (loginthread != null) loginthread.Abort();
            if (sendthread != null) sendthread.Abort();
            Application.Exit();
        }

        #endregion 

     
        #region  //点击功能列表项
        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices[0] == 0)
            {

                textBox1.Text = "考试状态";
                StateForm f = new StateForm();
                ShowForm(f);
            }
            else if (listView1.SelectedIndices[0] == 1)
            {
                textBox1.Text = "改卷";
                CorrectForm f = new CorrectForm();
                ShowForm(f);
            }
            else if (listView1.SelectedIndices[0] == 2)
            {
                textBox1.Text = "学生信息";
                StudentInformationForm f = new StudentInformationForm();
                ShowForm(f);
            }
            else if (listView1.SelectedIndices[0] == 3)
            {
                textBox1.Text = "题库";
                TKForm f = new TKForm();
                ShowForm(f);
            }
            else if (listView1.SelectedIndices[0] == 4)
            {
                textBox1.Text = "管理员";
                UserForm f = new UserForm();
                ShowForm(f);
            }
        }
        #endregion

        /// <summary>
        /// 将子窗体载入panel控件内
        /// </summary>
        /// <param name="form1">子窗体</param>
        private void ShowForm(Form form1)
        {
            panel1.Controls.Clear();// 移除 panel1内的所有控件

            form1.TopLevel = false;//指示子窗体非顶级窗体

            form1.FormBorderStyle = FormBorderStyle.None;//隐藏子窗体边框（去除最小花，最大化，关闭等按钮）
            
            form1.Dock = DockStyle.Fill;//填充

            form1.Parent = panel1;//指定子窗体的父容器为panel1

            panel1.Controls.Add(form1);//将子窗体载入panel

            form1.Show();
        }

        
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.FromArgb(65, 204, 212, 230);
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {

        }
    }
}
