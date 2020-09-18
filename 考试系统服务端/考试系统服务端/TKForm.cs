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
    public partial class TKForm : Form
    {
        public TKForm()
        {
            InitializeComponent();
        }

        #region //方法及字段定义
        DataSet NewMyDataSet = null;//定义临时本地数据库名，用于查询
        SqlOperation SQL = null;
        ArrayList array = new ArrayList(1) { "全部" };//存储班级

        /// <summary>
        /// 将章节存入数组array
        /// </summary>
        private void MemoryZj()
        {
            array.Clear();
            array.Add("全部");
            for (int i = 0; i < SQL.MyDataSet.Tables["tk"].Rows.Count; i++)
            {
                if (SQL.MyDataSet.Tables["tk"].Rows[i].RowState == DataRowState.Deleted)
                    continue;
                if (OfIndex(SQL.MyDataSet.Tables["tk"].Rows[i]["tmzj"].ToString().Trim()) == false)
                {
                    array.Add(SQL.MyDataSet.Tables["tk"].Rows[i]["tmzj"].ToString().Trim());
                }
            }
        }
        /// <summary>
        /// comboBox1控件重新绑定数据源
        /// </summary>
        private void SourceData()
        {
            comboBox1.DataSource = null;//断开绑定数据源
            comboBox1.DataSource = array;//数组重新绑定comboBox控件
            comboBox1.SelectedIndex = 0;
        }
        /// <summary>
        /// 查找array数组内是否有相同的值，有true，无false
        /// </summary>
        /// <param name="str">要查询的值</param>
        /// <returns></returns>
        private bool OfIndex(string str)
        {
            if (array.IndexOf(str) == -1)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 刷新数据表，即dataGridView重新绑定MyDataSet数据源和源表名
        /// </summary>
        public void ShowData()
        {
            dataGridView1.DataSource = SQL.MyDataSet.Tables["tk"]; //dataGridView1的数据源设为MyDataSet
        }
        /// <summary>
        /// 刷新数据表，即dataGridView重新绑定NewMyDataSet数据源和源表名
        /// </summary>
        public void ShowData(string listName, string value)
        {
            NewMyDataSet.Tables[0].Clear();//清空上次的数据
            /*Clone和Copy：
             * 使用Copy方法会创建与原DataSet具有相同结构和相同行的新DataSet.
             * 使用Clone方法会创建具有相同结构的新DataSet，但不包含任何行。*/
            NewMyDataSet = SQL.MyDataSet.Clone();
            DataTable dt = SQL.MyDataSet.Tables["tk"];//选定数据表
            DataRow[] drs = dt.Select(listName + "= '" + value + "'");//查询数据
            foreach (DataRow dr in drs)
            {
                NewMyDataSet.Tables[0].NewRow();//NewRow() 创建与该表具有相同架构的新DataRow
                NewMyDataSet.Tables[0].Rows.Add(dr.ItemArray);//ItemArray：获取或设置行中所有列的值
            }

            dataGridView1.DataSource = NewMyDataSet.Tables[0]; //dataGridView1的数据源设为NewMyDataSet
        }
        /// <summary>
        /// 查询判断(string)
        /// </summary>
        /// <param name="value">要查询的值(string)</param>
        /// <param name="listName">要查询的值的列名</param>
        /// <returns></returns>
        public bool QueryU(string value, string listName)
        {
            string uxh = ".";
            try
            {
                for (int i = 0; i < SQL.MyDataSet.Tables["tk"].Rows.Count; i++)
                {
                    if (SQL.MyDataSet.Tables["tk"].Rows[i].RowState == DataRowState.Deleted)
                        continue;
                    uxh = Convert.ToString(SQL.MyDataSet.Tables["tk"].Rows[i][listName]);
                    if (value == uxh)
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
        /// <summary>
        /// 查询判断(int)
        /// </summary>
        /// <param name="value">要查询的值(int)</param>
        /// <param name="listName">要查询的值的列名</param>
        /// <returns></returns>
        public bool QueryU(int value, string listName)
        {
            int uxh;
            try
            {
                for (int i = 0; i < SQL.MyDataSet.Tables["tk"].Rows.Count; i++)
                {
                    if (SQL.MyDataSet.Tables["tk"].Rows[i].RowState == DataRowState.Deleted)
                        continue;
                    uxh = Convert.ToInt32(SQL.MyDataSet.Tables["tk"].Rows[i][listName]);
                    if (value == uxh)
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

        #region //窗体初始化 
        private void TKForm_Load(object sender, EventArgs e)
        {
            SQL = new SqlOperation();//连接数据库
            NewMyDataSet = new DataSet();
            SQL.LoadData("tk", "tk");
            NewMyDataSet.Tables.Add("newtk");

            MemoryZj();//将章节存入数组，并绑定控件
            SourceData();//comboBox1控件重新绑定数据源

            string[] dn = { "A", "B", "C", "D" };
            comboBox2.DataSource = dn;
            comboBox2.SelectedIndex = 0;

            textBox1.Focus();

            ShowData();

            this.BackColor = Color.FromArgb(95, 151, 198);

            dataGridView1.Columns[0].HeaderText = "题  号";
            dataGridView1.Columns[1].HeaderText = "题目内容";
            dataGridView1.Columns[2].HeaderText = "题目章节";
            dataGridView1.Columns[3].HeaderText = "选项A";
            dataGridView1.Columns[4].HeaderText = "选项B";
            dataGridView1.Columns[5].HeaderText = "选项C";
            dataGridView1.Columns[6].HeaderText = "选项D";
            dataGridView1.Columns[7].HeaderText = "答  案";
        }
        #endregion

        #region //查询
        /// <summary>
        /// 章节查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "全部")
            {
                ShowData();
            }
            else
            {
                ShowData("tmzj", comboBox1.SelectedItem.ToString());
            }
        }
        /// <summary>
        /// 题号检查
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
                if (QueryU(textBox11.Text.Trim(), "th"))
                {
                    ShowData("th", textBox11.Text.Trim());
                }
                else
                {
                    MessageBox.Show("未查询到该题号！", "提示");
                }
            }
        }

        #endregion

        #region //删除
        /// <summary>
        /// 按章节删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedItem.ToString() == "全部")
                {
                    MessageBox.Show("错误！只能按章节删除！", "提示");
                }
                else
                {
                    if (QueryU(comboBox1.SelectedItem.ToString(), "tmzj"))//判断输入的章节是否存在
                    {
                        //获取tk表中tmzj列的所有数据转换成数组str
                        string[] str = SQL.MyDataSet.Tables["tk"].AsEnumerable().Select(d => d.Field<string>("tmzj")).ToArray();
                        ArrayList id = new ArrayList();
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (str[i] == comboBox1.SelectedItem.ToString())
                            {
                                id.Add(i);//记录要删除的行
                            }
                        }
                        foreach(int j in id)
                        {
                            DataRow drs = SQL.MyDataSet.Tables["tk"].Rows[j];
                            drs.Delete();//删除表中指定行
                        }
                        MemoryZj();//刷新章节选项
                        SourceData();//comboBox1控件重新绑定数据源
                        ShowData();//刷新数据表，默认刷新显示全部
                        MessageBox.Show("删除成功！", "提示");
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "提示");
            }
        }
        /// <summary>
        /// 按题号删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox11.Text == "")
                {
                    MessageBox.Show("请输入题号！", "提示");
                }
                else
                {
                    if (QueryU(textBox11.Text, "th"))//判断输入的题号是否存在
                    {
                        //获取tk表中th列的所有数据转换成数组str
                        int[] str = SQL.MyDataSet.Tables["tk"].AsEnumerable().Select(d => d.Field<int>("th")).ToArray();
                        int id = 0;
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (str[i].ToString() == textBox11.Text)
                            {
                                id = i;
                                break;
                            }
                        }
                        string zj = SQL.MyDataSet.Tables["tk"].Rows[id]["tmzj"].ToString().Trim();
                        DataRow drs = SQL.MyDataSet.Tables["tk"].Rows[id];
                        drs.Delete();//删除表中指定行

                        MemoryZj();//刷新题号选项
                        if (OfIndex(zj))//查询当前删除行的zj值在array数组中是否存在  
                        {
                            button4.PerformClick();//若存在 按照题号查询 刷新数据表
                        }
                        else
                        {
                            SourceData();//comboBox1控件重新绑定数据源
                            ShowData();//刷新数据表，默认刷新显示全部
                        }

                        MessageBox.Show("删除成功！", "提示");
                    }
                    else
                    {
                        MessageBox.Show("请输入正确的题号！", "提示");
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
                    MessageBox.Show("题号不能为空！", "提示");
                else if (textBox2.Text == "")
                    MessageBox.Show("章节不能为空！", "提示");
                else if (richTextBox1.Text == "")
                    MessageBox.Show("题目内容不能为空！", "提示");
                else if (richTextBox2.Text == "")
                    MessageBox.Show("选项A不能为空！", "提示");
                else if (richTextBox3.Text == "")
                    MessageBox.Show("选项C不能为空！", "提示");
                else if (richTextBox4.Text == "")
                    MessageBox.Show("选项B不能为空！", "提示");
                else if (richTextBox5.Text == "")
                    MessageBox.Show("选项D不能为空！", "提示");
                else
                {
                    int th;
                    try
                    {
                        th = Convert.ToInt32(textBox1.Text.Trim());
                    }
                    catch
                    {
                        MessageBox.Show("题号必须输入数字类型！", "提示");
                        return;
                    }
                    if (QueryU(th, "th"))
                        MessageBox.Show("该题号已存在！", "提示");
                    else
                    {
                        DataRow dr = SQL.MyDataSet.Tables["tk"].NewRow();//根据本地数据库中，名字为tk的表结构，复制一条空行
                        dr.BeginEdit();//开始编辑行
                        dr["th"] = Convert.ToInt32(textBox1.Text.Trim());
                        dr["tmzj"] = textBox2.Text.Trim();
                        dr["tmnr"] = richTextBox1.Text;
                        dr["answer1"] = richTextBox2.Text;
                        dr["answer2"] = richTextBox4.Text;
                        dr["answer3"] = richTextBox3.Text;
                        dr["answer4"] = richTextBox5.Text;
                        dr["result"] = comboBox2.SelectedItem.ToString();
                        SQL.MyDataSet.Tables["tk"].Rows.Add(dr); //将编辑的行添加到本地数据库中
                        dr.EndEdit();//结束编辑行

                        if (OfIndex(textBox2.Text.Trim()))//查询当前输入的tmzj值在array数组中是否存在                                        
                            button4.PerformClick();//若存在 按照章节查询 刷新数据表//调用button4的Click事件
                        else
                        {
                            MemoryZj();//刷新班级选项
                            SourceData();//comboBox1控件重新绑定数据源
                            ShowData();//刷新数据表，默认刷新显示全部
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
                if (textBox1.Text == "")
                    MessageBox.Show("题号不能为空！", "提示");
                else if (textBox2.Text == "")
                    MessageBox.Show("章节不能为空！", "提示");
                else if (richTextBox1.Text == "")
                    MessageBox.Show("题目内容不能为空！", "提示");
                else if (richTextBox2.Text == "")
                    MessageBox.Show("选项A不能为空！", "提示");
                else if (richTextBox3.Text == "")
                    MessageBox.Show("选项C不能为空！", "提示");
                else if (richTextBox4.Text == "")
                    MessageBox.Show("选项B不能为空！", "提示");
                else if (richTextBox5.Text == "")
                    MessageBox.Show("选项D不能为空！", "提示");
                else
                {
                    int th;
                    try
                    {
                        th = Convert.ToInt32(textBox1.Text.Trim());
                    }
                    catch
                    {
                        MessageBox.Show("题号必须输入数字类型！", "提示");
                        return;
                    }
                    if (QueryU(th, "th") == false)
                        MessageBox.Show("该题号不存在！", "提示");
                    else
                    {
                       
                        DataRow[] dr = SQL.MyDataSet.Tables["tk"].Select("th= '" + th + "'");//查询数据
                        for (int i = 0; i < dr.Length; i++)
                        {
                            dr[i]["tmzj"] = textBox2.Text.Trim();
                            dr[i]["tmnr"] = richTextBox1.Text;
                            dr[i]["answer1"] = richTextBox2.Text;
                            dr[i]["answer2"] = richTextBox4.Text;
                            dr[i]["answer3"] = richTextBox3.Text;
                            dr[i]["answer4"] = richTextBox5.Text;
                            dr[i]["result"] = comboBox2.SelectedItem.ToString();
                        }

                        if (OfIndex(textBox2.Text.Trim()))//查询当前输入的tmzj值在array数组中是否存在                                        
                            button4.PerformClick();//若存在 按照章节查询 刷新数据表//调用button4的Click事件
                        else
                        {
                            MemoryZj();//刷新班级选项
                            SourceData();//comboBox1控件重新绑定数据源
                            ShowData();//刷新数据表，默认刷新显示全部
                        }

                        MessageBox.Show("修改成功！", "提示");
                    }
                }
        }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "提示");
            }
}
        #endregion

        #region //保存
        private void button5_Click(object sender, EventArgs e)
        {
            SQL.UploadData("tk");
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

        private void TKForm_FormClosing(object sender, FormClosingEventArgs e)
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
                richTextBox1.Focus();//设置输入焦点
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
