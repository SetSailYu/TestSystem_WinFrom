using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//以下后续添加的命名空间
using DataModel;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using Seri;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace ExamUIL
{
    //UIL层主要是用于跟用户进行交互式操作

    public partial class login : Form
    {
        /// <summary>
        /// 模板里的一个类，里面包含服务器ip跟端口
        /// </summary>
        ServerIP sip = null; 
        /// <summary>
        /// 用来监听服务器线程
        /// </summary>
        Thread listen = null; 
        /// <summary>
        /// 模板里面的考生类，传递准考证跟命令
        /// </summary>
        Student ks = null;
        DateTime dt = new DateTime(); //时间类

        public login()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        bool beginMove = false;//初始化鼠标位置
        int currentXPosition;
        int currentYPosition;

        private void Login_Load(object sender, EventArgs e)
        {

            

            labelTime.Text = DateTime.Now.ToString();//初始时间
            timer1.Enabled = true;


            dw1.validStr = null;
            dw1.GetValidateCode();
            pictureBox1.Image = dw1.GetImgWithValidateCode();

            //窗口加载的时候开始启动监听线程
            textBoxzkz.Focus(); //默认的输入位置在准考证框           
            listen = new Thread(new ThreadStart(ListEnip));//新建线程，用来监听

            int StartTime = dt.Second;

            listen.IsBackground = true;  //后台线程                    
            listen.Start();
            btnStar.Enabled = false; //默认情况下，抽提按钮关闭
            textBoxxh.Enabled = false;
            textBoxxm.Enabled = false;
            textBoxbj.Enabled = false;

            int EndTime = dt.Second;
            if (EndTime >= StartTime + 5)
            {
                MessageBox.Show("线程堵塞，请重新打开程序", "提示");
            }
        }
        /// <summary>
        /// 监听方法
        /// </summary>
        public void ListEnip()
        {
           
            //以下是监听端口方法的内容
            UdpClient udpreceive = new UdpClient(10000); //绑定端口
            IPEndPoint tempip = null; //一个容器，用来存ip跟端口
           
            while (true)
            {
                try
                {
                  
                    
                    byte[] bytes = udpreceive.Receive(ref tempip);//接受服务器传来的信息,线程自动堵塞
                    sip = new Seri.SeriClass().DeSerializeBinary(new MemoryStream(bytes)) as ServerIP;

                  
                    //反序列化得到服务器ip，端口
                }
                catch
                {
                    break;
                }
                finally
                {
                    udpreceive.Close();
                }
            }
         
           
        }     

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (Char)13) //按回车
            {
                try
                {
                    IPAddress serverip = IPAddress.Parse(sip.Serverip); //将ip转化为ipaddress格式
                    ks = new Student(textBoxzkz.Text, Student.Way.LogIn);//将准考证号跟登入命令赋值给ks
                    ks = ExamBLL.Transaction.GetUser(ks, serverip, sip.Port);//传递给BLL层获得考生信息
                    if (ks.Zkz != null) //从服务器传递回来的信息赋值文本框
                    {
                        textBoxxh.Text = ks.Xh;
                        textBoxxm.Text = ks.Xm;
                        textBoxbj.Text = ks.Bj;
                        textBoxzkz.Text = ks.Zkz;
                        btnStar.Enabled = true;
                       
                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("服务器未找到", "提示");
                }
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
          
            if ((textBoxYzm.Text).ToLower() == dw1.validStr)
            {
                if (ks.State == "登入过")
                {
                    MessageBox.Show("用户已在考试", "提示");
                }
                else
                {
                    if ((textBoxYzm.Text).ToLower() == dw1.validStr)
                    {
                        ks.Cmd = Student.Way.Extracting; //将考生的命令转化为抽题，传递給抽屉UIL层    
                        chouti f = new chouti(ks.Xm, ks, IPAddress.Parse(sip.Serverip), sip.Port);
                        f.Show();
                        this.Hide();
                    }
                    else
                        MessageBox.Show("验证码不正确", "提示");
                }

            }
            else
                MessageBox.Show("验证码不正确", "提示");

            //姓名、ks、服务器ip、服务器端口号传递给抽题窗口          
           

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString();//显示时间
           
        }

        DrawValImg dw1 = new DrawValImg();
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
        }

        class DrawValImg
        {
            public string validStr = null;// 随机生成的字符串       
                                          // 产生指定个数的随机字符串
            public void GetValidateCode()
            {
                Random rd = new Random(); //创建随机数对象
                                          //产生由 charNum 个字母或数字组成的一个字符串            
                char code;
                int number;
                for (int i = 0; i < 4; i++)
                {
                    number = rd.Next();
                    if (number % 2 == 0)
                        code = (char)('0' + (char)(number % 10));
                    else
                        code = (char)('A' + (char)(number % 26));
                    validStr += code.ToString().ToLower();  //全部转化为小写
                }

            }
            // 由随机字符串，随机颜色背景，和随机线条产生的Image       
            public Bitmap GetImgWithValidateCode()//返回 Image
            {
                Bitmap bitMap = new Bitmap((int)((validStr.Length * 50.0)), 50); //创建位图对象   null;//声明一个位图对象
                Graphics gph = Graphics.FromImage(bitMap);//根据上面创建的位图对象创建绘图图面       null;//声明一个绘图画面            
                Random random = new Random();


                gph.Clear(Color.White); //设定验证码图片背景色

                for (int i = 0; i < 10; i++)//产生随机干扰线条
                {
                    Pen backPen = new Pen(Color.Gray, 2);
                    int x = random.Next(bitMap.Width);
                    int y = random.Next(bitMap.Height);
                    int x2 = random.Next(bitMap.Width);
                    int y2 = random.Next(bitMap.Height);
                    gph.DrawLine(backPen, x, y, x2, y2);//画直线,是一个几何图形
                }
                Font font = new Font("宋体", 27, (FontStyle.Bold | FontStyle.Italic));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, bitMap.Width, bitMap.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(bitMap.Width);
                    int y = random.Next(bitMap.Height);
                    bitMap.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                gph.DrawString(validStr, font, brush, 2, 2);
                //画图片的前景噪音点

                return bitMap;
            }
        }

        private void login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标
            }
           
        }

        private void login_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void login_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态
                currentYPosition = 0;
                beginMove = false;
            }
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

            dw1.validStr = null;
            dw1.GetValidateCode();
            pictureBox1.Image = dw1.GetImgWithValidateCode();
        }

        private void btnStar_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBoxYzm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (ks.State == "已登录")
                {
                    MessageBox.Show("用户已在考试", "提示");
                }
                else
                {
                    if ((textBoxYzm.Text).ToLower() == dw1.validStr)
                    {
                        ks.Cmd = Student.Way.Extracting; //将考生的命令转化为抽题，传递給抽屉UIL层    
                        chouti f = new chouti(ks.Xm, ks, IPAddress.Parse(sip.Serverip), sip.Port);
                        f.Show();
                        this.Hide();
                    }
                    else
                        MessageBox.Show("验证码不正确", "提示");
                }
            }
        }
    }
}
