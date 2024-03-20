using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NTP_P1
{
    public partial class UpdatePassword : Form
    {
        string hashedPassword, mail_address;
        public UpdatePassword()
        {
            InitializeComponent();
        }

        private void email_Click(object sender, EventArgs e)
        {

        }

        private void btnVerifyCodeSend_Click(object sender, EventArgs e)
        {
            if (sifre1Text.Text == sifre1CheckText.Text)
            {
                if (sifre1Text.TextLength >= 4)
                {
                    try
                    {
                        hashedPassword = Program.EncryptIt(sifre1Text.Text);
                        mail_address = Program.EncryptIt(ForgetPassword.mail_address);
                        Database.Connect.Open();
                        Database.BasicUpdate("Personel", "Sifre='" + hashedPassword + "'", "Mail='" + mail_address + "'");
                        Database.Connect.Close();
                        Program.ChangeForm(Program.loginPage, this);
                        Program.updatePassword.pictureBox2.Image = null;
                        Program.loginPage.pictureBox2.Image = Properties.Resources.ASYS_LOGO_FINAL_HALFSIZED;
                        sifre1Text.Text = sifre1CheckText.Text = "";
                        System.GC.Collect();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show(Program.Output[25]);
                }
            }
            else
            {
                MessageBox.Show(Program.Output[26]);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Program.loginPage.pictureBox2.Image = Properties.Resources.ASYS_LOGO_FINAL_HALFSIZED;
            Program.ChangeForm(Program.loginPage, this);
            Program.updatePassword.pictureBox2.Image = null;
            sifre1Text.Text = sifre1CheckText.Text = "";
            System.GC.Collect();
        }

        private void btnVerifyCodeSend_MouseHover(object sender, EventArgs e)
        {

        }

        private void UpdatePassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnVerifyCodeSend_MouseLeave(object sender, EventArgs e)
        {
            btnVerifyCodeSend.ForeColor = Color.White;
        }

        private void UpdatePassword_Load(object sender, EventArgs e)
        {

        }

        private async void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            label1.Visible = true;
            await Task.Delay(3000);
            label1.Visible = false;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private async void pictureBox5_MouseHover(object sender, EventArgs e)
        {
            label2.Visible = true;
            await Task.Delay(3000);
            label2.Visible = false;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnVerifyCodeSend_MouseEnter(object sender, EventArgs e)
        {
            btnVerifyCodeSend.ForeColor = DarkenColor(btnVerifyCodeSend.ForeColor, 0.4f);
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.exit_black_enter;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Image = pictureBox7.InitialImage;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            AnimatedShrink(pictureBox1, true);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            AnimatedShrink(pictureBox1, false);
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
                double[] shrinkAmounts = { 0.98, 0.98, 0.98, 0.98, 0.98, 0.98, 0.98, 0.98, 0.98 };

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
                double[] shrinkAmounts = { 1.02, 1.02, 1.02, 1.02, 1.02, 1.02, 1.02, 1.02, 1.02 };
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

        private void pictureBox1_MouseEnter_1(object sender, EventArgs e)
        {
            AnimatedShrink(pictureBox1, true);
        }

        private void pictureBox1_MouseLeave_1(object sender, EventArgs e)
        {
            AnimatedShrink(pictureBox1, false);
        }

        private Color DarkenColor(Color color, float factor)
        {
            int r = (int)(color.R * (1 - factor));
            int g = (int)(color.G * (1 - factor));
            int b = (int)(color.B * (1 - factor));

            return Color.FromArgb(r, g, b);
        }
        #endregion
    }
}
