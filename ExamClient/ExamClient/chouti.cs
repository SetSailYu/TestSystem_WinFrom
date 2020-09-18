using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//下面是需要添加命名空间
using DataModel;
using System.Net;
using System.Collections;

namespace ExamUIL
{
    public partial class chouti : Form
    {
        //初始化
        /// <summary>
        /// 模板里面的题库类实例对象
        /// </summary>
        List<TK> Tm = new List<TK>();
        string xm = null;//姓名
        /// <summary>
        /// 服务器ip
        /// </summary>
        IPAddress ip = null;
        /// <summary>
        /// 服务器端口
        /// </summary>
        int port = 0;
        Student ks = null;
      
        public chouti(string xm, Student ks, IPAddress ip, int port)
        {
            //接受前面UIL层登入信息，并赋值（构造函数）
            InitializeComponent();
            this.xm = xm;
            this.ip = ip;
            this.port = port;
            this.ks = ks;                 
        }

        private void Chouti_Load(object sender, EventArgs e)
        {
            //skinEngine1.SkinFile = Application.StartupPath + @"\MacOs.ssk"; //引用皮肤
            progressBar1.Value = 0;//进度条初始值为0
            timer1.Enabled = true;//默认时间控件开启
            button1.Enabled = false; //默认抽题按钮关闭
            Tm = ExamBLL.Transaction.GetTiMu(ks,ip, port);//传递到BLL层获得题目信息
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 10;
                //进度条按10叠加
            }
            else
            {
                button1.Enabled = true;//叠加倒满时，抽题按钮开启
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            ArrayList KeepArr = new ArrayList();
            KeepArr = Ran(); //新建动态数组用来调用函数并保存
            Form f = new exam(KeepArr,ks,xm, Tm, ip, port);// 将随机抽题数，ks、姓名、题目信息、ip跟端口传递给考试窗口
            f.Show();
            this.Hide();
        }

        private void ProgressBar1_Click(object sender, EventArgs e)
        {

        }

        public ArrayList Ran()  //实现随机抽题
        {
            ArrayList arr = new ArrayList(20);//随机数
            Random rnd = new Random();
            int i = 0;
            while (i < 20)
            {
                int num = rnd.Next(0, Tm.Count); 
                if (arr.IndexOf(num) == -1) //防止重复
                {
                    arr.Add(num);
                    i++;
                }
            }
            arr.Sort(); 
            return arr;
        }
    }
}

