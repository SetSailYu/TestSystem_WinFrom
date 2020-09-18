namespace ExamUIL
{
    partial class login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.label5 = new System.Windows.Forms.Label();
            this.btnStar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxbj = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxxm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxxh = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxzkz = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelTime = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBoxYzm = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("宋体", 15F);
            this.label5.Location = new System.Drawing.Point(472, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(249, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "输入准考证按回车核对信息";
            // 
            // btnStar
            // 
            this.btnStar.Location = new System.Drawing.Point(504, 403);
            this.btnStar.Name = "btnStar";
            this.btnStar.Size = new System.Drawing.Size(217, 51);
            this.btnStar.TabIndex = 13;
            this.btnStar.Text = "开始抽题";
            this.btnStar.UseVisualStyleBackColor = true;
            this.btnStar.Click += new System.EventHandler(this.Button1_Click_1);
            this.btnStar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btnStar_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 15F);
            this.label1.Location = new System.Drawing.Point(490, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "准考证";
            // 
            // textBoxbj
            // 
            this.textBoxbj.Location = new System.Drawing.Point(567, 275);
            this.textBoxbj.Name = "textBoxbj";
            this.textBoxbj.Size = new System.Drawing.Size(154, 21);
            this.textBoxbj.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("宋体", 15F);
            this.label2.Location = new System.Drawing.Point(490, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "学号";
            // 
            // textBoxxm
            // 
            this.textBoxxm.Location = new System.Drawing.Point(567, 222);
            this.textBoxxm.Name = "textBoxxm";
            this.textBoxxm.Size = new System.Drawing.Size(154, 21);
            this.textBoxxm.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("宋体", 15F);
            this.label3.Location = new System.Drawing.Point(490, 222);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "姓名";
            // 
            // textBoxxh
            // 
            this.textBoxxh.Location = new System.Drawing.Point(567, 166);
            this.textBoxxh.Name = "textBoxxh";
            this.textBoxxh.Size = new System.Drawing.Size(154, 21);
            this.textBoxxh.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("宋体", 15F);
            this.label4.Location = new System.Drawing.Point(490, 275);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "班级";
            // 
            // textBoxzkz
            // 
            this.textBoxzkz.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxzkz.Location = new System.Drawing.Point(567, 115);
            this.textBoxzkz.Name = "textBoxzkz";
            this.textBoxzkz.Size = new System.Drawing.Size(154, 21);
            this.textBoxzkz.TabIndex = 5;
            this.textBoxzkz.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox1_KeyPress);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = true;
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.Font = new System.Drawing.Font("宋体", 12F);
            this.labelTime.Location = new System.Drawing.Point(585, 23);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(0, 16);
            this.labelTime.TabIndex = 15;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(442, 314);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(163, 53);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // textBoxYzm
            // 
            this.textBoxYzm.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxYzm.Location = new System.Drawing.Point(621, 326);
            this.textBoxYzm.Name = "textBoxYzm";
            this.textBoxYzm.Size = new System.Drawing.Size(100, 29);
            this.textBoxYzm.TabIndex = 18;
            this.textBoxYzm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxYzm_KeyPress);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.White;
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.Location = new System.Drawing.Point(710, -2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(28, 26);
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ExamClient.Properties.Resources._1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(733, 475);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.textBoxYzm);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBoxxh);
            this.Controls.Add(this.btnStar);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.textBoxzkz);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxbj);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxxm);
            this.Controls.Add(this.label2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "福建船政交通职业学院考试系统";
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Load += new System.EventHandler(this.Login_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.login_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.login_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.login_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnStar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxbj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxxm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxxh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxzkz;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxYzm;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

