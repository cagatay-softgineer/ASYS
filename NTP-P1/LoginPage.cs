
#region Packages
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace NTP_P1
{
    public partial class LoginPage : Form
    {
        #region Variable Definition
        private Database db = new Database();
        public static string kullaniciAdi, sifre, KontrolKA, KontrolS;
        public static string errorSTR;
        public static string LanguageStr = "eng";
        public static string[] LanguagePack, relatedObject;
        public static string currentLang = "eng";
        public static string username, pass, mail;
        DateTime time = DateTime.Today;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public static Color AccentColor;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        public static Color CacheColor, AccentCurrentColor;
        private static object lastAccentColor, CurrentAccentColor;
        public static bool justStart, waiting, eyeClick;
        public static string timeChecker, checkTime;
        public static int[] cacheTimeChehker = new int[3];
        public static int[] cacheCheckTime = new int[3];
        private bool mouseDown;
        private Point lastLocation;
        public static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        #endregion

        #region Form Implementation
        public LoginPage()
        {
            InitializeComponent();
            Init_Data();

        }
        #endregion

        #region Try Login 
        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (txtBoxKullaniciAdi.Text.Length > 0 && txtBoxSifre.Text.Length > 0)
            {
                kullaniciAdi = Program.EncryptIt(txtBoxKullaniciAdi.Text);
                Database.Connect.Open();
                sifre = db.GetSifreFromUsername(kullaniciAdi);
                KontrolKA = Program.DecryptIt($"{db.GetKullaniciAdi(kullaniciAdi)}");
                KontrolS = Program.DecryptIt($"{db.GetSifreFromUsername(kullaniciAdi)}");
            }
            else
            {
                kullaniciAdi = "";
                sifre = "";
                KontrolKA = "";
                KontrolS = "";
            }
            if (KontrolKA.Length >= 2 && KontrolS.Length >= 4)
            {
                if (KontrolKA == txtBoxKullaniciAdi.Text)
                {
                    if (KontrolS == txtBoxSifre.Text)
                    {
                        Save_Data();

                        MainPage.controlClick = "0";
                        Program.mainPage.pictureBox1.Image = NTP_P1.Properties.Resources.logout;
                        Program.mainPage.DrawBlackRectangleWithTextBitmap(1, "ASYS", DateTime.Now.ToString("dd.MM.yyyy - HH:mm"), Program.mainPage.pictureBox3);
                        Program.ChangeForm(Program.mainPage, this);
                        Program.loginPage.CenterToScreen();
                        Program.loginPage.ActiveControl = null;
                        Program.loginPage.pictureBox2.Image = null;
                        Program.forgetPassword.pictureBox2.Image = null;
                        Program.ApplySettings();
                        Program.mainPage.YetkiKontrol();
                        System.GC.Collect();
                    }

                    else
                    {
                        MessageBox.Show(Program.Output[0]);
                    }
                }

                else
                {
                    MessageBox.Show(Program.Output[1]);
                }
            }
            else
            {
                errorSTR = "";
                if (txtBoxKullaniciAdi.Text.Length < 2)
                {
                    errorSTR += $"{Program.Output[2]}\n";
                }
                if (txtBoxSifre.Text.Length < 4)
                {
                    errorSTR += $"{Program.Output[3]}\n";
                }
                if (KontrolKA.Length < 1)
                {
                    errorSTR += $"{Program.Output[4]}\n";
                }
                else
                {
                    if (txtBoxSifre.Text.Length < 4)
                    {
                        //errorSTR += $"{Program.Output[3]}\n";
                    }
                    else if (KontrolS.Length < 1)
                    {
                        errorSTR += $"{Program.Output[0]}\n";
                    }
                }
                MessageBox.Show(errorSTR);
            }

            Database.Connect.Close();
        }
        #endregion

        #region Change Form To Forget Password Form
        private void button1_Click(object sender, EventArgs e)
        {
            Program.forgetPassword.pictureBox2.Image = Properties.Resources.ASYS_LOGO_FINAL_HALFSIZED;
            Program.ChangeForm(Program.forgetPassword, this);
            Program.loginPage.CenterToScreen();
            Program.loginPage.ActiveControl = null;
            System.GC.Collect();
        }
        #endregion

        #region Close Application
        private void LoginPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Empty Values of Text Box Text After Login
        public void EmptyEntryValues()
        {
            txtBoxKullaniciAdi.Text = null;
            txtBoxSifre.Text = null;
        }
        #endregion

        #region Change Form To Register Form
        private void button2_Click(object sender, EventArgs e)
        {
            Program.registerPage.pictureBox2.Image = Properties.Resources.ASYS_LOGO_FINAL_HALFSIZED;
            Program.ChangeForm(Program.registerPage, this);
            Program.forgetPassword.pictureBox2.Image = null;
            Program.loginPage.CenterToScreen();
            Program.loginPage.ActiveControl = null;
            System.GC.Collect();
        }
        #endregion

        #region PictureBoxs Animation
        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.BackColor = DarkenColor(button2.BackColor, 0.2f);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            //button2.BackColor = AccentCurrentColor;
            button2.BackColor = Color.White;
        }
        private void btnGiris_MouseHover(object sender, EventArgs e)
        {
            btnGiris.ForeColor = DarkenColor(btnGiris.ForeColor, 0.4f);
        }

        private void btnGiris_MouseLeave(object sender, EventArgs e)
        {
            //btnGiris.BackColor = AccentCurrentColor;
            btnGiris.ForeColor = Color.White;
        }
        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = DarkenColor(button1.BackColor, 0.2f);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            //button1.BackColor = AccentCurrentColor;
            button1.BackColor = Color.White;
        }
        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.exit_black_enter;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Image = pictureBox7.InitialImage;
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

        #region Check For Show Password
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Timer For Must Be Control Every Time Methods
        private void timer1_Tick(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Accent"))
            {
                if (key != null)
                {
                    CurrentAccentColor = key.GetValue("AccentColorMenu");
                    if (CurrentAccentColor != null && !CurrentAccentColor.Equals(lastAccentColor) || Program.AccentMustBeUpdated)
                    {
                        if (CurrentAccentColor != null && CurrentAccentColor is int)
                        {
                            Program.CursorChange();
                            int colorValue = (int)CurrentAccentColor;
                            AccentCurrentColor = Program.GetWindowsAccentColor();
                            lastAccentColor = CurrentAccentColor;
                            ChangeOutPageColor(AccentCurrentColor);
                            if (Program.SettingsValues[1] == "0")
                            {
                                ChangeAccentColorForAllForms(AccentCurrentColor);
                                Program.AccentMustBeUpdated = false;
                            }
                        }
                    }
                }
            }
            if (Program.SettingsValues[1] == "2" && Program.AccentMustBeUpdated)
            {
                ChangeAccentColorForAllForms(Color.Black);
                Program.AccentMustBeUpdated = false;
            }
            if (!waiting)
            {
                Task updateTask = UpdateDBChecker(cancellationTokenSource.Token);
            }


            System.GC.Collect();
        }
        #endregion

        #region Change Update Database Often
        public static async Task UpdateDBChecker(CancellationToken cancellationToken)
        {
            waiting = true;
            try
            {
                if (Properties.Settings.Default.DatabaseBackup == "0")
                {
                    await Task.Delay(900000, cancellationToken);
                }
                else if (Properties.Settings.Default.DatabaseBackup == "1")
                {
                    await Task.Delay(1800000, cancellationToken);
                }
                else if (Properties.Settings.Default.DatabaseBackup == "2")
                {
                    await Task.Delay(3600000, cancellationToken);
                }
                else if (Properties.Settings.Default.DatabaseBackup == "3")
                {
                    await Task.Delay(10800000, cancellationToken);
                }
                else if (Properties.Settings.Default.DatabaseBackup == "4")
                {
                    await Task.Delay(21600000, cancellationToken);
                }
                else if (Properties.Settings.Default.DatabaseBackup == "5")
                {
                    await Task.Delay(43200000, cancellationToken);
                }
                else
                {
                    await Task.Delay(86400000, cancellationToken);
                }

                cancellationToken.ThrowIfCancellationRequested();
                MainPage.UpdateDatabase(true);
                waiting = false;
            }
            catch (OperationCanceledException)
            {
                waiting = false;
                if (cancellationTokenSource?.IsCancellationRequested ?? true)
                {
                    cancellationTokenSource = new CancellationTokenSource();
                }
                if (!waiting)
                {
                    Task updateTask = UpdateDBChecker(cancellationTokenSource.Token);
                }

            }
        }

        private void txtBoxKullaniciAdi_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (txtBoxSifre.UseSystemPasswordChar == false)
            {
                pictureBox4.Image = Properties.Resources.open_eye;
                txtBoxSifre.UseSystemPasswordChar = true;
                eyeClick = true;
            }
            else if (txtBoxSifre.UseSystemPasswordChar == true)
            {
                pictureBox4.Image = Properties.Resources.close_eye;
                txtBoxSifre.UseSystemPasswordChar = false;
                eyeClick = true;
            }
        }

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

        private void LoginPage_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void LoginPage_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void LoginPage_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

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

        private void LoginPage_Click(object sender, EventArgs e)
        {
            Program.loginPage.ActiveControl = null;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            if (txtBoxSifre.UseSystemPasswordChar == false)
            {
                pictureBox4.Image = Properties.Resources.close_to_open;
            }
            else if (txtBoxSifre.UseSystemPasswordChar == true)
            {
                pictureBox4.Image = Properties.Resources.open_to_close;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private async void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            usernameLabel.Visible = true;
            await Task.Delay(3000);
            usernameLabel.Visible = false;
        }

        private async void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            passwordLabel.Visible = true;
            await Task.Delay(3000);
            passwordLabel.Visible = false;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            usernameLabel.Visible = false;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            passwordLabel.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            if (txtBoxSifre.UseSystemPasswordChar == false)
            {
                if (!eyeClick)
                    pictureBox4.Image = Properties.Resources.close_eye;

            }
            else if (txtBoxSifre.UseSystemPasswordChar == true)
            {
                if (!eyeClick)
                    pictureBox4.Image = Properties.Resources.open_eye;
            }
            eyeClick = false;
        }

        private protected void LoginPage_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, Width, Height));
        }
        #endregion

        #region Change Language
        private void Language_Click(object sender, EventArgs e)
        {

            LanguageUpdate();

        }


        public void LanguageUpdate()
        {
            if (LanguageStr == "eng")
            {
                LanguageStr = "tur";
                Language.Image = NTP_P1.Properties.Resources.turkey_lang;
                currentLang = "tur";
                if (AppSettings.AcikMi == true)
                {
                    MainPage.appSettings.Language.Image = NTP_P1.Properties.Resources.turkey_lang;
                }
            }
            else if (LanguageStr == "tur")
            {
                LanguageStr = "eng";
                Language.Image = NTP_P1.Properties.Resources.english_lang;
                currentLang = "eng";
                if (AppSettings.AcikMi == true)
                {
                    MainPage.appSettings.Language.Image = NTP_P1.Properties.Resources.english_lang;
                }
            }

            Program.GetLanguagePack(LanguageStr);
            Program.SetLanguage(Program.LanguagePack);
        }
        #endregion

        #region Change Backcolor Of Forms
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public static async void ChangeOutPageColor(Color CurrentColor)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            //Program.loginPage.btnGiris.BackColor = CurrentColor;
            //Program.loginPage.button1.BackColor = CurrentColor;
            //Program.loginPage.button2.BackColor = CurrentColor;
            //Program.loginPage.BackColor = CurrentColor;
            //Program.registerPage.btnVerifyCodeSend.BackColor = CurrentColor;
            //Program.updatePassword.btnVerifyCodeSend.BackColor = CurrentColor;
            //Program.forgetPassword.btnVerifyCodeSend.BackColor = CurrentColor;
            //Program.verifyPage.btnVerifyCodeCheck.BackColor = CurrentColor;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async void ChangeAccentColorForAllForms(Color CurrentColor)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            //ChangeOutPageColor(CurrentColor);

            if (MainPage.isAdminMenuOpen)
                Program.mainPage.AdminGetAllUsers();
            if (MainPage.isProductMenuOpen)
                if (MainPage.isRoot)
                {
                    Program.mainPage.GetAllProduct();
                }
                else if (MainPage.isEmployee)
                {
                    string group = db.GetGroupWithUsername(LoginPage.kullaniciAdi);
                    Program.mainPage.GetJustAccessedProducts(group);
                }
            if (MainPage.isUserMenuOpen)
                Program.mainPage.GetAllUsers();

            if (ProfitShow.AcikMi)
            {
                MainPage.profitShow.BackColor = CurrentColor;
            }
            if (DataGridShow.AcikMi)
            {
                ProductView.DataGridShowInstance.GetAll();
            }
            if (AddUser.AcikMi)
            {
                MainPage.addusers.BackColor = CurrentColor;
            }
            if (AddProduct.AcikMi)
            {
                MainPage.addProduct.BackColor = CurrentColor;
            }
            if (DetailedProductAnalysis.AcikMi)
            {
                ProductView.productAnalysis.BackColor = CurrentColor;
                if (DataGridShow.AcikMi)
                {
                    if (AddDailyProductData.AcikMi)
                    {
                        DataGridShow.addDailyProductData.BackColor = CurrentColor;
                    }
                    if (UpdateDailyProductData.AcikMi)
                    {
                        DatabaseView.updateDailyProductData.BackColor = CurrentColor;
                    }
                }
            }

            if (Update_Personel.AcikMi)
            {
                if (Update_Personel.AdminOrDefault)
                {
                    AdminPersonelView.updatePersonel.BackColor = CurrentColor;
                }
                else
                {
                    PersonelView.updatePersonel.BackColor = CurrentColor;
                }
            }
            if (AppSettings.AcikMi)
            {
                MainPage.appSettings.BackColor = CurrentColor;
                if (UpdateProfile.AcikMi)
                {
                    MainPage.appSettings.updateProfile.BackColor = CurrentColor;
                    //MainPage.appSettings.updateProfile.button2.ForeColor = CurrentColor;
                    //MainPage.appSettings.updateProfile.button3.ForeColor = CurrentColor;
                }
            }
        }
        #endregion

        #region Get Value From Application 
        private void Init_Data()
        {
            if (Properties.Settings.Default.username != string.Empty)
            {
                if (Properties.Settings.Default.password != string.Empty)
                {
                    if (Properties.Settings.Default.remember == true)
                    {
                        txtBoxKullaniciAdi.Text = Properties.Settings.Default.username;
                        txtBoxSifre.Text = Properties.Settings.Default.password;
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        txtBoxKullaniciAdi.Text = Properties.Settings.Default.username;
                    }
                }
            }
        }
        #endregion

        #region Save Data To Application
        private void Save_Data()
        {
            if (checkBox1.Checked)
            {
                Properties.Settings.Default.username = txtBoxKullaniciAdi.Text.Trim();
                Properties.Settings.Default.password = txtBoxSifre.Text.Trim();
                Properties.Settings.Default.remember = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.username = "";
                Properties.Settings.Default.password = "";
                Properties.Settings.Default.remember = false;
                Properties.Settings.Default.passwordShown = false;
                Properties.Settings.Default.Save();
            }
        }
        #endregion

        #region Form Load Processes
        private void LoginPage_Load(object sender, EventArgs e)
        {
            Program.loginPage.ActiveControl = null;
            timer1.Enabled = true;
            ChangeOutPageColor(AccentCurrentColor);
            Program.GetLanguagePack();
            Program.SetLanguage(Program.LanguagePack);
            Language.Image = NTP_P1.Properties.Resources.english_lang;
            currentLang = "eng";
            if (AppSettings.AcikMi == true)
            {
                MainPage.appSettings.Language.Image = NTP_P1.Properties.Resources.english_lang;
            }
            LanguageStr = "eng";
        }
        private void LoginPage_Shown(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.username != string.Empty)
            {
                if (Properties.Settings.Default.password != string.Empty)
                {
                    if (Properties.Settings.Default.remember == true)
                    {
                        btnGiris.PerformClick();
                    }
                }
            }
        }
        #endregion

        #region Color Change if is Required
        public void ColorChange()
        {
            if (Program.SettingsValues[1] != "0")
            {
                justStart = false;
                AccentColor = Program.GetWindowsAccentColor();
                ChangeAccentColorForAllForms(AccentColor);
            }
            else if (Program.SettingsValues[1] == "2")
            {
                justStart = false;
                AccentColor = SystemColors.Control;
                ChangeAccentColorForAllForms(AccentColor);
            }
            else if (Program.SettingsValues[1] == "1")
            {
                justStart = true;
                AccentColor = Program.GetWindowsAccentColor();
                ChangeAccentColorForAllForms(AccentColor);
            }
        }
        #endregion
    }
}