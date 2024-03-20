
#region Packages
using System;
#endregion

namespace NTP_P1
{
    internal class VerifyCode
    {
        #region Variable Definition
        public static Random rand = new Random();
        public static String Verify_Code;
        #endregion

        #region Generate Random Verify Code
        public static String GenerateVerifyCode()
        {
            Verify_Code = "";
            for (int i = 0; i < rand.Next(6, 8); i++)
            {
                Verify_Code += rand.Next(9).ToString();
            }
            return Verify_Code;
        }
        #endregion
    }
}