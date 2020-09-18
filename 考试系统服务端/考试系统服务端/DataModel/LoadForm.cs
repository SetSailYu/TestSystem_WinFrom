using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace DataModel
{
    public partial class LoadForm : Form
    {
        public int b;
        public LoadForm()
        {
            InitializeComponent();
        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                b += 10;
                if (b == 100)
                {
                    timer1.Enabled = false;
                    this.Hide();
                    this.Close();
                }
            }
            catch
            {
                this.Hide();
                this.Close();
            }
        }
    }
}
