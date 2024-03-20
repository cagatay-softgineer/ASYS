using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace NTP_P1
{
    public partial class UpdateDailyProductData : Form
    {
        public static bool AcikMi = false;
        Database db = new Database();
        private bool mouseDown;
        private Point lastLocation;
        public static string errorSTR;
        public static string IDx;
        DateTime time = DateTime.Now;
        public UpdateDailyProductData()
        {
            InitializeComponent();
        }

        private void UpdateDailyProductData_Load(object sender, EventArgs e)
        {
            AcikMi = true;
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentColor;

            Program.SetLanguage(Program.LanguagePack);
        }

        public void getDataFromSelectedUser(string ID, string satisMiktari, string satisFiyati, string alisFiyati, string tarih)
        {
            ProductName.Text = $"{ID}::{Program.currentProduct}";
            IDx = ID;
            SM.Text = satisMiktari;
            SF.Text = satisFiyati;
            AF.Text = alisFiyati;

            Tarih.Value = Convert.ToDateTime(tarih);

        }

        private void UpdateDailyProductData_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            AcikMi = false;
            System.GC.Collect();
            DetailedProductAnalysis.DataGridShowInstance.GetAll();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            AcikMi = false;
            System.GC.Collect();
            DetailedProductAnalysis.DataGridShowInstance.GetAll();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string[] idS = db.GetValuesOfColumn(DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.Substring(DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.IndexOf("-") + 1).ToString(), "ID");
            idS = idS.Where(element => element != IDx).ToArray();
            string formattedDate = String.Format("{0:dd/MM/yyyy}", Tarih.Value);
            errorSTR = "";
            if (IsNumeric(SM.Text) && IsNumeric(SF.Text) && IsNumeric(AF.Text) && AF.Text.Length > 0 && SF.Text.Length > 0 && SM.Text.Length > 0 && !db.DateExistsForIDs(formattedDate, idS, DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.Substring(DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.IndexOf("-") + 1).ToString()))
            {

                string[] changes = {
                        $"SatışMiktarı='{SM.Text}'",
                        $"SatışFiyatı='{SF.Text}'",
                        $"AlışFiyatı='{AF.Text}'",
                        $"Tarih='{formattedDate}'"
                        };

                Database.Connect.Open();
                Database.CommonUpdate(DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.Substring(DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.IndexOf("-") + 1).ToString(), changes, IDx);
                Database.Connect.Close();
                DetailedProductAnalysis.DataGridShowInstance.GetAll();
                if (db.CheckInfoExist(Program.currentProduct))
                    ProductView.productAnalysis.getDataFromSelectedProduct(Program.currentAnalysisData[0], Program.currentAnalysisData[1], Program.currentAnalysisData[2], Program.currentAnalysisData[3], Program.currentAnalysisData[4], Program.currentAnalysisData[5]);
            }
            else
            {
                if (SM.Text.Length < 1)
                {
                    errorSTR += $"{Program.Output[119]}\n";
                }
                else if (!IsNumeric(SM.Text))
                {
                    errorSTR += $"{Program.Output[120]}\n";
                }

                if (SF.Text.Length < 1)
                {
                    errorSTR += $"{Program.Output[121]}\n";
                }
                else if (!IsNumeric(SF.Text))
                {
                    errorSTR += $"{Program.Output[122]}\n";
                }

                if (AF.Text.Length < 1)
                {
                    errorSTR += $"{Program.Output[123]}\n";
                }
                else if (!IsNumeric(AF.Text))
                {
                    errorSTR += $"{Program.Output[124]}\n";
                }
                if (db.DateExistsForIDs(formattedDate, idS, DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.Substring(DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.IndexOf("-") + 1).ToString()))
                {
                    errorSTR += $"{Program.Output[125]}\n";
                }
                MessageBox.Show(errorSTR, Program.Output[19]);
            }
        }
        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _);
        }

        private void UpdateDailyProductData_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void UpdateDailyProductData_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void UpdateDailyProductData_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.BackColor = SystemColors.ControlDark;
            pictureBox7.Image = Properties.Resources.exit_darken;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.BackColor = Color.Transparent;
            pictureBox7.Image = pictureBox7.InitialImage;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = SystemColors.ControlDark;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
        }
    }
}
