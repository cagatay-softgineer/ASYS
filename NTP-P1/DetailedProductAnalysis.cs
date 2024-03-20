
#region Packages
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
#endregion

namespace NTP_P1
{
    public partial class DetailedProductAnalysis : Form
    {
        #region Variable Definition
        public static bool AcikMi = false;
        public static double KarOranı, ortAlis, ortSatis, ortMiktar;
        Database db = new Database();
        private bool mouseDown;
        private Point lastLocation;
        public static string dtTableName;
        public static string[] values, eachDate;
        public static double[] satis, alis, miktar;
        public static DataGridShow DataGridShowInstance;
        public static SeriesChartType ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
        public System.Windows.Forms.DataVisualization.Charting.Series satisFiyatiSeries, alisFiyatiSeries, satisMiktariSeries, stokMiktariSeries;
        #endregion

        #region Form Implementation
        public DetailedProductAnalysis()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Load Processes
        private void DetailedProductAnalysis_Load(object sender, EventArgs e)
        {
            AcikMi = true;
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentCurrentColor;
            Program.SetLanguage(Program.LanguagePack);
            label35.Text = $"{trackBar1.Value}";
            label37.Text = $"{trackBar2.Value}";
            AnimateGraph();
        }
        #endregion


        async void AnimateGraph()
        {
            try
            {
                string urunAdi = Program.currentAnalysisData[1];
                int max_count = db.GetCountOfColumn(urunAdi, "StokMiktari");
                for (int i = 0; i < max_count / 2; i++)
                {
                    if (AcikMi)
                    {
                        int value1 = i;
                        int value2 = max_count - i;
                        label35.Text = $"{trackBar1.Value = value1}";
                        chart1.ChartAreas[0].AxisX.Minimum = value1;
                        label37.Text = $"{trackBar2.Value = value2}";
                        chart1.ChartAreas[0].AxisX.Maximum = value2;
                        await Task.Delay(200 / max_count*2);
                    }
                }
                for (int i = max_count / 2; i < max_count; i++)
                {
                    if (AcikMi)
                    {
                        int value1 = max_count - i - 1;
                        int value2 = i + 1;
                        label35.Text = $"{trackBar1.Value = value1}";
                        chart1.ChartAreas[0].AxisX.Minimum = value1;
                        label37.Text = $"{trackBar2.Value = value2}";
                        chart1.ChartAreas[0].AxisX.Maximum = value2;
                        await Task.Delay(200 / max_count*2);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }


        #region Check Responsible Personel
        public void GetResponsiblePersonelName(string group)
        {
            if (MainPage.isRoot)
            {
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
                SorumluPersonelTable.Visible = true;
            }
            else
            {
                SorumluPersonelTable.Visible = false;
            }
        }
        #endregion

        #region Close The Form
        private void DetailedProductAnalysis_FormClosed(object sender, FormClosedEventArgs e)
        {
            AcikMi = false;
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
            this.Dispose();
            System.GC.Collect();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
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
            AcikMi = false;
            this.Dispose();
            System.GC.Collect();
        }
        #endregion

        #region Load Current Product's Data
        public void getDataFromSelectedProduct(string ID, string urunAdi, string urunGrubu, string urunSatisFiyati, string tarih,string stokMiktari)
        {
            Program.currentAnalysisData[0] = ID;
            Program.currentAnalysisData[1] = urunAdi;
            Program.currentAnalysisData[2] = urunGrubu;
            Program.currentAnalysisData[3] = urunSatisFiyati;
            Program.currentAnalysisData[4] = tarih;
            Program.currentAnalysisData[5] = stokMiktari;
            chart1.Series.Clear();


            int max_count = db.GetCountOfColumn(urunAdi, "StokMiktari");
            trackBar1.Maximum = max_count;
            trackBar2.Maximum = max_count;

            trackBar2.Value = max_count;
            label37.Text = $"{max_count}";

            ProductName.Text = $"{ID}-{urunAdi}";
            dtTableName = urunAdi;
            if (db.CheckInfoExist(urunAdi))
            {
                values = db.GetValuesOfColumnSortedByDate(urunAdi, "StokMiktari");
                alis = ConvertToIntArray(values);
                stokMiktariSeries = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = "Stok",
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100
                };

                chart1.Series.Add(stokMiktariSeries);
                stokMiktariSeries.ChartType = ChartType;
                stokMiktariSeries.BorderWidth = 5;

                foreach (var value in values)
                {
                    stokMiktariSeries.Points.AddY(double.Parse(value));
                }





                ortSatis = db.GetAverageOfColumn(urunAdi, "SatışFiyatı");
                ortMiktar = db.GetAverageOfColumn(urunAdi, "SatışMiktarı");
                ortAlis = db.GetAverageOfColumn(urunAdi, "AlışFiyatı");
                OrtSatisFiyati.Text = Convert.ToString(ortSatis);
                OrtSatisMiktari.Text = Convert.ToString(ortMiktar);
                OrtAlisFiyati.Text = Convert.ToString(ortAlis);
                KarOranı = ((db.GetAverageOfColumn(urunAdi, "SatışFiyatı") - db.GetAverageOfColumn(urunAdi, "AlışFiyatı")) * 100 / db.GetAverageOfColumn(urunAdi, "AlışFiyatı"));
                label10.Text = Convert.ToString($"%{KarOranı}");
                label12.Text = $"{(db.GetAverageOfColumn(urunAdi, "SatışFiyatı") - db.GetAverageOfColumn(urunAdi, "AlışFiyatı")) * db.GetAverageOfColumn(urunAdi, "SatışMiktarı")} ₺";

                eachDate = db.GetValuesOfColumnSortedByDate(urunAdi, "ID");
                values = db.GetValuesOfColumnSortedByDate(urunAdi, "SatışFiyatı");
                satis = ConvertToIntArray(values);
                satisFiyatiSeries = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = Program.Output[70],
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100
                };
                chart1.Series.Add(satisFiyatiSeries);
                //satisFiyatiSeries.Font.FontFamily = new FontFamily("JetMono");// FONT DEĞİŞMİYOR READ ONLY
                satisFiyatiSeries.ChartType = ChartType;
                satisFiyatiSeries.BorderWidth = 5;
                foreach (var value in values)
                {
                    satisFiyatiSeries.Points.AddY(double.Parse(value));
                }
                


                values = db.GetValuesOfColumnSortedByDate(urunAdi, "AlışFiyatı");
                alis = ConvertToIntArray(values);
                alisFiyatiSeries = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = Program.Output[71],
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100
                };

                chart1.Series.Add(alisFiyatiSeries);
                alisFiyatiSeries.ChartType = ChartType;
                alisFiyatiSeries.BorderWidth = 5;

                foreach (var value in values)
                {
                    alisFiyatiSeries.Points.AddY(double.Parse(value));
                }



                values = db.GetValuesOfColumnSortedByDate(urunAdi, "SatışMiktarı");
                miktar = ConvertToIntArray(values);
                satisMiktariSeries = new System.Windows.Forms.DataVisualization.Charting.Series
                {
                    Name = Program.Output[72],
                    ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100
                };
                chart1.Series.Add(satisMiktariSeries);
                satisMiktariSeries.ChartType = ChartType;
                satisMiktariSeries.BorderWidth = 5;
                foreach (var value in values)
                {
                    satisMiktariSeries.Points.AddY(double.Parse(value));
                }


                SetValuesAtLabel();
            }
            else
            {
                MessageBox.Show(Program.Output[74], Program.Output[19], MessageBoxButtons.OK);
                OrtAlisFiyati.Text = OrtSatisFiyati.Text = OrtSatisMiktari.Text = label8.Text = label27.Text = label28.Text = label29.Text = label30.Text = label31.Text = label32.Text = label33.Text = label34.Text = "0";
                label2.Text = label13.Text = label14.Text = label16.Text = label24.Text = label17.Text = label25.Text = label19.Text = label26.Text = Program.Output[75];
            }
        }
        #endregion

