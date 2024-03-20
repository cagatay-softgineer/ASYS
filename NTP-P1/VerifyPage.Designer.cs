namespace NTP_P1
{
    partial class VerifyPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerifyPage));
            this.txtBoxVerifyCode = new System.Windows.Forms.TextBox();
            this.btnVerifyCodeCheck = new System.Windows.Forms.Button();
            this.VerifyCode = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBoxVerifyCode
            // 
            this.txtBoxVerifyCode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtBoxVerifyCode.BackColor = System.Drawing.Color.Gainsboro;
            this.txtBoxVerifyCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxVerifyCode.Font = new System.Drawing.Font("JetBrains Mono", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxVerifyCode.Location = new System.Drawing.Point(448, 249);
            this.txtBoxVerifyCode.Name = "txtBoxVerifyCode";
            this.txtBoxVerifyCode.Size = new System.Drawing.Size(523, 32);
            this.txtBoxVerifyCode.TabIndex = 0;
            this.txtBoxVerifyCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBoxVerifyCode.TextChanged += new System.EventHandler(this.txtBoxVerifyCode_TextChanged);
            // 
            // btnVerifyCodeCheck
            // 
            this.btnVerifyCodeCheck.BackColor = System.Drawing.Color.Black;
            this.btnVerifyCodeCheck.Font = new System.Drawing.Font("JetBrains Mono", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVerifyCodeCheck.ForeColor = System.Drawing.Color.White;
            this.btnVerifyCodeCheck.Location = new System.Drawing.Point(448, 295);
            this.btnVerifyCodeCheck.Name = "btnVerifyCodeCheck";
            this.btnVerifyCodeCheck.Size = new System.Drawing.Size(523, 64);
            this.btnVerifyCodeCheck.TabIndex = 2;
            this.btnVerifyCodeCheck.Text = "Kontrol Et";
            this.btnVerifyCodeCheck.UseVisualStyleBackColor = false;
            this.btnVerifyCodeCheck.Click += new System.EventHandler(this.btnVerifyCodeCheck_Click);
            this.btnVerifyCodeCheck.MouseEnter += new System.EventHandler(this.btnVerifyCodeCheck_MouseHover);
            this.btnVerifyCodeCheck.MouseLeave += new System.EventHandler(this.btnVerifyCodeCheck_MouseLeave);
            // 
            // VerifyCode
            // 
            this.VerifyCode.Font = new System.Drawing.Font("JetBrains Mono", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VerifyCode.Location = new System.Drawing.Point(448, 52);
            this.VerifyCode.Name = "VerifyCode";
            this.VerifyCode.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.VerifyCode.Size = new System.Drawing.Size(523, 115);
            this.VerifyCode.TabIndex = 1;
            this.VerifyCode.Text = "DOĞRULAMA KODU";
            this.VerifyCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VerifyCode.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NTP_P1.Properties.Resources.previous2;
            this.pictureBox1.InitialImage = global::NTP_P1.Properties.Resources.previous2;
            this.pictureBox1.Location = new System.Drawing.Point(409, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.Image = global::NTP_P1.Properties.Resources.shield;
            this.pictureBox3.InitialImage = global::NTP_P1.Properties.Resources.verified;
            this.pictureBox3.Location = new System.Drawing.Point(408, 249);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 40;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox7.Image = global::NTP_P1.Properties.Resources.exit_black;
            this.pictureBox7.InitialImage = global::NTP_P1.Properties.Resources.exit_black;
            this.pictureBox7.Location = new System.Drawing.Point(980, 11);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(32, 32);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 39;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.Click += new System.EventHandler(this.pictureBox7_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Black;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(400, 512);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 35;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // VerifyPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 512);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnVerifyCodeCheck);
            this.Controls.Add(this.VerifyCode);
            this.Controls.Add(this.txtBoxVerifyCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VerifyPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verify Code";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChangePassword_FormClosed);
            this.Click += new System.EventHandler(this.pictureBox2_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VerifyPage_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VerifyPage_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VerifyPage_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtBoxVerifyCode;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button btnVerifyCodeCheck;
        public System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox7;
        public System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.Label VerifyCode;
    }
}