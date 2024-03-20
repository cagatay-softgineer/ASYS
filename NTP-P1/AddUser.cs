
#region Packages
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
#endregion

namespace NTP_P1
{
    public partial class AddUser : Form
    {
        #region Variable Definition
        public static bool AcikMi;
        Database db = new Database();
        static string errorSTR, kullaniciAdi, mailAdress, root, defaultUser, employee;
        public static bool AdminOrDefault;
        private bool mouseDown;
        private Point lastLocation;
        DateTime time = DateTime.Now;
        #endregion

        #region Form Implementation
        public AddUser()
        {
            InitializeComponent();
        }
        #endregion

        #region Close The Form 
        private void AddUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            System.GC.Collect();
            AcikMi = false;
            if (!AdminOrDefault)
            {
                Program.mainPage.GetAllUsers();
            }
            if (AdminOrDefault)
            {
                Program.mainPage.AdminGetAllUsers();
            }
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            System.GC.Collect();
            AcikMi = false;
            if (!AdminOrDefault)
            {
                Program.mainPage.GetAllUsers();
            }
            if (AdminOrDefault)
            {
                Program.mainPage.AdminGetAllUsers();
            }
        }
        #endregion

        #region Move The Form
        private void AddUser_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void AddUser_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void AddUser_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
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

        #region Password Validity Check
        static bool IsValidPassword(string password)
        {
            // Define a regular expression pattern to match invalid characters
            string pattern = @"^[^\s'""\\%&/#=?!*]+$";

            // Create a Regex object
            Regex regex = new Regex(pattern);

            // Use the Regex.IsMatch method to check if the password matches the pattern
            return regex.IsMatch(password);
        }
        #endregion

        #region Add User Method
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show($"{Program.Output[126]}");
            }
            else
            {
                kullaniciAdi = Program.EncryptIt(KullaniciAdi.Text);
                mailAdress = Mail.Text;
                mailAdress = Program.EncryptIt(mailAdress);
                if (!db.CheckUsernameExists(kullaniciAdi) && !db.CheckEmailExists(mailAdress) && KullaniciAdi.Text.Length >= 2 && Sifre.Text.Length >= 4 && Mail.Text != null && IsValidEmail(Mail.Text) && IsValidPassword(Sifre.Text) && Ad.Text.Length > 1 && Soyad.Text.Length > 1)
                {
                    if (comboBox1.SelectedIndex == 0)//Admin
                    {
                        root = "-1";
                        defaultUser = "0";
                        employee = "0";
                    }
                    else if (comboBox1.SelectedIndex == 1)//DefaultUser
                    {
                        root = "0";
                        defaultUser = "-1";
                        employee = "0";
                    }
                    else if (comboBox1.SelectedIndex == 2)//Employee
                    {
                        root = "0";
                        defaultUser = "0";
                        employee = "-1";
                    }
                    else if (comboBox1.SelectedIndex == 3)//Yetkisiz
                    {
                        root = "0";
                        defaultUser = "0";
                        employee = "0";
                    }
                    else
                    {

                    }
                    string[] columns = { "KullaniciAdi", "Sifre", "Root", "defaultUser", "Employee", "Mail", "GirisZamani" ,"Ad", "Soyad"};
                    string[] values = { kullaniciAdi, Program.EncryptIt(Sifre.Text), root, defaultUser, employee, mailAdress, String.Format("{0:dd/MM/yyyy}", time), Program.EncryptIt(Ad.Text), Program.EncryptIt(Soyad.Text) };
                    Database.Connect.Open();
                    Database.Insert("Personel", columns, values);
                    Database.Connect.Close();
                    if (!AdminOrDefault)
                    {
                        Program.mainPage.GetAllUsers();
                    }
                    if (AdminOrDefault)
                    {
                        Program.mainPage.AdminGetAllUsers();
                    }
                }
                else
                {
                    errorSTR = "";
                    if (KullaniciAdi.Text.Length < 2)
                    {
                        errorSTR += $"{Program.Output[2]}\n";
                        //MessageBox.Show("Kullanıcı Adı Yetersiz Uzunlukta!");
                    }
                    if (Sifre.Text.Length < 4)
                    {
                        errorSTR += $"{Program.Output[3]}(Min:4)\n";
                    }
                    if (db.CheckEmailExists(mailAdress))
                    {
                        errorSTR += $"{Program.Output[21]}\n";
                    }
                    if (db.CheckUsernameExists(kullaniciAdi))
                    {
                        errorSTR += $"{Program.Output[20]}\n";
                    }
                    if (!IsValidEmail(Mail.Text))
                    {
                        errorSTR += $"{Program.Output[17]}\n";

                    }
                    if (Mail.Text.Length < 1)
                    {
                        errorSTR += $"{Program.Output[112]}\n";
                    }
                    if (!IsValidPassword(Sifre.Text))
                    {
                       errorSTR += ($"{Program.Output[135]}\n" +
                            $"[^\\s'\"\\\\%&/#=?!*]\n\n");
                    }
                    if (Ad.Text.Length < 2)
                    {
                        errorSTR += $"Girilen Ad Çok Kısa\n";
                    }
                    if (Soyad.Text.Length < 2)
                    {
                        errorSTR += $"Girilen Soyad Çok Kısa\n";
                    }
                    //errorSTR += comboBox1.Text;
                    MessageBox.Show(errorSTR);
                }
            }
        }
        #endregion

        #region Email Validity Check
        static bool IsValidEmail(string email)
        {
            // Define a regular expression for a simple email validation
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            // Check if the email matches the pattern
            return regex.IsMatch(email);
        }
        #endregion

        #region Form Load Processes
        private void AddUser_Load(object sender, EventArgs e)
        {
            AcikMi = true;
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentCurrentColor;

            Program.SetLanguage(Program.LanguagePack);
            //comboBox1.SelectedValue = 1;
        }
        #endregion

        #region Check Admin
        public void AdminControl(bool Admin)
        {
            AdminOrDefault = Admin;
        }
        #endregion
    }
}