
#region Packages
using System;
using System.Security.Cryptography;
using System.Text;
#endregion

namespace NTP_P1
{
    class hashCoder
    {
        #region Hash256 Encoder
        public static string Hash256(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        #endregion

        #region Hash512 Encoder
        public static string Hash512(string password)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        #endregion

        #region Double Hash512 Encoder
        public static string DoubleHash(string input)
        {
            return Hash512(Hash512(input));
        }
        #endregion
    }
}
