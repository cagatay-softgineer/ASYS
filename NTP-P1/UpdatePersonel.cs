using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace NTP_P1
{
    public partial class Update_Personel : Form
    {
        public static bool AcikMi = false;
        Database db = new Database();
        private bool mouseDown;
        private Point lastLocation;
        public static bool isRoot, isEmployee, isDefaultUser;
        public static bool AdminOrDefault, isThereResponsiblePerson;
        public static string errorSTR, kullaniciAdi, mailAdress, rootC, defaultUserC, employeeC,usernameCache;
        string[] groups;

        private void Update_Personel_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            AcikMi = false;
            System.GC.Collect();
            if (AdminOrDefault)
            {
                Program.mainPage.AdminGetAllUsers();
            }
            else
            {
                Program.mainPage.GetAllUsers();
            }

        }

        private void Update_Personel_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Update_Personel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Update_Personel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public Update_Personel()
        {
            InitializeComponent();
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Yetki_SelectedValueChanged(object sender, EventArgs e)
        {
            if(Yetki.SelectedIndex == 2)
            {
                urunGroup.Enabled = true;
            }
            else
            {
                urunGroup.Enabled = false;
            }
        }

        private void UpdatePersonel_Load(object sender, EventArgs e)
        {
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentCurrentColor;
            AcikMi = true;
            Program.SetLanguage(Program.LanguagePack);
            get_gruops();

            Database.Connect.Open();

            if (db.isAssignedAnyGroup(Program.EncryptIt(usernameCache)))
            {
                urunGroup.SelectedItem = db.GetGroupWithUsernameWithoutAutoOpenConnect(Program.EncryptIt(usernameCache));
            }
            Database.Connect.Close();
        }

        public void get_gruops()
        {
            urunGroup.Items.Clear();
            groups = db.GetValuesOfColumnJustDiffrentOnes("Urun", "UrunGrubu");

            foreach (string group in groups.OrderBy(group => group))
            {
                urunGroup.Items.Add(group);
            }
        }

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
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Yetki.SelectedIndex == -1)
            {
                MessageBox.Show(Program.Output[111]);
            }
            else
            {
                string[] idS = db.GetValuesOfColumn("Personel", "ID");
                idS = idS.Where(element => element != UrunID.Text).ToArray();
                kullaniciAdi = Program.EncryptIt(KullaniciAdi.Text);
                mailAdress = Mail.Text;
                mailAdress = Program.EncryptIt(mailAdress);
                if (urunGroup.SelectedIndex != -1 && Yetki.SelectedIndex == 2)
                {
                    isThereResponsiblePerson = db.CheckResponsiblePersonelAutoOpenConnectWithIDS(urunGroup.SelectedItem.ToString(), idS);
                    
                }
                else
                {
                    isThereResponsiblePerson = false;
                }
                if (!db.UsernameExistsForIDs(kullaniciAdi, idS) && !db.MailExistsForIDs(mailAdress, idS) && KullaniciAdi.Text.Length >= 2 && Sifre.Text.Length >= 4 && Mail.Text != null && IsValidPassword(Sifre.Text) && Ad.Text.Length>1 && Soyad.Text.Length>1 && !isThereResponsiblePerson)
                {
                    if (Yetki.SelectedIndex == 0)//Admin
                    {
                        rootC = "-1";
                        defaultUserC = "0";
                        employeeC = "0";
                    }
                    else if (Yetki.SelectedIndex == 1)//DefaultUser
                    {
                        rootC = "0";
                        defaultUserC = "-1";
                        employeeC = "0";
                    }
                    else if (Yetki.SelectedIndex == 2)//Employee
                    {
                        rootC = "0";
                        defaultUserC = "0";
                        employeeC = "-1";
                    }
                    else if (Yetki.SelectedIndex == 3)//Yetkisiz
                    {
                        rootC = "0";
                        defaultUserC = "0";
                        employeeC = "0";
                    }
                    else
                    {

                    }
                    string formattedDate = String.Format("{0:dd/MM/yyyy}", Tarih.Value);

                    if(Yetki.SelectedIndex == 2)
                    {
                        string[] changes = {
                        $"KullaniciAdi='{Program.EncryptIt(KullaniciAdi.Text)}'",
                        $"Sifre='{Program.EncryptIt(Sifre.Text)}'",
                        $"Mail='{Program.EncryptIt(Mail.Text)}'",
                        $"GirisZamani='{formattedDate}'",
                        $"Root='{rootC}'",
                        $"Employee='{employeeC}'",
                        $"defaultUser='{defaultUserC}'",
                        $"SorumluUrunGrubu='{urunGroup.SelectedItem}'",
                        $"Ad='{Program.EncryptIt(Ad.Text)}'",
                        $"Soyad='{Program.EncryptIt(Soyad.Text)}'"
                        };

                        Database.Connect.Open();
                        Database.CommonUpdate("Personel", changes, UrunID.Text);
                        Database.Connect.Close();
                    }
                    else
                    {
                        string[] changes = {
                        $"KullaniciAdi='{Program.EncryptIt(KullaniciAdi.Text)}'",
                        $"Sifre='{Program.EncryptIt(Sifre.Text)}'",
                        $"Mail='{Program.EncryptIt(Mail.Text)}'",
                        $"GirisZamani='{formattedDate}'",
                        $"Root='{rootC}'",
                        $"Employee='{employeeC}'",
                        $"defaultUser='{defaultUserC}'",
                         $"SorumluUrunGrubu=''",
                        $"Ad='{Program.EncryptIt(Ad.Text)}'",
                        $"Soyad='{Program.EncryptIt(Soyad.Text)}'"
                        };

                        Database.Connect.Open();
                        Database.CommonUpdate("Personel", changes, UrunID.Text);
                        Database.Connect.Close();
                    }

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
                    if (db.MailExistsForIDs(mailAdress, idS))
                    {
                        errorSTR += $"{Program.Output[21]}\n";
                    }
                    if (db.UsernameExistsForIDs(kullaniciAdi, idS))
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
                    if(Ad.Text.Length < 2)
                    {
                        errorSTR += $"Girilen Ad Çok Kısa\n";
                    }
                    if (Soyad.Text.Length < 2)
                    {
                        errorSTR += $"Girilen Soyad Çok Kısa\n";
                    }
                    if (isThereResponsiblePerson)
                    {
                        errorSTR += $"Seçilen Gruptan Sorumlu Personel Var! Başka Bir Grup Deneyiniz!\n";
                    }
                    //errorSTR += comboBox1.Text;
                    MessageBox.Show(errorSTR);
                }
            }

        }

        static bool IsValidEmail(string email)
        {
            // Define a regular expression for a simple email validation
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            // Check if the email matches the pattern
            return regex.IsMatch(email);
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            AcikMi = false;
            System.GC.Collect();
            if (AdminOrDefault)
            {
                Program.mainPage.AdminGetAllUsers();
            }
            else
            {
                Program.mainPage.GetAllUsers();
            }
        }

        public void cikis()
        {
            this.Dispose();
            AcikMi = false;
            System.GC.Collect();
            if (AdminOrDefault)
            {
                Program.mainPage.AdminGetAllUsers();
            }
            else
            {
                Program.mainPage.GetAllUsers();
            }
        }

        public void getDataFromSelectedUser(string ID, string kullaniciAdi, string sifre, string mail, string tarih, bool root, bool defaultUser, bool employee,string ad, string soyad)
        {
            ID_Name.Text = $"{Program.LanguagePack[182]}";
            UrunID.Text = ID;
            KullaniciAdi.Text = kullaniciAdi;
            Sifre.Text = sifre;
            Mail.Text = mail;
            Tarih.Value = Convert.ToDateTime(tarih);
            isRoot = root;
            isEmployee = employee;
            isDefaultUser = defaultUser;
            Ad.Text = Program.DecryptIt(ad);
            Soyad.Text = Program.DecryptIt(soyad);

            

            if (Directory.Exists($"{Application.StartupPath}\\Resim"))
            {
                if (File.Exists($"{Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{ID}")}.encrypted"))
                {
                    pictureBox2.Image = FileCrpyt.DecryptImage($"{Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{ID}")}.encrypted", Crypt.key);
                }
                else
                {
                    //MessageBox.Show("İlgili Resim Bulunamadı!");
                    //pictureBox3.Visible = false;

                }
            }

            if (isRoot)
            {
                Yetki.SelectedIndex = 0;
            }
            
            else if (isDefaultUser)
            {
                Yetki.SelectedIndex = 1;
            }
            else if (isEmployee)
            {
                Yetki.SelectedIndex = 2;
            }
            else
            {
                Yetki.SelectedIndex = 3;
            }

        }
    }
}
