using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NTP_P1
{
    public partial class UpdateProfile : Form
    {
        Database db = new Database();
        public static bool AcikMi = false;
        private bool mouseDown;
        private Point lastLocation;
        public static bool isRoot, isEmployee, isDefaultUser;
        public static bool AdminOrDefault, error;
        public static string errorSTR, kullaniciAdi, id, rootC, defaultUserC, employeeC;

        private void yetki_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (Directory.Exists($"{Application.StartupPath}\\Resim"))
            {
                string ImagePath = $"{Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{PersonelID.Text}")}.encrypted"; // Replace with your desired backup location

                if (System.IO.File.Exists(ImagePath))
                {
                    File.Delete(ImagePath);
                    pictureBox1.Image = pictureBox1.InitialImage;
                    MessageBox.Show(Program.Output[127], Program.Output[93]);
                    pictureBox3.Visible = false;
                }
            }
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

        private void UpdateProfile_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void UpdateProfile_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void UpdateProfile_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void UpdateProfile_FormClosed(object sender, FormClosedEventArgs e)
        {
            pictureBox1.Image = pictureBox1.InitialImage;
            this.Dispose();
            AcikMi = false;
            System.GC.Collect();
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.ForeColor = DarkenColor(button2.ForeColor, 0.4f);
        }


        private Color DarkenColor(Color color, float factor)
        {
            int r = (int)(color.R * (1 - factor));
            int g = (int)(color.G * (1 - factor));
            int b = (int)(color.B * (1 - factor));

            return Color.FromArgb(r, g, b);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.ForeColor = DarkenColor(button3.ForeColor, 0.4f);
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = Color.White;
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackColor = SystemColors.ControlDark;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Transparent;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.cancel_Colored_dark;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = pictureBox3.InitialImage;
        }

        private void mail_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = pictureBox1.InitialImage;
            this.Dispose();
            AcikMi = false;
            System.GC.Collect();
        }

        public UpdateProfile()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Image selectedImage = ImageLib.SelectImageFromFolderWithoutControl();
            if (selectedImage != null)
            {
                pictureBox1.Image = selectedImage;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.mainPage.changeProgressBar(10);
            if (pictureBox1.Image != null)
            {
                // Create a Bitmap from the PictureBox's Image
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Program.mainPage.changeProgressBar(20);
                if (Directory.Exists($"{Application.StartupPath}\\Resim"))
                {
                    Program.mainPage.changeProgressBar(30);
                    bmp.Save($"{Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{PersonelID.Text}")}.png");
                    if (!Directory.Exists($"{Application.StartupPath}\\Resim"))
                    {
                        // Create the "Backup" folder if it doesn't exist
                        Program.mainPage.changeProgressBar(40);
                        Directory.CreateDirectory($"{Application.StartupPath}\\Resim");
                    }
                    string ImagePath = $"{Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{PersonelID.Text}")}.png"; // Replace with your desired backup location
                    string zipOutputPath = $"{Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{PersonelID.Text}")}.encrypted"; // Replace with your desired backup location
                    Program.mainPage.changeProgressBar(50);
                    try
                    {
                        // Check if the database file exists
                        if (System.IO.File.Exists(ImagePath))
                        {
                            Program.mainPage.changeProgressBar(60);
                            FileCrpyt.EncryptFile(ImagePath, zipOutputPath, Crypt.key);
                            File.Delete($"{Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{PersonelID.Text}")}.png");
                            MessageBox.Show(Program.Output[128]);
                            pictureBox3.Visible = true;
                        }
                        else
                        {
                            MessageBox.Show(Program.Output[43]);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Program.Output[19]);
                    }
                }
                else
                {
                    try
                    {
                        // Create the folder
                        Directory.CreateDirectory($"{System.Windows.Forms.Application.StartupPath}\\Resim");

                        bmp.Save($"{System.Windows.Forms.Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{PersonelID.Text}")}.png");
                        string ImagePath = $"{Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{PersonelID.Text}")}.png"; // Replace with your desired backup location
                        string zipOutputPath = $"{Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{PersonelID.Text}")}.encrypted"; // Replace with your desired backup location
                        Program.mainPage.changeProgressBar(50);
                        try
                        {
                            // Check if the database file exists
                            if (System.IO.File.Exists(ImagePath))
                            {
                                Program.mainPage.changeProgressBar(60);
                                FileCrpyt.EncryptFile(ImagePath, zipOutputPath, Crypt.key);
                                File.Delete($"{Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{PersonelID.Text}")}.png");
                                MessageBox.Show(Program.Output[128]);
                                pictureBox3.Visible = true;
                            }
                            else
                            {
                                MessageBox.Show(Program.Output[43]);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, Program.Output[19]);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Display error message if folder creation fails
                        MessageBox.Show($"{Program.Output[130]} {ex.Message}");
                    }
                    // Save the image to the selected file format

                }

            }
            else
            {
                MessageBox.Show(Program.Output[131]);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void UpdateProfile_Load(object sender, EventArgs e)
        {
            AcikMi = true;
            Program.SetLanguage(Program.LanguagePack);
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Parent = pictureBox1;
            if (Program.SettingsValues[1] != "2")
            {
                this.BackColor = LoginPage.AccentCurrentColor;
                //this.button2.ForeColor = LoginPage.AccentColor;
                //this.button3.ForeColor = LoginPage.AccentColor;
            }
            Database.Connect.Open();
            id = db.GetIDWithUsername(LoginPage.kullaniciAdi);
            Database.Connect.Close();
            PersonelID.Text = id;
            KullaniciAdi.Text = Program.DecryptIt(db.GetKullaniciAdiWithID(id));
            mail.Text = Program.DecryptIt(db.GetMailWithID(id));
            tarih.Text = db.GetDateWithID(id);

            YetkiControl();

            if (Directory.Exists($"{Application.StartupPath}\\Resim"))
            {
                if (File.Exists($"{Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{PersonelID.Text}")}.encrypted"))
                {
                    pictureBox1.Image = FileCrpyt.DecryptImage($"{Application.StartupPath}\\Resim\\{hashCoder.DoubleHash($"{PersonelID.Text}")}.encrypted", Crypt.key);
                }
                else
                {
                    //MessageBox.Show("İlgili Resim Bulunamadı!");
                    pictureBox3.Visible = false;

                }
            }
            else
            {
                MessageBox.Show(Program.Output[132]);
                pictureBox3.Visible = false;
            }
        }
        public void YetkiControl()
        {
            if (MainPage.isRoot)
            {
                yetki.Text = Program.Output[36];
            }
            else if (MainPage.isDefaultUser)
            {
                yetki.Text = Program.Output[38];
            }
            else if (MainPage.isEmployee)
            {
                yetki.Text = Program.Output[37];
            }
            else
            {
                yetki.Text = Program.Output[39];
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            errorSTR = "";
            error = false;
            if (KullaniciAdi.Text.Length > 1)
            {
                string[] idS = db.GetValuesOfColumn("Personel", "ID");
                idS = idS.Where(element => element != PersonelID.Text).ToArray();
                kullaniciAdi = Program.EncryptIt(KullaniciAdi.Text);
                if (!db.UsernameExistsForIDs(kullaniciAdi, idS))
                {
                    string[] changes = {
                        $"KullaniciAdi='{Program.EncryptIt(KullaniciAdi.Text)}'"
                        };

                    Database.Connect.Open();
                    Database.CommonUpdate("Personel", changes, id);
                    Database.Connect.Close();
                    LoginPage.kullaniciAdi = Program.EncryptIt(KullaniciAdi.Text);
                    Program.mainPage.YetkiKontrol();
                }
                else
                {
                    error = true;
                    errorSTR += $"{Program.Output[20]}\n";
                }

            }
            else
            {
                error = true;
                errorSTR += $"{Program.Output[2]}\n";
            }
            if (error)
            {
                MessageBox.Show(errorSTR, Program.Output[19]);
            }
        }
    }
}
