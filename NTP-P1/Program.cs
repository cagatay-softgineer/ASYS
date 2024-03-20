
#region Packages
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace NTP_P1
{
    internal static class Program
    {

        #region FormLoad
        public static StartIcon startIcon = new StartIcon();
        public static LoginPage loginPage = new LoginPage();
        public static ForgetPassword forgetPassword = new ForgetPassword();
        public static VerifyPage verifyPage = new VerifyPage();
        public static MainPage mainPage = new MainPage();
        public static UpdatePassword updatePassword = new UpdatePassword();
        public static RegisterPage registerPage = new RegisterPage();

        #endregion

        #region Variable Definition
        public static string[] SettingsValues = new string[3];
        public static string[] LanguagePack = new string[235];
        public static string[] Output = new string[136];// Change Size Of Array With Max Usage Count!
        public static string DBsettings;
        public static bool SettingsSaved, AccentMustBeUpdated, LanguagePackTaken;
        static Database ProgramDatabase = new Database();
        public static string[] currentAnalysisData = new string[6];
        public static string currentProduct;
        public static string verifyType;
        public static Color AccentCurrentColor;
        private static object lastAccentColor, CurrentAccentColor;
        #endregion

        #region Application
        [STAThread]
        static void Main()
        {
            CheckDatabaseExistience();
            startIcon.Show();
            
            if (Properties.Settings.Default.DatabaseBackup == "0")
            {
                if (DateTime.Now > Properties.Settings.Default.lastBackupTime.AddMinutes(15))
                {
                    UpdateDatabase();
                }
            }
            else if (Properties.Settings.Default.DatabaseBackup == "1")
            {
                if (DateTime.Now > Properties.Settings.Default.lastBackupTime.AddMinutes(30))
                {
                    UpdateDatabase();
                }
            }
            else if (Properties.Settings.Default.DatabaseBackup == "2")
            {
                if (DateTime.Now > Properties.Settings.Default.lastBackupTime.AddHours(1))
                {
                    UpdateDatabase();
                }
            }
            else if (Properties.Settings.Default.DatabaseBackup == "3")
            {
                if (DateTime.Now > Properties.Settings.Default.lastBackupTime.AddHours(3))
                {
                    UpdateDatabase();
                }
            }
            else if (Properties.Settings.Default.DatabaseBackup == "4")
            {
                if (DateTime.Now > Properties.Settings.Default.lastBackupTime.AddHours(6))
                {
                    UpdateDatabase();
                }
            }
            else if (Properties.Settings.Default.DatabaseBackup == "5")
            {
                if (DateTime.Now > Properties.Settings.Default.lastBackupTime.AddHours(12))
                {
                    UpdateDatabase();
                }
            }
            else
            {
                if (DateTime.Now > Properties.Settings.Default.lastBackupTime.AddHours(24))
                {
                    UpdateDatabase();
                }
            }
            
            Application.Run();
        }
        #endregion

        #region Database Control
        public static void UpdateDatabase()
        {
            if (!Directory.Exists($"{Application.StartupPath}/Backup"))
            {
                // Create the "Backup" folder if it doesn't exist
                Directory.CreateDirectory($"{Application.StartupPath}/Backup");
            }
            string backupDate = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            string databasePath = $"{Application.StartupPath}\\ProjectDatabase.accdb"; // Replace with your actual database file path
            string backupPath = $"{Application.StartupPath}\\Backup\\ProjectDatabase{backupDate}.accdb"; // Replace with your desired backup location
            string zipOutputPath = $"{Application.StartupPath}\\Backup\\DatabaseBackup{backupDate}-ENCRYPTED.encrypted"; // Replace with your desired backup location
            string zipDecrpytPath = $"{Application.StartupPath}\\Backup\\DatabaseBackup{backupDate}-DECRYPTED.accdb";

            ///   string inputFile = "yourfile.accdb";
            ///   string encryptedFile = "encrypted.accdb";
            ///   string decryptedFile = "decrypted.accdb";
            ///   
            ///   string key = "YourSecretKey"; // You should use a strong, secure key
            ///   
            ///   EncryptFile(inputFile, encryptedFile, key);
            ///   DecryptFile(encryptedFile, decryptedFile, key);
            try
            {
                // Check if the database file exists
                if (System.IO.File.Exists(databasePath))
                {
                    // Create the backup by copying the database file
                    System.IO.File.Copy(databasePath, backupPath, false);
                    FileCrpyt.EncryptFileSilent(backupPath, zipOutputPath, Crypt.key);
                    //MessageBox.Show(Program.Output[42]);

                    //MessageBox.Show(Crypt.GetLastEncryptedFile($"{Application.StartupPath}\\Backup\\", ".encrypted"));
                    //FileCrpyt.DecryptFile(Crypt.GetLastEncryptedFile($"{Application.StartupPath}\\Backup\\", ".encrypted"), zipDecrpytPath, Crypt.key);
                }
                else
                {
                    //MessageBox.Show(Program.Output[43]);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"{Program.Output[44]} {ex}");
            }

            Properties.Settings.Default.lastBackupTime = DateTime.Now;
            Properties.Settings.Default.Save();
        }

        public static void CheckDatabaseExistience()
        {
            Application.EnableVisualStyles();
            if (!File.Exists($"{Application.StartupPath}\\ProjectDatabase.accdb"))
            {
                if (Directory.Exists($"{Application.StartupPath}\\Backup"))
                {
                    string zipDecrpytPath = $"{Application.StartupPath}\\ProjectDatabase.accdb";
                    FileCrpyt.DecryptFileSilent(Crypt.GetLastEncryptedFile($"{Application.StartupPath}\\Backup\\", ".encrypted"), zipDecrpytPath, Crypt.key);
                }
            }
        }
        #endregion

        #region User Settings Apply
        public static void ApplySettings()
        {
            DBsettings = "";
            DBsettings = ProgramDatabase.GetSettingsWithUsernameBase(LoginPage.kullaniciAdi);
            if (DBsettings.Length > 0)
            {
                DBsettings = Program.DecryptIt(DBsettings);
                SettingsValues[0] = $"{DBsettings[0]}";
                SettingsValues[1] = $"{DBsettings[1]}";
                SettingsValues[2] = $"{DBsettings[2]}";
            }
            else
            {
                SettingsValues[0] = SettingsValues[1] = "0";
            }
            SettingsSaved = true;
            if (SettingsValues[2] == "0")
            {
                GetLanguagePack("tur");
                SetLanguage(LanguagePack);
                loginPage.Language.Image = NTP_P1.Properties.Resources.turkey_lang;
                LoginPage.currentLang = "tur";
                LoginPage.LanguageStr = "tur";
                if (AppSettings.AcikMi == true)
                {
                    MainPage.appSettings.Language.Image = NTP_P1.Properties.Resources.turkey_lang;
                }
            }
            else if (SettingsValues[2] == "1")
            {
                GetLanguagePack("eng");
                SetLanguage(LanguagePack);
                loginPage.Language.Image = NTP_P1.Properties.Resources.english_lang;
                LoginPage.currentLang = "eng";
                LoginPage.LanguageStr = "eng";
                if (AppSettings.AcikMi == true)
                {
                    MainPage.appSettings.Language.Image = NTP_P1.Properties.Resources.english_lang;
                }
            }
        }
        #endregion

        #region Crpyting
        public static void ChangeForm(Form show, Form hide)
        {
            show.Show();
            hide.Hide();
        }
        public static string EncryptIt(string input)
        {
            return Crypt.AllInOneE(input);
        }
        public static string DecryptIt(string input)
        {
            return Crypt.AllInOneD(input);
        }
        #endregion

        #region Other Methods
        public static async void CursorChange()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                await Task.Delay(5000);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception)
            {
                Cursor.Current = Cursors.Default;
            }
        }
        public static Color GetWindowsAccentColor()
        {
            if (!LoginPage.justStart)
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Accent"))
                {
                    if (key != null)
                    {
                        object accentColor = key.GetValue("AccentColorMenu");
                        if (accentColor != null && accentColor is int)
                        {
                            int colorValue = (int)accentColor;
                            if ((byte)(colorValue & 0xFF) < 90 && (byte)((colorValue >> 8) & 0xFF) < 90 && (byte)((colorValue >> 16) & 0xFF) < 90)
                            {
                                return Color.LightGray;
                            }
                            else
                            {
                                return Color.FromArgb((byte)(colorValue & 0xFF), (byte)((colorValue >> 8) & 0xFF), (byte)((colorValue >> 16) & 0xFF));
                            }
                        }
                    }
                }
                // Default to a fallback color if accent color retrieval fails
                return Color.Gray;
            }
            else
            {
                return Color.Black;
            }
        }
        #endregion

        #region LanguagePack Load
        public static void GetLanguagePack(string Language = "eng")
        {
            LanguagePack = ProgramDatabase.GetLanguagePacksTest("Language", Language);
        }
        public static void SetLanguage(string[] LanguagePack)
        {
            if (LanguagePackTaken)
            {
                Program.loginPage.usernameLabel.Text = LanguagePack[0];
                Program.loginPage.passwordLabel.Text = LanguagePack[1];
                //Program.loginPage.checkBox2.Text = LanguagePack[2];
                Program.loginPage.label1.Text = LanguagePack[231];
                Program.loginPage.Text = LanguagePack[3];
                Program.loginPage.btnGiris.Text = LanguagePack[4];
                Program.loginPage.button1.Text = LanguagePack[5];
                Program.loginPage.checkBox1.Text = LanguagePack[6];
                Program.forgetPassword.btnVerifyCodeSend.Text = LanguagePack[7];
                Program.loginPage.button2.Text = LanguagePack[8];
                Program.forgetPassword.Text = LanguagePack[9];
                Program.forgetPassword.label1.Text = LanguagePack[232];
                Program.registerPage.Text = LanguagePack[10];
                Program.registerPage.label1.Text = LanguagePack[11];
                Program.registerPage.label2.Text = LanguagePack[12];
                Program.registerPage.label3.Text = LanguagePack[13];
                Program.registerPage.checkBox1.Text = LanguagePack[14];
                Program.registerPage.checkBox3.Text = LanguagePack[15];
                Program.registerPage.checkBox2.Text = LanguagePack[16];
                Program.registerPage.btnVerifyCodeSend.Text = LanguagePack[17];
                Program.verifyPage.Text = LanguagePack[18];
                Program.verifyPage.btnVerifyCodeCheck.Text = LanguagePack[19];
                Program.verifyPage.VerifyCode.Text = LanguagePack[20];
                Program.updatePassword.Text = LanguagePack[21];
                Program.updatePassword.label1.Text = LanguagePack[22];
                Program.updatePassword.label2.Text = LanguagePack[23];
                Program.updatePassword.label3.Text = LanguagePack[234];
                Program.updatePassword.btnVerifyCodeSend.Text = LanguagePack[24];
                Program.mainPage.Text = LanguagePack[25];
                Program.mainPage.label1.Text = LanguagePack[26];
                Program.mainPage.LabelKA.Text = LanguagePack[27];
                Program.mainPage.LabelTarih.Text = LanguagePack[28];
                Program.mainPage.LabelUA.Text = LanguagePack[29];
                Program.mainPage.LabelSF.Text = LanguagePack[30];
                Program.mainPage.LabelUG.Text = LanguagePack[31];
                Program.mainPage.label7.Text = LanguagePack[32];
                Program.mainPage.label14.Text = LanguagePack[33];
                Program.mainPage.label15.Text = LanguagePack[34];
                Program.mainPage.label12.Text = LanguagePack[35];
                Program.mainPage.hid1.Text = LanguagePack[72];//ROOT
                Program.mainPage.hid2.Text = LanguagePack[73]; ;//DEFU
                Program.mainPage.hid3.Text = LanguagePack[74]; ;//EMP
                Program.Output[0] = LanguagePack[36];
                Program.Output[1] = LanguagePack[37];
                Program.Output[2] = LanguagePack[38];
                Program.Output[3] = LanguagePack[39];
                Program.Output[4] = LanguagePack[40];
                Program.Output[5] = LanguagePack[41];
                Program.Output[6] = LanguagePack[42];
                Program.Output[7] = LanguagePack[43];
                Program.Output[8] = LanguagePack[44];
                Program.Output[9] = LanguagePack[45];
                Program.Output[10] = LanguagePack[46];
                Program.Output[11] = LanguagePack[47];
                Program.Output[12] = LanguagePack[48];
                Program.Output[13] = LanguagePack[49];
                Program.Output[14] = LanguagePack[50];
                Program.Output[15] = LanguagePack[51];
                Program.Output[16] = LanguagePack[52];
                Program.Output[17] = LanguagePack[53];
                Program.Output[18] = LanguagePack[54];
                Program.Output[19] = LanguagePack[55];
                Program.Output[20] = LanguagePack[56];
                Program.Output[21] = LanguagePack[57];
                Program.Output[22] = LanguagePack[58];
                Program.Output[23] = LanguagePack[59];
                Program.Output[24] = LanguagePack[60];
                Program.Output[25] = LanguagePack[61];
                Program.Output[26] = LanguagePack[62];
                Program.Output[27] = LanguagePack[63];
                Program.Output[28] = LanguagePack[64];
                Program.Output[29] = LanguagePack[65];
                Program.Output[30] = LanguagePack[66];
                Program.Output[31] = LanguagePack[67];
                Program.Output[32] = LanguagePack[68];
                Program.Output[33] = LanguagePack[69];
                Program.Output[34] = LanguagePack[70];
                Program.Output[35] = LanguagePack[71];
                Program.Output[36] = LanguagePack[72];
                Program.Output[37] = LanguagePack[73];
                Program.Output[38] = LanguagePack[74];
                Program.Output[39] = LanguagePack[75];
                Program.Output[40] = LanguagePack[76];
                Program.Output[41] = LanguagePack[77];
                Program.Output[42] = LanguagePack[78];
                Program.Output[43] = LanguagePack[79];
                Program.Output[44] = LanguagePack[80];
                Program.Output[45] = LanguagePack[85];
                Program.Output[46] = LanguagePack[86];
                Program.Output[47] = LanguagePack[87];
                Program.Output[48] = LanguagePack[88];
                Program.Output[49] = LanguagePack[89];
                Program.Output[50] = LanguagePack[90];
                Program.Output[51] = LanguagePack[102];
                Program.Output[52] = LanguagePack[103];
                Program.Output[53] = LanguagePack[104];
                Program.Output[54] = LanguagePack[105];
                Program.Output[55] = LanguagePack[106];
                Program.Output[56] = LanguagePack[107];
                Program.Output[57] = LanguagePack[108];
                Program.Output[58] = LanguagePack[109];
                Program.Output[59] = LanguagePack[110];
                Program.Output[60] = LanguagePack[111];
                Program.Output[61] = LanguagePack[112];
                Program.Output[62] = LanguagePack[113];
                Program.Output[63] = LanguagePack[114];
                Program.Output[64] = LanguagePack[115];
                Program.Output[65] = LanguagePack[116];
                Program.Output[66] = LanguagePack[117];
                Program.Output[67] = LanguagePack[118];
                Program.Output[68] = LanguagePack[124];
                Program.Output[69] = LanguagePack[125];
                Program.Output[70] = LanguagePack[141];
                Program.Output[71] = LanguagePack[142];
                Program.Output[72] = LanguagePack[143];
                Program.Output[73] = LanguagePack[144];
                Program.Output[74] = LanguagePack[145];
                Program.Output[75] = LanguagePack[146];
                Program.Output[76] = LanguagePack[147];
                Program.Output[77] = LanguagePack[148];
                Program.Output[78] = LanguagePack[149];
                Program.Output[79] = LanguagePack[150];
                Program.Output[80] = LanguagePack[151];
                Program.Output[81] = LanguagePack[152];
                Program.Output[82] = LanguagePack[153];
                Program.Output[83] = LanguagePack[154];
                Program.Output[84] = LanguagePack[155];
                Program.Output[85] = LanguagePack[156];
                Program.Output[86] = LanguagePack[157];
                Program.Output[87] = LanguagePack[158];
                Program.Output[88] = LanguagePack[159];
                Program.Output[89] = LanguagePack[160];
                Program.Output[90] = LanguagePack[161];
                Program.Output[91] = LanguagePack[162];
                Program.Output[92] = LanguagePack[163];
                Program.Output[93] = LanguagePack[164];
                Program.Output[94] = LanguagePack[165];
                Program.Output[95] = LanguagePack[166];
                Program.Output[96] = LanguagePack[167];
                Program.Output[97] = LanguagePack[168];
                Program.Output[98] = LanguagePack[169];
                Program.Output[99] = LanguagePack[170];
                Program.Output[100] = LanguagePack[171];
                Program.Output[101] = LanguagePack[172];
                Program.Output[102] = LanguagePack[173];
                Program.Output[103] = LanguagePack[174];
                Program.Output[104] = LanguagePack[175];
                Program.Output[105] = LanguagePack[176];
                Program.Output[106] = LanguagePack[177];
                Program.Output[107] = LanguagePack[178];
                Program.Output[108] = LanguagePack[179];
                Program.Output[109] = LanguagePack[180];
                Program.Output[110] = LanguagePack[181];
                Program.Output[111] = LanguagePack[188];
                Program.Output[112] = LanguagePack[189];
                Program.Output[113] = LanguagePack[196];
                Program.Output[114] = LanguagePack[197];
                Program.Output[115] = LanguagePack[198];
                Program.Output[116] = LanguagePack[199];
                Program.Output[117] = LanguagePack[200];
                Program.Output[118] = LanguagePack[201];
                Program.Output[119] = LanguagePack[207];
                Program.Output[120] = LanguagePack[208];
                Program.Output[121] = LanguagePack[209];
                Program.Output[122] = LanguagePack[210];
                Program.Output[123] = LanguagePack[211];
                Program.Output[124] = LanguagePack[212];
                Program.Output[125] = LanguagePack[213];
                Program.Output[126] = LanguagePack[214];
                Program.Output[127] = LanguagePack[225];
                Program.Output[128] = LanguagePack[226];
                Program.Output[129] = LanguagePack[227];
                Program.Output[130] = LanguagePack[228];
                Program.Output[131] = LanguagePack[229];
                Program.Output[132] = LanguagePack[230];
                Program.Output[135] = LanguagePack[233];

                if (AddProduct.AcikMi == true)
                {
                    MainPage.addProduct.ProductName.Text = LanguagePack[81];
                    MainPage.addProduct.label1.Text = LanguagePack[82];
                    MainPage.addProduct.label2.Text = LanguagePack[83];
                    MainPage.addProduct.label3.Text = LanguagePack[84];
                    MainPage.addProduct.Text = LanguagePack[93];
                }
                if (AddUser.AcikMi == true)
                {
                    MainPage.addusers.ProductName.Text = LanguagePack[91];
                    MainPage.addusers.Text = LanguagePack[92];
                    MainPage.addusers.label1.Text = LanguagePack[94];
                    MainPage.addusers.label2.Text = LanguagePack[95];
                    MainPage.addusers.label4.Text = LanguagePack[96];
                    MainPage.addusers.comboBox1.Items.Clear();
                    MainPage.addusers.comboBox1.Items.Add(Program.Output[36]);
                    MainPage.addusers.comboBox1.Items.Add(Program.Output[37]);
                    MainPage.addusers.comboBox1.Items.Add(Program.Output[38]);
                    MainPage.addusers.comboBox1.Items.Add(Program.Output[39]);
                }
                if (AppSettings.AcikMi == true)
                {
                    MainPage.appSettings.Text = LanguagePack[97];
                    MainPage.appSettings.ID_Name.Text = LanguagePack[98];
                    MainPage.appSettings.label1.Text = LanguagePack[99];
                    MainPage.appSettings.label2.Text = LanguagePack[100];
                    MainPage.appSettings.label3.Text = LanguagePack[101];
                    MainPage.appSettings.comboBox1.Items.Clear();
                    MainPage.appSettings.comboBox1.Items.Add(Program.Output[51]);
                    MainPage.appSettings.comboBox1.Items.Add(Program.Output[52]);
                    MainPage.appSettings.comboBox2.Items.Clear();
                    MainPage.appSettings.comboBox2.Items.Add(Program.Output[51]);
                    MainPage.appSettings.comboBox2.Items.Add(Program.Output[53]);
                    MainPage.appSettings.comboBox2.Items.Add(Program.Output[54]);
                    if (UpdateProfile.AcikMi)
                    {
                        MainPage.appSettings.updateProfile.YetkiControl();
                        MainPage.appSettings.updateProfile.Text = LanguagePack[222];
                        MainPage.appSettings.updateProfile.label8.Text = LanguagePack[183];
                        MainPage.appSettings.updateProfile.label1.Text = LanguagePack[184];
                        MainPage.appSettings.updateProfile.label9.Text = LanguagePack[186];
                        MainPage.appSettings.updateProfile.label4.Text = LanguagePack[187];
                        MainPage.appSettings.updateProfile.button2.Text = LanguagePack[223];
                        MainPage.appSettings.updateProfile.button3.Text = LanguagePack[224];
                    }
                }
                if (DetailedProductAnalysis.AcikMi == true)
                {
                    if (ProgramDatabase.CheckInfoExist(currentProduct))
                    {
                        ProductView.productAnalysis.SetValuesAtLabel();
                    }
                    if (DataGridShow.AcikMi == true)
                    {
                        DetailedProductAnalysis.DataGridShowInstance.Text = LanguagePack[119];
                        DetailedProductAnalysis.DataGridShowInstance.LabelSM.Text = LanguagePack[120];
                        DetailedProductAnalysis.DataGridShowInstance.LabelSF.Text = LanguagePack[121];
                        DetailedProductAnalysis.DataGridShowInstance.LabelAF.Text = LanguagePack[122];
                        DetailedProductAnalysis.DataGridShowInstance.LabelTarih.Text = LanguagePack[123];
                        DetailedProductAnalysis.DataGridShowInstance.searchBar.Text = Program.Output[68];
                        if (AddDailyProductData.AcikMi == true)
                        {
                            DataGridShow.addDailyProductData.Text = LanguagePack[202];
                            DataGridShow.addDailyProductData.label1.Text = LanguagePack[203];
                            DataGridShow.addDailyProductData.label2.Text = LanguagePack[204];
                            DataGridShow.addDailyProductData.label3.Text = LanguagePack[205];
                            DataGridShow.addDailyProductData.label9.Text = LanguagePack[206];
                            DataGridShow.addDailyProductData.ProductName.Text = LanguagePack[217];
                        }
                        if (UpdateDailyProductData.AcikMi == true)
                        {
                            DatabaseView.updateDailyProductData.Text = LanguagePack[215];
                            DatabaseView.updateDailyProductData.label1.Text = LanguagePack[216];
                            DatabaseView.updateDailyProductData.ProductName.Text = LanguagePack[218];
                            DatabaseView.updateDailyProductData.label2.Text = LanguagePack[219];
                            DatabaseView.updateDailyProductData.label3.Text = LanguagePack[220];
                            DatabaseView.updateDailyProductData.label9.Text = LanguagePack[221];
                            DatabaseView.updateDailyProductData.label9.Text = LanguagePack[221];
                        }
                    }
                    ProductView.productAnalysis.Text = LanguagePack[126];
                    ProductView.productAnalysis.label5.Text = LanguagePack[127];
                    ProductView.productAnalysis.label6.Text = LanguagePack[128];
                    ProductView.productAnalysis.label7.Text = LanguagePack[129];
                    ProductView.productAnalysis.label11.Text = LanguagePack[130];
                    ProductView.productAnalysis.label4.Text = LanguagePack[131];
                    ProductView.productAnalysis.label1.Text = LanguagePack[132];
                    ProductView.productAnalysis.label3.Text = LanguagePack[133];
                    ProductView.productAnalysis.label9.Text = LanguagePack[134];
                    ProductView.productAnalysis.label15.Text = LanguagePack[135];
                    ProductView.productAnalysis.label21.Text = LanguagePack[136];
                    ProductView.productAnalysis.label18.Text = LanguagePack[137];
                    ProductView.productAnalysis.label22.Text = LanguagePack[138];
                    ProductView.productAnalysis.label20.Text = LanguagePack[139];
                    ProductView.productAnalysis.label23.Text = LanguagePack[140];
                }
                if (Update_Personel.AcikMi == true)
                {
                    if (Update_Personel.AdminOrDefault)
                    {
                        AdminPersonelView.updatePersonel.Text = LanguagePack[182];
                        AdminPersonelView.updatePersonel.label8.Text = LanguagePack[183];
                        AdminPersonelView.updatePersonel.label1.Text = LanguagePack[184];
                        AdminPersonelView.updatePersonel.label2.Text = LanguagePack[185];
                        AdminPersonelView.updatePersonel.label9.Text = LanguagePack[186];
                        AdminPersonelView.updatePersonel.label4.Text = LanguagePack[187];
                        AdminPersonelView.updatePersonel.Yetki.Items.Clear();
                        AdminPersonelView.updatePersonel.Yetki.Items.Add(Program.Output[36]);
                        AdminPersonelView.updatePersonel.Yetki.Items.Add(Program.Output[37]);
                        AdminPersonelView.updatePersonel.Yetki.Items.Add(Program.Output[38]);
                        AdminPersonelView.updatePersonel.Yetki.Items.Add(Program.Output[39]);
                    }
                    else
                    {
                        PersonelView.updatePersonel.Text = LanguagePack[182];
                        PersonelView.updatePersonel.label8.Text = LanguagePack[183];
                        PersonelView.updatePersonel.label1.Text = LanguagePack[184];
                        PersonelView.updatePersonel.label2.Text = LanguagePack[185];
                        PersonelView.updatePersonel.label9.Text = LanguagePack[186];
                        PersonelView.updatePersonel.label4.Text = LanguagePack[187];
                        PersonelView.updatePersonel.Yetki.Items.Clear();
                        PersonelView.updatePersonel.Yetki.Items.Add(Program.Output[36]);//Admin 
                        PersonelView.updatePersonel.Yetki.Items.Add(Program.Output[37]);//Çalışan
                        PersonelView.updatePersonel.Yetki.Items.Add(Program.Output[38]);//Sıradan
                        PersonelView.updatePersonel.Yetki.Items.Add(Program.Output[39]);//Yetkisiz
                    }
                }
                if (UpdateProducts.AcikMi)
                {
                    ProductView.operationsForProduct.Text = LanguagePack[190];
                    ProductView.operationsForProduct.label8.Text = LanguagePack[191];
                    ProductView.operationsForProduct.label1.Text = LanguagePack[192];
                    ProductView.operationsForProduct.label2.Text = LanguagePack[193];
                    ProductView.operationsForProduct.label3.Text = LanguagePack[194];
                    ProductView.operationsForProduct.label9.Text = LanguagePack[195];
                }
            }
            else
            {
                MessageBox.Show("Dil Paketi Yüklenirken Bir Sıkıntı Oluştu!\nThere was a problem loading the language pack!", "Error");
            }
        }
        #endregion

    }
}