using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Imaging;

namespace haisan.util
{
    class Util
    {
       public static byte[] ConvertImage(Image image)
        {
            Bitmap bmp = new Bitmap(image);
            BitmapData bitmapData = bmp.LockBits(new Rectangle(new Point(0, 0), image.Size), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            byte[] BGRValues = new byte[bitmapData.Stride * bitmapData.Height];

            IntPtr Ptr = bitmapData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(Ptr, BGRValues, 0, BGRValues.Length);

            bmp.UnlockBits(bitmapData);

            return BGRValues;
        }

        public static Image ReadImage(byte[] bytes)
        {
            MemoryStream ms = new System.IO.MemoryStream(bytes);
            return Image.FromStream(ms);
        }

        public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static bool isDecimal(string text)
        {
            try
            {
                decimal.Parse(text);
            }
            catch (Exception )
            {
                return false;
            }
            return true;
        }

        public static bool isAcceptByDecimal(char input)
        {
            return (input >= '0' && input <= '9') || input == '.' || input == 8;
        }
     
    }
}
