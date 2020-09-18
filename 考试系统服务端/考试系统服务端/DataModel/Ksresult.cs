using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Serializable]
    public class Ksresult
    {
        public string Zkz { get => zkz; set => zkz = value; }
        public int Ktxh { get => ktxh; set => ktxh = value; }
        public int Tkth { get => tkth; set => tkth = value; }
        public string Xsanswer { get => xsanswer; set => xsanswer = value; }
        public string Tkanswer { get => tkanswer; set => tkanswer = value; }
        public Student.Way Cmd { get => cmd; set => cmd = value; }

        private string zkz;
        private int ktxh;
        private int tkth;
        private string xsanswer;
        private string tkanswer;
        private Student.Way cmd;

        public Ksresult(){ }
    }
}
