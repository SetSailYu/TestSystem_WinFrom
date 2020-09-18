using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//以下是需要的命名空间
using DataModel;
using System.Collections;
using System.Net;
//using System.Drawing.Imaging;
//using System.Drawing.Drawing2D;



namespace ExamUIL
{
    public partial class exam : Form
    {
        #region 初始化
        /// <summary>
        /// 模板的题库类 保存UIL层抽窗口的题目信息
        /// </summary>
        List<TK> Tm = new List<TK>();
        /// <summary>
        /// 模板的题库类 题目信息
        /// </summary>
        List<TK> NewTm = new List<TK>(50);
        /// <summary>
        /// 考生类里面的子类，用于保存考生结果
        /// </summary>
        List<Ksresult> Ksresult = new List<Ksresult>(20);
        /// <summary>
        /// 题目编号，用于提示用户在第几题（textBoxTh内容）
        /// </summary>
        int tmid = 0; 
        /// <summary>
        /// 用于题库的序号，默认从0开始
        /// </summary>
        int row = 0;
        /// <summary>
        /// 用于保存随机出题数
        /// </summary>
        ArrayList arr = new ArrayList(20);
        /// <summary>
        /// 姓名
        /// </summary>
        string xm = null;     
        /// <summary>
        /// 服务器ip
        /// </summary>
        IPAddress ip = null;
       /// <summary>
       /// 服务器端口
       /// </summary>
        int port = 0;
        /// <summary>
        /// 分钟
        /// </summary>
        int minute = 59;
        /// <summary>
        /// 秒
        /// </summary>
        int second = 60;
        /// <summary>
        /// 模板里面的考生类实例化对象
        /// </summary>
        Student ks = null;
        #endregion

        public exam(ArrayList arr,Student ks,string xm, List<TK> tm, IPAddress ip, int port)
        {
            //接受抽题窗口传来的信息，构造函数
            InitializeComponent();
            this.Tm = tm;
            this.xm = xm;
            this.ip = ip;
            this.port = port;
            this.ks = ks;
            this.arr = arr;
        }
        private void Exam_Load(object sender, EventArgs e)
        {
            //skinEngine1.SkinFile = Application.StartupPath + @"\MacOs.ssk"; //引用皮肤
            timer1.Enabled = true;//打开时间控件
            labelKsxm.Text = xm; //姓名标签

            tmid = 1; 
            textBoxTh.Text =tmid.ToString();//文本框显示第一题

            int count = 0;// 序号
            while (count < 20)
            {
                int number =Convert.ToInt32(arr[count]); //将随机的数保存到number
                NewTm.Add(Tm[number]);  ///////////////////报错点             
                Ksresult ksResult = new Ksresult(); //考生结果类，为考生类的子类
                ksResult.Zkz = ks.Zkz; //准考证
                ksResult.Ktxh = count; //考题序号
                ksResult.Tkth = number; //题库题号

                ksResult.Tkanswer = NewTm[count].Result; 
                Ksresult.Add(ksResult); 
                count++;
            }          
            TiMu(); //调用显示题目方法   此时row=0 加载窗口的时候为第一题
        }


        #region 上一题
        private void BtnUp_Click(object sender, EventArgs e)//上一题点击事件
        {
            
            row--; //题目序号自减一
           if (row < 0)
            {
                MessageBox.Show("当前为第一题", "提示");
                btnUp.Enabled = false; //关闭上一题窗口       
                row++; 
            }
            else
            {
                ClearRadionCheck();//调用函数  刷新单选框
                keepRadioButtonClick();//调用函数 保存之前选择的单选框点击

                tmid--;             
                textBoxTh.Text = tmid.ToString(); //文本框显示
                TiMu(); //调用显示题目函数
                if (row<=20)
                {
                    btnDown.Enabled = true;//小于等于20题，可点击下一题
                }
            }
        }
        #endregion

        #region 下一题

        private void BtnDown_Click(object sender, EventArgs e)//下一题点击事件
        {          
            row++;      //题目序号自加一    
            if (row ==NewTm.Count) //等于题目的总数
            {
                MessageBox.Show("当前为最后一题", "提示");
                btnDown.Enabled = false;//关闭下一题的窗口
                row--;
            }
            else
            {
                tmid++;
                textBoxTh.Text = Convert.ToString(tmid); //文本框显示题号
                TiMu(); //调用显示题目方法

                ClearRadionCheck();//刷新单选框选择
                keepRadioButtonClick();//保存单选框选择

                if (row >0)
                {
                    btnUp.Enabled = true;//当题目大于0，可点击上一题
                }
            }         
        }
        #endregion

