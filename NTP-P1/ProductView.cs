
#region Packages
using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace NTP_P1
{
    public partial class ProductView : UserControl
    {
        #region Variable Definition
        public BProduct BProduct { get; set; }
        public static bool isRoot, isEmployee, isDefaultUser;
        public static DataGridShow DataGridShowInstance;
        public static UpdateProducts operationsForProduct;
        public static DetailedProductAnalysis productAnalysis;
        Database db = new Database();
        #endregion

        #region Form Implementation
        public ProductView()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Load Processes
        private void ProductView_Load(object sender, EventArgs e)
        {
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentCurrentColor;

            LabelSF.Text = Convert.ToString(BProduct.SatisFiyati);
            LabelUA.Text = Convert.ToString(BProduct.UrunAdı);
            LabelUG.Text = Convert.ToString(BProduct.UrunGrubu);
            IDLabel.Text = Convert.ToString(BProduct.Id);
            LabelSTOK.Text = Convert.ToString(BProduct.StokMiktari);
            LabelTarih.Text = Convert.ToDateTime(BProduct.Tarih).ToString("dd.MM.yyyy");
            if (isRoot)
            {
                updateBtn.Enabled = true;
                deleteBtn.Enabled = true;
                InItTable.Enabled = true;
                updateBtn.Visible = true;
                deleteBtn.Visible = true;
                InItTable.Visible = true;
            }
            else if (isDefaultUser)
            {
                updateBtn.Enabled = false;
                deleteBtn.Enabled = false;
                InItTable.Enabled = false;
                updateBtn.Visible = false;
                deleteBtn.Visible = false;
                InItTable.Visible = false;
            }
            else if (isEmployee)
            {
                updateBtn.Enabled = true;
                deleteBtn.Enabled = true;
                InItTable.Enabled = true;
                updateBtn.Visible = true;
                deleteBtn.Visible = true;
                InItTable.Visible = true;
            }
            else
            {
                updateBtn.Enabled = false;
                deleteBtn.Enabled = false;
                InItTable.Enabled = false;
                updateBtn.Visible = false;
                deleteBtn.Visible = false;
                InItTable.Visible = false;
            }
        }
        #endregion

        #region Show Detailed Product Analysis Form
        public void InItTable_Click(object sender, EventArgs e)
        {
            if (productAnalysis == null || productAnalysis.IsDisposed && !DetailedProductAnalysis.AcikMi)
            {
                Program.CursorChange();
                productAnalysis = new DetailedProductAnalysis();
                Program.currentProduct = this.LabelUA.Text;
                productAnalysis.getDataFromSelectedProduct(this.IDLabel.Text, this.LabelUA.Text, this.LabelUG.Text, this.LabelSF.Text, this.LabelTarih.Text,this.LabelSTOK.Text);
                productAnalysis.GetResponsiblePersonelName(this.LabelUG.Text);
                productAnalysis.Show();


                //DetailedProductAnalysis.tableName = this.BProduct.UrunAdı;
                //DataGridShowInstance = new DataGridShow();
                //DataGridShow.dtTableName = this.BProduct.UrunAdı;
                //DataGridShowInstance.Show();
                //DatabaseView.tableName = this.BProduct.UrunAdı;
            }
            else
            {
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region PictureBoxs Animation
        private void deleteBtn_MouseHover(object sender, EventArgs e)
        {
            deleteBtn.BackColor = SystemColors.ControlDark;
        }

        private void deleteBtn_MouseLeave(object sender, EventArgs e)
        {
            deleteBtn.BackColor = SystemColors.ControlLight;
        }

        private void updateBtn_MouseHover(object sender, EventArgs e)
        {
            updateBtn.BackColor = SystemColors.ControlDark;
        }

        private void updateBtn_MouseLeave(object sender, EventArgs e)
        {
            updateBtn.BackColor = SystemColors.ControlLight;
        }

        private void updateBtn_MouseEnter(object sender, EventArgs e)
        {
            updateBtn.BackColor = SystemColors.ControlDark;
        }

        private void InItTable_MouseEnter(object sender, EventArgs e)
        {
            InItTable.BackColor = SystemColors.ControlDark;
        }

        private void InItTable_MouseLeave(object sender, EventArgs e)
        {
            InItTable.BackColor = SystemColors.ControlLight;
        }

        private void LabelSTOK_Click(object sender, EventArgs e)
        {
            string userInput = Interaction.InputBox("Güncellemek İstediğiniz Stok Değerini Giriniz:", "Stok", this.LabelSTOK.Text);
            // Try to parse the input to an integer
            if (int.TryParse(userInput, out int stok))
            {
                if (stok > 0)
                {
                    string formattedDate = String.Format("{0:dd/MM/yyyy}", DateTime.Now);

                    string[] changes = {
                    $"StokMiktari='{stok}'",
                    $"EklenmeVeyaGuncellenmeTarihi='{formattedDate}'"
                    };

                    Database.Connect.Open();
                    Database.CommonUpdate("Urun", changes, this.IDLabel.Text);
                    Database.Connect.Close();
                    if (MainPage.isRoot)
                    {
                        Program.mainPage.GetAllProduct();
                    }
                    else if (MainPage.isEmployee)
                    {
                        string group = db.GetGroupWithUsername(LoginPage.kullaniciAdi);
                        Program.mainPage.GetJustAccessedProducts(group);
                    }
                }
                else
                {
                    MessageBox.Show("Girilen Değer 0'dan Büyük Olmalıdır!");
                }
            }
            else
            {
                MessageBox.Show("Sayısal Bir Değer Giriniz!");
            }
            
        }

        private void LabelSTOK_MouseEnter(object sender, EventArgs e)
        {
            LabelSTOK.BackColor = DarkenColor(LabelSTOK.BackColor, 0.2f);
        }

        private void LabelSTOK_MouseLeave(object sender, EventArgs e)
        {
            LabelSTOK.BackColor = Color.Gainsboro;
        }
        #endregion

        #region Darken Background Color For Every Color
        private Color DarkenColor(Color color, float factor)
        {
            int r = (int)(color.R * (1 - factor));
            int g = (int)(color.G * (1 - factor));
            int b = (int)(color.B * (1 - factor));

            return Color.FromArgb(r, g, b);
        }
        #endregion

        #region Delete Product
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Program.Output[92], Program.Output[93], MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Database.Connect.Open();
                Database.Delete("Urun", this.IDLabel.Text);
                Database.Connect.Close();
                if (MainPage.isRoot)
                {
                    Program.mainPage.GetAllProduct();
                }
                else if (MainPage.isEmployee)
                {
                    string group = db.GetGroupWithUsername(LoginPage.kullaniciAdi);
                    Program.mainPage.GetJustAccessedProducts(group);
                }
                System.GC.Collect();
            }
            else
            {
                //Do Nothing
            }

        }
        #endregion

        #region Update Product
        private void updateBtn_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"{this.BProduct.UrunGrubu}");
            if (operationsForProduct == null || operationsForProduct.IsDisposed && !UpdateProducts.AcikMi)
            {
                operationsForProduct = new UpdateProducts();
                operationsForProduct.getDataFromSelectedProduct(this.IDLabel.Text, this.LabelUA.Text, this.LabelUG.Text, this.LabelSF.Text, this.LabelTarih.Text, this.LabelSTOK.Text);
                operationsForProduct.Show();
            }
            else
            {
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion
    }
}
