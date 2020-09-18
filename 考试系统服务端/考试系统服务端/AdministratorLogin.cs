using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DataModel;

namespace 考试系统服务端
{
    public partial class AdministratorLogin : Form
    {
        string login;
        public AdministratorLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登录用户的权限
        /// </summary>
        public string ustate = null;
        /// <summary>
        /// 登录用户名
        /// </summary>
        public string uname = null;

        DrawValImg dw1 = new DrawValImg();//实例化验证码生成类

        #region   //创建一个DrawValImg随机生成验证码类
        class DrawValImg
        {
            /// <summary>
            /// 随机生成的字符串
            /// </summary>
            public string validStr = null;
            /// <summary>
            /// 产生指定个数的随机字符串
            /// </summary>
            /// <param name="num">指定个数</param>
            public void GetValidateCode(int num)
            {
                Random rd = new Random(); //创建随机数对象
                                          //产生由 charNum 个字母或数字组成的一个字符串            
                char code;
                int number;
                for (int i = 0; i < num; i++)
                {
                    number = rd.Next();
                    if (number % 2 == 0)
                        code = (char)('0' + (char)(number % 10));
                    else
                        code = (char)('A' + (char)(number % 26));
                    validStr += code.ToString().ToLower();  //全部转化为小写
                }

            }
            /// <summary>
            /// 由随机字符串，随机颜色背景，和随机线条产生的Image，返回 Image
            /// </summary>
            /// <returns>返回 Image</returns>
            public Bitmap GetImgWithValidateCode()
            {
                Bitmap bitMap = new Bitmap((int)((validStr.Length * 26.0)), 25); //创建位图对象   null;//声明一个位图对象
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
                Font font = new Font("宋体", 16, (FontStyle.Bold | FontStyle.Italic));
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


        #endregion

        /// <summary>
        /// 加载验证码生成事件
        /// </summary>
        private void LoadDrawValImg()
        {
            dw1.validStr = null;
            dw1.GetValidateCode(4);
            pictureBox1.Image = dw1.GetImgWithValidateCode();
        }

        private void AdministratorLogin_Load(object sender, EventArgs e)
        {
            textBox1.Focus();//设置输入焦点
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                textBox2.Focus();//设置输入焦点
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                textBox3.Focus();//设置输入焦点
                LoadDrawValImg();//生成验证码
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                button1.PerformClick();//调用button1的Click事件
                LoadDrawValImg();//生成验证码
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoadDrawValImg();//生成验证码
        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            if (dw1.validStr == textBox3.Text.ToString())//验证输入的验证码
            {
                SqlOperation SQL = new SqlOperation();
                try
                {
                    if (SQL.Query("select uname from userinfo where uname='" + textBox1.Text.Trim() + "'"))//验证用户名是否存在
                    {
                        if (SQL.Query("select upwd from userinfo where upwd='" + textBox2.Text.Trim() + "' and uname = '" + textBox1.Text.Trim() + "'"))//验证密码和用户名是否正确
                        {
                            SQL.LoadData("userinfo", "uname", textBox1.Text.Trim());//加载该登录用户的信息
                            SQL.OffData();//断开数据库连接
                            MessageBox.Show("登录成功！", "提示");
                            
                            ustate = SQL.MyDataSet.Tables[0].Rows[0]["ustate"].ToString().Trim();
                            uname = SQL.MyDataSet.Tables[0].Rows[0]["uname"].ToString().Trim();
                            SQL.Empty();//清空本地数据库
                            
                            Thread th = new Thread(delegate () { new ServerForm(uname,ustate).ShowDialog(); });

                            th.IsBackground = true;
                            th.SetApartmentState(ApartmentState.STA);//缺少这句话，就会出错误。
                            
                            th.Start();
                            this.Visible = false;//隐藏该窗体
                        }
                        else
                        {
                            MessageBox.Show("密码输入错误！", "提示");
                        }
                    }
                    else
                    {
                        MessageBox.Show("用户名输入错误！", "提示");
                    }
                }
                catch (Exception e1)
                {
                    SQL.OffData();
                    MessageBox.Show(e1.Message, "提示");
                }
                finally
                {
                    SQL.OffData();
                }
            }
            else
            {
                MessageBox.Show("验证码输入错误！", "提示");
            }
            
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            LoadDrawValImg();//生成验证码
        }

        private void PictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
            bool beginMove = false;//初始化鼠标位置
            int currentXPosition;
            int currentYPosition;

        private void AdministratorLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                beginMove = true;
                currentXPosition = MousePosition.X;//鼠标的x坐标为当前窗体左上角x坐标
                currentYPosition = MousePosition.Y;//鼠标的y坐标为当前窗体左上角y坐标
            }
        }

        private void AdministratorLogin_MouseMove(object sender, MouseEventArgs e)
        {
            if (beginMove)
            {
                this.Left += MousePosition.X - currentXPosition;//根据鼠标x坐标确定窗体的左边坐标x
                this.Top += MousePosition.Y - currentYPosition;//根据鼠标的y坐标窗体的顶部，即Y坐标
                currentXPosition = MousePosition.X;
                currentYPosition = MousePosition.Y;
            }
        }

        private void AdministratorLogin_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentXPosition = 0; //设置初始状态
                currentYPosition = 0;
                beginMove = false;
            }
        }
    }
}
