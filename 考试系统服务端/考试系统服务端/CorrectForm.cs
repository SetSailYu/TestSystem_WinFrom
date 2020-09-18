using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataModel;
using System.Collections;

namespace 考试系统服务端
{
    public partial class CorrectForm : Form
    {
        public CorrectForm()
        {
            InitializeComponent();
            try
            {
                SQL = new SqlOperation();//连接数据库
                SQL.LoadData("student", "student");//加载student表
            }
            catch
            {
                this.Close();
            }
        }

        SqlOperation SQL;//连接数据库

        private void CorrectForm_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(95, 151, 198);
            this.checkedListBox1.BackColor = Color.FromArgb(95, 151, 198);

            for (int i = 0; i < SQL.MyDataSet.Tables["student"].Rows.Count; i++)
            {
                checkedListBox1.Items.Add(SQL.MyDataSet.Tables["student"].Rows[i]["zkz"]);//在checkedListBox1框内显示学生的准考证号
                if (comboBox1.Items.IndexOf(SQL.MyDataSet.Tables["student"].Rows[i]["bj"].ToString().Trim()) == -1)
                {
                    comboBox1.Items.Add(SQL.MyDataSet.Tables["student"].Rows[i]["bj"].ToString().Trim());//在comboBox1内添加班级
                }
            }
            button2.Focus();//焦点
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                #region  //获取已登录过的考生准考证号
                ArrayList zkz = new ArrayList();
                for (int j = 0; j < SQL.MyDataSet.Tables["student"].Rows.Count; j++)
                {
                    if (SQL.MyDataSet.Tables["student"].Rows[j]["state"].ToString().Trim() == "已交卷")
                    {
                        SQL.MyDataSet.Tables["student"].Rows[j]["state"] = "已批阅";
                        zkz.Add(SQL.MyDataSet.Tables["student"].Rows[j]["zkz"].ToString().Trim());//获取已登录过的考生准考证号
                    }
                    else if (SQL.MyDataSet.Tables["student"].Rows[j]["state"].ToString().Trim() == "已批阅")
                    {
                        zkz.Add(SQL.MyDataSet.Tables["student"].Rows[j]["zkz"].ToString().Trim());//获取已登录过的考生准考证号
                    }
                }
                SQL.UploadData("student");//上传数据库
                #endregion

                #region  //标记已批阅的考生
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (zkz.IndexOf(checkedListBox1.Items[i].ToString()) != -1)//将有登录的考生准考证号标记
                    {
                        checkedListBox1.SetItemCheckState(i, CheckState.Checked);//显示选中状态
                    }
                }
                #endregion

                #region  //初始化已交卷考生的成绩
                //将zkz和bj导入chengji表内
                string sql1 = "insert into chengji(zkz,bj) select zkz, bj from student ";
                SQL.ExecuteNonQuery(sql1);//有连接方式，执行传入的SQL语句

                SQL.LoadData("chengji", "chengji");
                for (int k = 0; k < SQL.MyDataSet.Tables["chengji"].Rows.Count; k++)
                {
                    for (int h = 0; h < zkz.Count; h++)
                    {
                        if (SQL.MyDataSet.Tables["chengji"].Rows[k]["zkz"].ToString().Trim() == zkz[h].ToString())
                        {
                            SQL.MyDataSet.Tables["chengji"].Rows[k]["score"] = 0;
                        }
                    }
                }
                SQL.UploadData("chengji");//上传数据库chengji表
                SQL.MyDataSet.Tables["chengji"].Clear();//清除本地的chengji表
                #endregion