        #region Write Current Product's Data On Labels
        public void SetValuesAtLabel()
        {
            label2.Text = $"{KorelastyonIlgiCheck(PearsonCorrelation(satis, ortSatis, alis, ortAlis))}";
            label13.Text = $"{KorelastyonIlgiCheck(PearsonCorrelation(satis, ortSatis, miktar, ortMiktar))}";
            label14.Text = $"{KorelastyonIlgiCheck(PearsonCorrelation(miktar, ortMiktar, alis, ortAlis))}";

            label8.Text = $"{Program.Output[73]} {PearsonCorrelation(satis, ortSatis, alis, ortAlis)}";
            label27.Text = $"{Program.Output[73]} {PearsonCorrelation(satis, ortSatis, miktar, ortMiktar)}";
            label28.Text = $"{Program.Output[73]} {PearsonCorrelation(miktar, ortMiktar, alis, ortAlis)}";

            label16.Text = $"{CarpiklikCheck(CarpiklikHesabi(satis, ortSatis))}";
            label17.Text = $"{CarpiklikCheck(CarpiklikHesabi(alis, ortAlis))}";
            label19.Text = $"{CarpiklikCheck(CarpiklikHesabi(miktar, ortMiktar))}";

            label29.Text = $"{Program.Output[73]} {CarpiklikHesabi(satis, ortSatis)}";
            label31.Text = $"{Program.Output[73]} {CarpiklikHesabi(alis, ortAlis)}";
            label33.Text = $"{Program.Output[73]} {CarpiklikHesabi(miktar, ortMiktar)}";

            label24.Text = $"{BasiklikCheck(BasiklikHesabi(satis, ortSatis))}";
            label25.Text = $"{BasiklikCheck(BasiklikHesabi(alis, ortAlis))}";
            label26.Text = $"{BasiklikCheck(BasiklikHesabi(miktar, ortMiktar))}";

            label30.Text = $"{Program.Output[73]} {BasiklikHesabi(satis, ortSatis)}";
            label32.Text = $"{Program.Output[73]} {BasiklikHesabi(alis, ortAlis)}";
            label34.Text = $"{Program.Output[73]} {BasiklikHesabi(miktar, ortMiktar)}";

            satisFiyatiSeries.Name = Program.Output[70];
            alisFiyatiSeries.Name = Program.Output[71];
            satisMiktariSeries.Name = Program.Output[72];
        }
        #endregion

