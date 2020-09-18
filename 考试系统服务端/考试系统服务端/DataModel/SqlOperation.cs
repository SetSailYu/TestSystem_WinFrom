using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Net;
using System.Data;
using System.Windows.Forms;

namespace DataModel
{
    /// <summary>
    /// 数据库操作类，连接数据库操作，并操作的数据存储在本地数据库MyDataSet中
    /// </summary>
    public class SqlOperation
    {
        /// <summary>
        /// 连接的全局变量
        /// </summary>
        private SqlConnection conn = null;
        /// <summary>
        /// 适配器
        /// </summary>
        public SqlDataAdapter da = null;
        /// <summary>
        /// 本地数据库名
        /// </summary>
        public DataSet MyDataSet = null;

        /// <summary>
        /// 连接数据库，并开启
        /// </summary>
        public SqlOperation()
        {
            try
            {
                string s = "data source=127.0.0.1;database=examDb;Integrated Security=True;Pooling=False";//windows身份验证模式
                conn = new SqlConnection(s);
                conn.Open();

                da = new SqlDataAdapter();
                MyDataSet = new DataSet();
            }
            catch
            {
                MessageBox.Show("连接数据库失败！", "提示");
            }
        }
        /// <summary>
        /// 无连接操作，加载查询的数据库，精确查询，并将查询到的数据存储在本地中
        /// </summary>
        /// <param name="tableName">要查询的表名</param>
        /// <param name="listName">要查询的列名</param>
        /// <param name="value">要查询的值</param>
        public void LoadData(string tableName,string listName,string value)
        {
            string sql = "select * from "+ tableName +" where "+listName+"='" + value + "'";
            da = new SqlDataAdapter(sql, conn);//定义操作命令
            da.Fill(MyDataSet);
        }
        /// <summary>
        /// 无连接操作，加载查询的数据库，查询表，并将查询到的数据存储在本地中,自定义本地数据库的表名
        /// </summary>
        /// <param name="tableName">要查询的表名</param>
        /// <param name="MyDataTableName">自定义本地数据库的表名</param>
        public void LoadData(string tableName,string MyDataTableName)
        {
            string sql = "select * from " + tableName;
            da = new SqlDataAdapter(sql, conn);//定义操作命令
            da.Fill(MyDataSet,MyDataTableName);
        }
        /// <summary>
        /// 无连接操作，加载查询的数据库，查询表，并将查询到的数据存储在本地中
        /// </summary>
        /// <param name="tableName">要查询的表名</param>
        public void LoadData(string tableName)
        {
            string sql = "select * from " + tableName;
            da = new SqlDataAdapter(sql, conn);//定义操作命令
            da.Fill(MyDataSet);
        }
        /// <summary>
        /// 无连接操作，加载执行的数据库，执行sql语句，并加载相关数据到本地,自定义本地数据库的表名
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="MyDataTableName">自定义本地数据库的表名</param>
        public void LoadExecuteData(string sql,string MyDataTableName)
        {
            da = new SqlDataAdapter(sql, conn);//定义操作命令
            da.Fill(MyDataSet, MyDataTableName);
        }
        /// <summary>
        /// 无连接操作，加载执行的数据库，执行sql语句，并加载相关数据到本地
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        public void LoadExecuteData(string sql)
        {
            da = new SqlDataAdapter(sql, conn);//定义操作命令
            da.Fill(MyDataSet);
        }

        /// <summary>
        /// 数据库有连接操作，对连接执行Transact-SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="sql">Sql操作命令语句</param>
        public int ExecuteNonQuery(string sql)
        {
            SqlCommand dacj = new SqlCommand(sql, conn);
            return dacj.ExecuteNonQuery();
        }
        /// <summary>
        /// 数据库有连接操作，数据库查询，传入查询语句，返回bool值
        /// </summary>
        /// <param name="sqlstr">查询的语句</param>
        /// <returns></returns>
        public bool Query(string sqlstr)
        {
            SqlCommand comm = new SqlCommand(sqlstr,conn);
            string str = Convert.ToString(comm.ExecuteScalar());
            if (str != "")
            {
                //MessageBox.Show(str,"有值");
                return true;
            }
            else
            {
                //MessageBox.Show(str, "无值");
                return false;
            }
        }

        /// <summary>
        /// 无连接操作，上传本地数据库
        /// </summary>
        public void UploadData()
        {
            try
            {
                SqlCommandBuilder myCB = new SqlCommandBuilder(da); //将数据适配器封装的sql语句和连接对象转换成可以被执行的命令格式
                //记录在本地数据库中的操作，以便在更新到数据源时可以自动生成更新命令
                da.Update(MyDataSet);//将更改后的本地数据库更新到数据库服务器
                //MessageBox.Show("保存成功！", "提示");
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "提示");
            }
        }
        /// <summary>
        /// 无连接操作，上传本地数据库
        /// </summary>
        /// <param name="MyDataTableName">自定义本地数据库的表名</param>
        public void UploadData(string MyDataTableName)
        {
            try
            {
                SqlCommandBuilder myCB = new SqlCommandBuilder(da); //将数据适配器封装的sql语句和连接对象转换成可以被执行的命令格式
                //记录在本地数据库中的操作，以便在更新到数据源时可以自动生成更新命令
                da.Update(MyDataSet, MyDataTableName);//将更改后的本地数据库更新到数据库服务器
                MessageBox.Show("保存成功！", "提示");
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message, "提示");
            }
        }
        /// <summary>
        /// 无连接操作，清空本地数据库
        /// </summary>
        public void Empty()
        {
            if (MyDataSet != null)
                MyDataSet.Clear();//清空本地储存的所有数据
        }
        /// <summary>
        /// 断开数据库连接，释放资源（只释放连接时产生的资源，不会将本地数据库里的数据清除）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OffData()
        {
            conn.Close();//关闭数据库连接
            conn.Dispose();//释放资源
        }

        

    }
}
