using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTP_P1
{
    public partial class VerifyPage : Form
    {
        DateTime time = DateTime.Today;
        public static String Password, username, mail_adress,Fname,Sname;
        private bool mouseDown;
        private Point lastLocation;
        public VerifyPage()
        {
            InitializeComponent();
        }

        private void ChangePassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Program.verifyType.Equals("P"))
            {
                Program.forgetPassword.pictureBox2.Image = Properties.Resources.ASYS_LOGO_FINAL_HALFSIZED;
                Program.ChangeForm(Program.forgetPassword, this);
                Program.verifyPage.CenterToScreen();
                Program.verifyPage.pictureBox2.Image = null;
                System.GC.Collect();
            }
            if (Program.verifyType.Equals("R"))
            {
                Program.registerPage.pictureBox2.Image = Properties.Resources.ASYS_LOGO_FINAL_HALFSIZED;
                Program.ChangeForm(Program.registerPage, this);
                Program.verifyPage.pictureBox2.Image = null;
                Program.verifyPage.CenterToScreen();
                System.GC.Collect();
            }
        }

        private void btnVerifyCodeCheck_MouseHover(object sender, EventArgs e)
        {
            btnVerifyCodeCheck.ForeColor = DarkenColor(btnVerifyCodeCheck.ForeColor, 0.2f);
        }

        private void btnVerifyCodeCheck_MouseLeave(object sender, EventArgs e)
        {
            btnVerifyCodeCheck.ForeColor = Color.White;
        }

        private void txtBoxVerifyCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            AnimatedShrink(pictureBox1, true);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            AnimatedShrink(pictureBox1, false);
        }
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
            // for (int i = 0; i < 10; i++)
            // {
            //     RotateImage(-36, pictureBox);
            //     await Task.Delay(100);
            // }
            //int totalDegree=1;
            //while(totalDegree <= 360)
            //{
            //    RotateImage(totalDegree, pictureBox);
            //    await Task.Delay(100-totalDegree/10);
            //    totalDegree = totalDegree + (10-totalDegree / 10);
            //}


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
                double[] shrinkAmounts = { 0.99, 0.99, 0.99, 0.99, 0.99, 0.99, 0.99, 0.99, 0.99 };

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
                double[] shrinkAmounts = { 1.01, 1.01, 1.01, 1.01, 1.01, 1.01, 1.01, 1.01, 1.01 };
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

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void VerifyPage_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void VerifyPage_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void VerifyPage_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private Color DarkenColor(Color color, float factor)
        {
            int r = (int)(color.R * (1 - factor));
            int g = (int)(color.G * (1 - factor));
            int b = (int)(color.B * (1 - factor));

            return Color.FromArgb(r, g, b);
        }

        private void btnVerifyCodeCheck_Click(object sender, EventArgs e)
        {
            if (txtBoxVerifyCode.Text == ForgetPassword.Verify_Code)
            {
                if (Program.verifyType.Equals("P"))
                {
                    Program.ChangeForm(Program.updatePassword, this);
                    Program.verifyPage.pictureBox2.Image = null;
                    Program.updatePassword.pictureBox2.Image = Properties.Resources.ASYS_LOGO_FINAL_HALFSIZED;
                    txtBoxVerifyCode.Text = "";
                    System.GC.Collect();
                }
            }
            else if (txtBoxVerifyCode.Text == RegisterPage.Verify_Code)
            {
                if (Program.verifyType.Equals("R"))
                {
                    username = RegisterPage.username;
                    Fname = RegisterPage.Fname;
                    Sname = RegisterPage.Sname;
                    Password = Program.EncryptIt(RegisterPage.password);
                    mail_adress = Program.EncryptIt(RegisterPage.mail_address);


                    string[] columns = { "KullaniciAdi", "Sifre", "Root", "defaultUser", "Employee", "Mail", "GirisZamani","Ad","Soyad" };
                    string[] values = { username, Password, RegisterPage.root, RegisterPage.default_user, "0", mail_adress, String.Format("{0:dd/MM/yyyy}", time), Fname, Sname };
                    Database.Connect.Open();
                    Database.Insert("Personel", columns, values);
                    Database.Connect.Close();
                    Program.ChangeForm(Program.loginPage, this);
                    Program.verifyPage.pictureBox2.Image = null;
                    Program.loginPage.pictureBox2.Image = Properties.Resources.ASYS_LOGO_FINAL_HALFSIZED;
                    txtBoxVerifyCode.Text = "";
                    System.GC.Collect();
                }
            }
            else
            {
                MessageBox.Show(Program.Output[27]);
            }
        }
    }
}