        #region Statistics Methods
        public string KorelastyonIlgiCheck(double Korelasyon)
        {
            if (Korelasyon == 1)
            {
                return Program.Output[76];
            }
            else if (Korelasyon <= 1 && Korelasyon >= 0.5)
            {
                return Program.Output[77];
            }
            else if (Korelasyon < 0.5 && Korelasyon > 0)
            {
                return Program.Output[78];
            }
            else if (Korelasyon == 0)
            {
                return Program.Output[79];
            }
            else if (Korelasyon < 0 && Korelasyon >= -0.5)
            {
                return Program.Output[80];
            }
            else if (Korelasyon < -0.5 && Korelasyon > -1)
            {
                return Program.Output[81];
            }
            else if (Korelasyon == -1)
            {
                return Program.Output[82];
            }
            else
            {
                return Program.Output[83];
            }
        }
        public string CarpiklikCheck(double Carpiklik)
        {
            if (Carpiklik == 0)
            {
                return Program.Output[84];
            }
            else if (Carpiklik < 0)
            {
                return Program.Output[85];
            }
            else if (Carpiklik > 0)
            {
                return Program.Output[86];
            }
            else
            {
                return Program.Output[87];
            }
        }

        public string BasiklikCheck(double Basiklik)
        {
            if (Basiklik == 0)
            {
                return Program.Output[88];
            }
            else if (Basiklik < 0)
            {
                return Program.Output[89];
            }
            else if (Basiklik > 0)
            {
                return Program.Output[90];
            }
            else
            {
                return Program.Output[91];
            }
        }

