using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using DataModel;

namespace 考试系统服务端
{
    public partial class StudentInformationForm : Form
    {
        public StudentInformationForm()
        {
            InitializeComponent();
        }

        #region //方法及字段定义
        DataSet NewMyDataSet = null;//定义临时本地数据库名，用于查询
        SqlOperation SQL = null;
        ArrayList arrayBj = new ArrayList(1) { "全部" };//存储班级
        ArrayList arraySt = new ArrayList(1) { "全部" };//存储状态

        /// <summary>
        /// 将班级存入数组，并绑定控件
        /// </summary>
        private void MemoryBj()
        {
            arrayBj.Clear();
            arrayBj.Add("全部");
            for (int i = 0; i < SQL.MyDataSet.Tables["student"].Rows.Count; i++)
            {
                if (SQL.MyDataSet.Tables["student"].Rows[i].RowState == DataRowState.Deleted)
                    continue;
                if (OfIndex(SQL.MyDataSet.Tables["student"].Rows[i]["bj"].ToString().Trim()) == false)
                {
                    arrayBj.Add(SQL.MyDataSet.Tables["student"].Rows[i]["bj"].ToString().Trim());
                }
            }
            arraySt.Clear();
            arraySt.Add("全部");
            for (int i = 0; i < SQL.MyDataSet.Tables["student"].Rows.Count; i++)
            {
                if (SQL.MyDataSet.Tables["student"].Rows[i].RowState == DataRowState.Deleted)
                    continue;
                if (OfIndexSt(SQL.MyDataSet.Tables["student"].Rows[i]["state"].ToString().Trim()) == false)
                {
                    arraySt.Add(SQL.MyDataSet.Tables["student"].Rows[i]["state"].ToString().Trim());
                }
            }
        }
        /// <summary>
        /// comboBox1控件重新绑定数据源
        /// </summary>
        private void SourceData()
        {
            comboBox1.DataSource = null;//断开绑定数据源
            comboBox1.DataSource = arrayBj;//数组重新绑定comboBox控件
            comboBox1.SelectedIndex = 0;

            comboBox3.DataSource = null;//断开绑定数据源
            comboBox3.DataSource = arraySt;//数组重新绑定comboBox控件
            comboBox3.SelectedIndex = 0;
        }
        /// <summary>
        /// 查找arrayBj数组内是否有相同的值，有true，无false
        /// </summary>
        /// <param name="str">要查询的值</param>
        /// <returns></returns>
        private bool OfIndex(string str)
        {
            if (arrayBj.IndexOf(str) == -1)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 查找arraySt数组内是否有相同的值，有true，无false
        /// </summary>
        /// <param name="str">要查询的值</param>
        /// <returns></returns>
        private bool OfIndexSt(string str)
        {
            if (arraySt.IndexOf(str) == -1)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 刷新数据表，即dataGridView重新绑定MyDataSet数据源和源表名
        /// </summary>
        public void ShowData1()
        {
            dataGridView1.DataSource = SQL.MyDataSet.Tables["student"]; //dataGridView1的数据源设为MyDataSet
        }
        /// <summary>
        /// 刷新数据表，即dataGridView重新绑定NewMyDataSet数据源和源表名
        /// </summary>
        public void ShowData2(string listName,string value)
        {
            NewMyDataSet.Tables[0].Clear();//清空上次的数据
            /*Clone和Copy：
             * 使用Copy方法会创建与原DataSet具有相同结构和相同行的新DataSet.
             * 使用Clone方法会创建具有相同结构的新DataSet，但不包含任何行。*/
            NewMyDataSet = SQL.MyDataSet.Clone();
            DataTable dt = SQL.MyDataSet.Tables["student"];//选定数据表
            DataRow[] drs = dt.Select(listName +"= '" + value + "'");//查询数据
            foreach (DataRow dr in drs)
            {
                NewMyDataSet.Tables[0].NewRow();//NewRow() 创建与该表具有相同架构的新DataRow
                NewMyDataSet.Tables[0].Rows.Add(dr.ItemArray);//ItemArray：获取或设置行中所有列的值
            }
           
            dataGridView1.DataSource = NewMyDataSet.Tables[0]; //dataGridView1的数据源设为NewMyDataSet
        }
        /// <summary>
        /// 查询判断
        /// </summary>
        /// <param name="xh">要查询的值</param>
        /// <param name="listName">要查询的值的列名</param>
        /// <returns></returns>
        public bool QueryU(string xh,string listName)
        {
            string uxh = ".";
            try
            {
                for (int i = 0; i < SQL.MyDataSet.Tables["student"].Rows.Count; i++)
                {
                    if (SQL.MyDataSet.Tables["student"].Rows[i].RowState == DataRowState.Deleted)
                        continue;
                    uxh = Convert.ToString(SQL.MyDataSet.Tables["student"].Rows[i][listName]);
                    if (xh == uxh)
                    {
                        return true;
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "提示");
            }
            return false;
        }
        #endregion

        #region  /窗体初始化，并连接数据库
        private void StudentInformationForm_Load(object sender, EventArgs e)
        {
            SQL = new SqlOperation();//连接数据库
            NewMyDataSet = new DataSet();
            SQL.LoadData("student", "student");
            NewMyDataSet.Tables.Add("newstudent");
            
            MemoryBj();//将班级存入数组，并绑定控件
            SourceData();//comboBox1控件重新绑定数据源

            string[] str = new string[] {"未登录","已登录", "已交卷", "已批阅" };
            comboBox2.DataSource = str;//数组绑定comboBox控件
            comboBox2.SelectedIndex = 0;

            textBox11.Text = "";
            textBox1.Focus();
            
            ShowData1();

            this.BackColor = Color.FromArgb(95, 151, 198);

            dataGridView1.Columns[0].HeaderText = "准考证";
            dataGridView1.Columns[1].HeaderText = "学  号";
            dataGridView1.Columns[2].HeaderText = "姓  名";
            dataGridView1.Columns[3].HeaderText = "班  级";
            dataGridView1.Columns[4].HeaderText = "状  态";
            dataGridView1.Columns[5].HeaderText = "IP地址";
        }
        #endregion

        #region  //查询
        /// <summary>
        /// 按班级查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "全部")
            {
                ShowData1();
            }
            else
            {
                ShowData2("bj", comboBox1.SelectedItem.ToString());
            }
        }
        /// <summary>
        /// 按状态查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() == "全部")
            {
                ShowData1();
            }
            else
            {
                ShowData2("state", comboBox3.SelectedItem.ToString());
            }
        }
        /// <summary>
        /// 按学号查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox11.Text == "")
            {
                MessageBox.Show("请输入内容再查询！", "提示");
            }
            else
            {
                if (QueryU(textBox11.Text.Trim(),"xh"))
                {
                    ShowData2("xh", textBox11.Text.Trim());
                }
                else
                {
                    MessageBox.Show("未查询到该学号！", "提示");
                }
            }

        }
        #endregion
        
