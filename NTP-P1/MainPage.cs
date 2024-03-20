
#region Packages
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
#endregion

namespace NTP_P1
{
    public partial class MainPage : Form
    {
        #region Variable Definition
        Database db = new Database();
        public static bool isRoot = false;
        public static bool isEmployee = false;
        public static bool isDefaultUser = false;
        public static string RootControl = "0";
        public static string DefaultUserControl = "0";
        public static string EmployeeControl = "0";
        public static string dtTableName;
        string[] groups;
        DataTable DTUrun = new DataTable();
        DateTime time = DateTime.Today;
        public static string controlClick = "0";
        public static String tempProductName;
        public static String tempProductInfo;
        public static bool isUserMenuOpen, isProductMenuOpen, isAdminMenuOpen, isProfitOpen;
        public static AddUser addusers;
        public static AddProduct addProduct;
        public static AppSettings appSettings;
        public static ProfitShow profitShow;
        public static int previousMinute = DateTime.Now.Minute;
        public static string group;
        private bool mouseDown;
        private Point lastLocation;
        private double cur_percent = 100;
        Random random = new Random();
        #endregion

        #region Form Implementation
        public MainPage()
        {
            InitializeComponent();
        }
        #endregion

        #region Sign Out
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            controlClick = "1";
            pictureBox1.Image = NTP_P1.Properties.Resources.door;

            if (AddUser.AcikMi || DataGridShow.AcikMi || AddProduct.AcikMi || DetailedProductAnalysis.AcikMi || UpdateProducts.AcikMi || Update_Personel.AcikMi || ProfitShow.AcikMi || AppSettings.AcikMi)
            {
                DialogResult result1 = MessageBox.Show(Program.Output[28], Program.Output[29], MessageBoxButtons.YesNo);
                if (result1 == DialogResult.No)
                {
                    controlClick = "0";
                    pictureBox1.Image = NTP_P1.Properties.Resources.logout;
                }
                else
                {
                    controlClick = "0";

                    if (AddUser.AcikMi)
                    {
                        AddUser.AcikMi = false;
                        addusers.Dispose();
                    }
                    if (AddProduct.AcikMi)
                    {
                        AddProduct.AcikMi = false;
                        addProduct.Dispose();
                    }
                    if (ProfitShow.AcikMi)
                    {
                        ProfitShow.AcikMi = false;
                        profitShow.Dispose();
                    }
                    if (DetailedProductAnalysis.AcikMi)
                    {
                        if (DataGridShow.AcikMi)
                        {
                            if (UpdateDailyProductData.AcikMi)
                            {
                                UpdateDailyProductData.AcikMi = false;
                                DatabaseView.updateDailyProductData.Dispose();
                            }
                            if (AddDailyProductData.AcikMi)
                            {
                                AddDailyProductData.AcikMi = false;
                                DataGridShow.addDailyProductData.Dispose();
                            }
                            DataGridShow.AcikMi = false;
                            DetailedProductAnalysis.DataGridShowInstance.Dispose();
                        }
                        DetailedProductAnalysis.AcikMi = false;
                        ProductView.productAnalysis.Dispose();
                    }
                    if (UpdateProducts.AcikMi)
                    {
                        UpdateProducts.AcikMi = false;
                        ProductView.operationsForProduct.Dispose();
                    }
                    if (Update_Personel.AcikMi)
                    {
                        Update_Personel.AcikMi = false;
                        if (AdminPersonelView.updatePersonel != null)
                        {
                            AdminPersonelView.updatePersonel.Dispose();
                        }
                        if (PersonelView.updatePersonel != null)
                        {
                            PersonelView.updatePersonel.Dispose();
                        }
                    }
                    if (AppSettings.AcikMi)
                    {
                        AppSettings.AcikMi = false;
                        MainPage.appSettings.Dispose();
                        if (UpdateProfile.AcikMi)
                        {
                            UpdateProfile.AcikMi = false;
                            MainPage.appSettings.updateProfile.Dispose();
                        }
                    }
                    QuitFromUser();
                }
            }
            else
            {
                DialogResult result = MessageBox.Show(Program.Output[30], Program.Output[29], MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    QuitFromUser();
                }
            }
        }

        private void QuitFromUser()
        {
            Program.ChangeForm(Program.loginPage, this);
            Program.loginPage.pictureBox2.Image = Properties.Resources.ASYS_LOGO_FINAL_HALFSIZED;
            System.GC.Collect();
            Properties.Settings.Default.username = "";
            Properties.Settings.Default.password = "";
            Properties.Settings.Default.remember = false;
            Properties.Settings.Default.passwordShown = false;
            Properties.Settings.Default.Save();
            Program.loginPage.EmptyEntryValues();

            groupBox1.Visible = false;
            showcase.Visible = false;
            userCol.Visible = false;
            productCol.Visible = false;
            AdminUserCol.Visible = false;
            MainPage.isRoot = false;
            MainPage.isEmployee = false;
            MainPage.isDefaultUser = false;
            userMenuBox.Visible = false;
            productMenuBox.Visible = false;
            AdminMenuBox.Visible = false;
            groupBox1.Visible = false;
            DatabaseBackup.Visible = false;
            showcase.Visible = false;
            userCol.Visible = false;
            productCol.Visible = false;
            Urun_Group.Visible = false;
            pictureBox14.Visible = false;
            isProfitOpen = false;

            label6.Visible = false;
            isProductMenuOpen = isAdminMenuOpen = isUserMenuOpen = false;

            userMenuBox.Image = userMenuBox.InitialImage;
            productMenuBox.Image = productMenuBox.InitialImage;
            AdminMenuBox.Image = AdminMenuBox.InitialImage;
            pictureBox2.Image = pictureBox2.InitialImage;
            Profit.Image = Profit.InitialImage;

            flowLayoutPanel1.Controls.Clear();
            System.GC.Collect();
        }
        #endregion

        #region Close Application
        private void MainPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Form Load Processes
        private void MainPage_Load(object sender, EventArgs e)
        {
            finishProcess();
            Program.loginPage.ColorChange();
            Program.ApplySettings();

            label4.Text = DateTime.Now.ToString("dd.MM.yyyy - HH:mm");
        }
        #endregion