        public double KoreleasyonHesabı(double[] value1, double value1Avg, double[] value2, double value2Avg)
        {
            double[] cache1 = new double[value1.Length];
            double[] cache2 = new double[value1.Length];
            double[] cache3 = new double[value1.Length];
            for (int i = 0; i < value1.Length; i++)
            {
                cache1[i] = Math.Pow((value1[i] - value1Avg), 2);  //Bottom1
                cache2[i] = Math.Pow((value2[i] - value2Avg), 2);  //Bottom2
                cache3[i] = (value1[i] - value1Avg) * (value2[i] - value1Avg);  //Top
            }
            return cache3.Sum() / Math.Sqrt(cache1.Sum() * cache2.Sum());
        }

        public double PearsonCorrelation(double[] values1, double mean1, double[] values2, double mean2)
        {
            double[] numerator = new double[values1.Length];
            double denominator1 = 0;
            double denominator2 = 0;

            for (int i = 0; i < values1.Length; i++)
            {
                numerator[i] = (values1[i] - mean1) * (values2[i] - mean2);
                denominator1 += Math.Pow(values1[i] - mean1, 2);
                denominator2 += Math.Pow(values2[i] - mean2, 2);
            }

            double correlation = numerator.Sum() / Math.Sqrt(denominator1 * denominator2);
            return correlation;
        }

        public double CarpiklikHesabi(double[] values, double valueAvg)
        {
            int middleIndex = values.Length / 2;
            double middleValue;
            if (values.Length % 2 == 0)
            {
                middleValue = (values[middleIndex - 1] + values[middleIndex]) / 2.0;
            }
            else
            {
                middleValue = values[middleIndex];
            }

            double denominator = Math.Sqrt(VaryansHesabi(values, valueAvg));

            if (denominator == 0)
            {
                return 0.0;
            }

            return 3 * (valueAvg - middleValue) / denominator;

            //return 3 * (valueAvg-middleValue)/Math.Sqrt(VaryansHesabi(values,valueAvg));
        }

        public double VaryansHesabi(double[] values, double valueAvg)
        {
            double denominator1 = 0;

            for (int i = 0; i < values.Length; i++)
            {
                denominator1 += Math.Pow(values[i] - valueAvg, 2);
            }
            return denominator1 / values.Length;
        }

        public double[] ConvertToIntArrayOld(string[] array)
        {
            double[] cache = new double[array.Length];

            foreach (string value in array)
            {
                cache[value.Length] = Convert.ToDouble(value);
            }
            return cache;
        }

        public double[] ConvertToIntArray(string[] array)
        {
            double[] cache = new double[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                cache[i] = Convert.ToDouble(array[i]);
            }
            return cache;
        }
        public double[] ConvertToIntArray(double[] array)
        {
            return array;
        }

        public double BasiklikHesabi(double[] values, double valueAvg)
        {
            double n = values.Length;

            if (n < 4)
            {
                return double.NaN;
            }

            double sum4thMoment = 0;
            //double sum2ndMoment = 0;

            for (int i = 0; i < n; i++)
            {
                double deviation = values[i] - valueAvg;
                sum4thMoment += Math.Pow(deviation, 4);
                //sum2ndMoment += Math.Pow(deviation, 2);
            }

            double m4 = sum4thMoment / n;
            //double m2 = sum2ndMoment / n;

            //double variance = m2 * m2;


            double variance = VaryansHesabi(values, valueAvg);

            if (variance == 0)
            {
                // Avoid division by zero
                return double.NaN;
            }
            double standart_deviation = Math.Sqrt(variance);
            double kurtosis = m4 / (Math.Pow(standart_deviation, 4)) - 3.0;

            return kurtosis;
        }
        #endregion

        #region Deactive Chart's Control 
        private void chart1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }
        #endregion

