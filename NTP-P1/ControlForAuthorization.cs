using LoginPage_ForgetPassPage;
using NTP_P1;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginPage_ForgetPassPage
{
    internal static class ControlForAuthorization
    {
        #region definements
        private static Boolean Root = false;
        private static Boolean DefaultUser = false;
        public static string authorityLabels = "Çalışan";
        #endregion



        #region Authorization Controls
        public static void IsAuthority(string root, string defaultuser)//will change defined values according to login  
        {
            if (root == "True")
            {
                Root = true;
                authorityLabels = "Root";
            }

            else if (defaultuser == "True")
            {
                DefaultUser = true;
                authorityLabels = "Kullanıcı";
            }
            else
            {
                authorityLabels = "Çalışan";
            }

        }

        public static Boolean isRoot()// checks if Root is true
        {
            if (Root == true)
                return true;
            return false;
        }

        public static Boolean isDefaultUser()// checks if SatisBirimi is true
        {
            if (DefaultUser == true)
                return true;
            return false;
        }
        #endregion



        #region Entry Control Functions
        public static void EntryKontrolRoot(char controlType, Form sender)//Will check Root for Yonetim  
        {
            if (Root == true)
            {
                //Program.adminpanel.YonetimForward(controlType, sender);
            }
            else if (Root == false)
            {
                MessageBox.Show("Üzgünüz Bu Alana Erişim Yetkiniz Bulunmamaktadır");
            }

        }
        public static void EntryKontrolSatisBirimi(Form target, Form sender) // Will check Root and SatisBirimi for all except Yonetim
        {
            if (DefaultUser == true || Root == true)
            {
                Program.ChangeForm(target, sender);
            }
            else
            {
                MessageBox.Show("Üzgünüz Bu Alana Erişim Yetkiniz Bulunmamaktadır");
            }
        }
        #endregion


        public static void LogOut(Form hide)//This will be called when logout buttons clicked
        {

            Root = false;
            DefaultUser = false;

            Program.loginPage.EmptyEntryValues();

            Program.loginPage.Show();
            hide.Hide();

        }

    }
}