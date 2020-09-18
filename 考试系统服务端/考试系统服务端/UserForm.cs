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
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }

        #region //方法及字段定义
        DataSet NewMyDataSet = null;//定义临时本地数据库名，用于查询
        SqlOperation SQL = null;
        ArrayList array = new ArrayList(1) { "全部" };//存储班级

        /// <summary>
        /// 将权限存入数组，并绑定控件
        /// </summary>
        private void MemoryState()
        {
            array.Clear();
            array.Add("全部");
            for (int i = 0; i < SQL.MyDataSet.Tables["userinfo"].Rows.Count; i++)
            {
                if (SQL.MyDataSet.Tables["userinfo"].Rows[i].RowState == DataRowState.Deleted)
                    continue;
                if (OfIndex(SQL.MyDataSet.Tables["userinfo"].Rows[i]["ustate"].ToString().Trim()) == false)
                {
                    array.Add(SQL.MyDataSet.Tables["userinfo"].Rows[i]["ustate"].ToString().Trim());
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
            dataGridView1.DataSource = SQL.MyDataSet.Tables["userinfo"]; //dataGridView1的数据源设为MyDataSet
        }
        /// <summary>
        /// 刷新数据表，即dataGridView重新绑定NewMyDataSet数据源和源表名
        /// </summary>
        /// <param name="listName">列表名</param>
        /// <param name="value">值</param>
        public void ShowData(string listName, string value)
        {
            NewMyDataSet.Tables[0].Clear();//清空上次的数据
            /*Clone和Copy：
             * 使用Copy方法会创建与原DataSet具有相同结构和相同行的新DataSet.
             * 使用Clone方法会创建具有相同结构的新DataSet，但不包含任何行。*/
            NewMyDataSet = SQL.MyDataSet.Clone();
            DataTable dt = SQL.MyDataSet.Tables["userinfo"];//选定数据表
            DataRow[] drs = dt.Select(listName + "= '" + value + "'");//查询数据
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
        public bool QueryU(string xh, string listName)
        {
            string uxh = ".";
            try
            {
                for (int i = 0; i < SQL.MyDataSet.Tables["userinfo"].Rows.Count; i++)
                {
                    if (SQL.MyDataSet.Tables["userinfo"].Rows[i].RowState == DataRowState.Deleted)
                        continue;
                    uxh = Convert.ToString(SQL.MyDataSet.Tables["userinfo"].Rows[i][listName]);
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

        #region //初始化窗体
        private void UserForm_Load(object sender, EventArgs e)
        {
            SQL = new SqlOperation();//连接数据库
            NewMyDataSet = new DataSet();
            SQL.LoadData("userinfo", "userinfo");
            NewMyDataSet.Tables.Add("newuserinfo");

            MemoryState();//将班级存入数组，并绑定控件
            SourceData();//comboBox1控件重新绑定数据源

            string[] str = { "管理员", "普通用户"};
            string[] str1 = { "管理员", "普通用户" };
            comboBox2.DataSource = str;//数组绑定comboBox控件
            comboBox2.SelectedIndex = 0;
            comboBox3.DataSource = str1;//数组绑定comboBox控件
            comboBox3.SelectedIndex = 0;

            textBox11.Text = "";
            textBox1.Focus();

            ShowData();

            this.BackColor = Color.FromArgb(95, 151, 198);
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;//让列宽能按比例填充显示区域
            }

            dataGridView1.Columns[0].HeaderText = "用户名";
            dataGridView1.Columns[1].HeaderText = "密 码";
            dataGridView1.Columns[2].HeaderText = "权 限";
        }
        #endregion

        #region //删除
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text == "")
                {
                    MessageBox.Show("请输入用户名！", "提示");
                }
                else
                {
                    if (QueryU(textBox5.Text, "uname"))//判断输入的学号是否存在
                    {
                        //获取userinfo表中uname列的所有数据转换成数组str
                        string[] str = SQL.MyDataSet.Tables["userinfo"].AsEnumerable().Select(d => d.Field<string>("uname")).ToArray();
                        int id = 0;
                        for (int i = 0; i < str.Length; i++)
                        {
                            if (str[i] == textBox5.Text)
                            {
                                id = i;
                                break;
                            }
                        }
                        string state = SQL.MyDataSet.Tables["userinfo"].Rows[id]["ustate"].ToString().Trim();
                        DataRow drs = SQL.MyDataSet.Tables["userinfo"].Rows[id];
                        drs.Delete();//删除表中指定行

                        MemoryState();//刷新权限选项（查询窗）
                        if (OfIndex(state))//查询当前删除行的state值在array数组中是否存在  
                        {
                            button4.PerformClick();//若存在 按照班级查询 刷新数据表//调用button4的Click事件
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
                        MessageBox.Show("该用户名不存在！", "提示");
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "提示");
            }
}
        #endregion

        #region //查询
        /// <summary>
        /// 按权限查询
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
                ShowData("ustate", comboBox1.SelectedItem.ToString());
            }
        }
        /// <summary>
        /// 按用户名查询
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
                if (QueryU(textBox11.Text.Trim(), "uname"))
                {
                    ShowData("uname", textBox11.Text.Trim());
                }
                else
                {
                    MessageBox.Show("未查询到该用户！", "提示");
                }
            }
        }
        #endregion

        #region //添加
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                    MessageBox.Show("用户名不能为空！", "提示");
                else if (textBox2.Text == "")
                    MessageBox.Show("密码不能为空！", "提示");
                else
                {
                    if (QueryU(textBox1.Text, "uname"))
                        MessageBox.Show("该用户已存在！", "提示");
                    else
                    {
                        DataRow dr = SQL.MyDataSet.Tables["userinfo"].NewRow();//根据本地数据库中，名字为userinfo的表结构，复制一条空行
                        dr.BeginEdit();//开始编辑行
                        dr["uname"] = textBox1.Text.Trim();
                        dr["upwd"] = textBox2.Text.Trim();
                        dr["ustate"] = comboBox2.SelectedItem.ToString();
                        SQL.MyDataSet.Tables["userinfo"].Rows.Add(dr); //将编辑的行添加到本地数据库中
                        dr.EndEdit();//结束编辑行

                        if (OfIndex(comboBox2.SelectedItem.ToString()))//查询当前输入的ustate值在array数组中是否存在                                        
                            button4.PerformClick();//若存在 按照权限查询 刷新数据表//调用button4的Click事件
                        else
                        {
                            MemoryState();//刷新权限选项（查询窗）
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
                if (textBox9.Text == "")
                    MessageBox.Show("用户名不能为空！", "提示");
                else if (textBox8.Text == "")
                    MessageBox.Show("密码不能为空！", "提示");
                else
                {
                    if (QueryU(textBox9.Text, "uname") == false)
                        MessageBox.Show("该用户不存在！", "提示");
                    else
                    {
                        DataRow[] dr = SQL.MyDataSet.Tables["userinfo"].Select("uname= '" + textBox9.Text + "'");//查询数据
                        for (int i = 0; i < dr.Length; i++)
                        {
                            dr[i]["upwd"] = textBox8.Text.Trim();
                            dr[i]["ustate"] = comboBox3.SelectedItem.ToString();
                        }
                        if (OfIndex(comboBox3.SelectedItem.ToString()))//查询当前输入的ustate值在array数组中是否存在                                        
                            button4.PerformClick();//若存在 按照权限查询 刷新数据表//调用button4的Click事件
                        else
                        {
                            MemoryState();//刷新权限选项（查询窗）
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
            SQL.UploadData("userinfo");
        }
        #endregion

        #region //关闭
        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //button7.PerformClick();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SQL.Empty();
            SQL.OffData();
            GC.Collect();
            this.Close();
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
                comboBox2.Focus();//设置输入焦点
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                button1.Focus();//设置输入焦点
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                textBox8.Focus();//设置输入焦点
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                comboBox3.Focus();//设置输入焦点
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                button3.Focus();//设置输入焦点
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)13)
            {
                button2.Focus();//设置输入焦点
            }
        }
        #endregion
    }
}
