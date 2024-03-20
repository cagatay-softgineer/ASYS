
#region Packages
using System;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace NTP_P1
{
    public partial class ForgetPassword : Form
    {
        #region Variable Definition
        Database db = new Database();
        public static String Verify_Code, mail_address;
        public static string username;
        private bool mouseDown;
        private Point lastLocation;
        #endregion

        #region Form Implementation
        public ForgetPassword()
        {
            InitializeComponent();
        }
        #endregion

        #region Close To Application
        private void ForgetPassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Return To Login Page
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Program.loginPage.pictureBox2.Image = Properties.Resources.ASYS_LOGO_FINAL_HALFSIZED;
            Program.ChangeForm(Program.loginPage, this);
            Program.forgetPassword.CenterToScreen();
            textBox1.Text = "";
            System.GC.Collect();
        }
        #endregion

        #region Check Validty Of Email
        static bool IsValidEmail(string email)
        {
            // Define a regular expression for a simple email validation
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            // Check if the email matches the pattern
            return regex.IsMatch(email);
        }
        #endregion

        #region Send Email With Verify Code
        private void btnVerifyCodeSend_Click(object sender, EventArgs e)
        {
            Program.CursorChange();
            if (IsValidEmail(textBox1.Text))
            {


                Verify_Code = VerifyCode.GenerateVerifyCode();
                Program.verifyType = "P";
                try
                {
                    if (db.CheckEmailExists(Program.EncryptIt(textBox1.Text)))
                    {
                        username = db.GetKullaniciAdiFromMail(Program.EncryptIt(textBox1.Text));
                        username = Program.DecryptIt(username);
                        MailMessage msg = new MailMessage();
                        msg.From = new MailAddress($"{Crypt.AllInOneDMAIL(NTP_P1.Properties.Settings.Default.appMail)}");
                        msg.To.Add(textBox1.Text);
                        msg.Subject = Program.Output[5];
                        //msg.Body = "Doğrulama Kodunuz : " + Verify_Code + "";   
                        string base64String = "iVBORw0KGgoAAAANSUhEUgAAAkQAAABkCAYAAABn0y9FAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAA2ZpVFh0WE1MOmNvbS5hZG9iZS54bXAAAAAAADw/eHBhY2tldCBiZWdpbj0i77u/IiBpZD0iVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkIj8+IDx4OnhtcG1ldGEgeG1sbnM6eD0iYWRvYmU6bnM6bWV0YS8iIHg6eG1wdGs9IkFkb2JlIFhNUCBDb3JlIDUuMy1jMDExIDY2LjE0NTY2MSwgMjAxMi8wMi8wNi0xNDo1NjoyNyAgICAgICAgIj4gPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4gPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgeG1sbnM6eG1wTU09Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8iIHhtbG5zOnN0UmVmPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvc1R5cGUvUmVzb3VyY2VSZWYjIiB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iIHhtcE1NOk9yaWdpbmFsRG9jdW1lbnRJRD0ieG1wLmRpZDowREJFOTg4Mzk0OTZFRTExODE3MUMwRTAwMzFFNEY5MCIgeG1wTU06RG9jdW1lbnRJRD0ieG1wLmRpZDo2QTQ1OTgwRDk3NTkxMUVFQUFCQURDNzAxMEMzODA0NCIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDo2QTQ1OTgwQzk3NTkxMUVFQUFCQURDNzAxMEMzODA0NCIgeG1wOkNyZWF0b3JUb29sPSJBZG9iZSBQaG90b3Nob3AgQ1M2IChXaW5kb3dzKSI+IDx4bXBNTTpEZXJpdmVkRnJvbSBzdFJlZjppbnN0YW5jZUlEPSJ4bXAuaWlkOkIxM0E4RDI1OTY5NkVFMTFCNTU4REMzMDJBQTQ3MTdCIiBzdFJlZjpkb2N1bWVudElEPSJ4bXAuZGlkOjBCQkU5ODgzOTQ5NkVFMTE4MTcxQzBFMDAzMUU0RjkwIi8+IDwvcmRmOkRlc2NyaXB0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";
                        string base64String2 = "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";
                        LinkedResource linkedResource = new LinkedResource(GetImageStream(base64String), "image/png");
                        linkedResource.ContentId = "image1";
                        LinkedResource linkedResource2 = new LinkedResource(GetImageStream(base64String2), "image/png");
                        linkedResource2.ContentId = "image2";
                        string htmlString = $@"<!DOCTYPE html>
<html lang=""tr"">
<head>
  <meta charset=""UTF-8"">
  <title>ASYS</title>
</head>
<body>
  <div style=""background-color:#0b0e0f;padding:30px 20px;font-family:Inter,ui-sans-serif,system-ui,-apple-system,'Segoe UI',sans-serif;color:#fff;aspect-ratio:3/2"">
        <table align=""center"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
          <tbody><tr>
            <td style=""width:346px;max-width:100%;border-width:1px"">
              <table cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""width:100%;border-radius:4px;overflow:hidden"">
                <tbody><tr>
                  <td>
                    
                    
                    <table style=""width:100%;height:228px;background-position:center"" cellspacing=""0"" cellpadding=""0"">
                      <tbody><tr>
                        <td style=""vertical-align:top;text-align:center"">
                          <div style=""display:inline-block;padding-top:20px"">
                            <img src='cid:{linkedResource.ContentId}' alt='ASYS' width=""auto"" height=""auto"" data-bit=""iit"">
                          </div>
                        </td>
                      </tr>
                      <tr>
                        <td style=""vertical-align:bottom"">
                          <div style=""padding:20px;font-weight:700;font-size:28px"">
                            {Program.Output[6]}
                          </div>
                        </td>
                      </tr>
                    </tbody></table>
                    
                    
                  </td>
                </tr>
                <tr>
                  <td>
                    <div style=""background-color:#191b1f;padding:24px 14px;max-width:85%"">
                      
                      
<div style=""margin-bottom:6px;font-size:16px;font-weight:700"">
  Hey {username},
</div>
<div style=""font-size:12px;color:#b1bcc3;margin-bottom:16px"">

<table cellspacing=""0"" cellpadding=""0"" style=""padding-top:16px"">
        <tbody><tr>
            <td style=""padding-left:14px"">
               <div style=""font-weight:400;font-size:12px;color:#fff"">{Program.Output[7]}</div>
               <div style=""font-weight:400;font-size:12px;color:#fff""><h2>[{Verify_Code}]</h2></div>
                <div style=""font-weight:400;font-size:12px;color:#fff"">{Program.Output[8]}</div>
                <div style=""font-weight:400;font-size:12px;color:#fff"">{Program.Output[9]} <h2><strong><div class='moving-text'>ASYS</div></strong></h2></div>
                <div style=""font-weight:400;font-size:12px;color:#fff"">{Program.Output[10]}</div>
                <div style=""font-weight:400;font-size:12px;color:#fff"">{Program.Output[11]}</div>
                <div style=""font-weight:400;font-size:12px;color:#fff"">{Program.Output[12]}<br><h5>-ASYS-</h5><br></div>
            </td>
        </tr>
    </tbody></table>
  </div>

<div>
  
</div>


 
<div style=""margin-top:24px"">
<section>
    
        <h5 style=""font-weight:700;font-size:16px;margin:0;color:#fff"">{Program.Output[18]}</h5>
     
    
    
       
     
    
    
    <table cellspacing=""0"" cellpadding=""0"" style=""padding-top:16px"">
        <tbody><tr>
            <td valign=""top"">
                <div style=""width:32px;height:32px;background-color:rgba(83,252,24,0.15);border-radius:4px;text-align:center"">
                    <table style=""width:100%;height:100%"">
                        <tbody>
						<tr><td style=""text-align: center;"">
						<img src='cid:{linkedResource2.ContentId}' alt='ASYS' style=""width:32px;height:32px;"" data-bit=""iit"">
						</td></tr>
                    </tbody></table>
                </div>
            </td>
            <td style=""padding-left:14px"">
               <div style=""font-weight:700;font-size:12px;color:#fff"">ASYS</div>
               <div style=""font-weight:400;font-size:12px;color:#b1bcc3;margin-top:2px"">{Program.Output[13]}</div>
            </td>
        </tr>
    </tbody></table>
    
</section>
</div>

                      
                    </div>
                  </td>
                </tr>
              </tbody></table>
            </td>
          </tr>
          <tr>
            <td>
              
              <table style=""width:100%"" cellpadding=""0"" cellspacing=""0"" role=""presentation"">
    <tbody><tr>
        <td>
            <div style=""margin-top:30px"">
                <div style=""margin-bottom:10px""></div>
                <div style=""margin-bottom:16px;font-size:10px;color:#b1bcc3"">{Program.Output[14]}</div>
            </div>
        </td>
        <td style=""text-align:right"">
            
        </td>
    </tr>
    <tr>
        <td>
            <table cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                <tbody><tr>
                    <td>
					
					</td>
                    <td style=""padding-left:10px""> </td>
                </tr>
            </tbody></table>
        </td>
        <td align=""right"">
            <table cellpadding=""0"" cellspacing=""0"" role=""presentation"">
                <tbody><tr>
                    <td align=""right"">
                        
                    </td>
                    <td align=""right"" style=""padding-left:13px"">
                        
                    </td>
                </tr>
            </tbody></table>
        </td>
    </tr>
</tbody></table>
              
            </td>
          </tr>
        </tbody></table><div class=""yj6qo""></div><div class=""adL"">
      </div></div>
</body>
</html>

                     ";

                        msg.Body = htmlString;
                        msg.IsBodyHtml = true;



                        // Create a LinkedResource with the base64 image data

                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlString, Encoding.UTF8, MediaTypeNames.Text.Html);
                        htmlView.LinkedResources.Add(linkedResource);
                        htmlView.LinkedResources.Add(linkedResource2);
                        msg.AlternateViews.Add(htmlView);

                        SmtpClient smt = new SmtpClient();
                        smt.Host = "smtp.office365.com";
                        System.Net.NetworkCredential ntcd = new NetworkCredential();
                        ntcd.UserName = Crypt.AllInOneDMAIL(NTP_P1.Properties.Settings.Default.appMail);
                        ntcd.Password = Crypt.AllInOneDMAIL(NTP_P1.Properties.Settings.Default.appMailPass);
                        smt.Credentials = ntcd;
                        smt.EnableSsl = true;
                        smt.Port = 587;
                        smt.Send(msg);
                        mail_address = textBox1.Text;

                        MessageBox.Show(Program.Output[15]);

                        Program.verifyPage.pictureBox2.Image = Properties.Resources.ASYS_LOGO_FINAL_HALFSIZED;
                        Program.ChangeForm(Program.verifyPage, this);
                        pictureBox2.Image = null;
                        Program.forgetPassword.CenterToScreen();
                        System.GC.Collect();
                    }
                    //mail_address = textBox1.Text;  // 
                    else
                    {
                        MessageBox.Show(Program.Output[16]);
                    }


                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(Program.Output[17]);
            }
        }
        #endregion

        #region PictureBoxs Animation
        private void btnVerifyCodeSend_MouseHover(object sender, EventArgs e)
        {
            btnVerifyCodeSend.ForeColor = DarkenColor(btnVerifyCodeSend.ForeColor, 0.4f);
        }

        private void btnVerifyCodeSend_MouseLeave(object sender, EventArgs e)
        {
            btnVerifyCodeSend.ForeColor = Color.White;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            AnimatedShrink(pictureBox1, true);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            AnimatedShrink(pictureBox1, false);
        }
        #endregion

        #region Convert Base64 String To Image
        static System.IO.Stream GetImageStream(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            return new System.IO.MemoryStream(bytes);
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

        private void ForgetPassword_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.Image = Properties.Resources.exit_black_enter;
        }

        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.Image = pictureBox7.InitialImage;
        }

        private void ForgetPassword_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void ForgetPassword_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void ForgetPassword_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void ForgetPassword_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;
        }

        private async void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            email.Visible = true;
            await Task.Delay(3000);
            email.Visible = false;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            email.Visible = false;
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
