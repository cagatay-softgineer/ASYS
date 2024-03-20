
#region Packages
using System;
using System.Drawing;
using System.IO;
using System.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace NTP_P1
{
    public partial class AppSettings : Form
    {
        #region Variable Definition
        public static bool AcikMi = false;
        Database db = new Database();
        public UpdateProfile updateProfile;
        private bool mouseDown;
        private Point lastLocation;
        public static bool AdminOrDefault;
        public static string errorSTR, kullaniciAdi, mailAdress, rootC, defaultUserC, employeeC;
        public static string DBsettings, cache, id;
        public static string[] Settings = new string[3];
        public static string[] cacheSettings = new string[3];
        #endregion

        #region Decrypt Last Database Backup 
        private async void pictureBox2_Click(object sender, EventArgs e)
        {
            Program.mainPage.changeProgressBar(10);
            try
            {
                Program.mainPage.changeProgressBar(20);
                string backupDate = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
                Program.mainPage.changeProgressBar(30);
                string zipDecrpytPath = $"{Application.StartupPath}\\Backup\\Decrypted\\DatabaseBackup{backupDate}-DECRYPTED.accdb";
                Program.mainPage.changeProgressBar(40);

                DialogResult result = MessageBox.Show($"Tüm Database Yedeklerinin Şifresini Çözmek için YES'e, Sadece Sonuncu Yedeğin Şifresini Çözmek İçin NO'a Basınız!", $"Custom MessageBox", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string backupDirectory = $"{Application.StartupPath}\\Backup\\";

                    string[] encryptedFiles = Directory.GetFiles(backupDirectory, "*.encrypted");
                    double all_product_count = encryptedFiles.Length;
                    double cur_product = 0;
                    foreach (string encryptedFile in encryptedFiles)
                    {
                        cur_product++;
                        double Process_percent = cur_product / all_product_count * 40 + 40;
                        Program.mainPage.changeProgressBar(Process_percent);
                        // Construct the destination path for the decrypted file
                        string decryptedFilePath = Path.Combine($"{Application.StartupPath}\\Backup\\Decrypted\\", Path.GetFileNameWithoutExtension(encryptedFile) + "-DECRYPTED.accdb");

                        // Decrypt the file
                        if (cur_product < all_product_count)
                        {
                            FileCrpyt.DecryptFileSilent(encryptedFile, decryptedFilePath, Crypt.key);
                        }
                        else
                        {
                            FileCrpyt.DecryptFile(encryptedFile, decryptedFilePath, Crypt.key);
                        }
                    }
                }
                else
                {
                    FileCrpyt.DecryptFile(Crypt.GetLastEncryptedFile($"{Application.StartupPath}\\Backup\\", ".encrypted"), zipDecrpytPath, Crypt.key);
                }
                
                Program.mainPage.changeProgressBar(90);

            }
            catch (ArgumentNullException)
            {
                await Task.Run(() =>
                {
                    MessageBox.Show($"{Program.Output[55]} {Application.StartupPath}\\Backup", $"{Program.Output[56]}");
                });
            }
            catch (FileNotFoundException ex)
            {
                await Task.Run(() =>
                {
                    MessageBox.Show($"{Program.Output[57]} {ex.Message}");
                });
            }
            catch (IOException ex)
            {
                DialogResult result1 = MessageBox.Show($"{Program.Output[58]} {Application.StartupPath}\\Backup \n\n{Program.Output[59]}", $"{Program.Output[60]}", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.No)
                {

                }
                else
                {
                    try
                    {
                        Program.mainPage.changeProgressBar(20);
                        Directory.CreateDirectory($"{Application.StartupPath}\\Backup\\Decrypted");
                        Program.mainPage.changeProgressBar(30);
                        string backupDate = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
                        Program.mainPage.changeProgressBar(40);
                        string zipDecrpytPath = $"{Application.StartupPath}\\Backup\\Decrypted\\DatabaseBackup{backupDate}-DECRYPTED.accdb";
                        DialogResult result = MessageBox.Show($"Tüm Database Yedeklerinin Şifresini Çözmek için YES'e, Sadece Sonuncu Yedeğin Şifresini Çözmek İçin NO'a Basınız!", $"Custom MessageBox", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            string backupDirectory = $"{Application.StartupPath}\\Backup\\";

                            string[] encryptedFiles = Directory.GetFiles(backupDirectory, "*.encrypted");
                            double all_product_count = encryptedFiles.Length;
                            double cur_product = 0;
                            foreach (string encryptedFile in encryptedFiles)
                            {
                                cur_product++;
                                double Process_percent = cur_product / all_product_count * 40 + 40;
                                Program.mainPage.changeProgressBar(Process_percent);
                                // Construct the destination path for the decrypted file
                                string decryptedFilePath = Path.Combine($"{Application.StartupPath}\\Backup\\Decrypted\\", Path.GetFileNameWithoutExtension(encryptedFile) + "-DECRYPTED.accdb");

                                // Decrypt the file
                                if (cur_product < all_product_count)
                                {
                                    FileCrpyt.DecryptFileSilent(encryptedFile, decryptedFilePath, Crypt.key);
                                }
                                else
                                {
                                    FileCrpyt.DecryptFile(encryptedFile, decryptedFilePath, Crypt.key);
                                }
                            }
                        }
                        else
                        {
                            FileCrpyt.DecryptFile(Crypt.GetLastEncryptedFile($"{Application.StartupPath}\\Backup\\", ".encrypted"), zipDecrpytPath, Crypt.key);
                        }
                    }
                    catch (Exception ex1)
                    {
                        await Task.Run(() =>
                        {
                            MessageBox.Show($"{ex} :: {ex1}");
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    MessageBox.Show($"{Program.Output[19]}: {ex.Message}");
                });
            }
            Program.mainPage.finishProcessWithAnim();
        }
        #endregion

        #region Delete All Decrpyted Database Backup 
        private async void pictureBox3_Click(object sender, EventArgs e)
        {
            Program.mainPage.changeProgressBar(10);
            try
            {
                Program.mainPage.changeProgressBar(20);
                if (Directory.Exists($"{Application.StartupPath}\\Backup\\Decrypted"))
                {
                    Program.mainPage.changeProgressBar(30);
                    Directory.Delete($"{Application.StartupPath}\\Backup\\Decrypted", true);
                    Program.mainPage.changeProgressBar(40);
                    await Task.Run(() =>
                    {
                        Program.mainPage.finishProcessWithAnim();
                        MessageBox.Show(Program.Output[61], Program.Output[62]);
                    });
                    Program.mainPage.changeProgressBar(50);
                }
                else
                {
                    await Task.Run(() =>
                    {
                        Program.mainPage.finishProcessWithAnim();
                        MessageBox.Show(Program.Output[63], Program.Output[64]);
                    });
                }

            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    MessageBox.Show($"{ex}");
                });
            }
            Program.mainPage.finishProcessWithAnim();
        }
        #endregion

        #region Change Application Language
        private void Language_Click(object sender, EventArgs e)
        {
            Program.loginPage.LanguageUpdate();
            Program.mainPage.YetkiKontrol();
            getSettingsAtDatabase();
        }
        #endregion

        #region Move The Form
        private void AppSettings_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void AppSettings_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void AppSettings_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
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
            pictureBox3.BackColor = SystemColors.ControlDark;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void productMenuBox_MouseEnter(object sender, EventArgs e)
        {
            Profile.BackColor = SystemColors.ControlDark;
        }

        private void productMenuBox_MouseLeave(object sender, EventArgs e)
        {
            Profile.BackColor = Color.Transparent;
        }

        private void Language_MouseEnter(object sender, EventArgs e)
        {
            Language.BackColor = SystemColors.ControlDark;
        }

        private void Language_MouseLeave(object sender, EventArgs e)
        {
            Language.BackColor = Color.Transparent;
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

        #region Open Update Profile Form 
        private void productMenuBox_Click(object sender, EventArgs e)
        {
            Program.mainPage.changeProgressBar(10);
            if (UpdateProfile.AcikMi == false)
            {
                Program.mainPage.changeProgressBar(20);
                updateProfile = new UpdateProfile();
                updateProfile.Show();
                Program.mainPage.changeProgressBar(30);
            }
            else
            {
                Program.mainPage.finishProcessWithAnim();
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            Program.mainPage.finishProcessWithAnim();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void AppSettings_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        #endregion

        #region Save Settings
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Save_Settings();
        }

        async void Save_Settings()
        {
            Program.mainPage.changeProgressBar(10);
            errorSTR = cache = "";
            if (comboBox1.SelectedIndex == -1)
            {
                errorSTR += $"{Program.Output[65]}\n";
            }
            else
            {
                cacheSettings[0] = $"{comboBox1.SelectedIndex}";
            }

            if (comboBox2.SelectedIndex == -1)
            {
                errorSTR += $"{Program.Output[66]}\n";
            }
            else
            {
                cacheSettings[1] = $"{comboBox2.SelectedIndex}";
            }

            if (comboBox3.SelectedIndex == -1)
            {
                errorSTR += $"{Program.Output[67]}\n";
            }
            else
            {
                //Do Nothing
            }

            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
            {
                await Task.Run(() =>
                {
                    MessageBox.Show(errorSTR);
                });
            }
            else
            {
                Program.mainPage.changeProgressBar(20);
                foreach (string Char in cacheSettings)
                {
                    cache += Char;
                }
                Program.mainPage.changeProgressBar(30);
                if (LoginPage.currentLang == "tur")
                {
                    cache += 0;
                }
                else if (LoginPage.currentLang == "eng")
                {
                    cache += 1;
                }
                Program.mainPage.changeProgressBar(40);
                string[] changes = {
                        $"Settings='{Program.EncryptIt(cache)}'",
                        };

                Program.mainPage.changeProgressBar(50);
                Database.Connect.Open();
                Program.mainPage.changeProgressBar(60);
                id = db.GetIDWithUsername(LoginPage.kullaniciAdi);
                Database.Connect.Close();
                Program.mainPage.changeProgressBar(70);
                Database.Connect.Open();
                Database.CommonUpdate("Personel", changes, id);
                Database.Connect.Close();
                Program.mainPage.changeProgressBar(80);
                Save_Data();
                getSettingsAtDatabase();

                Program.mainPage.changeProgressBar(90);
                Program.SettingsSaved = false;
                if (!Program.SettingsSaved)
                {
                    Program.ApplySettings();
                    Program.AccentMustBeUpdated = true;
                }
                else
                {

                }

                LoginPage.cancellationTokenSource.Cancel();

            }
            Program.mainPage.finishProcessWithAnim();
        }
        #endregion

        #region Save Database Backup Often To Program Itself
        private void Save_Data()
        {
            Properties.Settings.Default.DatabaseBackup = $"{comboBox3.SelectedIndex}";
            Properties.Settings.Default.Save();
        }
        #endregion

        #region Close The Form
        private void AppSettings_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            AcikMi = false;
            if (UpdateProfile.AcikMi)
            {
                UpdateProfile.AcikMi = false;
                MainPage.appSettings.updateProfile.Dispose();
            }
            System.GC.Collect();

            Program.mainPage.YetkiKontrol();

        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            AcikMi = false;
            if (UpdateProfile.AcikMi)
            {
                UpdateProfile.AcikMi = false;
                MainPage.appSettings.updateProfile.Dispose();
            }
            System.GC.Collect();

            Program.mainPage.YetkiKontrol();
        }
        #endregion

        #region Form Implementation
        public AppSettings()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Load Processes
        private void AppSettings_Load(object sender, EventArgs e)
        {
            AcikMi = true;
            if (MainPage.isRoot)
            {
                pictureBox2.Visible = pictureBox3.Visible = label3.Visible = comboBox3.Visible = Enabled;
            }
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentCurrentColor;

            Program.SetLanguage(Program.LanguagePack);
            getSettingsAtDatabase();
            if (LoginPage.currentLang == "tur")
            {
                Language.Image = NTP_P1.Properties.Resources.turkey_lang;
            }
            else if (LoginPage.currentLang == "eng")
            {
                Language.Image = NTP_P1.Properties.Resources.english_lang;
            }
            else
            {

            }
        }
        #endregion

        #region Load Settings Of User From Database
        public void getSettingsAtDatabase()
        {
            DBsettings = "";
            Database.Connect.Open();
            DBsettings = db.GetSettingsWithUsername(LoginPage.kullaniciAdi);
            Database.Connect.Close();
            if (Properties.Settings.Default.DatabaseBackup != null)
            {
                if (Properties.Settings.Default.DatabaseBackup == "0")
                {
                    comboBox3.SelectedIndex = 0;
                }
                else if (Properties.Settings.Default.DatabaseBackup == "1")
                {
                    comboBox3.SelectedIndex = 1;
                }
                else if (Properties.Settings.Default.DatabaseBackup == "2")
                {
                    comboBox3.SelectedIndex = 2;
                }
                else if (Properties.Settings.Default.DatabaseBackup == "3")
                {
                    comboBox3.SelectedIndex = 3;
                }
                else if (Properties.Settings.Default.DatabaseBackup == "4")
                {
                    comboBox3.SelectedIndex = 4;
                }
                else if (Properties.Settings.Default.DatabaseBackup == "5")
                {
                    comboBox3.SelectedIndex = 5;
                }
                else if (Properties.Settings.Default.DatabaseBackup == "6")
                {
                    comboBox3.SelectedIndex = 6;
                }
            }

            if (DBsettings.Length > 0)
            {
                DBsettings = Program.DecryptIt(DBsettings);
                Settings[0] = $"{DBsettings[0]}";
                Settings[1] = $"{DBsettings[1]}";
                //Settings[2] = $"{DBsettings[2]}";
            }
            else
                Settings[0] = Settings[1] = "0";

            if (Settings[0] == "0")
            {
                comboBox1.SelectedIndex = 0;
            }
            else if (Settings[0] == "1")
            {
                comboBox1.SelectedIndex = 1;
            }
            if (Settings[1] == "0")
            {
                comboBox2.SelectedIndex = 0;
            }
            else if (Settings[1] == "1")
            {
                comboBox2.SelectedIndex = 1;
            }
            else if (Settings[1] == "2")
            {
                comboBox2.SelectedIndex = 2;
            }
        }
        #endregion
    }
}