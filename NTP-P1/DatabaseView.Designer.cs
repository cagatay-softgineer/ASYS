namespace NTP_P1
{
    partial class DatabaseView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LabelAF = new System.Windows.Forms.Label();
            this.LabelSF = new System.Windows.Forms.Label();
            this.IDLabel = new System.Windows.Forms.Label();
            this.LabelSM = new System.Windows.Forms.Label();
            this.deleteBtn = new System.Windows.Forms.PictureBox();
            this.updateBtn = new System.Windows.Forms.PictureBox();
            this.LabelTarih = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deleteBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updateBtn)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.BackgroundImage = global::NTP_P1.Properties.Resources._45_degree_fabric_light;
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.42857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.42857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.42857F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.71429F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Controls.Add(this.LabelAF, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelSF, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.IDLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelSM, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.deleteBtn, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.updateBtn, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelTarih, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(880, 47);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // LabelAF
            // 
            this.LabelAF.BackColor = System.Drawing.Color.Gainsboro;
            this.LabelAF.Font = new System.Drawing.Font("JetBrains Mono", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelAF.Location = new System.Drawing.Point(369, 0);
            this.LabelAF.Name = "LabelAF";
            this.LabelAF.Size = new System.Drawing.Size(147, 47);
            this.LabelAF.TabIndex = 6;
            this.LabelAF.Text = "Alış Fiyatı";
            this.LabelAF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelSF
            // 
            this.LabelSF.BackColor = System.Drawing.Color.Gainsboro;
            this.LabelSF.Font = new System.Drawing.Font("JetBrains Mono", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSF.Location = new System.Drawing.Point(216, 0);
            this.LabelSF.Name = "LabelSF";
            this.LabelSF.Size = new System.Drawing.Size(147, 47);
            this.LabelSF.TabIndex = 5;
            this.LabelSF.Text = "Satış Fiyatı";
            this.LabelSF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IDLabel
            // 
            this.IDLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.IDLabel.Font = new System.Drawing.Font("JetBrains Mono", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDLabel.Location = new System.Drawing.Point(3, 0);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(54, 47);
            this.IDLabel.TabIndex = 3;
            this.IDLabel.Text = "ID";
            this.IDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelSM
            // 
            this.LabelSM.BackColor = System.Drawing.Color.Gainsboro;
            this.LabelSM.Font = new System.Drawing.Font("JetBrains Mono", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSM.Location = new System.Drawing.Point(63, 0);
            this.LabelSM.Name = "LabelSM";
            this.LabelSM.Size = new System.Drawing.Size(147, 47);
            this.LabelSM.TabIndex = 4;
            this.LabelSM.Text = "Satış Miktarı";
            this.LabelSM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // deleteBtn
            // 
            this.deleteBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.deleteBtn.Image = global::NTP_P1.Properties.Resources.delete_Colored;
            this.deleteBtn.Location = new System.Drawing.Point(825, 3);
            this.deleteBtn.Name = "deleteBtn";
            this.deleteBtn.Size = new System.Drawing.Size(41, 41);
            this.deleteBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.deleteBtn.TabIndex = 17;
            this.deleteBtn.TabStop = false;
            this.deleteBtn.Click += new System.EventHandler(this.pictureBox1_Click);
            this.deleteBtn.MouseEnter += new System.EventHandler(this.deleteBtn_MouseEnter);
            this.deleteBtn.MouseLeave += new System.EventHandler(this.deleteBtn_MouseLeave);
            // 
            // updateBtn
            // 
            this.updateBtn.BackColor = System.Drawing.SystemColors.ControlLight;
            this.updateBtn.Image = global::NTP_P1.Properties.Resources.update_daily_Data;
            this.updateBtn.InitialImage = global::NTP_P1.Properties.Resources.update_daily_Data;
            this.updateBtn.Location = new System.Drawing.Point(778, 3);
            this.updateBtn.Name = "updateBtn";
            this.updateBtn.Size = new System.Drawing.Size(41, 41);
            this.updateBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.updateBtn.TabIndex = 16;
            this.updateBtn.TabStop = false;
            this.updateBtn.Click += new System.EventHandler(this.updateBtn_Click);
            this.updateBtn.MouseEnter += new System.EventHandler(this.updateBtn_MouseEnter);
            this.updateBtn.MouseLeave += new System.EventHandler(this.updateBtn_MouseLeave);
            // 
            // LabelTarih
            // 
            this.LabelTarih.BackColor = System.Drawing.Color.Gainsboro;
            this.LabelTarih.Font = new System.Drawing.Font("JetBrains Mono", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTarih.Location = new System.Drawing.Point(522, 0);
            this.LabelTarih.Name = "LabelTarih";
            this.LabelTarih.Size = new System.Drawing.Size(250, 47);
            this.LabelTarih.TabIndex = 7;
            this.LabelTarih.Text = "Tarih";
            this.LabelTarih.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DatabaseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "DatabaseView";
            this.Size = new System.Drawing.Size(886, 53);
            this.Load += new System.EventHandler(this.DatabaseView_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.deleteBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updateBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label LabelAF;
        private System.Windows.Forms.Label LabelSF;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.Label LabelSM;
        private System.Windows.Forms.Label LabelTarih;
        private System.Windows.Forms.PictureBox deleteBtn;
        private System.Windows.Forms.PictureBox updateBtn;
    }
}
