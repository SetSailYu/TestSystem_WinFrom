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

namespace 考试系统服务端
{
    public partial class ViewResultsForm : Form
    {
        public ViewResultsForm()
        {
            InitializeComponent();
        }
        SqlOperation SQL = null;
        /// <summary>
        /// 新生成的数据库
        /// </summary>
        DataSet newDS = null;
        /// <summary>
        /// 新生成数据库的表
        /// </summary>
        DataTable newDT = null;

        private void Form4_Load(object sender, EventArgs e)
        {
            SQL = new SqlOperation();
            string sql = "select chengji.zkz,student.xh,student.xm,chengji.score,chengji.bj from chengji,student where student.zkz=chengji.zkz";
            SQL.LoadExecuteData(sql);
            SQL.OffData();

            newDS = new DataSet();//创建DataSet
            newDT = new DataTable("Table");//创建一个名为DSTable的DataTalbe
            newDT.Columns.Add(new DataColumn("zkz", typeof(string)));//为dt_dry表内建立Column
            newDT.Columns.Add(new DataColumn("xh", typeof(string)));
            newDT.Columns.Add(new DataColumn("xm", typeof(string)));
            newDT.Columns.Add(new DataColumn("score", typeof(int)));

            bool falg = false;
            for (int i = 0; i < SQL.MyDataSet.Tables[0].Rows.Count; i++)
            {
                if (comboBox1.Items.IndexOf(SQL.MyDataSet.Tables[0].Rows[i]["bj"].ToString().Trim()) == -1)
                {
                    comboBox1.Items.Add(SQL.MyDataSet.Tables[0].Rows[i]["bj"].ToString().Trim());//在comboBox1内添加班级
                    falg = true;
                }
            }
            
            NewLoadData();//根据comboBox1的选定项生成新表

            newDS.Tables.Add(newDT);//别忘记向newDT中添加table
            dataGridView1.DataSource = newDS.Tables["Table"];
            dataGridView1.Columns[0].HeaderText = "准考证号";
            dataGridView1.Columns[1].HeaderText = "学 号";
            dataGridView1.Columns[2].HeaderText = "姓 名";
            dataGridView1.Columns[3].HeaderText = "成 绩";

            if (falg)
                comboBox1.SelectedItem = comboBox1.Items[0];//默认选定项
            else
            {
                MessageBox.Show("成绩未提交！", "提示");
                this.Close();
            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            newDS.Clear();//清空上次生成的数据库
            NewLoadData();//根据comboBox1的选定项生成新表
            dataGridView1.DataSource = newDS.Tables["Table"];
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;//让列宽能按比例填充显示区域
            }
        }
        /// <summary>
        /// 根据comboBox1的选定项生成新表
        /// </summary>
        private void NewLoadData()
        {
            DataRow dr = newDT.NewRow();//注意这边创建dt的新行的方法。指定类型是DataRow而不是TableRow，然后不用new直接的用创建的DataTable下面的NewRow方法。
            for (int i = 0; i < SQL.MyDataSet.Tables[0].Rows.Count; i++)
            {
                if (SQL.MyDataSet.Tables[0].Rows[i]["bj"].ToString().Trim() == comboBox1.Text.Trim())
                {
                    dr = newDT.NewRow();
                    dr["zkz"] = SQL.MyDataSet.Tables[0].Rows[i]["zkz"];
                    dr["xh"] = SQL.MyDataSet.Tables[0].Rows[i]["xh"];
                    dr["xm"] = SQL.MyDataSet.Tables[0].Rows[i]["xm"];
                    dr["score"] = SQL.MyDataSet.Tables[0].Rows[i]["score"];
                    newDT.Rows.Add(dr);
                }
            }
            //newDS.Tables.Add(newDT);//别忘记向newDT中添加table
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            SQL.Empty();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ExportToExcel d = new ExportToExcel();
            d.OutputAsExcelFile(dataGridView1);

            #region  //IO文件流操作
            ////将数据表中的数据保存到excel文件中
            //SaveFileDialog dlg = new SaveFileDialog();//声明保存对话框
            //dlg.Filter = "txt files (*.txt)|*.txt|excel files (*.xls)|*.xls|All files (*.*)|*.*";//设置文件类型，文件后缀列表
            //dlg.FilterIndex = 2;//设置默认文件类型显示顺序
            //dlg.Title = "存盘设置";//设置对话框标题
            //dlg.RestoreDirectory = true;//保存对话框是否记忆上次打开的目录，即关闭对话框之后，下次打开恢复原来的目录
            //dlg.CreatePrompt = true;//设置如果文件不存在、是否提示用户创建该文件
            //dlg.ShowDialog();

            ////接受客户端响应、当用户单击保存后执行
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    Stream myStream = dlg.OpenFile();
            //    //if ((myStream = dlg.OpenFile()) != null)//OpenFile方法提供写入到文件流，此方法提供Stream可以写入的对象

            //    //创建写入器
            //    StreamWriter streamWrite = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));  //利用系统默认编码格式，将字符写入到文件流中。
            //    try
            //    {
            //        if (dlg.FilterIndex == 2)
            //        {
            //            //写入列标题
            //            string columnTitle = "";
            //            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            //            {
            //                if (i > 0)
            //                {
            //                    columnTitle += "\t";
            //                }
            //                columnTitle += dataGridView1.Columns[i].HeaderText;//获取列标题单元格的标题文本
            //            }
            //            streamWrite.WriteLine(columnTitle);//将标题写入文件流

            //            //写入行内容
            //            for (int i = 0; i < dataGridView1.RowCount; i++)//行数
            //            {
            //                string columnValue = "";//每行单元格内容
            //                for (int j = 0; j < dataGridView1.ColumnCount; j++)//列数
            //                {
            //                    if (j > 0)
            //                    {
            //                        columnValue += "\t";
            //                    }
            //                    if (dataGridView1.Rows[i].Cells[j].Value == null)
            //                        columnValue += "";
            //                    else
            //                        columnValue += dataGridView1.Rows[i].Cells[j].Value.ToString().Trim();
            //                }
            //                streamWrite.WriteLine(columnValue);//将行数据写入文件流
            //            }  
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show("提示" + ex);
            //    }
            //    finally
            //    {
            //        //关闭文件流
            //        streamWrite.Close();
            //        myStream.Close();
            //    }


            //}
            #endregion
        }

        private void ViewResultsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SQL.Empty();
            GC.Collect();
        }
    }
}
