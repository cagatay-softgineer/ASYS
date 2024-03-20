
#region Packages
using System;
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace NTP_P1
{
    public partial class DatabaseView : UserControl
    {
        #region Variable Definition
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public static string tableName;
        public static UpdateDailyProductData updateDailyProductData;
        #endregion

        #region Form Implementation
        public DatabaseView()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Load Processes
        private void DatabaseView_Load(object sender, EventArgs e)
        {
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentCurrentColor;

            LabelSF.Text = Convert.ToString(Product.SatisFiyati);
            LabelSM.Text = Convert.ToString(Product.SatisMiktari);
            LabelAF.Text = Convert.ToString(Product.AlisFiyati);
            IDLabel.Text = Convert.ToString(Product.Id);
            LabelTarih.Text = Convert.ToDateTime(Product.Tarih).ToString("dd.MM.yyyy");
        }
        #endregion

        #region Delete Daily Product Data
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Program.Output[105], Program.Output[93], MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Database.Connect.Open();
                Database.Delete($"{tableName}", this.IDLabel.Text);
                Database.Connect.Close();
                DetailedProductAnalysis.DataGridShowInstance.GetAll();
                ProductView.productAnalysis.getDataFromSelectedProduct(Program.currentAnalysisData[0], Program.currentAnalysisData[1], Program.currentAnalysisData[2], Program.currentAnalysisData[3], Program.currentAnalysisData[4], Program.currentAnalysisData[5]);
                System.GC.Collect();
            }
            else
            {
                //Do Nothing
            }
        }
        #endregion

        #region Open Update Daily Product Data Form
        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (updateDailyProductData == null || updateDailyProductData.IsDisposed && !UpdateDailyProductData.AcikMi)
            {
                updateDailyProductData = new UpdateDailyProductData();
                updateDailyProductData.getDataFromSelectedUser(this.IDLabel.Text, this.LabelSM.Text, this.LabelSF.Text, this.LabelAF.Text, this.LabelTarih.Text);
                updateDailyProductData.Show();
            }
            else
            {
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region PictureBoxs Animation
        private void updateBtn_MouseEnter(object sender, EventArgs e)
        {
            updateBtn.BackColor = SystemColors.ControlDark;
        }

        private void updateBtn_MouseLeave(object sender, EventArgs e)
        {
            updateBtn.BackColor = SystemColors.ControlLight;
        }

        private void deleteBtn_MouseEnter(object sender, EventArgs e)
        {
            deleteBtn.BackColor = SystemColors.ControlDark;
        }

        private void deleteBtn_MouseLeave(object sender, EventArgs e)
        {
            deleteBtn.BackColor = SystemColors.ControlLight;
        }
        #endregion
    }
}
