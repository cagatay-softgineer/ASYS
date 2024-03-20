
#region Packages
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace NTP_P1
{
    public partial class DataGridShow : Form
    {
        #region Variable Definition
        DataTable DTUrun = new DataTable();
        public static string dtTableName;
        public static bool AcikMi = false;
        private bool mouseDown;
        private Point lastLocation;
        public DatabaseView databaseView = new DatabaseView();
        public static AddDailyProductData addDailyProductData = new AddDailyProductData();
        public Product product;
        public bool searchControl = false;
        public bool isCompute = false;
        Database db = new Database();
        #endregion

        #region Form Implementation
        public DataGridShow()
        {
            InitializeComponent();
        }
        #endregion

        #region Close The Form
        private void DataGridShow_FormClosed(object sender, FormClosedEventArgs e)
        {
            AcikMi = false;
            this.Dispose();
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
            System.GC.Collect();
            ProductView.productAnalysis.AnimatedMenu(ProductView.productAnalysis.InItTable, true);
        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            AcikMi = false;
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
            ProductView.productAnalysis.AnimatedMenu(ProductView.productAnalysis.InItTable, true);
            this.Dispose();
            System.GC.Collect();
        }
        #endregion

        #region Form Load Processes
        private void DataGridShow_Load(object sender, EventArgs e)
        {
            GetAll();
        }
        #endregion

        #region Load All Daily Product Datas
        public async void GetAll()
        {
            Program.mainPage.changeProgressBar(10);
            AcikMi = true;
            Program.CursorChange();
            AnimatedRotate(pictureBox9);
            Program.mainPage.changeProgressBar(20);
            System.GC.Collect();
            Program.mainPage.changeProgressBar(30);
            Program.SetLanguage(Program.LanguagePack);
            ProductName.Text = dtTableName;
            flowLayoutPanel1.Controls.Clear();
            Program.mainPage.changeProgressBar(40);
            if (!isCompute)
            {

                if (Database.DGWShow(DTUrun, $"[{dtTableName}]", "ID") != null)
                {
                    Program.mainPage.changeProgressBar(50);
                    isCompute = true;
                    pictureBox9.Enabled = false;
                    double all_product_count = (double)db.GetCountOfColumn(dtTableName, "ID");
                    double cur_product = 0;
                    foreach (var product in Database.RetrieveProductsFromDatabase(dtTableName))
                    {
                        cur_product++;
                        double Process_percent = cur_product / all_product_count * 30 + 50;
                        Program.mainPage.changeProgressBar(Process_percent);
                        DatabaseView databaseView = new DatabaseView();
                        databaseView.Product = Product.FromTuple(product);
                        flowLayoutPanel1.Controls.Add(databaseView);
                        System.GC.Collect();
                    }
                    Program.mainPage.changeProgressBar(90);
                }
                else
                {
                    this.Dispose();
                    System.GC.Collect();
                    MessageBox.Show(Program.Output[34], Program.Output[35], MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            Program.mainPage.finishProcessWithAnim();
            await Task.Delay(1000);
            isCompute = false;
            pictureBox9.Enabled = true;
        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
            System.GC.Collect();
            if (!isCompute)
            {
                GetAll();
            }
            else
            {
                MessageBox.Show($"{Program.Output[69]}");
            }

            System.GC.Collect();
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async void UpdateAfterProcess()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            this.GetAll();
        }
        #endregion

        #region Load Daily Product Data From ID
        private void SearchByID()
        {
            Program.mainPage.changeProgressBar(10);
            Program.CursorChange();
            Program.mainPage.changeProgressBar(20);
            if (searchControl)
            {
                Program.mainPage.changeProgressBar(30);
                if (searchBar.Text.Length == 0)
                {
                    //Do Nothing
                }
                else
                {
                    Program.mainPage.changeProgressBar(40);
                    flowLayoutPanel1.Controls.Clear();
                    if (IsNumeric(searchBar.Text))
                    {
                        if (Database.DGWShow(DTUrun, $"[{dtTableName}]", "ID") != null)
                        {
                            Program.mainPage.changeProgressBar(50);
                            double all_product_count = (double)db.GetCountOfColumn(dtTableName, "ID");
                            double cur_product = 0;
                            foreach (var product in Database.Search(dtTableName, Convert.ToInt32(searchBar.Text)))
                            {
                                cur_product++;
                                double Process_percent = cur_product / all_product_count * 30 + 50;
                                Program.mainPage.changeProgressBar(Process_percent);
                                DatabaseView databaseView = new DatabaseView();
                                databaseView.Product = Product.FromTuple(product);
                                flowLayoutPanel1.Controls.Add(databaseView);
                            }
                            Program.mainPage.changeProgressBar(90);
                        }
                        else
                        {
                            this.Dispose();
                            System.GC.Collect();
                            MessageBox.Show(Program.Output[34], Program.Output[35], MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            Program.mainPage.finishProcessWithAnim();
        }
        private void searchBar_TextChanged(object sender, EventArgs e)
        {
            SearchByID();
        }
        private void searchBar_Leave(object sender, EventArgs e)
        {
            searchControl = false;
            searchBar.Text = Program.Output[68];
        }

        private void searchBar_MouseClick(object sender, MouseEventArgs e)
        {
            searchControl = true;
            searchBar.Text = "";
        }
        private void searchBar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
            this.Focus();
            this.GetAll();
        }
        #endregion

        #region Load Daily Product Data From Date
        private void SearchByDate()
        {
            Program.mainPage.changeProgressBar(10);
            Program.CursorChange();
            Program.mainPage.changeProgressBar(20);
            if (searchControl)
            {
                Program.mainPage.changeProgressBar(30);
                flowLayoutPanel1.Controls.Clear();
                Program.mainPage.changeProgressBar(40);
                if (Database.DGWShow(DTUrun, $"[{dtTableName}]", "ID") != null)
                {
                    Program.mainPage.changeProgressBar(50);
                    double all_product_count = (double)db.GetCountOfColumn(dtTableName, "ID", lowerDate.Value, upperDate.Value);
                    double cur_product = 0;
                    foreach (var product in Database.Search(dtTableName, lowerDate.Value, upperDate.Value))
                    {
                        cur_product++;
                        double Process_percent = cur_product / all_product_count * 30 + 50;
                        Program.mainPage.changeProgressBar(Process_percent);
                        DatabaseView databaseView = new DatabaseView();
                        databaseView.Product = Product.FromTuple(product);
                        flowLayoutPanel1.Controls.Add(databaseView);
                    }
                    Program.mainPage.changeProgressBar(90);
                }
                else
                {
                    this.Dispose();
                    System.GC.Collect();
                    MessageBox.Show(Program.Output[34], Program.Output[35], MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            Program.mainPage.finishProcessWithAnim();
        }
        private void lowerDate_ValueChanged(object sender, EventArgs e)
        {
            searchControl = true;
            SearchByDate();
            searchControl = false;
            this.ActiveControl = null;
        }
        #endregion

        #region Move The Form
        private void DataGridShow_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;

        }

        private void DataGridShow_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void DataGridShow_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion

        #region Check The Numericity Of The Input
        private bool IsNumeric(string text)
        {
            return int.TryParse(text, out _);
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

            foreach (int rotateAngel in rotateAngels)
            {
                RotateImage(-rotateAngel, pictureBox);
                await Task.Delay(100 - (100 + rotateAngel) % 36);
            }
            pictureBox.Image = pictureBox.InitialImage;
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

        private async void AnimatedShrink(PictureBox pictureBox, bool ShrinkDirection)
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

        #region Open Add Daily Product Data Form
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            if (AddDailyProductData.AcikMi == false)
            {
                addDailyProductData = new AddDailyProductData();
                addDailyProductData.Show();
            }
            else
            {
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region PictureBox Animation
        private void pictureBox9_MouseEnter(object sender, EventArgs e)
        {
            pictureBox9.BackColor = SystemColors.ControlDark;
        }

        private void pictureBox9_MouseLeave(object sender, EventArgs e)
        {
            pictureBox9.BackColor = SystemColors.ControlLight;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.BackColor = SystemColors.ControlDark;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackColor = SystemColors.ControlLight;
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
    }
}
