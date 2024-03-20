using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NTP_P1
{
    public partial class StartIcon : Form
    {

        bool[] check = new bool[23];
        Random random = new Random();

        public StartIcon()
        {
            InitializeComponent();
        }

        private void StartIcon_Load(object sender, EventArgs e)
        {
            /*
            Loading assets... ██████████ 100 %
            Assets successfully loaded.
            
            Building modules... ██████████ 100 %
            Modules successfully compiled and linked.
            
            Initialization complete.
            
            Welcome to ModuleForge v2.0 Terminal Interface.
            */
        }
        bool islem = false;

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (islem == false)
            {
                this.Opacity += random.NextDouble() * (0.03 - 0.01);
            }
            if (this.Opacity == 1.0)
            {
                islem = true;
            }
            if (islem)
            {
                this.Opacity -= 0.03;
            }
            if (this.Opacity > 0.01 && this.Opacity < 0.04)
            {
                if (!check[0])
                {
                    label1.Text = "Loading assets... █ 10 %";
                    check[0] = true;
                }
            }
            else if (this.Opacity > 0.04 && this.Opacity < 0.07)
            {
                if (!check[1])
                {
                    label1.Text = "Loading assets... ██ 20 %";
                    check[1] = true;
                }
            }
            else if (this.Opacity > 0.07 && this.Opacity < 0.11)
            {
                if (!check[2])
                {
                    label1.Text = "Loading assets... ██ 30 %";
                    check[2] = true;
                }
            }
            else if (this.Opacity > 0.11 && this.Opacity < 0.15)
            {
                if (!check[3])
                {
                    label1.Text = "Loading assets... ████ 40 %";
                    check[3] = true;
                }
            }
            else if (this.Opacity > 0.15 && this.Opacity < 0.19)
            {
                if (!check[4])
                {
                    label1.Text = "Loading assets... █████ 50 %";
                    check[4] = true;
                }
            }
            else if (this.Opacity > 0.19 && this.Opacity < 0.23)
            {
                if (!check[5])
                {
                    label1.Text = "Loading assets... ██████ 60 %";
                    check[5] = true;
                }
            }
            else if (this.Opacity > 0.23 && this.Opacity < 0.27)
            {
                if (!check[6])
                {
                    label1.Text = "Loading assets... ███████ 70 %";
                    check[6] = true;
                }
            }
            else if (this.Opacity > 0.27 && this.Opacity < 0.31)
            {
                if (!check[7])
                {
                    label1.Text = "Loading assets... ████████ 80 %";
                    check[7] = true;
                }
            }
            else if (this.Opacity > 0.31 && this.Opacity < 0.35)
            {
                if (!check[8])
                {
                    label1.Text = "Loading assets... █████████ 90 %";
                    check[8] = true;
                }
            }
            else if (this.Opacity > 0.35 && this.Opacity < 0.39)
            {
                if (!check[9])
                {
                    label1.Text = "Loading assets... ██████████ 100 %";
                    check[9] = true;
                }
            }
            else if (this.Opacity > 0.39 && this.Opacity < 0.43)
            {
                if (!check[10])
                {
                    label1.Text = "Assets successfully loaded.";
                    check[10] = true;
                }
            }
            else if (this.Opacity > 0.43 && this.Opacity < 0.47)
            {
                if (!check[11])
                {
                    label1.Text = "Building modules... █ 10 %";
                    check[11] = true;
                }
            }
            else if (this.Opacity > 0.47 && this.Opacity < 0.51)
            {
                if (!check[12])
                {
                    label1.Text = "Building modules... ██ 20 %";
                    check[12] = true;
                }
            }
            else if (this.Opacity > 0.51 && this.Opacity < 0.55)
            {
                if (!check[13])
                {
                    label1.Text = "Building modules... ███ 30 %";
                    check[13] = true;
                }
            }
            else if (this.Opacity > 0.55 && this.Opacity < 0.59)
            {
                if (!check[14])
                {
                    label1.Text = "Building modules... ████ 40 %";
                    check[14] = true;
                }
            }
            else if (this.Opacity > 0.59 && this.Opacity < 0.63)
            {
                if (!check[15])
                {
                    label1.Text = "Building modules... █████ 50 %";
                    check[15] = true;
                }
            }
            else if (this.Opacity > 0.63 && this.Opacity < 0.67)
            {
                if (!check[16])
                {
                    label1.Text = "Building modules... ██████ 60 %";
                    check[16] = true;
                }
            }
            else if (this.Opacity > 0.67 && this.Opacity < 0.71)
            {
                if (!check[17])
                {
                    label1.Text = "Building modules... ███████ 70 %";
                    check[17] = true;
                }
            }
            else if (this.Opacity > 0.71 && this.Opacity < 0.75)
            {
                if (!check[17])
                {
                    label1.Text = "Building modules... ████████ 80 %";
                    check[17] = true;
                }
            }
            else if (this.Opacity > 0.75 && this.Opacity < 0.79)
            {
                if (!check[18])
                {
                    label1.Text = "Building modules... █████████ 90 %";
                    check[18] = true;
                }
            }
            else if (this.Opacity > 0.79 && this.Opacity < 0.83)
            {
                if (!check[19])
                {
                    label1.Text = "Building modules... ██████████ 100 %";
                    check[19] = true;
                }
            }
            else if (this.Opacity > 0.83 && this.Opacity < 0.87)
            {
                if (!check[20])
                {
                    label1.Text = "Modules successfully compiled and linked.";
                    check[20] = true;
                }
            }
            else if (this.Opacity > 0.87 && this.Opacity < 0.91)
            {
                if (!check[21])
                {
                    label1.Text = "Initialization complete."; 
                    check[21] = true;
                }
            }
            else if (this.Opacity > 0.91 && this.Opacity < 1.0)
            {
                if (!check[22])
                {
                    label1.Text = "Welcome to ASYS.";
                    check[22] = true;
                }
            }

            if (this.Opacity == 0)
            {
                Program.loginPage.pictureBox2.Image = Properties.Resources.ASYS_LOGO_FINAL_HALFSIZED;
                Program.ChangeForm(Program.loginPage, this);
                Program.loginPage.Focus();
                timer1.Enabled = false;
                pictureBox1.Image = null;
                this.Dispose();
                System.GC.Collect();
                System.GC.Collect();
                System.GC.Collect();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
