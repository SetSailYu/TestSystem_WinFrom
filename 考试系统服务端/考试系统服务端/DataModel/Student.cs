using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Serializable]
    public class Student
    {
        /// <summary>
        /// 方法 枚举
        /// </summary>
        public enum Way : int
        {
            /// <summary>
            /// 登录
            /// </summary>
            LogIn,
            /// <summary>
            /// 抽题
            /// </summary>
            Extracting,
            /// <summary>
            /// 交卷
            /// </summary>
            PutIn
        }

        public string Zkz { get => zkz; set => zkz = value; }
        public string Xh { get => xh; set => xh = value; }
        public string Xm { get => xm; set => xm = value; }
        public string Bj { get => bj; set => bj = value; }
        public string State { get => state; set => state = value; }
        public string Ip { get => ip; set => ip = value; }
        public Way Cmd { get => cmd; set => cmd = value; }
        public List<Ksresult> Result;

        private string zkz;
        private string xh;
        private string xm;
        private string bj;
        private string state;
        private string ip;
        private Way cmd;

        public Student(string s1,Way way)
        {
            this.Zkz = s1;
            this.Cmd = way;
        }

    }
}
