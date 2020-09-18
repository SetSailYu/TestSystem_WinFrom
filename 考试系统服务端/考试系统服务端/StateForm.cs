using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using DataModel;



namespace 考试系统服务端
{
    public partial class StateForm : Form
    {
        public StateForm()
        {
            InitializeComponent();
            this.listView1.View = System.Windows.Forms.View.Details;//显示添加的列,在控件中的显示方式 Details
            this.listView1.Columns.Add("准考证", (this.listView1.Width / 100) * 18, HorizontalAlignment.Center);//设置列标题，设置列宽度，设置列的对齐方式
            this.listView1.Columns.Add("学号", (this.listView1.Width / 100) * 23, HorizontalAlignment.Center);
            this.listView1.Columns.Add("姓名", (this.listView1.Width / 100) * 23, HorizontalAlignment.Center);
            this.listView1.Columns.Add("班级", (this.listView1.Width / 100) * 23, HorizontalAlignment.Center);
            this.listView1.Columns.Add("IP地址", (this.listView1.Width / 100) * 25, HorizontalAlignment.Center);
            this.listView1.Columns.Add("考试状态", (this.listView1.Width / 100) * 20, HorizontalAlignment.Center);

            textBox1.Text = Convert.ToString(time / 1000);
            try
            {
                ShowData();
                Timer();//初始化数据完毕后，开始执行定时循环方法
            }
            catch
            {
                this.Close();
            }
            
        }

        private int time = 30000;
        System.Timers.Timer t;//= new System.Timers.Timer(time);//实例化Timer类，设置间隔时间为1000毫秒；
        /// <summary>
        /// 显示加载的数据表，即连接数据库重新绑定student表内的数据，并显示
        /// </summary>
        private void ShowData()
        {
            SqlOperation sqlOperation = new SqlOperation();//连接数据库
            sqlOperation.LoadData("student");//加载数据库到本地
            sqlOperation.OffData();//断开连接

            this.listView1.BeginUpdate();//数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            for (int i = 0; i < sqlOperation.MyDataSet.Tables[0].Rows.Count; i++)   //每行添加数据
            {
                ListViewItem lvi = new ListViewItem();
                //lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标
                lvi.Text = sqlOperation.MyDataSet.Tables[0].Rows[i]["zkz"].ToString();//第1列
                lvi.SubItems.Add(sqlOperation.MyDataSet.Tables[0].Rows[i]["xh"].ToString());//第2列
                lvi.SubItems.Add(sqlOperation.MyDataSet.Tables[0].Rows[i]["xm"].ToString());//第3列
                lvi.SubItems.Add(sqlOperation.MyDataSet.Tables[0].Rows[i]["bj"].ToString());//第4列
                lvi.SubItems.Add(sqlOperation.MyDataSet.Tables[0].Rows[i]["ip"].ToString());//第5列
                lvi.SubItems.Add(sqlOperation.MyDataSet.Tables[0].Rows[i]["state"].ToString());//第6列
                this.listView1.Items.Add(lvi);
            }
            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。

            sqlOperation.Empty();//清空本地数据库
        }
        /// <summary>
        /// 定时方法
        /// </summary>
        private void Timer()
        {
            t = new System.Timers.Timer(time);//实例化Timer类，设置间隔时间为1000毫秒；
            t.Elapsed += new System.Timers.ElapsedEventHandler(OrderTimer_Tick);//到时间的时候执行事件； 
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)； 
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件； 
        }
        /// <summary>
        /// 定时执行的方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void OrderTimer_Tick(object source, System.Timers.ElapsedEventArgs e)
        {
            this.listView1.Items.Clear();  //只移除listView的所有的项。
            ShowData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            t.Enabled = false;//关闭计时器
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool falg = true;
            if (e.KeyChar == (Char)13)//回车
            {
                try
                {
                    time = Convert.ToInt32(textBox1.Text.Trim());
                    time = time * 1000;
                }
                catch 
                {
                    MessageBox.Show("输入格式错误！只允许输入数字！","提示" );
                    falg = false;
                }
                if (falg)
                {
                    this.listView1.Focus();//为控件设置输入焦点
                    t.Enabled = false;//关闭计时器
                    Timer();
                }
                
            }

        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);   //采用特定颜色绘制标题列,这里我用的灰色
                e.DrawText();   //采用默认方式绘制标题文本
            }
            else if (e.ColumnIndex == 1)
            {
                e.Graphics.FillRectangle(Brushes.DarkGray, e.Bounds);   //采用特定颜色绘制标题列,这里我用的灰色
                e.DrawText();   //采用默认方式绘制标题文本
            }
            else if (e.ColumnIndex == 2)
            {
                e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);   //采用特定颜色绘制标题列,这里我用的灰色
                e.DrawText();   //采用默认方式绘制标题文本
            }
            else if (e.ColumnIndex == 3)
            {
                e.Graphics.FillRectangle(Brushes.DarkGray, e.Bounds);   //采用特定颜色绘制标题列,这里我用的灰色
                e.DrawText();   //采用默认方式绘制标题文本
            }
            else if (e.ColumnIndex == 4)
            {
                e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);   //采用特定颜色绘制标题列,这里我用的灰色
                e.DrawText();   //采用默认方式绘制标题文本
            }
            else if (e.ColumnIndex == 5)
            {
                e.Graphics.FillRectangle(Brushes.DarkGray, e.Bounds);   //采用特定颜色绘制标题列,这里我用的灰色
                e.DrawText();   //采用默认方式绘制标题文本
            }

        }

        private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true; //采用系统默认方式绘制项
        }

        private void StateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (t.Enabled)
                t.Enabled = false;//关闭计时器
            GC.Collect();
        }

        private void StateForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(95, 151, 198);
            this.listView1.BackColor = Color.FromArgb(95, 151, 198);
            //this.BackColor = Color.FromArgb(65, 204, 212, 230);
        }
    }
}
