
#region Packages
using System;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
#endregion

namespace NTP_P1
{
    public partial class AdminPersonelView : UserControl
    {
        #region Variable Definition
        public static Update_Personel updatePersonel;
        public Personel Personel { get; set; }
        string cache_pass;
        bool on_text;
        char passwordChar = '$';
        #endregion

        #region Form Implementation
        public AdminPersonelView()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Load Processes
        private void AdminPersonelView_Load(object sender, EventArgs e)
        {
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentCurrentColor;

            LabelMail.Text = Program.DecryptIt(Personel.Mail);
            LabelKA.Text = Program.DecryptIt(Personel.KullaniciAdi);
            //cache_pass = LabelPW.Text = Program.DecryptIt(Personel.Sifre);
            LabelPW.Text = Personel.Sifre;
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 20, LabelPW.Font.Style);
            //LabelPW.Text = new string(passwordChar, LabelPW.Text.Length);

            IDLabel.Text = Convert.ToString(Personel.Id);
            LabelTarih.Text = Convert.ToDateTime(Personel.GirisZamani).ToString("dd.MM.yyyy");
            if (Personel.isRoot)
            {
                RootCheck.Image = NTP_P1.Properties.Resources.check_Colored;
            }
            else
            {
                RootCheck.Image = NTP_P1.Properties.Resources.cancel_Colored;
            }
            if (Personel.isDefaultUser)
            {
                DefaultUserCheck.Image = NTP_P1.Properties.Resources.check_Colored;
            }
            else
            {
                DefaultUserCheck.Image = NTP_P1.Properties.Resources.cancel_Colored;
            }
            if (Personel.isEmployee)
            {
                EmployeeCheck.Image = NTP_P1.Properties.Resources.check_Colored;
            }
            else
            {
                EmployeeCheck.Image = NTP_P1.Properties.Resources.cancel_Colored;
            }
        }
        #endregion

        #region Delete User Method
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"{this.BProduct.UrunAdı}");
            DialogResult result = MessageBox.Show(Program.Output[94], Program.Output[93], MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Database.Connect.Open();
                Database.Delete("Personel", this.IDLabel.Text);
                Database.Connect.Close();
                Program.mainPage.AdminGetAllUsers();
                System.GC.Collect();
            }
            else
            {
                //Do nothing!
            }
        }
        #endregion

        #region Update User Method
        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (updatePersonel == null || updatePersonel.IsDisposed && !Update_Personel.AcikMi)
            {
                updatePersonel = new Update_Personel();
                Update_Personel.usernameCache = this.LabelKA.Text;
                updatePersonel.getDataFromSelectedUser(this.IDLabel.Text, this.LabelKA.Text, Program.DecryptIt(this.LabelPW.Text), this.LabelMail.Text, this.LabelTarih.Text, Personel.isRoot, Personel.isDefaultUser, Personel.isEmployee, Personel.Ad, Personel.Soyadi);
                Update_Personel.AdminOrDefault = true;
                updatePersonel.Show();
            }
            else
            {
                MessageBox.Show(Program.Output[40], Program.Output[41], MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region PictureBoxs Animation
        private void updateBtn_MouseEnter(object sender, EventArgs e)
        {
            updateBtn.BackColor = SystemColors.ControlDark;
        }

        private void updateBtn_MouseLeave(object sender, EventArgs e)
        {
            updateBtn.BackColor = Color.Transparent;
        }

        private void deleteBtn_MouseEnter(object sender, EventArgs e)
        {
            deleteBtn.BackColor = SystemColors.ControlDark;
        }

        private void deleteBtn_MouseLeave(object sender, EventArgs e)
        {
            deleteBtn.BackColor = Color.Transparent;
        }
        #endregion

        private async void LabelPW_MouseHover(object sender, EventArgs e)
        {
            on_text = true;
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 19, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 18, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 17, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 16, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 15, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 14, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 13, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 12, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 11, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 10, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 9, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 8, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 7, LabelPW.Font.Style);
            await Task.Delay(100);
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 6, LabelPW.Font.Style);
            await Task.Delay(100);

            if (on_text)
            {
                string pass_Cache = Program.DecryptIt(Personel.Sifre);
                for(int i = 0;i < pass_Cache.Length;i++)
                {
                    if (on_text)
                    {
                        //LabelPW.Text = $"{pass_Cache.Substring(0, i+1)}{new string(passwordChar, pass_Cache.Length-i-1)}";
                        LabelPW.Text = $"{pass_Cache.Substring(0, i + 1)}";

                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 11, LabelPW.Font.Style);
                    }
                    else
                    {
                        LabelPW.Text = Personel.Sifre;
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 6, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 7, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 8, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 9, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 10, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 11, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 12, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 13, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 14, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 15, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 16, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 17, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 18, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 19, LabelPW.Font.Style);
                        await Task.Delay(100);
                        LabelPW.Font = new Font(LabelPW.Font.FontFamily, 20, LabelPW.Font.Style);
                    }
                }
            }
            else
            {
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 6, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 7, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 8, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 9, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 10, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 11, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 12, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 13, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 14, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 15, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 16, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 17, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 18, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 19, LabelPW.Font.Style);
                await Task.Delay(100);
                LabelPW.Text = Personel.Sifre;
                LabelPW.Font = new Font(LabelPW.Font.FontFamily, 20, LabelPW.Font.Style);
            }
            
        }

        private void LabelPW_MouseLeave(object sender, EventArgs e)
        {
            on_text = false;
            //LabelPW.Text = new string(passwordChar, LabelPW.Text.Length);
            LabelPW.Text = Personel.Sifre;
            LabelPW.Font = new Font(LabelPW.Font.FontFamily, 20, LabelPW.Font.Style);
        }
    }
}
