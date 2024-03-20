
#region Packages
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
#endregion

namespace NTP_P1
{
    public partial class AddProduct : Form
    {
        #region Variable Definition
        string[] groups;
        string group;
        Database db = new Database();
        private bool mouseDown;
        private Point lastLocation;
        public static bool AcikMi;
        object selectedValue;
        DateTime time = DateTime.Now;
        string errorSTR;
        #endregion

        #region Form Implementation
        public AddProduct()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Load Processes
        private void AddProduct_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            AcikMi = true;
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentCurrentColor;

            Program.SetLanguage(Program.LanguagePack);
            if (MainPage.isRoot)
            {
                groups = db.GetValuesOfColumnDiff("Urun", "UrunGrubu");
                checkBox1.Visible = true;
                foreach(var group in groups)
                {
                    comboBox1.Items.Add(group);
                }
            }
            else if (MainPage.isEmployee)
            {
                group = db.GetGroupWithUsername(LoginPage.kullaniciAdi);
                comboBox1.Items.Add(group);
                checkBox1.Visible = false;
            }
            else
            {
                checkBox1.Visible = true;
            }
            

            

        }
        #endregion

        #region Add Product Method
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            errorSTR = "";
            selectedValue = comboBox1.SelectedItem;
            if (!db.CheckProductExists(UrunAdi.Text))
            {
                if (IsNumeric(UrunSatisFiyati.Text))
                {

                    if (comboBox1.SelectedIndex != -1 || checkBox1.Checked)
                    {

                        if (checkBox1.Checked)
                        {
                            string[] columns = { "UrunAdi", "UrunGrubu", "UrunSatisFiyati", "EklenmeVeyaGuncellenmeTarihi" };
                            string[] values = { UrunAdi.Text, comboBox1.Text, UrunSatisFiyati.Text, String.Format("{0:dd/MM/yyyy}", time) };

                            Database.Connect.Open();
                            Database.InsertSilent("Urun", columns, values);
                            Database.Connect.Close();
                            Database.TableCreateSilent(UrunAdi.Text);
                            string[] col = { "SatışMiktarı", "SatışFiyatı", "AlışFiyatı", "Tarih", "StokMiktari" };
                            string[] val = { "0", UrunSatisFiyati.Text, "0", String.Format("{0:dd/MM/yyyy}", time), "0" };
                            Database.Connect.Open();
                            Database.Insert(UrunAdi.Text, col, val);
                            Database.Connect.Close();
                        }
                        else
                        {
                            string[] columns = { "UrunAdi", "UrunGrubu", "UrunSatisFiyati", "EklenmeVeyaGuncellenmeTarihi" };
                            string[] values = { UrunAdi.Text, Convert.ToString(selectedValue), UrunSatisFiyati.Text, String.Format("{0:dd/MM/yyyy}", time) };

                            Database.Connect.Open();
                            Database.InsertSilent("Urun", columns, values);
                            Database.Connect.Close();
                            Database.TableCreateSilent(UrunAdi.Text);
                            string[] col = { "SatışMiktarı", "SatışFiyatı", "AlışFiyatı", "Tarih", "StokMiktari" };
                            string[] val = { "0", UrunSatisFiyati.Text, "0", String.Format("{0:dd/MM/yyyy}", time), "0" };
                            Database.Connect.Open();
                            Database.Insert(UrunAdi.Text, col, val);
                            Database.Connect.Close();
                        }
                            
                        
                        this.Dispose();
                        System.GC.Collect();
                        AcikMi = false;
                        if (ProfitShow.AcikMi)
                        {
                            MainPage.profitShow.Update_Group();
                        }

                        if (MainPage.isRoot)
                        {
                            Program.mainPage.GetAllProduct();
                            Program.mainPage.get_gruops();
                        }
                        else if (MainPage.isEmployee)
                        {
                            string group = db.GetGroupWithUsername(LoginPage.kullaniciAdi);
                            Program.mainPage.GetJustAccessedProducts(group);
                        }
                    }
                    else
                    {
                        if (UrunAdi.Text.Length < 2)
                        {
                            errorSTR += $"{Program.Output[45]}\n";
                            //MessageBox.Show("Kullanıcı Adı Yetersiz Uzunlukta!");
                        }
                        if (Convert.ToString(selectedValue).Length < 1)
                        {
                            errorSTR += $"{Program.Output[47]}\n";
                        }
                        if (db.CheckProductExists(UrunAdi.Text))
                        {
                            errorSTR += $"{Program.Output[48]}\n";
                        }

                        //errorSTR += comboBox1.Text;
                        MessageBox.Show(errorSTR);
                    }
                }
                else
                {
                    if (UrunAdi.Text.Length < 2)
                    {
                        errorSTR += $"{Program.Output[49]}\n";
                        //MessageBox.Show("Kullanıcı Adı Yetersiz Uzunlukta!");
                    }
                    if (Convert.ToString(selectedValue).Length < 1)
                    {
                        errorSTR += $"{Program.Output[47]}\n";
                    }
                    if (db.CheckProductExists(UrunAdi.Text))
                    {
                        errorSTR += $"{Program.Output[48]}\n";
                    }

                    errorSTR += $"{Program.Output[50]}";

                    MessageBox.Show(errorSTR);
                }
            }
            else
            {
                errorSTR = "";
                if (UrunAdi.Text.Length < 2)
                {
                    errorSTR += $"{Program.Output[45]}\n";
                    //MessageBox.Show("Kullanıcı Adı Yetersiz Uzunlukta!");
                }
                if (Convert.ToString(selectedValue).Length < 1)
                {
                    errorSTR += $"{Program.Output[47]}\n";
                }
                if (db.CheckProductExists(UrunAdi.Text))
                {
                    errorSTR += $"{Program.Output[48]}\n";
                }

                //errorSTR += comboBox1.Text;
                MessageBox.Show(errorSTR);
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
            //Program.mainPage.GetAllProduct();
        }
        private void AddProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            System.GC.Collect();
            AcikMi = false;
        }
        #endregion

        #region Move The Form 
        private void AddProduct_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void AddProduct_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void AddProduct_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion

        #region Select Product Group
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
        }
        #endregion

        #region PictureBoxs Animation
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = SystemColors.ControlDark;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Transparent;
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
        #endregion

        private void checkBox1_MouseEnter(object sender, EventArgs e)
        {
            checkBox1.BackColor = DarkenColor(checkBox1.BackColor, 0.2f);
        }

        private void checkBox1_MouseLeave(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.BackColor = Color.DarkGray;
            }
            else
            {
                checkBox1.BackColor = Color.White;
            }
        }

        #region Darken Background Color For Every Color
        private Color DarkenColor(Color color, float factor)
        {
            int r = (int)(color.R * (1 - factor));
            int g = (int)(color.G * (1 - factor));
            int b = (int)(color.B * (1 - factor));

            return Color.FromArgb(r, g, b);
        }
        #endregion
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.BackColor = DarkenColor(checkBox1.BackColor, 0.2f);
            }
            else
            {
                checkBox1.BackColor = Color.White;
            }
        }
    }
}