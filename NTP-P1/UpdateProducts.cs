using System;
using System.Drawing;
using System.Windows.Forms;

namespace NTP_P1
{
    public partial class UpdateProducts : Form
    {
        public static bool AcikMi = false;
        public static double KarOranı;
        Database db = new Database();
        private bool mouseDown;
        private Point lastLocation;
        public UpdateProducts()
        {
            InitializeComponent();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            System.GC.Collect();
            AcikMi = false;
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void operationsForUsers_Load(object sender, EventArgs e)
        {
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentCurrentColor;
            AcikMi = true;
            Program.SetLanguage(Program.LanguagePack);
        }

        public void getDataFromSelectedProduct(string ID, string urunAdi, string urunGrubu, string urunSatisFiyati, string tarih,string stokMiktari)
        {
            ProductName.Text = $"{ID}::{urunAdi}";
            UrunID.Text = ID;
            UrunAdi.Text = urunAdi;
            UrunGrubu.Text = urunGrubu;
            UrunSatisFiyati.Text = urunSatisFiyati;
            Tarih.Value = Convert.ToDateTime(tarih);
            Stok.Text = stokMiktari;
        }



        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string formattedDate = String.Format("{0:dd/MM/yyyy}", Tarih.Value);

            string[] changes = {
            $"UrunAdi='{UrunAdi.Text}'",
            $"UrunGrubu='{UrunGrubu.Text}'",
            $"UrunSatisFiyati='{UrunSatisFiyati.Text}'",
            $"StokMiktari='{Stok.Text}'",
            $"EklenmeVeyaGuncellenmeTarihi='{formattedDate}'"
            };
            
            Database.Connect.Open();
            Database.CommonUpdate("Urun", changes, UrunID.Text);
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

        private void UpdateProducts_FormClosed(object sender, FormClosedEventArgs e)
        {
            AcikMi = false;
            this.Dispose();
            System.GC.Collect();
        }

        private void UpdateProducts_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void UpdateProducts_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void UpdateProducts_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.BackColor = SystemColors.ControlDark;
            pictureBox7.Image = Properties.Resources.exit_darken;
        }

        private void pictureBox7_MouseMove(object sender, MouseEventArgs e)
        {

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
