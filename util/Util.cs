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
using CrystalDecisions.Shared;

namespace haisan.util
{
    class Util
    {

       private static string regexOfLengthOrWidth = "^([1-9][0-9]*|[1-9][0-9]*\\([1-9][0-9]*\\))$";
       private static string regexOfDigital = "^(0|[1-9][0-9]*)$";
       private static string regexOfDigitalGreaterZero = "^([1-9][0-9]*)$";

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
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
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

        public static bool isDigitalGreaterZero(string text)
        {
            Regex regex = new Regex(regexOfDigitalGreaterZero);
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

        public static int getIntValue(string str)
        {
            int value;
            if(int.TryParse(str,out value))
                return value;
            return 0;
        }

        // 如果str为"000", "0.00", ".00"返回的结果都是0
        public static decimal getDecimalValue(string str)
        {
            decimal  value;
            if (decimal.TryParse(str, out value))
                return value;
            return 0;
        }

        public static bool isZeroDecimal(string str)
        {
            return 0 == getDecimalValue(str);
        }

        public static void showError(string message)
        {
            MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void showWarning(string message)
        {
            MessageBox.Show(message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void showInformation(string message)
        {
            MessageBox.Show(message, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static Image getThumbnailImage(Image image)
        {
            if (null == image) return null;

            Image ReducedImage;
            Image.GetThumbnailImageAbort callb = new Image.GetThumbnailImageAbort(ThumbnailCallback);

            ReducedImage = image.GetThumbnailImage(Parameter.THUMBNAIL_LENGTH, Parameter.THUMBNAIL_WIDTH, callb, IntPtr.Zero);

            return ReducedImage;
        }

        public static byte[] getBytes(string fileName)
        {
            FileStream dwgF = new FileStream(fileName, FileMode.Open, FileAccess.Read); //文件流
            byte[] bytes = new byte[dwgF.Length];
            dwgF.Read(bytes, 0, Convert.ToInt32(dwgF.Length));
            dwgF.Close();

            return bytes;
        }

        public static bool ThumbnailCallback()
        {
            return false;
        }

        public static void addParameterField(ParameterFields paramFields, string name, string value)
        {
            ParameterField paramField = new ParameterField();
            ParameterDiscreteValue paramDiscreteValue = new ParameterDiscreteValue();
            paramField.Name = name;
            paramDiscreteValue.Value = value;
            paramField.CurrentValues.Add(paramDiscreteValue);
            paramField.HasCurrentValue = true;
            paramFields.Add(paramField);
        }

        
    }
}
