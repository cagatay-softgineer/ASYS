
#region Packages
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#endregion

namespace NTP_P1
{
    internal class Crypt
    {
        #region KEYs
        public static string[] cookbook = { "!", "#", "$", "%", "&", "'", "(", ")", "*", "+", ",", "-", ".", "/", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", ":", ";", "<", "=", ">", "?", "@", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "[", "]", "^", "_", "`", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "{", "|", "}", "~" };
        public static string key = "BDEFUVhsvwzAJpqK23gRlmnNcdLijPXYzWxT4567890IkQbefoCaCzK3hG7jL9mN2pR8q";
        public static long memory;
        #endregion

        #region CookBook Encoder
        public static string ConvertToCookBook(long input)
        {
            int count = 0;
            string save_data = "0";
            count = 0;
            memory = input;
            while (cookbook.Length < memory)
            {
                memory = memory - cookbook.Length;
                count += 1;
            }

            if (count == 0)
            {
                save_data = cookbook[memory - 1];
            }
            else
            {
                save_data = cookbook[memory - 1] + count;
            }
            return save_data;
        }

        public static string ConvertToCookBookA(string input)
        {
            int count = 0;
            string save_data = "0";

            string[] chunks = input.Split(new string[] { "99199" }, StringSplitOptions.None);
            string result = "";
            foreach (string chunk in chunks)
            {
                count = 0;
                memory = Convert.ToInt64(chunk);
                while (cookbook.Length < memory)
                {
                    memory = memory - cookbook.Length;
                    count += 1;
                }

                if (count == 0)
                {
                    save_data = cookbook[memory - 1];
                }
                else
                {
                    save_data = cookbook[memory - 1] + count;
                }
                result += Convert.ToString(save_data + " ");
            }
            return result.TrimEnd(' ');
        }
        public static string ConvertFromCookBookA(string input)
        {
            int cookbookData = 0;
            string countS = "0";
            string[] chunks = input.Split(new string[] { " " }, StringSplitOptions.None);
            string resultA = "";
            foreach (string chunk in chunks)
            {
                if (!string.IsNullOrEmpty(chunk))
                {
                    string memoryS = chunk[0].ToString();

                    for (int k = 0; k < cookbook.Length; k++)
                    {
                        if (cookbook[k] == memoryS)
                        {
                            cookbookData = k + 1;
                            countS = null;

                            if (chunk.Length > 1)
                            {
                                for (int y = 1; y < chunk.Length; y++)
                                {
                                    countS += chunk[y];
                                }

                                if (int.TryParse(countS, out int result))
                                {
                                    int count = result;

                                    while (count != 0)
                                    {
                                        cookbookData = cookbookData + cookbook.Length;
                                        count += -1;
                                    }
                                }
                            }
                        }
                    }
                }
                resultA += Convert.ToString(cookbookData + "99199");
            }

            return resultA.TrimEnd('9', '9', '1', '9', '9');
        }
        public static string ConvertLargeNumberToCookBook(string input)
        {
            string[] chunks = input.Split(new string[] { "99199" }, StringSplitOptions.None);
            StringBuilder result = new StringBuilder();

            foreach (string chunk in chunks)
            {
                result.Append(ConvertToCookBook(int.Parse(chunk)) + " ");
            }
            if (result.Length >= 1)
            {
                result.Length -= 1;
            }
            return result.ToString();
        }
        public static string DecodeCookBook(string encodedInput)
        {
            string[] chunks = encodedInput.Split(new string[] { " " }, StringSplitOptions.None);
            StringBuilder result = new StringBuilder();

            foreach (string chunk in chunks)
            {
                result.Append(ConvertFromCookBook(chunk) + "99199");
            }
            if (result.Length >= 5)
            {
                result.Length -= 5;
            }
            return result.ToString();
        }

        public static int ConvertFromCookBook(string input)
        {
            int cookbookData = 0;
            string countS = "0";

            if (!string.IsNullOrEmpty(input))
            {
                string memoryS = input[0].ToString();

                for (int k = 0; k < cookbook.Length; k++)
                {
                    if (cookbook[k] == memoryS)
                    {
                        cookbookData = k + 1;
                        countS = null;

                        if (input.Length > 1)
                        {
                            for (int y = 1; y < input.Length; y++)
                            {
                                countS += input[y];
                            }

                            if (int.TryParse(countS, out int result))
                            {
                                int count = result;

                                while (count != 0)
                                {
                                    cookbookData = cookbookData + cookbook.Length;
                                    count += -1;
                                }
                            }
                        }
                    }
                }
            }

            return cookbookData;
        }
        #endregion

        #region All in One Encoder and Decoder
        public static string AllInOneE(string input)
        {
            string step1 = SameLong.Encode(input, key);
            string step2 = Encrypt(step1);
            string step3 = EncodeToNumbers(step2).TrimEnd('9', '9', '1', '9', '9');
            string step4 = ConvertLargeNumberToCookBook(step3);
            string step5 = EncodeToNumbers(step4).TrimEnd('9', '9', '1', '9', '9');
            string step6 = ConvertToCookBookA(step5);
            string step7 = SameLong.Encode(step6, key);

            return step7;
        }
        public static string AllInOneD(string input)
        {
            string step1 = SameLong.Decode(input, key);
            string step2 = ConvertFromCookBookA(step1);
            string step3 = DecodeFromNumbers(step2);
            string step4 = DecodeCookBook(step3);
            string step5 = DecodeFromNumbers(step4);
            string step6 = Decrypt(step5);
            string step7 = SameLong.Decode(step6, key);

            return step7;
        }
        #endregion

        #region All in One Encoder and Decoder FOR MAIL
        public static string AllInOneEMAIL(string input)
        {
            string step2 = Encrypt(input);
            string step3 = EncodeToNumbers(step2).TrimEnd('9', '9', '1', '9', '9');
            string step4 = ConvertLargeNumberToCookBook(step3);


            return step4;
        }
        public static string AllInOneDMAIL(string input)
        {
            string step4 = DecodeCookBook(input);
            string step5 = DecodeFromNumbers(step4);
            string step6 = Decrypt(step5);

            return step6;
        }
        #endregion

        #region Simple Encoder and Decoder
        public static string Encrypt(string input)
        {
            return EncDec.EncryptA(input);
        }

        public static string Decrypt(string input)
        {
            return EncDec.DecryptA(input);
        }
        #endregion

        #region Number Encoder and Decoder
        public static string EncodeToNumbers(string input)
        {
            StringBuilder encoded = new StringBuilder();

            foreach (char c in input)
            {
                // Append the ASCII value of each character with separator
                encoded.Append(((int)c).ToString() + "99199");
            }

            // Remove the trailing separator
            return encoded.ToString();  //  .TrimEnd('9','9', '1', '9', '9');
        }

        public static string DecodeFromNumbers(string encodedInput)
        {
            // Split the encoded string into individual numbers
            string[] numberStrings = encodedInput.Split(new[] { "99199" }, StringSplitOptions.None);

            StringBuilder decoded = new StringBuilder();

            foreach (string numberString in numberStrings)
            {
                if (int.TryParse(numberString, out int asciiValue))
                {
                    // Convert the ASCII value to a character
                    decoded.Append((char)asciiValue);
                }
            }

            return decoded.ToString();
        }
        #endregion

        #region Last Encrypted File Finder
        public static string GetLastEncryptedFile(string directoryPath, string encryptedExtension)
        {
            try
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);

                // Get all files with the specified extension in the directory
                var encryptedFiles = directoryInfo.GetFiles($"*{encryptedExtension}")
                    .OrderByDescending(file => file.CreationTime)
                    .ToList();

                // Return the full path of the last file
                return encryptedFiles.FirstOrDefault()?.FullName;
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"{ex.Message}", Program.Output[19]);
                return null;
            }
        }
        #endregion
    }
    public class SameLong
    {
        #region Hash256xKey Encoder and Decoder
        public static string Encode(string text, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetValidKey(key, aesAlg.KeySize);
                aesAlg.IV = new byte[16]; // Use a random IV for each encryption

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(text);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decode(string encodedText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetValidKey(key, aesAlg.KeySize);
                aesAlg.IV = new byte[16]; // Use the same IV used for encryption

                try
                {
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(encodedText)))
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
                catch (FormatException ex)
                {
                    // MessageBox.Show($"{ex.Message}", Program.Output[19]);
                    // Handle the error as needed
                    return string.Empty; // Or throw an exception or return an error value
                }
            }
        }

        public static byte[] GetValidKey(string key, int keySize)
        {
            using (SHA512 sha256 = SHA512.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                return hashedBytes.Take(keySize / 8).ToArray();
            }
        }
        #endregion
    }
    public class FileCrpyt
    {
        #region File Encoder and Decoder
        public static void EncryptFile(string inputFile, string outputFile, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetValidKey(key, aesAlg.KeySize);
                aesAlg.IV = new byte[16]; // You may need to generate a random IV for each file

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                using (CryptoStream cs = new CryptoStream(fsOutput, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    int read;
                    byte[] buffer = new byte[4096];

                    while ((read = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cs.Write(buffer, 0, read);
                    }
                }
            }


            File.Delete(inputFile);
            MessageBox.Show(Program.Output[103]);

        }

        public static void EncryptFileSilent(string inputFile, string outputFile, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetValidKey(key, aesAlg.KeySize);
                aesAlg.IV = new byte[16]; // You may need to generate a random IV for each file

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                using (CryptoStream cs = new CryptoStream(fsOutput, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    int read;
                    byte[] buffer = new byte[4096];

                    while ((read = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cs.Write(buffer, 0, read);
                    }
                }
            }


            File.Delete(inputFile);
            //MessageBox.Show(Program.Output[103]);

        }

        public static void DecryptFile(string inputFile, string outputFile, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetValidKey(key, aesAlg.KeySize);
                aesAlg.IV = new byte[16]; // You may need to use the same IV used during encryption

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                //using (FileStream fsOutput = new FileStream(outputile, FileMode.Create))
                using (CryptoStream cs = new CryptoStream(fsOutput, aesAlg.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    int read;
                    byte[] buffer = new byte[4096];

                    while ((read = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cs.Write(buffer, 0, read);
                    }

                }


            }
            Program.mainPage.finishProcessWithAnim();
            MessageBox.Show(Program.Output[104]);

        }

        public static void DecryptFileSilent(string inputFile, string outputFile, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetValidKey(key, aesAlg.KeySize);
                aesAlg.IV = new byte[16]; // You may need to use the same IV used during encryption

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                //using (FileStream fsOutput = new FileStream(outputile, FileMode.Create))
                using (CryptoStream cs = new CryptoStream(fsOutput, aesAlg.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    int read;
                    byte[] buffer = new byte[4096];

                    while ((read = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cs.Write(buffer, 0, read);
                    }

                }


            }

            //MessageBox.Show(Program.Output[104]);
        }

        public static Image DecryptImage(string inputFile, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = GetValidKey(key, aesAlg.KeySize);
                aesAlg.IV = new byte[16]; // You may need to use the same IV used during encryption

                using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
                using (MemoryStream msOutput = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(msOutput, aesAlg.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    int read;
                    byte[] buffer = new byte[4096];

                    while ((read = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cs.Write(buffer, 0, read);
                    }

                    // cs will be closed automatically when exiting the using block

                    // Create an Image from the decrypted bytes in the MemoryStream
                    msOutput.Position = 0; // Reset the position before creating the Image
                    Image decryptedImage = Image.FromStream(msOutput);

                    return decryptedImage;
                }
            }
        }

        public static byte[] GetValidKey(string key, int keySize)
        {
            using (SHA512 sha256 = SHA512.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(key));
                return hashedBytes.Take(keySize / 8).ToArray();
            }
        }
        #endregion
    }
}