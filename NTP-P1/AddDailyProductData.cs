
#region Packages
using System;
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace NTP_P1
{
    public partial class AddDailyProductData : Form
    {
        #region Variable Definition
        string[] groups;
        Database db = new Database();
        private bool mouseDown;
        private Point lastLocation;
        public static bool AcikMi;
        object selectedValue;
        DateTime time = DateTime.Now;
        string errorSTR;
        #endregion

        #region Form Implementation
        public AddDailyProductData()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Load Processes
        private void AddDailyProductData_Load(object sender, EventArgs e)
        {
            Tarih.Value = time;
            AcikMi = true;
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentCurrentColor;

            Program.SetLanguage(Program.LanguagePack);
            if (db.CheckInfoExist(Program.currentAnalysisData[1]))
            {
                string[] SFS = db.GetValuesOfColumnSortedByDate(Program.currentAnalysisData[1], "SatışFiyatı");
                string[] AFS = db.GetValuesOfColumnSortedByDate(Program.currentAnalysisData[1], "AlışFiyatı");
                SF.Text = SFS[SFS.Length-1];
                AF.Text = AFS[AFS.Length-1];
            }
        }
        #endregion

        #region Add Daily Product Data Method
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string formattedDate = String.Format("{0:dd/MM/yyyy}", Tarih.Value);
            errorSTR = "";
            if (IsNumeric(SM.Text) && IsNumeric(SF.Text) && IsNumeric(AF.Text) && AF.Text.Length > 0 && SF.Text.Length > 0 && SM.Text.Length > 0 && !Database.DateExistsInDatabase(DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.Substring(DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.IndexOf("-") + 1).ToString(), formattedDate) && !(int.Parse(Program.currentAnalysisData[5])<int.Parse(SM.Text)))
            {
                string stok = $"{int.Parse(Program.currentAnalysisData[5]) - int.Parse(SM.Text)}";

                string[] columns = { "SatışMiktarı", "SatışFiyatı", "AlışFiyatı", "Tarih" , "StokMiktari" };
                string[] values = { SM.Text, SF.Text, AF.Text, formattedDate, stok};
                Database.Connect.Open();
                Database.Insert(DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.Substring(DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.IndexOf("-") + 1).ToString(), columns, values);
                Database.Connect.Close();

                string formattedDateNow = String.Format("{0:dd/MM/yyyy}", DateTime.Now);

                string[] changes = {
                      $"StokMiktari='{stok}'",
                      $"EklenmeVeyaGuncellenmeTarihi='{formattedDate}'"
                      };

                Database.Connect.Open();
                Database.CommonUpdateSilent("Urun", changes, Program.currentAnalysisData[0]);
                Database.Connect.Close();

                if (MainPage.isRoot)
                {
                    Program.mainPage.GetAllProduct();
                }
                else if (MainPage.isEmployee)
                {
                    MainPage.group = db.GetGroupWithUsername(LoginPage.kullaniciAdi);
                    Program.mainPage.GetJustAccessedProducts(MainPage.group);
                }

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
                if (Database.DateExistsInDatabase(DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.Substring(DetailedProductAnalysis.DataGridShowInstance.ProductName.Text.IndexOf("-") + 1).ToString(), formattedDate))
                {
                    errorSTR += $"{Program.Output[125]}\n";
                }
                if (SM.Text.Length>0)
                {
                    if (int.Parse(Program.currentAnalysisData[5]) < int.Parse(SM.Text))
                    {
                        errorSTR += $"Girilen Satış Miktarı Stok Sayısından Fazla!\n";
                    }
                }
                MessageBox.Show(errorSTR, Program.Output[19]);
            }
        }
        #endregion

        #region Check The Numericity Of The Input
        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _);
        }
        #endregion

        #region Close The Form 
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            System.GC.Collect();
            AcikMi = false;
            DetailedProductAnalysis.DataGridShowInstance.GetAll();
        }

        private void AddDailyProductData_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            System.GC.Collect();
            AcikMi = false;
            DetailedProductAnalysis.DataGridShowInstance.GetAll();
        }
        #endregion

        #region Move The Form 
        private void AddDailyProductData_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void AddDailyProductData_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void AddDailyProductData_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }
        #endregion

        #region PictureBox Animation
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
        #endregion

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