        #region //删除
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text == "")
                {
                    MessageBox.Show("请输入学号！", "提示");
                }
                else
                {
                    if (QueryU(textBox5.Text,"xh"))//判断输入的学号是否存在
                    {
                        //获取student表中xh列的所有数据转换成数组str
                        string[] str = SQL.MyDataSet.Tables["student"].AsEnumerable().Select(d => d.Field<string>("xh")).ToArray();
                        int id = 0;
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (str[i] == textBox5.Text)
                            {
                                id = i;
                                break;
                            }
                        }
                        string bj = SQL.MyDataSet.Tables["student"].Rows[id]["bj"].ToString().Trim();
                        DataRow drs = SQL.MyDataSet.Tables["student"].Rows[id];
                        drs.Delete();//删除表中指定行

                        MemoryBj();//刷新班级选项
                        if (OfIndex(bj))//查询当前删除行的bj值在array数组中是否存在  
                        {
                            button4.PerformClick();//若存在 按照班级查询 刷新数据表//调用button4的Click事件
                        }
                        else
                        {
                            SourceData();//comboBox1控件重新绑定数据源
                            ShowData1();//刷新数据表，默认刷新显示全部
                        }

                        MessageBox.Show("删除成功！", "提示");
                    }
                    else
                    {
                        MessageBox.Show("请输入正确的学号！", "提示");
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "提示");
            }
        }
        #endregion

        #region //添加
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                    MessageBox.Show("准考证不能为空！", "提示");
                else if (textBox2.Text == "")
                    MessageBox.Show("学号不能为空！", "提示");
                else if (textBox3.Text == "")
                    MessageBox.Show("姓名不能为空！", "提示");
                else if (textBox4.Text == "")
                    MessageBox.Show("班级不能为空！", "提示");
                else
                {
                    if (QueryU(textBox1.Text , "zkz"))
                        MessageBox.Show("该准考证已存在！", "提示");
                    else if (QueryU(textBox2.Text, "xh"))
                        MessageBox.Show("该学号已存在！", "提示");
                    else
                    {
                        DataRow dr = SQL.MyDataSet.Tables["student"].NewRow();//根据本地数据库中，名字为student的表结构，复制一条空行
                        dr.BeginEdit();//开始编辑行
                        dr["zkz"] = textBox1.Text.Trim();
                        dr["xh"] = textBox2.Text.Trim();
                        dr["xm"] = textBox3.Text.Trim();
                        dr["bj"] = textBox4.Text.Trim() ;
                        dr["state"] = "未登录";
                        SQL.MyDataSet.Tables["student"].Rows.Add(dr); //将编辑的行添加到本地数据库中
                        dr.EndEdit();//结束编辑行

                        if (OfIndex(textBox4.Text.Trim()))//查询当前输入的bj值在array数组中是否存在                                        
                            button4.PerformClick();//若存在 按照班级查询 刷新数据表//调用button4的Click事件
                        else
                        {
                            MemoryBj();//刷新班级选项
                            SourceData();//comboBox1控件重新绑定数据源
                            ShowData1();//刷新数据表，默认刷新显示全部
                        }
                        
                        MessageBox.Show("添加成功！", "提示");
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "提示");
            }
        }
        #endregion

        #region //修改
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox9.Text == "")
                    MessageBox.Show("准考证不能为空！", "提示");
                else if (textBox8.Text == "")
                    MessageBox.Show("学号不能为空！", "提示");
                else if (textBox7.Text == "")
                    MessageBox.Show("姓名不能为空！", "提示");
                else if (textBox6.Text == "")
                    MessageBox.Show("班级不能为空！", "提示");
                else
                {
                    if (QueryU(textBox8.Text, "xh") == false)
                        MessageBox.Show("该学号不存在！", "提示");
                    else
                    {
                        #region //获取 该学号 的 班级
                        string zkz = null;
                        DataRow[] dr = SQL.MyDataSet.Tables["student"].Select("xh= '" + textBox8.Text + "'");//查询数据
                        for (int j = 0; j < dr.Length; j++)
                        {
                            zkz = dr[j]["zkz"].ToString().Trim();
                        }
                        #endregion
                        #region // 判断输入的准考证是否与其他准考证重复或不存在
                        bool falg = true;
                        if (QueryU(textBox9.Text, "zkz"))//如果输入的准考证存在
                        {
                            if (zkz == textBox9.Text.Trim())//如果输入的准考证和该考生原来的一样
                                falg = true;
                            else                            //如果输入的准考证和该考生原来的不一样
                            {
                                falg = false;
                                MessageBox.Show("该准考证已存在！", "提示");
                            }
                        }
                        else
                            falg = true;
                        #endregion
                        #region //根据判断 做出修改操作
                        if (falg)
                        {
                            for (int i = 0; i < dr.Length; i++)
                            {
                                dr[i]["zkz"] = textBox9.Text.Trim();
                                dr[i]["xm"] = textBox7.Text.Trim();
                                dr[i]["bj"] = textBox6.Text.Trim();
                                dr[i]["state"] = comboBox2.SelectedItem.ToString();
                            }
                            if (OfIndex(textBox6.Text.Trim()))//查询当前输入的bj值在array数组中是否存在                                        
                                button4.PerformClick();//若存在 按照班级查询 刷新数据表//调用button4的Click事件
                            else
                            {
                                MemoryBj();//刷新班级选项
                                SourceData();//comboBox1控件重新绑定数据源
                                ShowData1();//刷新数据表，默认刷新显示全部
                            }
                            MessageBox.Show("修改成功！", "提示");
                        }
                        #endregion
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "提示");
            }
        }
        #endregion

        #region  //保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            SQL.UploadData("student");
        }
        #endregion

        #region //关闭
        private void button7_Click(object sender, EventArgs e)
        {
            SQL.Empty();
            SQL.OffData();
            GC.Collect();
            this.Close();
        }

        private void StudentInformationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //button7.PerformClick();
        }
        #endregion

        #region //注入焦点
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
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                textBox4.Focus();//设置输入焦点
            }
        }
        
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                button1.Focus();//设置输入焦点
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                textBox9.Focus();//设置输入焦点
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                textBox7.Focus();//设置输入焦点
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                textBox6.Focus();//设置输入焦点
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                comboBox2.Focus();//设置输入焦点
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                button3.Focus();//设置输入焦点
            }
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                button2.Focus();//设置输入焦点
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                button4.Focus();//设置输入焦点
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                button6.Focus();//设置输入焦点
            }
        }

        #endregion

        
    }
}
