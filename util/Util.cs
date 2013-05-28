using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace haisan.util
{
    class Util
    {

       private static string regexOfLengthOrWidth = "^(0|[1-9][0-9]*|[1-9][0-9]*\\([1-9][0-9]*\\))$";
       private static string regexOfDigital = "^(0|[1-9][0-9]*)$";

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
     
        public static string constructIds(DataGridView dataGridView)
        {
            StringBuilder ids = new StringBuilder();
            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                ids.Append(row.Cells["Id"].Value + ",");
            }

            if (ids.Length > 0)
                ids.Remove(ids.Length - 1, 1);

            return ids.ToString();
        }

        public static bool isLengthOrWidth(string text)
        {
            Regex regex = new Regex(regexOfLengthOrWidth);
            return regex.IsMatch(text);
        }

        public static bool isDigital(string text)
        {
            Regex regex = new Regex(regexOfDigital);
            return regex.IsMatch(text);
        }

        //L: length, W: width, T:thickness
        public static int getValueOfLWT(string next)
        {
            int index = next.IndexOf("(");
            if (-1 != index)
            {
                return int.Parse(next.Substring(index + 1, next.IndexOf(")") - index - 1));
            }
            return int.Parse(next);
        }

        //L: length, W: width, T:thickness
        public static int getValueOfLWT2(string next)
        {
            int index = next.IndexOf("(");
            if (-1 != index)
            {
                return int.Parse(next.Substring(0, index - 1));
            }
            return int.Parse(next);
        }

    }
}
