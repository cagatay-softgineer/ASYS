
#region Packages
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
#endregion

namespace NTP_P1
{
    internal class ImageLib
    {
        #region Image to String Methods 
        public static string BasicImageToBase64(Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, image.RawFormat);
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        public static string JpegImageToBase64(Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the image to the memory stream in a specific format (JPEG in this case)
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                // Convert the byte array to Base64 string
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }

        public static byte[] ImageToByteArray(Image image)
        {
            MemoryStream memoryStream = new MemoryStream();
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            return memoryStream.ToArray();
        }

        public static Image ByteArrayToImage(byte[] imageData)
        {
            using (MemoryStream memoryStream = new MemoryStream(imageData))
            {
                // Specify the image format based on your original image format
                return Image.FromStream(memoryStream);
            }
        }

        public static Image JpegBase64ToImage(string base64String)
        {
            try
            {
                // Convert the Base64 string to a byte array
                byte[] imageBytes = Convert.FromBase64String(base64String);

                // Create a memory stream from the byte array
                using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                {
                    // Create an Image from the memory stream
                    Image image = Image.FromStream(memoryStream);
                    return image;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", Program.Output[19]);
                return null;
            }
        }

        public static string ImageToBase64(Image image, long quality = 50L)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Get the JPEG codec info
                ImageCodecInfo jpegCodec = GetEncoderInfo(ImageFormat.Jpeg);

                // Set quality parameters for the encoder
                EncoderParameters encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                // Save the image to the MemoryStream with specified quality
                image.Save(memoryStream, jpegCodec, encoderParameters);

                // Convert the stream to a byte array and then to Base64
                byte[] imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }


        // Get the codec info for the specified image format
        private static ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            // Default to JPEG if not found
            return codecs[1]; // JPEG
        }


        public static Image Base64ToImage(string base64String)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64String);

                // Byte dizisi bir resim dosyasını temsil etmiyorsa hata işleme
                if (IsImage(imageBytes))
                {
                    using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                    {
                        return Image.FromStream(memoryStream);
                    }
                }
                else
                {
                    MessageBox.Show(Program.Output[106], Program.Output[19]);
                    return null; // veya başka bir işlem
                }
            }
            catch (System.FormatException ex)
            {
                // Geçersiz base64 kodu durumunda hata işleme
                MessageBox.Show($"{ex.Message}", Program.Output[107]);
                return null; // veya başka bir işlem
            }
        }
        #endregion

        #region IsImage Check
        private static bool IsImage(byte[] bytes)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    Image.FromStream(ms).Dispose();
                }
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }
        #endregion

        #region Image Select From Folder
        public static Image SelectImageFromFolder()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = Program.Output[108];
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (Image.FromFile(openFileDialog.FileName).Width <= 1024 && Image.FromFile(openFileDialog.FileName).Height <= 1024)
                        {
                            FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                            // Check if the file size is larger than 512 KB (512 * 1024 bytes)
                            if (fileInfo.Length > 512 * 1024)
                            {
                                MessageBox.Show(Program.Output[109]);
                                return null;
                            }
                            else
                            {
                                return Image.FromFile(openFileDialog.FileName);
                            }
                        }
                        else
                        {
                            MessageBox.Show(Program.Output[110]);
                            return null;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}", Program.Output[19]);
                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
        }

        public static Image SelectImageFromFolderWithoutControl()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = Program.Output[108];
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {

                        FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                        return Image.FromFile(openFileDialog.FileName);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex.Message}", Program.Output[19]);
                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
        }
        #endregion
    }
}