
#region Packages
using System;
using System.Drawing;
using System.Windows.Forms;
#endregion

namespace NTP_P1
{
    public partial class PersonelView : UserControl
    {
        #region Variable Definition
        public Personel Personel { get; set; }
        public static Update_Personel updatePersonel;
        #endregion

        #region Form Implementation
        public PersonelView()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Load Processes
        private void PersonelView_Load(object sender, EventArgs e)
        {
            if (Program.SettingsValues[1] != "2")
                this.BackColor = LoginPage.AccentCurrentColor;

            LabelMail.Text = Program.DecryptIt(Personel.Mail);
            LabelKA.Text = Program.DecryptIt(Personel.KullaniciAdi);
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

        #region Delete User
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(Program.Output[94], Program.Output[93], MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Database.Connect.Open();
                Database.Delete("Personel", this.IDLabel.Text);
                Database.Connect.Close();
                Program.mainPage.GetAllUsers();
                System.GC.Collect();
            }
            else
            {
                //Do Nothing
            }
        }
        #endregion

        #region Update User
        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (updatePersonel == null || updatePersonel.IsDisposed && !Update_Personel.AcikMi)
            {
                updatePersonel = new Update_Personel();
                Update_Personel.usernameCache = this.LabelKA.Text;
                updatePersonel.getDataFromSelectedUser(this.IDLabel.Text, this.LabelKA.Text, Program.DecryptIt(Personel.Sifre), this.LabelMail.Text, this.LabelTarih.Text, Personel.isRoot, Personel.isDefaultUser, Personel.isEmployee, Personel.Ad, Personel.Soyadi);
                Update_Personel.AdminOrDefault = false;
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
    }
}
