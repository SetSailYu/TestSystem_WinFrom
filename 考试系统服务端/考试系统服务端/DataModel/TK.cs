using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Serializable]
    public class TK
    {
        private int th;
        private string tmnr;
        private string tmzj;
        private string answer1;
        private string answer2;
        private string answer3;
        private string answer4;
        private string result;

        public int Th { get => th; set => th = value; }
        public string Tmnr { get => tmnr; set => tmnr = value; }
        public string Tmzj { get => tmzj; set => tmzj = value; }
        public string Answer1 { get => answer1; set => answer1 = value; }
        public string Answer2 { get => answer2; set => answer2 = value; }
        public string Answer3 { get => answer3; set => answer3 = value; }
        public string Answer4 { get => answer4; set => answer4 = value; }
        public string Result { get => result; set => result = value; }

        public TK(int th, string tmnr, string tmzj, string answer1, string answer2, string answer3, string answer4, string result)
        {
            this.Th = th;
            this.Tmnr = tmnr;
            this.Tmzj = tmzj;
            this.Answer1 = answer1;
            this.Answer2 = answer2;
            this.Answer3 = answer3;
            this.Answer4 = answer4;
            this.Result = result;
        }
    }
}
