using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataModel;
using ExamDAL;
using System.Net;

namespace ExamBLL
{
    public class Transaction
    {
        //BLL层主要起中转作用 
        #region 得到用户信息
        public static Student GetUser(Student ks, IPAddress ip, int port)
        {
            Student sk1 = ExamDAL.DataAccess.GetUser(ks, ip, port);
            return sk1;
        }
        #endregion

        #region 得到题目
        public static List<TK> GetTiMu(Student ks, IPAddress ip, int port)
        {
            List<TK> tm = ExamDAL.DataAccess.GetTiMu(ks, ip, port);
            return tm;
        }
        #endregion

        #region 交卷       
        public static string UpResult(Student ks, IPAddress ip, int port)
        {
            string msg = ExamDAL.DataAccess.UpResult(ks, ip, port);
            return msg;
        }
        #endregion
    }
}

