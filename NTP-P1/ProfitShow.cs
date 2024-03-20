using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NTP_P1
{
    public partial class ProfitShow : Form
    {
        Database db = new Database();
        string[] groups;
        public static bool AcikMi = false;
        public static SeriesChartType ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
        public System.Windows.Forms.DataVisualization.Charting.Series KarMiktariSeries;
        private bool mouseDown;
        private Point lastLocation;

        public ProfitShow()
        {
            InitializeComponent();
        }

        private void ProfitShow_Load(object sender, EventArgs e)
        {
            AcikMi = true;
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentCurrentColor;
            Update_Group();
            ToplamKar();
        }

        public void Update_Group()
        {
            ProductGroups.Items.Clear();
            groups = db.GetValuesOfColumnJustDiffrentOnes("Urun", "UrunGrubu");

            foreach (string group in groups.OrderBy(group => group))
            {
                ProductGroups.Items.Add(group);
            }
        }

        public void ToplamKar()
        {
            Program.mainPage.changeProgressBar(10);
            chart1.Series.Clear();
            string[] Products = db.GetValuesOfColumn("Urun", "UrunAdi");
            double toplamKar = 0;
            int registered_data_count = 0;
            Program.mainPage.changeProgressBar(20);
            double all_product_count = Products.Length;
            double cur_product = 0;
            foreach (string product in Products)
            {
                cur_product++;
                double Process_percent = cur_product / all_product_count * 20 + 20;
                Program.mainPage.changeProgressBar(Process_percent);
                if (registered_data_count < db.GetCountOfColumn(product, "SatışFiyatı"))
                {
                    registered_data_count = db.GetCountOfColumn(product, "SatışFiyatı");
                }
                toplamKar += (db.GetAverageOfColumn(product, "SatışFiyatı") - db.GetAverageOfColumn(product, "AlışFiyatı")) * db.GetAverageOfColumn(product, "SatışMiktarı");
            }
            string formattedValue = toplamKar.ToString("C", new System.Globalization.CultureInfo("tr-TR"));

            label12.Text = formattedValue;
            Program.mainPage.changeProgressBar(50);

            double[] values = new double[registered_data_count];
            //MessageBox.Show($"{registered_data_count}");

            all_product_count = registered_data_count;
            cur_product = 0;
            for (int i = 1; i < registered_data_count; i++)
            {
                cur_product++;
                double Process_percent = cur_product / all_product_count * 20 + 50;
                Program.mainPage.changeProgressBar(Process_percent);
                toplamKar = 0;
                foreach (string product in Products)
                {
                    //MessageBox.Show($"{i}");
                    toplamKar += (db.GetAverageOfColumn(product, "SatışFiyatı", $"{i}") - db.GetAverageOfColumn(product, "AlışFiyatı", $"{i}")) * db.GetAverageOfColumn(product, "SatışMiktarı", $"{i}");
                }
                values[i] = toplamKar;
            }
            Program.mainPage.changeProgressBar(80);
            KarMiktariSeries = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = "Kar Miktarı",
            };

            chart1.Series.Add(KarMiktariSeries);
            KarMiktariSeries.ChartType = ChartType;
            KarMiktariSeries.BorderWidth = 5;
            Program.mainPage.changeProgressBar(90);
            foreach (var value in values)
            {
                KarMiktariSeries.Points.AddY(value);
            }
            trackBar1.Maximum = values.Length;
            trackBar2.Maximum = values.Length;


            trackBar1.Value = 0;
            trackBar2.Value = trackBar2.Maximum;

            label35.Text = $"{trackBar1.Value}";
            label37.Text = $"{trackBar2.Value}";

            chart1.ChartAreas[0].AxisX.Minimum = trackBar1.Value;

            chart1.ChartAreas[0].AxisX.Maximum = trackBar2.Value;

            Program.mainPage.finishProcessWithAnim();
        }

        public void GetResponsiblePersonelName(string group)
        {
            Database.Connect.Open();
            string[] names = new string[2];
            names = db.GetNameAndSurnameWithGroup(group);
            if (names[0].Length > 0 && names[1].Length > 0)
            {
                SorumluPersonel.Text = $"{Program.DecryptIt(names[0])} {Program.DecryptIt(names[1])}";
            }
            else
            {
                SorumluPersonel.Text = $"None";
            }
            Database.Connect.Close();

        }

        private void ProductGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.mainPage.changeProgressBar(5);
            if (ProductGroups.SelectedIndex != -1)
            {
                chart1.Series.Clear();
                string group = $"{ProductGroups.SelectedItem}";
                double toplamKar = 0;
                int registered_data_count = 0;

                string[] Products = db.GetValuesOfColumnWithGroup("Urun", "UrunAdi", $"{group}");
                Program.mainPage.changeProgressBar(10);
                double all_product_count = Products.Length;
                double cur_product = 0;
                foreach (string product in Products)
                {
                    cur_product++;
                    double Process_percent = cur_product / all_product_count * 5 + 10;
                    Program.mainPage.changeProgressBar(Process_percent);
                    if (registered_data_count < db.GetCountOfColumn(product, "SatışFiyatı"))
                    {
                        registered_data_count = db.GetCountOfColumn(product, "SatışFiyatı");
                    }
                    toplamKar += (db.GetAverageOfColumn(product, "SatışFiyatı") - db.GetAverageOfColumn(product, "AlışFiyatı")) * db.GetAverageOfColumn(product, "SatışMiktarı");
                }
                string formattedValue = toplamKar.ToString("C", new System.Globalization.CultureInfo("tr-TR"));

                GetResponsiblePersonelName(group);

                profitAmount.Text = formattedValue;

                int wanted_count = 7;
                toplamKar = 0;
                all_product_count = Products.Length;
                cur_product = 0;
                foreach (string product in Products)
                {
                    cur_product++;
                    double Process_percent = cur_product / all_product_count * 5 + 15;
                    Program.mainPage.changeProgressBar(Process_percent);
                    toplamKar += (db.GetAverageOfColumn(product, "SatışFiyatı", $"{wanted_count}") - db.GetAverageOfColumn(product, "AlışFiyatı", $"{wanted_count}")) * db.GetAverageOfColumn(product, "SatışMiktarı", $"{wanted_count}");
                }
                formattedValue = toplamKar.ToString("C", new System.Globalization.CultureInfo("tr-TR"));

                label6.Text = formattedValue;

                wanted_count = 15;
                toplamKar = 0;
                all_product_count = Products.Length;
                cur_product =0;
                foreach (string product in Products)
                {
                    cur_product++;
                    double Process_percent = cur_product / all_product_count * 5 + 20;
                    Program.mainPage.changeProgressBar(Process_percent);
                    toplamKar += (db.GetAverageOfColumn(product, "SatışFiyatı", $"{wanted_count}") - db.GetAverageOfColumn(product, "AlışFiyatı", $"{wanted_count}")) * db.GetAverageOfColumn(product, "SatışMiktarı", $"{wanted_count}");
                }
                formattedValue = toplamKar.ToString("C", new System.Globalization.CultureInfo("tr-TR"));

                label3.Text = formattedValue;


                wanted_count = 30;
                toplamKar = 0;
                all_product_count = Products.Length;
                cur_product = 0;
                foreach (string product in Products)
                {
                    cur_product++;
                    double Process_percent = cur_product / all_product_count * 5 + 25;
                    Program.mainPage.changeProgressBar(Process_percent);
                    toplamKar += (db.GetAverageOfColumn(product, "SatışFiyatı", $"{wanted_count}") - db.GetAverageOfColumn(product, "AlışFiyatı", $"{wanted_count}")) * db.GetAverageOfColumn(product, "SatışMiktarı", $"{wanted_count}");
                }
                formattedValue = toplamKar.ToString("C", new System.Globalization.CultureInfo("tr-TR"));

                label8.Text = formattedValue;


                wanted_count = 365;
                toplamKar = 0;
                all_product_count = Products.Length;
                cur_product = 0;
                foreach (string product in Products)
                {
                    cur_product++;
                    double Process_percent = cur_product / all_product_count * 5 + 30;
                    Program.mainPage.changeProgressBar(Process_percent);
                    toplamKar += (db.GetAverageOfColumn(product, "SatışFiyatı", $"{wanted_count}") - db.GetAverageOfColumn(product, "AlışFiyatı", $"{wanted_count}")) * db.GetAverageOfColumn(product, "SatışMiktarı", $"{wanted_count}");
                }
                formattedValue = toplamKar.ToString("C", new System.Globalization.CultureInfo("tr-TR"));

                label10.Text = formattedValue;

                double[] values = new double[registered_data_count];
                //MessageBox.Show($"{registered_data_count}");
                all_product_count = registered_data_count;
                cur_product = 0;
                for (int i = 1; i < registered_data_count; i++)
                {
                    cur_product++;
                    double Process_percent = cur_product / all_product_count * 5 + 35;
                    Program.mainPage.changeProgressBar(Process_percent);
                    toplamKar = 0;
                    foreach (string product in Products)
                    {
                        //MessageBox.Show($"{i}");
                        toplamKar += (db.GetAverageOfColumn(product, "SatışFiyatı", $"{i}") - db.GetAverageOfColumn(product, "AlışFiyatı", $"{i}")) * db.GetAverageOfColumn(product, "SatışMiktarı", $"{i}");
                    }
                    values[i] = toplamKar;
                }

                KarMiktariSeries = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Kar Miktarı",
                };

                chart1.Series.Add(KarMiktariSeries);
                KarMiktariSeries.ChartType = ChartType;
                KarMiktariSeries.BorderWidth = 5;
                all_product_count = values.Length;
                cur_product = 0;
                foreach (var value in values)
                {
                    cur_product++;
                    double Process_percent = cur_product / all_product_count * 50 + 40;
                    Program.mainPage.changeProgressBar(Process_percent);
                    KarMiktariSeries.Points.AddY(value);
                }
                trackBar1.Maximum = values.Length;
                trackBar2.Maximum = values.Length;


                trackBar1.Value = 0;
                trackBar2.Value = trackBar2.Maximum;

                label35.Text = $"{trackBar1.Value}";
                label37.Text = $"{trackBar2.Value}";

                chart1.ChartAreas[0].AxisX.Minimum = trackBar1.Value;

                chart1.ChartAreas[0].AxisX.Maximum = trackBar2.Value;
                Program.mainPage.changeProgressBar(90);
                //MessageBox.Show($"{group}:{formattedValue}");
            }
            else
            {
                SorumluPersonel.Text = "None";
                profitAmount.Text = "None";
                label6.Text = "None";
                label3.Text = "None";
                label8.Text = "None";
                label10.Text = "None";
            }
            Program.mainPage.finishProcessWithAnim();
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            ProductGroups.SelectedIndex = -1;
            AnimatedRotate(updateBtn);
            ToplamKar();
        }


        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Dispose();
            System.GC.Collect();
            AcikMi = false;
            if (MainPage.isProfitOpen)
            {
                if (Program.SettingsValues[0] == "0")
                    Program.mainPage.AnimatedShrink(Program.mainPage.Profit, false);
                else
                    Program.mainPage.Profit.Image = Program.mainPage.Profit.InitialImage;
                MainPage.isProfitOpen = false;
            }
        }

        private void ProfitShow_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            System.GC.Collect();
            AcikMi = false;
            if (MainPage.isProfitOpen)
            {
                if (Program.SettingsValues[0] == "0")
                    Program.mainPage.AnimatedShrink(Program.mainPage.Profit, false);
                else
                    Program.mainPage.Profit.Image = Program.mainPage.Profit.InitialImage;
                MainPage.isProfitOpen = false;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label35.Text = $"{trackBar1.Value}";

            chart1.ChartAreas[0].AxisX.Minimum = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label37.Text = $"{trackBar2.Value}";

            chart1.ChartAreas[0].AxisX.Maximum = trackBar2.Value;
        }

        private void ProfitShow_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void ProfitShow_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void ProfitShow_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.exit_darken;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Image = pictureBox7.InitialImage;
        }

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
    }
}
