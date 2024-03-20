namespace NTP_P1
{
    partial class DataGridShow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataGridShow));
            this.ProductName = new System.Windows.Forms.Label();
            this.searchBar = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lowerDate = new System.Windows.Forms.DateTimePicker();
            this.upperDate = new System.Windows.Forms.DateTimePicker();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.LabelAF = new System.Windows.Forms.Label();
            this.LabelSF = new System.Windows.Forms.Label();
            this.IDLabel = new System.Windows.Forms.Label();
            this.LabelSM = new System.Windows.Forms.Label();
            this.LabelTarih = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // ProductName
            // 
            this.ProductName.BackColor = System.Drawing.Color.Transparent;
            this.ProductName.Font = new System.Drawing.Font("JetBrains Mono", 32F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductName.Location = new System.Drawing.Point(72, 20);
            this.ProductName.Name = "ProductName";
            this.ProductName.Size = new System.Drawing.Size(800, 53);
            this.ProductName.TabIndex = 14;
            this.ProductName.Text = "Product Name";
            this.ProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // searchBar
            // 
            this.searchBar.BackColor = System.Drawing.Color.Gainsboro;
            this.searchBar.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchBar.CausesValidation = false;
            this.searchBar.Font = new System.Drawing.Font("JetBrains Mono", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBar.Location = new System.Drawing.Point(20, 41);
            this.searchBar.Name = "searchBar";
            this.searchBar.Size = new System.Drawing.Size(215, 20);
            this.searchBar.TabIndex = 0;
            this.searchBar.Text = "Search ID";
            this.searchBar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.searchBar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.searchBar_MouseClick);
            this.searchBar.TextChanged += new System.EventHandler(this.searchBar_TextChanged);
            this.searchBar.Leave += new System.EventHandler(this.searchBar_Leave);
            this.searchBar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.searchBar_MouseDoubleClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackgroundImage = global::NTP_P1.Properties.Resources._45_degree_fabric_light;
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 152F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 153F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 154F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 232F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 102F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox9, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelAF, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelSF, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.IDLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelSM, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.LabelTarih, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 5, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 73);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(912, 47);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lowerDate);
            this.groupBox1.Controls.Add(this.upperDate);
            this.groupBox1.Location = new System.Drawing.Point(856, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(53, 41);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // lowerDate
            // 
            this.lowerDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lowerDate.CalendarMonthBackground = System.Drawing.Color.Gainsboro;
            this.lowerDate.Font = new System.Drawing.Font("JetBrains Mono", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lowerDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.lowerDate.Location = new System.Drawing.Point(3, 0);
            this.lowerDate.Name = "lowerDate";
            this.lowerDate.Size = new System.Drawing.Size(49, 20);
            this.lowerDate.TabIndex = 1;
            this.lowerDate.Value = new System.DateTime(2023, 11, 7, 0, 0, 0, 0);
            this.lowerDate.ValueChanged += new System.EventHandler(this.lowerDate_ValueChanged);
            // 
            // upperDate
            // 
            this.upperDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.upperDate.CalendarMonthBackground = System.Drawing.Color.Gainsboro;
            this.upperDate.Font = new System.Drawing.Font("JetBrains Mono", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upperDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.upperDate.Location = new System.Drawing.Point(4, 21);
            this.upperDate.Name = "upperDate";
            this.upperDate.Size = new System.Drawing.Size(49, 20);
            this.upperDate.TabIndex = 2;
            this.upperDate.Value = new System.DateTime(2023, 11, 20, 0, 0, 0, 0);
            this.upperDate.ValueChanged += new System.EventHandler(this.lowerDate_ValueChanged);
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox9.Image = global::NTP_P1.Properties.Resources.update;
            this.pictureBox9.InitialImage = global::NTP_P1.Properties.Resources.update;
            this.pictureBox9.Location = new System.Drawing.Point(801, 3);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(41, 41);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox9.TabIndex = 26;
            this.pictureBox9.TabStop = false;
            this.pictureBox9.Click += new System.EventHandler(this.pictureBox9_Click);
            this.pictureBox9.MouseEnter += new System.EventHandler(this.pictureBox9_MouseEnter);
            this.pictureBox9.MouseLeave += new System.EventHandler(this.pictureBox9_MouseLeave);
            // 
            // LabelAF
            // 
            this.LabelAF.BackColor = System.Drawing.Color.Gainsboro;
            this.LabelAF.Font = new System.Drawing.Font("JetBrains Mono", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelAF.Location = new System.Drawing.Point(368, 0);
            this.LabelAF.Name = "LabelAF";
            this.LabelAF.Size = new System.Drawing.Size(148, 47);
            this.LabelAF.TabIndex = 6;
            this.LabelAF.Text = "Alış Fiyatı";
            this.LabelAF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelSF
            // 
            this.LabelSF.BackColor = System.Drawing.Color.Gainsboro;
            this.LabelSF.Font = new System.Drawing.Font("JetBrains Mono", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSF.Location = new System.Drawing.Point(215, 0);
            this.LabelSF.Name = "LabelSF";
            this.LabelSF.Size = new System.Drawing.Size(147, 47);
            this.LabelSF.TabIndex = 5;
            this.LabelSF.Text = "Satış Fiyatı";
            this.LabelSF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IDLabel
            // 
            this.IDLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.IDLabel.Font = new System.Drawing.Font("JetBrains Mono", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.LabelSM.Font = new System.Drawing.Font("JetBrains Mono", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSM.Location = new System.Drawing.Point(63, 0);
            this.LabelSM.Name = "LabelSM";
            this.LabelSM.Size = new System.Drawing.Size(146, 47);
            this.LabelSM.TabIndex = 4;
            this.LabelSM.Text = "Satış Miktarı";
            this.LabelSM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelTarih
            // 
            this.LabelTarih.BackColor = System.Drawing.Color.Gainsboro;
            this.LabelTarih.Font = new System.Drawing.Font("JetBrains Mono", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTarih.Location = new System.Drawing.Point(522, 0);
            this.LabelTarih.Name = "LabelTarih";
            this.LabelTarih.Size = new System.Drawing.Size(226, 47);
            this.LabelTarih.TabIndex = 7;
            this.LabelTarih.Text = "Tarih";
            this.LabelTarih.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::NTP_P1.Properties.Resources.data_analytics;
            this.pictureBox1.InitialImage = global::NTP_P1.Properties.Resources.update;
            this.pictureBox1.Location = new System.Drawing.Point(754, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 41);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BackgroundImage = global::NTP_P1.Properties.Resources._45_degree_fabric_light;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(13, 121);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(919, 548);
            this.flowLayoutPanel1.TabIndex = 16;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox7.Image = global::NTP_P1.Properties.Resources.exit;
            this.pictureBox7.InitialImage = global::NTP_P1.Properties.Resources.exit;
            this.pictureBox7.Location = new System.Drawing.Point(868, 3);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(64, 64);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox7.TabIndex = 15;
            this.pictureBox7.TabStop = false;
            this.pictureBox7.Click += new System.EventHandler(this.pictureBox7_Click);
            this.pictureBox7.MouseEnter += new System.EventHandler(this.pictureBox7_MouseEnter);
            this.pictureBox7.MouseLeave += new System.EventHandler(this.pictureBox7_MouseLeave);
            // 
            // DataGridShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(944, 681);
            this.Controls.Add(this.searchBar);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.ProductName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataGridShow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataGridShow";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.DataGridShow_FormClosed);
            this.Load += new System.EventHandler(this.DataGridShow_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridShow_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DataGridShow_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DataGridShow_MouseUp);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public new System.Windows.Forms.Label ProductName;
        private System.Windows.Forms.PictureBox pictureBox7;
        public System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.DateTimePicker lowerDate;
        private System.Windows.Forms.DateTimePicker upperDate;
        public System.Windows.Forms.TextBox searchBar;
        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Label LabelTarih;
        public System.Windows.Forms.Label LabelSM;
        public System.Windows.Forms.Label LabelSF;
        public System.Windows.Forms.Label LabelAF;
    }
}