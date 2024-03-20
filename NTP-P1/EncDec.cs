#region Packages
#endregion

namespace NTP_P1
{
    public class EncDec
    {

        #region KEYs
        private static readonly int Key = 5; // You can adjust the key value
        public static string AddedKey = "BDEFUVhsvwzAJpqK23gRlmnNcdLijPXYzWxT4567890IkQbefoCaCz";
        #endregion

        #region Encrypt and Decrypt Methods
        public static string Encrypt(string input)
        {
            char[] inputArray = input.ToCharArray();

            for (int i = 0; i < inputArray.Length; i++)
            {
                // Encrypt each character
                inputArray[i] = (char)(inputArray[i] + Key);
            }

            return new string(inputArray);
        }

        public static string Decrypt(string encrypted)
        {
            char[] encryptedArray = encrypted.ToCharArray();

            for (int i = 0; i < encryptedArray.Length; i++)
            {
                // Decrypt each character
                encryptedArray[i] = (char)(encryptedArray[i] - Key);

            }
            return new string(encryptedArray);
        }

        public static string EncryptE(string input)
        {
            char[] inputArray = input.ToCharArray();

            for (int i = 0; i < inputArray.Length; i++)
            {
                // Encrypt each character
                inputArray[i] = (char)(inputArray[i] + Key);

                // Handle overflow
                if (inputArray[i] > char.MaxValue)
                {
                    inputArray[i] = (char)(inputArray[i] - char.MaxValue - 1);
                }
            }

            return new string(inputArray);
        }

        public static string DecryptE(string encrypted)
        {
            char[] encryptedArray = encrypted.ToCharArray();

            for (int i = 0; i < encryptedArray.Length; i++)
            {
                // Decrypt each character
                encryptedArray[i] = (char)(encryptedArray[i] - Key);

                // Handle underflow
                if (encryptedArray[i] < char.MinValue)
                {
                    encryptedArray[i] = (char)(encryptedArray[i] + char.MaxValue + 1);
                }
            }

            return new string(encryptedArray);
        }
        public static string EncryptA(string input)
        {
            char[] inputArray = input.ToCharArray();

            for (int i = 0; i < inputArray.Length; i++)
            {
                // Encrypt each character using modular arithmetic
                inputArray[i] = (char)((inputArray[i] + Key - char.MinValue) % (char.MaxValue - char.MinValue + 1) + char.MinValue);
            }

            return new string(inputArray);
        }

        public static string DecryptA(string encrypted)
        {
            char[] encryptedArray = encrypted.ToCharArray();

            for (int i = 0; i < encryptedArray.Length; i++)
            {
                // Decrypt each character using modular arithmetic
                encryptedArray[i] = (char)((encryptedArray[i] - Key - char.MinValue + char.MaxValue + 1) % (char.MaxValue - char.MinValue + 1) + char.MinValue);
            }

            return new string(encryptedArray);
        }
        #endregion

    }
}