        #region Timer For Update Time
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Minute != previousMinute)
            {
                label4.Text = DateTime.Now.ToString("dd.MM.yyyy - HH:mm");
                previousMinute = DateTime.Now.Minute;
                DrawBlackRectangleWithTextOptinal(cur_percent, "ASYS", DateTime.Now.ToString("dd.MM.yyyy - HH:mm"), pictureBox3);
            }
        }
        #endregion

        #region Open Menu
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Menu();
        }

        public void Menu()
        {
            changeProgressBar(0);
            if (groupBox1.Visible == true)
            {
                System.GC.Collect();
                if (Program.SettingsValues[0] == "0")
                    AnimatedMenu(pictureBox2, true);
                else
                    pictureBox2.Image = pictureBox2.InitialImage;

                //pictureBox2.Image = NTP_P1.Properties.Resources.menu;
                groupBox1.Visible = false;
                AdminUserCol.Visible = false;
                showcase.Visible = false;
                userCol.Visible = false;
                productCol.Visible = false;
                isAdminMenuOpen = isProductMenuOpen = isUserMenuOpen = false;
                label6.Visible = false;
                DatabaseBackup.Visible = false;
                Urun_Group.Visible = false;
                pictureBox14.Visible = false;
                flowLayoutPanel1.Controls.Clear();

                userMenuBox.Image = NTP_P1.Properties.Resources.user;
                productMenuBox.Image = NTP_P1.Properties.Resources.goods;
                AdminMenuBox.Image = NTP_P1.Properties.Resources.security;
                System.GC.Collect();

            }
            else
            {
                if (Program.SettingsValues[0] == "0")
                    AnimatedMenu(pictureBox2, false);
                else
                    pictureBox2.Image = NTP_P1.Properties.Resources.Xo1;
                //pictureBox2.Image = NTP_P1.Properties.Resources.Xo1;
                groupBox1.Visible = true;
                userMenuBox.Visible = true;
                productMenuBox.Visible = true;
                AdminMenuBox.Visible = true;
                System.GC.Collect();
            }
            finishProcessWithAnim();
        }
        #endregion

        #region Open User Tab
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Program.CursorChange();
            if (isUserMenuOpen)
            {
                userCol.Visible = false;
                if (Program.SettingsValues[0] == "0")
                    AnimatedShrink(userMenuBox, false);
                else
                    userMenuBox.Image = userMenuBox.InitialImage;

                label6.Visible = false;
                isUserMenuOpen = false;
                showcase.Visible = false;
                label6.Text = "";
                flowLayoutPanel1.Controls.Clear();
                System.GC.Collect();
            }
            else
            {
                if (Program.SettingsValues[0] == "0")
                {
                    AnimatedShrink(userMenuBox, true);
                }
                else
                {
                    userMenuBox.Image = NTP_P1.Properties.Resources.Xo1; ;
                }
                if (isProductMenuOpen)
                {
                    if (Program.SettingsValues[0] == "0")
                    {
                        AnimatedShrink(productMenuBox, false);
                    }
                    else
                    {
                        productMenuBox.Image = productMenuBox.InitialImage;
                    }
                }
                if (isProfitOpen)
                {
                    if (Program.SettingsValues[0] == "0")
                    {
                        AnimatedShrink(Profit, false);
                    }
                    else
                    {
                        Profit.Image = Profit.InitialImage;
                    }
                }
                if (isAdminMenuOpen)
                {
                    if (Program.SettingsValues[0] == "0")
                    {
                        AnimatedShrink(AdminMenuBox, false);
                    }
                    else
                    {
                        AdminMenuBox.Image = AdminMenuBox.InitialImage;
                    }
                }
                GetAllUsers();
                isProductMenuOpen = false;
                isUserMenuOpen = true;
                isAdminMenuOpen = false;
                DatabaseBackup.Visible = false;
                showcase.Visible = true;
                Urun_Group.Visible = false;
                pictureBox14.Visible = false;
                userCol.Visible = true;
                AdminUserCol.Visible = false;
                label6.Visible = true;
                isProfitOpen = false;
                label6.Text = Program.Output[31];

                #region Hide Unwanted Object
                productCol.Visible = false;

                #endregion
                System.GC.Collect();
            }
        }
        #endregion

        #region Open Product Tab
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Program.CursorChange();
            if (isProductMenuOpen)
            {
                productCol.Visible = false;
                if (Program.SettingsValues[0] == "0")
                    AnimatedShrink(productMenuBox, false);
                else
                    productMenuBox.Image = productMenuBox.InitialImage;

                label6.Visible = false;
                isProductMenuOpen = false;
                showcase.Visible = false;
                Urun_Group.Visible = false;
                pictureBox14.Visible = false;
                label6.Text = "";
                flowLayoutPanel1.Controls.Clear();
                System.GC.Collect();
            }
            else
            {

                if (Program.SettingsValues[0] == "0")
                {
                    AnimatedShrink(productMenuBox, true);
                }
                else
                {
                    productMenuBox.Image = NTP_P1.Properties.Resources.Xo1; ;
                }
                if (isAdminMenuOpen)
                {
                    if (Program.SettingsValues[0] == "0")
                    {
                        AnimatedShrink(AdminMenuBox, false);
                    }
                    else
                    {
                        AdminMenuBox.Image = AdminMenuBox.InitialImage;
                    }
                }
                if (isProfitOpen)
                {
                    if (Program.SettingsValues[0] == "0")
                    {
                        AnimatedShrink(Profit, false);
                    }
                    else
                    {
                        Profit.Image = Profit.InitialImage;
                    }
                }
                if (isUserMenuOpen)
                {
                    if (Program.SettingsValues[0] == "0")
                    {
                        AnimatedShrink(userMenuBox, false);
                    }
                    else
                    {
                        userMenuBox.Image = userMenuBox.InitialImage;
                    }
                }

                if (MainPage.isRoot)
                {
                    Program.mainPage.GetAllProduct();
                }
                else if (MainPage.isEmployee)
                {
                    group = db.GetGroupWithUsername(LoginPage.kullaniciAdi);
                    Program.mainPage.GetJustAccessedProducts(group);
                }
                isUserMenuOpen = false;
                isProfitOpen = false;
                isProductMenuOpen = true;
                isAdminMenuOpen = false;
                showcase.Visible = true;
                userCol.Visible = false;
                productCol.Visible = true;
                AdminUserCol.Visible = false;
                label6.Visible = true;
                if (isRoot)
                {
                    Urun_Group.Visible = true;
                    pictureBox14.Visible = true;
                    get_gruops();
                }
                DatabaseBackup.Visible = false;
                label6.Text = Program.Output[32];
                if (isRoot || isEmployee)
                {

                }

                #region Hide Unwanted Object

                #endregion
                System.GC.Collect();
            }
        }

        public void get_gruops()
        { 
            Urun_Group.Items.Clear();
            groups = db.GetValuesOfColumnJustDiffrentOnes("Urun", "UrunGrubu");

            foreach (string group in groups.OrderBy(group => group))
            {
                Urun_Group.Items.Add(group);
            }
        }
        #endregion

        #region Open Admin Panel Tab
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Program.CursorChange();
            if (isAdminMenuOpen)
            {
                if (Program.SettingsValues[0] == "0")
                    AnimatedShrink(AdminMenuBox, false);
                else
                    AdminMenuBox.Image = AdminMenuBox.InitialImage;

                label6.Visible = false;
                isAdminMenuOpen = false;
                showcase.Visible = false;
                AdminUserCol.Visible = false;
                DatabaseBackup.Visible = false;
                label6.Text = "";
                flowLayoutPanel1.Controls.Clear();
                System.GC.Collect();
            }
            else
            {
                if (Program.SettingsValues[0] == "0")
                {
                    AnimatedShrink(AdminMenuBox, true);
                }
                else
                {
                    AdminMenuBox.Image = NTP_P1.Properties.Resources.Xo1; ;
                }
                if (isProductMenuOpen)
                {
                    if (Program.SettingsValues[0] == "0")
                    {
                        AnimatedShrink(productMenuBox, false);
                    }
                    else
                    {
                        productMenuBox.Image = productMenuBox.InitialImage;
                    }
                }
                if (isProfitOpen)
                {
                    if (Program.SettingsValues[0] == "0")
                    {
                        AnimatedShrink(Profit, false);
                    }
                    else
                    {
                        Profit.Image = Profit.InitialImage;
                    }
                }
                if (isUserMenuOpen)
                {
                    if (Program.SettingsValues[0] == "0")
                    {
                        AnimatedShrink(userMenuBox, false);
                    }
                    else
                    {
                        userMenuBox.Image = userMenuBox.InitialImage;
                    }
                }
                AdminGetAllUsers();
                isUserMenuOpen = false;
                DatabaseBackup.Visible = true;
                isAdminMenuOpen = true;
                isProfitOpen = false;
                isProductMenuOpen = false;
                AdminUserCol.Visible = true;
                showcase.Visible = true;
                userCol.Visible = false;
                productCol.Visible = false;
                Urun_Group.Visible = false;
                pictureBox14.Visible = false;
                label6.Visible = true;
                label6.Text = Program.Output[33];
                if (isRoot || isEmployee)
                {

                }

                #region Hide Unwanted Object

                #endregion
                System.GC.Collect();
            }
        }
        #endregion

        #region Logout Button Animation
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = NTP_P1.Properties.Resources.door;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (controlClick.Equals("1"))
            {

            }
            else
            {
                pictureBox1.Image = NTP_P1.Properties.Resources.logout;
            }

        }
        #endregion

        #region Auth Check
        public void YetkiKontrol()
        {
            changeProgressBar(10);
            isRoot = db.CheckRoot(LoginPage.kullaniciAdi);
            changeProgressBar(30);
            isDefaultUser = db.CheckDefaultUser(LoginPage.kullaniciAdi);
            changeProgressBar(50);
            isEmployee = db.CheckEmployee(LoginPage.kullaniciAdi);
            changeProgressBar(60);

            string[] names = new string[2];
            names = db.GetNameAndSurnameWithUsername(LoginPage.kullaniciAdi);
            changeProgressBar(70);
            label3.Text = $"{Program.DecryptIt(names[0])} {Program.DecryptIt(names[1])}";

            changeProgressBar(75);
            if (isRoot)
            {
                label2.Text = Program.Output[36];
                pictureBox2.Visible = true;
                pictureBox5.Visible = true;
                Profit.Visible = true;
            }
            else if (isDefaultUser)
            {
                label2.Text = Program.Output[37];
                pictureBox2.Visible = false;
                userMenuBox.Visible = false;
                AdminMenuBox.Visible = false;
                groupBox1.Visible = true;
                productMenuBox.Visible = true;
                pictureBox5.Visible = false;
                Profit.Visible = false;
            }
            else if (isEmployee)
            {
                label2.Text = Program.Output[38];
                pictureBox2.Visible = false;
                userMenuBox.Visible = false;
                AdminMenuBox.Visible = false;
                groupBox1.Visible = true;
                productMenuBox.Visible = true;
                pictureBox5.Visible = true;
                Profit.Visible = false;
            }
            else
            {
                label2.Text = Program.Output[39];
                pictureBox2.Visible = false;
                Profit.Visible = false;
            }
            finishProcess();
        }
        #endregion

        #region Load All Product
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            if (MainPage.isRoot)
            {
                GetAllProduct();
                get_gruops();
            }
            else if (MainPage.isEmployee)
            {
                group = db.GetGroupWithUsername(LoginPage.kullaniciAdi);
                GetJustAccessedProducts(group);
            }
        }

        public async void GetAllProduct()
        {
            this.ActiveControl = null;
            Urun_Group.Text = "Filter By Groups";
            Urun_Group.SelectedIndex = -1;
            changeProgressBar(0);
            Program.CursorChange();
            flowLayoutPanel1.Controls.Clear();
            changeProgressBar(10);
            System.GC.Collect();
            changeProgressBar(20);
            if (flowLayoutPanel1.Controls.Count == 0 || productMenuBox.Image == NTP_P1.Properties.Resources.goods)
            {
                changeProgressBar(30);
                dtTableName = "Urun";

                AnimatedRotate(pictureBox10);
                changeProgressBar(40);
                if (Database.DGWShow(DTUrun, $"[{dtTableName}]", "ID") != null)
                {
                    changeProgressBar(50);
                    pictureBox10.Enabled = false;
                    double all_product_count = (double)db.GetCountOfColumn(dtTableName,"ID");
                    double cur_product = 0;
                    foreach (var product in Database.GetAllProduct(dtTableName))
                    {
                        cur_product++;
                        double Process_percent = cur_product / all_product_count * 30 + 50;
                        changeProgressBar(Process_percent);
                        ProductView productView = new ProductView();
                        ProductView.isRoot = isRoot;
                        ProductView.isEmployee = isEmployee;
                        ProductView.isDefaultUser = isDefaultUser;
                        productView.BProduct = BProduct.FromTuple(product);
                        flowLayoutPanel1.Controls.Add(productView);
                    }
                    changeProgressBar(90);
                }
                else
                {
                    this.Dispose();
                    System.GC.Collect();
                    MessageBox.Show(Program.Output[34], Program.Output[35], MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finishProcess();
            await Task.Delay(1000);
            pictureBox10.Enabled = true;
            //pictureBox10.Image = NTP_P1.Properties.Resources.update; 
        }
        #endregion

        #region Load All User For User Tab
        private void pictureBox11_Click(object sender, EventArgs e)
        {
            GetAllUsers();
        }
        public async void GetAllUsers()
        {
            changeProgressBar(0);
            flowLayoutPanel1.Controls.Clear();
            changeProgressBar(10);
            System.GC.Collect();
            changeProgressBar(20);
            if (flowLayoutPanel1.Controls.Count == 0 || userMenuBox.Image == NTP_P1.Properties.Resources.user)
            {
                changeProgressBar(30);
                dtTableName = "Personel";

                AnimatedRotate(pictureBox11);
                changeProgressBar(40);
                if (Database.DGWShow(DTUrun, $"[{dtTableName}]", "ID") != null)
                {
                    changeProgressBar(50);
                    pictureBox11.Enabled = false;
                    double all_count = (double)db.GetCountOfColumn(dtTableName, "ID");
                    double cur_index = 0;
                    foreach (var user in Database.GetAllUsers(dtTableName))
                    {
                        cur_index++;
                        double Process_percent = cur_index / all_count * 30 + 50;
                        changeProgressBar(Process_percent);
                        PersonelView userView = new PersonelView();
                        userView.Personel = Personel.FromTuple(user);
                        flowLayoutPanel1.Controls.Add(userView);
                    }
                    changeProgressBar(90);
                }
                else
                {
                    this.Dispose();
                    System.GC.Collect();
                    MessageBox.Show(Program.Output[34], Program.Output[35], MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finishProcess();
            await Task.Delay(1000);
            pictureBox11.Enabled = true;
            //pictureBox11.Image = NTP_P1.Properties.Resources.update;
            
        }
        #endregion

        #region Add Product
        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            if (AddProduct.AcikMi == false)
            {
                addProduct = new AddProduct();
                //addProduct.AdminControl(false);
                addProduct.Show();
            }
            else
            {
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region Add User For User Tab
        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            if (AddUser.AcikMi == false)
            {
                addusers = new AddUser();
                addusers.AdminControl(false);
                addusers.Show();
            }
            else
            {
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region Better Animations
        private async void AnimatedMenu(PictureBox pictureBox, bool RotateDirection)
        {
            int[] rotateAngels = { 1, 1, 2, 3, 3, 4, 5, 5, 6, 7, 8, 9, 10, 11, 15 };

            foreach (int rotateAngel in rotateAngels)
            {
                if (RotateDirection)
                {
                    RotateImage(rotateAngel, pictureBox);
                    await Task.Delay(10);
                    //await Task.Delay(100 - (100 + rotateAngel) % 9);
                }
                else
                {
                    RotateImage(-rotateAngel, pictureBox);
                    await Task.Delay(10);
                    //await Task.Delay(100 - (100 + rotateAngel) % 9);
                }
            }
        }

        private async void AnimatedRotate(PictureBox pictureBox)
        {
            int[] rotateAngels = { 1, 20, 40, 60, 80, 100, 120, 140, 160, 180, 200, 220, 240, 260, 280, 300, 320, 340, 359 };
            try
            {
                //await Task.Delay(5000);
                foreach (int rotateAngel in rotateAngels)
                {
                    RotateImage(-rotateAngel, pictureBox);
                    await Task.Delay(100 - (100 + rotateAngel) % 36);
                }
            }
            catch (Exception)
            {
                pictureBox.Image = pictureBox.InitialImage;
            }
            finally
            {
                pictureBox.Image = pictureBox.InitialImage;
            }
            System.GC.Collect();
        }

        private void RotateImage(float angle, PictureBox pictureBox)
        {
            // Create a new bitmap with the same size as the original image
            Bitmap rotatedImage = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);

            // Create a Graphics object from the new bitmap
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(pictureBox.Image.Width / 2, pictureBox.Image.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-pictureBox.Image.Width / 2, -pictureBox.Image.Height / 2);

                // Draw the original image onto the rotated image
                g.DrawImage(pictureBox.Image, new Point(0, 0));
                g.Dispose();
                System.GC.Collect();
            }

            // Set the rotated image as the PictureBox's image
            pictureBox.Image = rotatedImage;

        }

        public async void AnimatedShrink(PictureBox pictureBox, bool ShrinkDirection)
        {
            Bitmap composedImage = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);
            if (ShrinkDirection)
            {
                double[] shrinkAmounts = { 0.95, 0.95, 0.95, 0.95, 0.95, 0.95, 0.95, 0.95, 0.95 };

                foreach (float shrinkAmount in shrinkAmounts)
                {


                    ShrinkImage(shrinkAmount, pictureBox);
                    await Task.Delay(10);
                    //await Task.Delay(100 - (100 + shrinkAmount) % 36);

                }
                using (Graphics g = Graphics.FromImage(composedImage))
                {
                    g.TranslateTransform(pictureBox.Image.Width / 2, pictureBox.Image.Height / 2);
                    g.ScaleTransform(1.0f, 1.0f);
                    g.TranslateTransform(-pictureBox.Image.Width / 2, -pictureBox.Image.Height / 2);

                    // Draw the original image onto the scaled image
                    g.DrawImage(pictureBox.Image, new Point(0, 0));
                    g.Dispose();
                    System.GC.Collect();
                }
                pictureBox.Image = composedImage;
            }
            else
            {
                double[] shrinkAmounts = { 1.05, 1.05, 1.05, 1.05, 1.05, 1.05, 1.05, 1.05, 1.05 };
                foreach (float shrinkAmount in shrinkAmounts)
                {
                    ShrinkImage(shrinkAmount, pictureBox);
                    await Task.Delay(10);
                    //await Task.Delay(100 - (100 + shrinkAmount) % 36);
                }
                using (Graphics g = Graphics.FromImage(composedImage))
                {
                    g.TranslateTransform(pictureBox.Image.Width / 2, pictureBox.Image.Height / 2);
                    g.ScaleTransform(1.0f, 1.0f);
                    g.TranslateTransform(-pictureBox.Image.Width / 2, -pictureBox.Image.Height / 2);

                    // Draw the original image onto the scaled image
                    g.DrawImage(pictureBox.Image, new Point(0, 0));
                    g.Dispose();
                    System.GC.Collect();
                }
                pictureBox.Image = pictureBox.InitialImage;
            }


            System.GC.Collect();
        }

        private void ShrinkImage(float shrinkAmount, PictureBox pictureBox)
        {
            // Create a new bitmap with the same size as the original image
            Bitmap scaledImage = new Bitmap(pictureBox.Image.Width, pictureBox.Image.Height);

            // Create a Graphics object from the new bitmap
            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.TranslateTransform(pictureBox.Image.Width / 2, pictureBox.Image.Height / 2);
                g.ScaleTransform(shrinkAmount, shrinkAmount);
                g.TranslateTransform(-pictureBox.Image.Width / 2, -pictureBox.Image.Height / 2);

                // Draw the original image onto the scaled image
                g.DrawImage(pictureBox.Image, new Point(0, 0));
                g.Dispose();
                System.GC.Collect();
            }

            // Set the scaled image as the PictureBox's image
            pictureBox.Image = scaledImage;

        }
        #endregion

        #region Add User For Admin Panel Tab
        private void pictureBox6_Click_2(object sender, EventArgs e)
        {
            if (AddUser.AcikMi == false)
            {
                addusers = new AddUser();
                addusers.AdminControl(true);
                addusers.Show();
            }
            else
            {
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region Load All User For Admin Panel Tab
        private void updateBtn_Click_1(object sender, EventArgs e)
        {
            AdminGetAllUsers();
        }
        public async void AdminGetAllUsers()
        {
            changeProgressBar(0);
            flowLayoutPanel1.Controls.Clear();
            changeProgressBar(10);
            System.GC.Collect();
            changeProgressBar(20);
            if (flowLayoutPanel1.Controls.Count == 0 || userMenuBox.Image == NTP_P1.Properties.Resources.user)
            {
                changeProgressBar(30);
                dtTableName = "Personel";

                AnimatedRotate(updateBtn);
                changeProgressBar(40);
                if (Database.DGWShow(DTUrun, $"[{dtTableName}]", "ID") != null)
                {
                    changeProgressBar(50);
                    updateBtn.Enabled = false;
                    double all_count = (double)db.GetCountOfColumn(dtTableName, "ID");
                    double cur_index = 0;
                    foreach (var user in Database.GetAllUsers(dtTableName))
                    {
                        cur_index++;
                        double Process_percent = cur_index / all_count * 30 + 50;
                        changeProgressBar(Process_percent);
                        AdminPersonelView userView = new AdminPersonelView();
                        userView.Personel = Personel.FromTuple(user);
                        flowLayoutPanel1.Controls.Add(userView);
                    }
                    changeProgressBar(90);
                }
                else
                {
                    this.Dispose();
                    System.GC.Collect();
                    MessageBox.Show(Program.Output[34], Program.Output[35], MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            finishProcess();
            await Task.Delay(1000);
            updateBtn.Enabled = true;
            //updateBtn.Image = NTP_P1.Properties.Resources.update;
            
        }
        #endregion

        #region Backup Database
        private void pictureBox12_Click(object sender, EventArgs e)
        {

            UpdateDatabase();

        }

        public async static void UpdateDatabase(bool silent = false)
        {
            Program.mainPage.changeProgressBar(10);
            if (!Directory.Exists($"{Application.StartupPath}/Backup"))
            {
                // Create the "Backup" folder if it doesn't exist
                Program.mainPage.changeProgressBar(20);
                Directory.CreateDirectory($"{Application.StartupPath}/Backup");
            }
            Program.mainPage.changeProgressBar(30);
            string backupDate = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
            string databasePath = $"{Application.StartupPath}\\ProjectDatabase.accdb"; // Replace with your actual database file path
            string backupPath = $"{Application.StartupPath}\\Backup\\ProjectDatabase{backupDate}.accdb"; // Replace with your desired backup location
            string zipOutputPath = $"{Application.StartupPath}\\Backup\\DatabaseBackup{backupDate}-ENCRYPTED.encrypted"; // Replace with your desired backup location
            string zipDecrpytPath = $"{Application.StartupPath}\\Backup\\DatabaseBackup{backupDate}-DECRYPTED.accdb";
            Program.mainPage.changeProgressBar(40);

            try
            {
                // Check if the database file exists
                if (System.IO.File.Exists(databasePath))
                {
                    Program.mainPage.changeProgressBar(50);
                    if (!silent)
                    {
                        // Create the backup by copying the database file
                        System.IO.File.Copy(databasePath, backupPath, false);
                        Program.mainPage.changeProgressBar(60);
                        FileCrpyt.EncryptFileSilent(backupPath, zipOutputPath, Crypt.key);
                        await Task.Run(() =>
                        {
                            Program.mainPage.finishProcessWithAnim();
                            MessageBox.Show(Program.Output[42]);
                        });
                    }
                    else
                    {

                        System.IO.File.Copy(databasePath, backupPath, false);
                        FileCrpyt.EncryptFileSilent(backupPath, zipOutputPath, Crypt.key);
                        Program.mainPage.finishProcessWithAnim();
                    }

                }
                else
                {
                    await Task.Run(() =>
                    {
                        MessageBox.Show(Program.Output[43]);
                    });
                }
            }
            catch (Exception ex)
            {
                await Task.Run(() =>
                {
                    MessageBox.Show($"{Program.Output[44]} {ex}");
                });
            }
            Properties.Settings.Default.lastBackupTime = DateTime.Now;
            Properties.Settings.Default.Save();
            Program.mainPage.finishProcessWithAnim();
        }
        #endregion

        #region App Settings
        private void pictureBox12_Click_1(object sender, EventArgs e)
        {
            if (AppSettings.AcikMi == false)
            {
                if (groupBox1.Visible == true)
                    Menu();
                appSettings = new AppSettings();
                //appSettings.AdminControl(false);
                appSettings.getSettingsAtDatabase();
                appSettings.Show();
            }
            else
            {
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region PictureBoxs Animation
        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.BackColor = SystemColors.ControlDark;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.BackColor = Color.Transparent;
        }

        private void pictureBox10_MouseEnter(object sender, EventArgs e)
        {
            pictureBox10.BackColor = SystemColors.ControlDark;
        }

        private void pictureBox10_MouseLeave(object sender, EventArgs e)
        {
            pictureBox10.BackColor = Color.Transparent;
        }

        private void DatabaseBackup_MouseEnter(object sender, EventArgs e)
        {
            DatabaseBackup.BackColor = SystemColors.ControlDark;
        }

        private void DatabaseBackup_MouseLeave(object sender, EventArgs e)
        {
            DatabaseBackup.BackColor = Color.Transparent;
        }

        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.BackColor = SystemColors.ControlDark;
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BackColor = Color.Transparent;
        }

        private void updateBtn_MouseEnter(object sender, EventArgs e)
        {
            updateBtn.BackColor = SystemColors.ControlDark;
        }

        private void updateBtn_MouseLeave(object sender, EventArgs e)
        {
            updateBtn.BackColor = Color.Transparent;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.BackColor = SystemColors.ControlDark;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Transparent;
        }

        private void pictureBox11_MouseEnter(object sender, EventArgs e)
        {
            pictureBox11.BackColor = SystemColors.ControlDark;
        }

        private void pictureBox11_MouseLeave(object sender, EventArgs e)
        {
            pictureBox11.BackColor = Color.Transparent;
        }

        private void pictureBox12_MouseEnter(object sender, EventArgs e)
        {
            AnimatedMenu(pictureBox12, true);
        }

        private void pictureBox12_MouseLeave(object sender, EventArgs e)
        {
            AnimatedMenu(pictureBox12, false);
        }

        private void userMenuBox_MouseEnter(object sender, EventArgs e)
        {
            userMenuBox.BackColor = SystemColors.ControlDark;
        }

        private void userMenuBox_MouseLeave(object sender, EventArgs e)
        {
            userMenuBox.BackColor = Color.Transparent;
        }

        private void productMenuBox_MouseLeave(object sender, EventArgs e)
        {
            productMenuBox.BackColor = Color.Transparent;
        }

        private void productMenuBox_MouseEnter(object sender, EventArgs e)
        {
            productMenuBox.BackColor = SystemColors.ControlDark;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox13_MouseEnter(object sender, EventArgs e)
        {
            pictureBox13.Image = Properties.Resources.exit_black_enter;
        }

        private void pictureBox13_MouseLeave(object sender, EventArgs e)
        {
            pictureBox13.Image = pictureBox13.InitialImage;
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private async void root_MouseHover(object sender, EventArgs e)
        {
            hid1.Visible = true;
            await Task.Delay(3000);
            hid1.Visible = false;
        }

        private void root_MouseLeave(object sender, EventArgs e)
        {
            hid1.Visible = false;
        }

        private async void defaultUser_MouseHover(object sender, EventArgs e)
        {
            hid2.Visible = true;
            await Task.Delay(3000);
            hid2.Visible = false;
        }

        private void defaultUser_MouseLeave(object sender, EventArgs e)
        {
            hid2.Visible = false;
        }

        private async void employee_MouseHover(object sender, EventArgs e)
        {
            hid3.Visible = true;
            await Task.Delay(3000);
            hid3.Visible = false;
        }

        private void employee_MouseLeave(object sender, EventArgs e)
        {
            hid3.Visible = false;
        }

        private void MainPage_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void MainPage_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void AdminMenuBox_MouseEnter(object sender, EventArgs e)
        {
            AdminMenuBox.BackColor = SystemColors.ControlDark;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void AdminMenuBox_MouseLeave(object sender, EventArgs e)
        {
            AdminMenuBox.BackColor = Color.Transparent;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        #region Custom ProgressBar
        public void DrawBlackRectangleWithTextOptinal(double a, string text, string text1,PictureBox pictureBox)
        {
            DrawWhiteRectangle();

            // Calculate the width of the rectangle (30% of PictureBox width)
            int rectangleWidth = (int)(pictureBox.Width * a);

            // Create a black brush and a rectangle
            Brush blackBrush = new SolidBrush(Color.Black);
            Rectangle rectangle = new Rectangle(0, 0, rectangleWidth, pictureBox.Height);

            // Use the PictureBox's CreateGraphics method to get the Graphics object
            using (Graphics g = pictureBox.CreateGraphics())
            {
                // Fill the rectangle with the black brush
                g.FillRectangle(blackBrush, rectangle);

                // Draw text in the middle of the PictureBox
                Font font = new Font("JetBrains Mono ExtraBold", 19);
                Font font1 = new Font("JetBrains Mono", 19);

                // Choose the text color based on the rectangle color
                Brush textBrush = (rectangleWidth < pictureBox.Width / 2) ? Brushes.Black : Brushes.White;

                // Draw the text in the middle
                SizeF textSize = g.MeasureString(text, font);
                PointF location = new PointF((pictureBox.Width - textSize.Width) / 2, (pictureBox.Height - textSize.Height) / 2);
                g.DrawString(text, font, textBrush, location);

                textBrush = (rectangleWidth < pictureBox.Width - 300) ? Brushes.Black : Brushes.White;
                float startX = pictureBox.Width - rectangleWidth + 10; // Adjust 10 pixels for padding

                // Draw the text from right to left
                textSize = g.MeasureString(text1, font1);
                location = new PointF(pictureBox.Width - 300, (pictureBox.Height - textSize.Height) / 2);
                g.DrawString(text1, font1, textBrush, location);
            }
        }

        private void DrawWhiteRectangle()
        {
            // Use the PictureBox's CreateGraphics method to get the Graphics object
            using (Graphics g = pictureBox3.CreateGraphics())
            {
                // Create a white brush
                Brush whiteBrush = Brushes.White;

                // Create a rectangle that covers the full size of the PictureBox
                Rectangle rectangle = new Rectangle(0, 0, pictureBox3.Width, pictureBox3.Height);

                // Fill the rectangle with the white brush
                g.FillRectangle(whiteBrush, rectangle);
            }
        }

        public void DrawBlackRectangleWithTextBitmap(double a, string text, string text1, PictureBox pictureBox)
        {
            cur_percent = a;
            // Create a bitmap to draw on
            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);

            // Use the bitmap's CreateGraphics method to get the Graphics object
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Calculate the width of the rectangle (30% of PictureBox width)
                int rectangleWidth = (int)(pictureBox.Width * a);

                // Create a black brush and a rectangle
                Brush blackBrush = new SolidBrush(Color.Black);
                Rectangle rectangle = new Rectangle(0, 0, rectangleWidth, pictureBox.Height);

                // Fill the rectangle with the black brush
                g.FillRectangle(blackBrush, rectangle);

                // Draw text in the middle of the PictureBox
                Font font = new Font("JetBrains Mono ExtraBold", 19);
                Font font1 = new Font("JetBrains Mono", 19);

                // Choose the text color based on the rectangle color
                Brush textBrush = (rectangleWidth < pictureBox.Width / 2) ? Brushes.Black : Brushes.White;

                // Draw the text in the middle
                SizeF textSize = g.MeasureString(text, font);
                PointF location = new PointF((pictureBox.Width - textSize.Width) / 2, (pictureBox.Height - textSize.Height) / 2);
                g.DrawString(text, font, textBrush, location);

                textBrush = (rectangleWidth < pictureBox.Width - 300) ? Brushes.Black : Brushes.White;
                float startX = pictureBox.Width - rectangleWidth + 10; // Adjust 10 pixels for padding

                // Draw the text from right to left
                textSize = g.MeasureString(text1, font1);
                location = new PointF(pictureBox.Width - 300, (pictureBox.Height - textSize.Height) / 2);
                g.DrawString(text1, font1, textBrush, location);
            }

            // Display the bitmap on the PictureBox
            pictureBox.Image = bitmap;
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            double totalIterations = 0;
            bool check1, check2;
            double product_amount = 0;
            double daily_data_amount_For_each_product = 0;
            string userInput1 = Interaction.InputBox("Oluşturmak İstediğiniz Ürün Sayısı:", "Ürün Sayısı");
            if (double.TryParse(userInput1, out double result1))
            {
                check1 = true;
            }
            else
            {
                check1 = false;
            }
            string userInput2 = Interaction.InputBox("Oluşturmak İstediğiniz Ürün Başına Günlük Veri Sayısı:", "Günlük Veri sayısı");
            if (double.TryParse(userInput2, out double result2))
            {
                check2 = true;
            }
            else
            {
                check2 = false;
            }

            if(check1 && check2)
            {
                product_amount = int.Parse(userInput1);
                daily_data_amount_For_each_product = int.Parse(userInput2);
            }
            else
            {
                product_amount = 0;
                daily_data_amount_For_each_product = 0;
            }
            

            await Task.Run(() =>
            {
                for (int i = 0; i < product_amount; i++)
                {
                    time = DateTime.Today;
                    time = time.AddDays(i);
                    int length = 10; // You can change the length as needed

                    string val1 = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", length)
                                                      .Select(s => s[random.Next(s.Length)])
                                                      .ToArray());
                    string val2 = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz", 1)
                                                      .Select(s => s[random.Next(s.Length)])
                                                      .ToArray());
                    string val3 = $"{random.NextDouble() * 1000}";


                    string[] columns = { "UrunAdi", "UrunGrubu", "UrunSatisFiyati", "EklenmeVeyaGuncellenmeTarihi" };
                    string[] values = { val1, val2, val3, String.Format("{0:dd/MM/yyyy}", time) };
                    Database.Connect.Open();
                    Database.InsertSilent("Urun", columns, values);
                    Database.Connect.Close();
                    Database.TableCreateSilent(val1);
                    for (int j = 0; j < daily_data_amount_For_each_product; j++)
                    {
                        string val4 = $"{random.NextDouble() * 1000}";
                        string val5 = $"{random.NextDouble() * 1000}";
                        string val6 = $"{random.NextDouble() * 1000}";

                        string[] col = { "SatışMiktarı", "SatışFiyatı", "AlışFiyatı", "Tarih", "StokMiktari" };
                        string[] val = { val4, val3, val5, String.Format("{0:dd/MM/yyyy}", time), val6 };
                        time = time.AddDays(1);
                        Database.Connect.Open();
                        Database.InsertSilent(val1, col, val);
                        Database.Connect.Close();
                        
                        if (j == daily_data_amount_For_each_product-1)
                        {
                            string[] changes = {
                            $"StokMiktari='{val6}'"
                            };

                            Database.Connect.Open();
                            Database.CommonUpdateSilent("Urun", changes, $"{28 + i}");
                            Database.Connect.Close();
                            
                        }
                        if (this.IsHandleCreated)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                button1.Text = $"{(double)(totalIterations / (product_amount * daily_data_amount_For_each_product) * 100)}%";
                                changeProgressBar((double)(totalIterations / (product_amount * daily_data_amount_For_each_product) * 100));
                            });
                        }
                        totalIterations++;
                    }
                }
            });
            if (check1 && check2)
            {
                button1.Text = $"{(double)(totalIterations / (product_amount * daily_data_amount_For_each_product) * 100)}%";
                MessageBox.Show($"Bitti!");
            }
            else
            {
                button1.Text = $"Ürün Ekle";
                MessageBox.Show($"Geçersiz İşlem");
            }
            if (ProfitShow.AcikMi)
            {
                profitShow.Update_Group();
            }
            
        }

        public void finishProcess()
        {
            cur_percent = 100;
            DrawBlackRectangleWithTextOptinal(100, "ASYS", DateTime.Now.ToString("dd.MM.yyyy - HH:mm"),pictureBox3);
        }

        private void Urun_Group_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeProgressBar(10);
            Program.CursorChange();
            changeProgressBar(20);
            flowLayoutPanel1.Controls.Clear();
            changeProgressBar(30);
            if (Database.DGWShow(DTUrun, $"Urun", "ID") != null)
            {
                changeProgressBar(40); //GetCountOfColumn
                double all_product_count = (double)db.GetCountOfColumn(dtTableName, "ID", Convert.ToString(Urun_Group.SelectedItem));
                double cur_product = 0;
                changeProgressBar(50);
                foreach (var product in Database.GetProductWithGroup(dtTableName, Convert.ToString(Urun_Group.SelectedItem)))
                {
                    cur_product++;
                    double Process_percent = cur_product / all_product_count * 30 + 50;
                    changeProgressBar(Process_percent);
                    ProductView productView = new ProductView();
                    productView.BProduct = BProduct.FromTuple(product);
                    flowLayoutPanel1.Controls.Add(productView);
                }
                changeProgressBar(90);
            }
            else
            {
                this.Dispose();
                System.GC.Collect();
                MessageBox.Show(Program.Output[34], Program.Output[35], MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finishProcessWithAnim();
        }
  
        public void GetJustAccessedProducts(string group)
        {
            changeProgressBar(10);
            Program.CursorChange();
            changeProgressBar(20);
            flowLayoutPanel1.Controls.Clear();
            changeProgressBar(30);
            if (Database.DGWShow(DTUrun, $"Urun", "ID") != null)
            {
                changeProgressBar(40); //GetCountOfColumn
                double all_product_count = (double)db.GetCountOfColumn($"Urun", "ID", Convert.ToString(group));
                double cur_product = 0;
                changeProgressBar(50);
                foreach (var product in Database.GetProductWithGroup($"Urun", Convert.ToString(group)))
                {
                    cur_product++;
                    double Process_percent = cur_product / all_product_count * 30 + 50;
                    changeProgressBar(Process_percent);
                    ProductView productView = new ProductView();
                    ProductView.isRoot = isRoot;
                    ProductView.isEmployee = isEmployee;
                    ProductView.isDefaultUser = isDefaultUser;
                    productView.BProduct = BProduct.FromTuple(product);
                    flowLayoutPanel1.Controls.Add(productView);
                }
                changeProgressBar(90);
            }
            else
            {
                this.Dispose();
                System.GC.Collect();
                MessageBox.Show(Program.Output[34], Program.Output[35], MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finishProcessWithAnim();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            string[] Products = db.GetValuesOfColumn("Urun", "UrunAdi");
            double toplamKar = 0;
            foreach (string product in Products)
            {
                toplamKar += (db.GetAverageOfColumn(product, "SatışFiyatı") - db.GetAverageOfColumn(product, "AlışFiyatı")) * db.GetAverageOfColumn(product, "SatışMiktarı");
            }
            string formattedValue = toplamKar.ToString("C", new System.Globalization.CultureInfo("tr-TR"));


            MessageBox.Show($"{formattedValue}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] groups = db.GetValuesOfColumnDiff("Urun", "UrunGrubu");
            double toplamKar = 0;
            foreach (string group in groups)
            {
                string[] Products = db.GetValuesOfColumnWithGroup("Urun","UrunAdi", $"{group}");
                foreach (string product in Products)
                {
                    toplamKar = (db.GetAverageOfColumn(product, "SatışFiyatı") - db.GetAverageOfColumn(product, "AlışFiyatı")) * db.GetAverageOfColumn(product, "SatışMiktarı");
                }
                string formattedValue = toplamKar.ToString("C", new System.Globalization.CultureInfo("tr-TR"));
                MessageBox.Show($"{group}:{formattedValue}");
            }
        }

        private void Urun_Group_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MainPage.isRoot)
            {
                GetAllProduct();
            }
            else if (MainPage.isEmployee)
            {
                group = db.GetGroupWithUsername(LoginPage.kullaniciAdi);
                GetJustAccessedProducts(group);
            }
        }

        private async void pictureBox14_MouseHover(object sender, EventArgs e)
        {
            usernameLabel.Visible = true;
            await Task.Delay(3000);
            usernameLabel.Visible = false;
        }

        private void pictureBox14_MouseLeave(object sender, EventArgs e)
        {
            usernameLabel.Visible = false;
        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            Program.CursorChange();
            if (ProfitShow.AcikMi == false)
            {
                if (isProfitOpen)
                {
                    productCol.Visible = false;
                    if (Program.SettingsValues[0] == "0")
                        AnimatedShrink(Profit, false);
                    else
                        Profit.Image = Profit.InitialImage;

                    label6.Visible = false;
                    isProductMenuOpen = false;
                    showcase.Visible = false;
                    Urun_Group.Visible = false;
                    pictureBox14.Visible = false;
                    isProfitOpen = false;
                    label6.Text = "";
                    flowLayoutPanel1.Controls.Clear();
                    System.GC.Collect();
                }
                else
                {

                    if (Program.SettingsValues[0] == "0")
                    {
                        AnimatedShrink(Profit, true);
                    }
                    else
                    {
                        productMenuBox.Image = NTP_P1.Properties.Resources.Xo1; ;
                    }
                    if (isAdminMenuOpen)
                    {
                        if (Program.SettingsValues[0] == "0")
                        {
                            AnimatedShrink(AdminMenuBox, false);
                        }
                        else
                        {
                            AdminMenuBox.Image = AdminMenuBox.InitialImage;
                        }
                    }
                    if (isUserMenuOpen)
                    {
                        if (Program.SettingsValues[0] == "0")
                        {
                            AnimatedShrink(userMenuBox, false);
                        }
                        else
                        {
                            userMenuBox.Image = userMenuBox.InitialImage;
                        }
                    }
                    if (isProductMenuOpen)
                    {
                        if (Program.SettingsValues[0] == "0")
                        {
                            AnimatedShrink(productMenuBox, false);
                        }
                        else
                        {
                            productMenuBox.Image = productMenuBox.InitialImage;
                        }
                    }

                    if (MainPage.isRoot)
                    {
                        Program.mainPage.GetAllProduct();
                    }
                    else if (MainPage.isEmployee)
                    {
                        group = db.GetGroupWithUsername(LoginPage.kullaniciAdi);
                        Program.mainPage.GetJustAccessedProducts(group);
                    }
                    isUserMenuOpen = false;
                    isAdminMenuOpen = false;
                    userCol.Visible = false;
                    isProductMenuOpen = false;
                    AdminUserCol.Visible = false;
                    DatabaseBackup.Visible = false;
                    label6.Visible = false;
                    showcase.Visible = false;
                    isProfitOpen = true;
                    Urun_Group.Visible = false;
                    pictureBox14.Visible = false;
                    productCol.Visible = false;

                    #region Hide Unwanted Object
                    openProfitForm();


                    #endregion
                    flowLayoutPanel1.Controls.Clear();
                    System.GC.Collect();
                }
            }
            else
            {
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        void GetAdminMail()
        {
            string[] mails = db.GetMailFromAdmins();
            string mailsStr = "";
            foreach (string mail in mails)
            {
                mailsStr += $"{Program.DecryptIt(mail)}\n";
            }
            MessageBox.Show(mailsStr);
        }

        public void openProfitForm()
        {
            if (ProfitShow.AcikMi == false)
            {
                profitShow = new ProfitShow();
                profitShow.Show();
            }
            else
            {
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void finishProcessWithAnim()
        {
            for(int i = 0; i < 100; i++)
            {
                if (cur_percent < 1)
                {
                    cur_percent = cur_percent + 0.01;
                    DrawBlackRectangleWithTextOptinal(cur_percent, "ASYS", DateTime.Now.ToString("dd.MM.yyyy - HH:mm"), pictureBox3);
                    //await Task.Delay(10);
                }
            }
            
        }


        public void changeProgressBar(double percent)
        {
            percent = percent / 100;
            cur_percent = percent;
            DrawBlackRectangleWithTextOptinal(percent, "ASYS", DateTime.Now.ToString("dd.MM.yyyy - HH:mm"), pictureBox3);
        }
        #endregion
    }
}