                #region  //计算考生成绩，并添加
                //将每个考生计算完的成绩加载到本地
                string sql = "select zkz,count(*)*2 as cj from ksresult where xsanswer=tkanswer group by zkz";
                SQL.LoadExecuteData(sql, "score"); //无连接方式，执行传入的SQL语句
                if (SQL.MyDataSet.Tables["score"].Rows.Count != 0)
                {
                    for (int i = 0; i < SQL.MyDataSet.Tables["score"].Rows.Count; i++)
                    {
                        //将加载到本地的考生成绩上传到chengji表中，即有连接方式修改chengji表中的score列
                        string sql2 = "update chengji set score=" + SQL.MyDataSet.Tables["score"].Rows[i]["cj"] + "where chengji.zkz='" + SQL.MyDataSet.Tables["score"].Rows[i]["zkz"].ToString().Trim() + "'";
                        SQL.ExecuteNonQuery(sql2);//有连接方式，执行传入的SQL语句
                    }
                }
                #endregion
                
                MessageBox.Show("改卷结束", "提示");
            }
            catch//(Exception ex)
            {
                MessageBox.Show("改卷已完成" ,"提示");
            }
            finally
            {
                comboBox1.Enabled = true;
                comboBox1.Focus();

                SQL.LoadData("chengji", "chengji");//从新加载更新过的chengji表

                comboBox1.SelectedItem = comboBox1.Items[0];//默认选定项
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string bj1 = comboBox1.Text.Trim();
            #region  //获取班级人数
            int num1 = 0;
            for (int i = 0; i < SQL.MyDataSet.Tables["student"].Rows.Count; i++)
            {
                if (SQL.MyDataSet.Tables["student"].Rows[i]["bj"].ToString().Trim() == bj1)
                {
                    num1++;
                }
            }
            textBox1.Text = Convert.ToString(num1);
            #endregion

            #region  //获取参加考试人数
            int num2 = 0;
            for (int i = 0; i < SQL.MyDataSet.Tables["student"].Rows.Count; i++)
            {
                if (SQL.MyDataSet.Tables["student"].Rows[i]["bj"].ToString().Trim() == bj1)
                {
                    if (SQL.MyDataSet.Tables["student"].Rows[i]["state"].ToString().Trim() != "未登录")
                    {
                        num2++;
                    }
                }
            }
            textBox4.Text = Convert.ToString(num2);
            #endregion

            #region   //获取缺考人数
            textBox2.Text = Convert.ToString(num1 - num2);
            #endregion

            #region //获取不及格人数
            int num3 = 0;
            for (int i = 0; i < SQL.MyDataSet.Tables["chengji"].Rows.Count; i++)
            {
                if (SQL.MyDataSet.Tables["chengji"].Rows[i]["bj"].ToString().Trim() == bj1)
                {
                    if (!Convert.IsDBNull(SQL.MyDataSet.Tables["chengji"].Rows[i]["score"]))//判断从数据库返回的值是否为DBNull类型
                    {
                        if (Convert.ToInt32(SQL.MyDataSet.Tables["chengji"].Rows[i]["score"]) >= 60)
                        {
                            num3++;
                        }
                    }
                }
            }
            textBox3.Text = Convert.ToString(num1 - num3);
            #endregion

            #region //获取及格人数
            textBox5.Text = Convert.ToString(num3);
            #endregion        

            #region //求平均分
            int num5 = 0;
            for (int i = 0; i < SQL.MyDataSet.Tables["chengji"].Rows.Count; i++)
            {
                if (SQL.MyDataSet.Tables["chengji"].Rows[i]["bj"].ToString().Trim() == bj1)
                {
                    if (!Convert.IsDBNull(SQL.MyDataSet.Tables["chengji"].Rows[i]["score"]))//判断从数据库返回的值是否为DBNull类型
                    {
                        num5 += Convert.ToInt32(SQL.MyDataSet.Tables["chengji"].Rows[i]["score"]);
                    }
                }
            }
            textBox6.Text = Convert.ToString(num5 / num1);
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //打开查看成绩窗体
            ViewResultsForm f = new ViewResultsForm();
            f.ShowDialog();//模态窗体
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SQL.Empty();
            SQL.OffData();
            GC.Collect();
            this.Close();
        }

        private void CorrectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