        #region  交卷按钮      
        private void BtnGet_Click(object sender, EventArgs e) //用来提交考卷
        {       
            string msg = "";
            for (int i = 1; i < NewTm.Count; i++)
            {
                if (Ksresult[i].Xsanswer == null)
                {
                    msg += i.ToString()+" ";
                }
            }
            if (msg == "")
            {               
                ks.Cmd = Student.Way.PutIn;
                ks.Result = Ksresult; //学生类包含考生答案               
                string str = ExamBLL.Transaction.UpResult(ks, ip, port);
                MessageBox.Show(str, "提示");
                if (str=="交卷成功")
                {
                    btnGet.Enabled = false;
                    Application.Exit();
                }
                timer1.Enabled = false;
             
            }
            else
            {
                if (MessageBox.Show("第"+msg+"题未完成，确定是否提交", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ks.Cmd = Student.Way.PutIn;
                    ks.Result = Ksresult; //学生类包含考生答案               
                    string str = ExamBLL.Transaction.UpResult(ks, ip, port);
                    MessageBox.Show(str, "提示");
                    if (str == "交卷成功")
                    {
                        btnGet.Enabled = false;
                        Application.Exit();
                    }
                    timer1.Enabled = false;
                }
            }
        }
        #endregion

        #region   选择答案    
        private void RadioButton1_Click(object sender, EventArgs e) //绑定其他三个单选框click事件
        {
            string answer = null;
            if (radioButton1.Checked == true)
                answer = "A";
            if (radioButton2.Checked == true)
                answer = "B";
            if (radioButton3.Checked == true)
                answer = "C";
            if (radioButton4.Checked == true)
                answer = "D";

            if (answer == null)
            {
                MessageBox.Show("未选择答案", "提示");
            }
            else
                Ksresult[row].Xsanswer = answer; //选择的答案保存到对应的题目答案类的学生答案中      
        }
        #endregion

        #region 用于保存选择的答案
        public void keepRadioButtonClick() 
        {
            string answer = Ksresult[row].Xsanswer;
            if (answer == "A")
            {
                radioButton1.Checked = true;
            }
            if (answer == "B")
            {
                radioButton2.Checked = true;
            }
            if (answer == "C")
            {
                radioButton3.Checked = true;
            }
            if (answer == "D")
            {
                radioButton4.Checked = true;
            }
        }
        #endregion

        #region  倒计时显示

        private void Timer1_Tick(object sender, EventArgs e)
        {
            second--;
            if (second == 0)
            {
                if (minute == 0)
                {
                    MessageBox.Show("考试时间结束,请提交考卷", "提示");
                    btnDown.Enabled = false;
                    btnUp.Enabled = false;
                    btnGet.Enabled = false;//三个按钮全部关闭

                    ks.Cmd = Student.Way.PutIn;//修改命令为交卷
                    ks.Result = Ksresult; //学生类包含考生答案  
                    string str = ExamBLL.Transaction.UpResult(ks, ip, port);//强制交卷
                    MessageBox.Show(str, "提示");     //服务器返回提示
                    
                    if (str == "交卷成功") //防止交卷失败无法再一次提交
                    {
                        btnGet.Enabled = false;
                    }
                    timer1.Enabled = false;
                }
                else
                {
                    if (minute >= 0)
                    {
                        minute--; 
                        second = 60;
                    }
                }
            }
            labelTime.Text = minute + ":" + second; //输出格式
        }
        #endregion

        #region 单选框点击刷新   
        public void ClearRadionCheck()
        {      //全部为不选中状态
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
        }
        #endregion

        #region 显示题目
        public void TiMu()
        {
            richTextBox1.Text = NewTm[row].Tmnr; //题目内容
            radioButton1.Text = NewTm[row].Answer1;//A
            radioButton2.Text = NewTm[row].Answer2;//B
            radioButton3.Text = NewTm[row].Answer3;//C
            radioButton4.Text = NewTm[row].Answer4;//D
        }
        #endregion

        private void labelSySJ_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void exam_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}

