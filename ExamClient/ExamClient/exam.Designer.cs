namespace ExamUIL
{
    partial class exam
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(exam));
            this.textBoxTh = new System.Windows.Forms.TextBox();
            this.labelTime = new System.Windows.Forms.Label();
            this.labelKsxm = new System.Windows.Forms.Label();
            this.btnGet = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSySJ = new System.Windows.Forms.Label();
            this.labelXm = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxTh
            // 
            this.textBoxTh.Location = new System.Drawing.Point(90, 55);
            this.textBoxTh.Name = "textBoxTh";
            this.textBoxTh.Size = new System.Drawing.Size(55, 21);
            this.textBoxTh.TabIndex = 22;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.Font = new System.Drawing.Font("宋体", 14F);
            this.labelTime.Location = new System.Drawing.Point(492, 25);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(0, 19);
            this.labelTime.TabIndex = 21;
            // 
            // labelKsxm
            // 
            this.labelKsxm.AutoSize = true;
            this.labelKsxm.Font = new System.Drawing.Font("宋体", 14F);
            this.labelKsxm.Location = new System.Drawing.Point(139, 25);
            this.labelKsxm.Name = "labelKsxm";
            this.labelKsxm.Size = new System.Drawing.Size(0, 19);
            this.labelKsxm.TabIndex = 20;
            // 
            // btnGet
            // 
            this.btnGet.Location = new System.Drawing.Point(456, 365);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(84, 43);
            this.btnGet.TabIndex = 19;
            this.btnGet.Text = "提交";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.BtnGet_Click);
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(263, 365);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(83, 43);
            this.btnDown.TabIndex = 18;
            this.btnDown.Text = "下一题";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.BtnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(63, 366);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(82, 42);
            this.btnUp.TabIndex = 17;
            this.btnUp.Text = "上一题";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.BtnUp_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("宋体", 20F);
            this.richTextBox1.Location = new System.Drawing.Point(39, 82);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(555, 101);
            this.richTextBox1.TabIndex = 15;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.Location = new System.Drawing.Point(35, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "题号";
            // 
            // labelSySJ
            // 
            this.labelSySJ.AutoSize = true;
            this.labelSySJ.BackColor = System.Drawing.Color.Transparent;
            this.labelSySJ.Font = new System.Drawing.Font("宋体", 14F);
            this.labelSySJ.Location = new System.Drawing.Point(337, 25);
            this.labelSySJ.Name = "labelSySJ";
            this.labelSySJ.Size = new System.Drawing.Size(142, 19);
            this.labelSySJ.TabIndex = 13;
            this.labelSySJ.Text = "考试剩余时间：";
            this.labelSySJ.Click += new System.EventHandler(this.labelSySJ_Click);
            // 
            // labelXm
            // 
            this.labelXm.AutoSize = true;
            this.labelXm.BackColor = System.Drawing.Color.Transparent;
            this.labelXm.Font = new System.Drawing.Font("宋体", 14F);
            this.labelXm.ForeColor = System.Drawing.Color.Black;
            this.labelXm.Location = new System.Drawing.Point(38, 25);
            this.labelXm.Name = "labelXm";
            this.labelXm.Size = new System.Drawing.Size(95, 19);
            this.labelXm.TabIndex = 12;
            this.labelXm.Text = "考生姓名:";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("宋体", 12F);
            this.radioButton1.Location = new System.Drawing.Point(3, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(14, 13);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            this.radioButton1.Click += new System.EventHandler(this.RadioButton1_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("宋体", 12F);
            this.radioButton2.Location = new System.Drawing.Point(3, 51);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(14, 13);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Click += new System.EventHandler(this.RadioButton1_Click);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("宋体", 12F);
            this.radioButton3.Location = new System.Drawing.Point(3, 90);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(14, 13);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.Click += new System.EventHandler(this.RadioButton1_Click);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Font = new System.Drawing.Font("宋体", 12F);
            this.radioButton4.Location = new System.Drawing.Point(3, 128);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(14, 13);
            this.radioButton4.TabIndex = 3;
            this.radioButton4.TabStop = true;
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.Click += new System.EventHandler(this.RadioButton1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-14, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(11, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "B";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Location = new System.Drawing.Point(39, 198);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 161);
            this.panel1.TabIndex = 16;
            // 
            // exam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ExamClient.Properties.Resources._1;
            this.ClientSize = new System.Drawing.Size(631, 432);
            this.Controls.Add(this.textBoxTh);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelKsxm);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelSySJ);
            this.Controls.Add(this.labelXm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "exam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "福建船政交通职业学院考试系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.exam_FormClosing);
            this.Load += new System.EventHandler(this.Exam_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxTh;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Label labelKsxm;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelSySJ;
        private System.Windows.Forms.Label labelXm;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
    }
}