        #region Move The Form
        private void DetailedProductAnalysis_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void DetailedProductAnalysis_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void DetailedProductAnalysis_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }
        #endregion

        #region Open Daily Product Data Form
        private void InItTable_Click_1(object sender, EventArgs e)
        {
            if (DataGridShowInstance == null || DataGridShowInstance.IsDisposed && !DataGridShow.AcikMi)
            {
                Program.CursorChange();
                AnimatedMenu(InItTable, false);
                DataGridShowInstance = new DataGridShow();
                DataGridShow.dtTableName = dtTableName;
                DataGridShowInstance.Show();
                DatabaseView.tableName = dtTableName;
            }
            else
            {
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region Better Animations
        public async void AnimatedMenu(PictureBox pictureBox, bool RotateDirection)
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

        int GetOpenedCount()
        {
            int count = 0;

            if (stokMiktariSeries.Enabled){ count++; }
            if (satisFiyatiSeries.Enabled) { count++; }
            if (satisMiktariSeries.Enabled) { count++; }
            if (alisFiyatiSeries.Enabled) { count++; }

            return count;
        }

        private string FindMaxSeries(Chart chart)
        {
            double max = double.MinValue;
            string maxSeriesName = null;

            // Iterate through all series in the chart
            foreach (Series series in chart.Series)
            {
                // Find the maximum Y-value in the series
                double seriesMax = double.MinValue;
                if (series.Enabled)
                {
                    foreach (DataPoint point in series.Points)
                    {
                        if (point.YValues[0] > seriesMax)
                        {
                            seriesMax = point.YValues[0];
                        }
                    }
                }

                // Check if the maximum Y-value in this series is greater than the current maximum
                if (seriesMax > max)
                {
                    max = seriesMax;
                    maxSeriesName = series.Name;
                }
            }

            return maxSeriesName;
        }

        private void SetMaxYAxisToSeriesMax(Chart chart, string seriesName)
        {
            Series series = chart.Series[seriesName];

            // Find the maximum Y value in the series
            double max = double.MinValue;
            foreach (DataPoint point in series.Points)
            {
                if (point.YValues[0] > max)
                {
                    max = point.YValues[0];
                }
            }

            // Set the maximum Y-axis value to the maximum value in the series
            chart.ChartAreas[0].AxisY.Maximum = max+max/4;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(stokMiktariSeries.Enabled)
                stokMiktariSeries.Enabled = false;
            else
                stokMiktariSeries.Enabled = true;

            if (GetOpenedCount() != 0)
            {
                string seriesName = FindMaxSeries(chart1);
                SetMaxYAxisToSeriesMax(chart1, seriesName);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (satisFiyatiSeries.Enabled)
                satisFiyatiSeries.Enabled = false;
            else
                satisFiyatiSeries.Enabled = true;

            if(GetOpenedCount()!=0)
            {
                string seriesName = FindMaxSeries(chart1);
                SetMaxYAxisToSeriesMax(chart1, seriesName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (alisFiyatiSeries.Enabled)
                alisFiyatiSeries.Enabled = false;
            else
                alisFiyatiSeries.Enabled = true;

            if (GetOpenedCount() != 0)
            {
                string seriesName = FindMaxSeries(chart1);
                SetMaxYAxisToSeriesMax(chart1, seriesName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (satisMiktariSeries.Enabled)
                satisMiktariSeries.Enabled = false;
            else
                satisMiktariSeries.Enabled = true;

            if (GetOpenedCount() != 0)
            {
                string seriesName = FindMaxSeries(chart1);
                SetMaxYAxisToSeriesMax(chart1, seriesName);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

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

        #region PictureBoxs Animation
        private void InItTable_MouseEnter(object sender, EventArgs e)
        {
            InItTable.BackColor = SystemColors.ControlDark;
        }

        private void InItTable_MouseLeave(object sender, EventArgs e)
        {
            InItTable.BackColor = Color.White;
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
