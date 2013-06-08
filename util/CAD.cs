using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace haisan.util
{

    struct BITMAPFILEHEADER
    {
        public short bfType;
        public int bfSize;
        public short bfReserved1;
        public short bfReserved2;
        public int bfOffBits;
    }

    class CAD
    {
        private static int EDGE = 2;
        public static System.Drawing.Image GetDwgImage(string FileName)
        {
            Console.WriteLine("Application.StartupPath:" + Application.StartupPath);

            if (!(File.Exists(FileName)))
            {
                throw new FileNotFoundException("文件没有被找到");
            }

            FileStream DwgF = null;   //文件流
            int PosSentinel;   //文件描述块的位置
            BinaryReader br = null;   //读取二进制文件
            int TypePreview;   //缩略图格式
            int PosBMP;    //缩略图位置 
            int LenBMP;    //缩略图大小
            short biBitCount; //缩略图比特深度 
            BITMAPFILEHEADER biH; //BMP文件头，DWG文件中不包含位图文件头，要自行加上去
            byte[] BMPInfo;    //包含在DWG文件中的BMP文件体
            MemoryStream BMPF = new MemoryStream(); //保存位图的内存文件流
            BinaryWriter bmpr = new BinaryWriter(BMPF); //写二进制文件类
            System.Drawing.Image myImg = null;


            try
            {

                DwgF = new FileStream(FileName, FileMode.Open, FileAccess.Read); //文件流

                br = new BinaryReader(DwgF);
                DwgF.Seek(13, SeekOrigin.Begin); //从第十三字节开始读取
                PosSentinel = br.ReadInt32();   //第13到17字节指示缩略图描述块的位置
                DwgF.Seek(PosSentinel + 30, SeekOrigin.Begin);   //将指针移到缩略图描述块的第31字节
                TypePreview = br.ReadByte();   //第31字节为缩略图格式信息，2 为BMP格式，3为WMF格式
                if (TypePreview == 1)
                {
                }
                else if (TypePreview == 2 || TypePreview == 3)
                {
                    PosBMP = br.ReadInt32(); //DWG文件保存的位图所在位置
                    LenBMP = br.ReadInt32(); //位图的大小
                    DwgF.Seek(PosBMP + 14, SeekOrigin.Begin); //移动指针到位图块
                    biBitCount = br.ReadInt16(); //读取比特深度
                    DwgF.Seek(PosBMP, SeekOrigin.Begin); //从位图块开始处读取全部位图内容备用
                    BMPInfo = br.ReadBytes(LenBMP); //不包含文件头的位图信息
                    br.Close();
                    DwgF.Close();
                    biH.bfType = 19778; //建立位图文件头
                    if (biBitCount < 9)
                    {
                        biH.bfSize = 54 + 4 * (int)(Math.Pow(2, biBitCount)) + LenBMP;
                    }
                    else
                    {
                        biH.bfSize = 54 + LenBMP;
                    }
                    biH.bfReserved1 = 0; //保留字节
                    biH.bfReserved2 = 0; //保留字节
                    biH.bfOffBits = 14 + 40 + 1024; //图像数据偏移
                    //以下开始写入位图文件头
                    bmpr.Write(biH.bfType); //文件类型
                    bmpr.Write(biH.bfSize);   //文件大小
                    bmpr.Write(biH.bfReserved1); //0
                    bmpr.Write(biH.bfReserved2); //0
                    bmpr.Write(biH.bfOffBits); //图像数据偏移
                    bmpr.Write(BMPInfo); //写入位图
                    BMPF.Seek(0, SeekOrigin.Begin); //指针移到文件开始处 
                    myImg = System.Drawing.Image.FromStream(BMPF); //创建位图文件对象                    
                    bmpr.Close();
                    BMPF.Close();
                }
                return myImg;
            }
            catch (EndOfStreamException)
            {
                throw new EndOfStreamException("文件不是标准的DWG格式文件，无法预览！");
            }
            catch (IOException ex)
            {
                if (ex.Message == "试图将文件指针移到文件开头之前。\r\n")
                {
                    throw new IOException("文件不是标准的DWG格式文件，无法预览！");
                }
                else if (ex.Message == "文件“" + FileName + "”正由另一进程使用，因此该进程无法访问该文件。")
                {
                    //复制文件，继续预览
                   
                    File.Copy(FileName, Application.StartupPath + @"\linshi.dwg", true);
                    Image image = GetDwgImage(Application.StartupPath + @"\linshi.dwg");
                    File.Delete(Application.StartupPath + @"\linshi.dwg");
                    return image;
                }
                else
                {
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (DwgF != null)
                {
                    DwgF.Close();
                }
                if (br != null)
                {
                    br.Close();
                }
                bmpr.Close();
                BMPF.Close();

            }
        }


        public static System.Drawing.Image getThumbnailDWG(string FilePath)
        {
            System.Drawing.Image image = GetDwgImage(FilePath);
            Bitmap bitmap = new Bitmap(image);
            int Height = bitmap.Height;
            int Width = bitmap.Width;
           
            Bitmap oldbitmap = (Bitmap)bitmap;

            Color pixel;
            int left = 0;
            int right = 0;
            int top = 0;
            int bottom = 0;

            pixel = oldbitmap.GetPixel(0,0);
          //  Console.WriteLine( "name:["+pixel.Name+"]");
                
            for (int x = 0; x < Width; x++)
            {
                pixel = oldbitmap.GetPixel(x, Height / 2);
           //     Console.WriteLine("x:["+x+"] y:["+Height/2+"] name:["+pixel.Name+"]");
            }

            for (int x = 0; x < Width; x ++)
            {
                pixel = oldbitmap.GetPixel(x, Height/2);
                if (pixel.Name.Equals(Parameter.GRAY))
                {
                    left++;
                }
                else
                    break;
            }
            left += EDGE;

            for (int x = Width - 1; x >= 0; x --)
            {
                pixel = oldbitmap.GetPixel(x, Height / 2);
                if (pixel.Name.Equals(Parameter.GRAY))
                {
                    right++;
                }
                else
                    break;
            }
            right += EDGE; // right的边的宽度为两像素

            for (int y = 0; y < Height; y ++)
            {
                pixel = oldbitmap.GetPixel(Width / 2, y);
                if (pixel.Name.Equals(Parameter.GRAY))
                {
                    top++;
                }
                else
                    break;
            }
            top += EDGE;


            for (int y = Height - 1; y >= 0; y --)
            {
                pixel = oldbitmap.GetPixel(Width / 2, y);
                if (pixel.Name.Equals(Parameter.GRAY))
                {
                    bottom++;
                }
                else
                    break;
            }
            bottom += EDGE; //底部边宽度为2像素

            int newWidth = Width - left - right + 1;
            int newHight = Height - top - bottom + 1;
            Bitmap newbitmap = new Bitmap(newWidth, newHight);

           // Console.WriteLine("newWidth:["+newWidth+"] newHeight:["+newHight+"]");
            //Color pixel;
            for (int x = left; x < left  + newWidth; x++)
            {
                for (int y = top; y < top  + newHight; y++)
                {
                    pixel = oldbitmap.GetPixel(x, y);
                    newbitmap.SetPixel(x - left, y - top, Color.FromArgb(pixel.R, pixel.G, pixel.B));
                }
            }
            return newbitmap;
        }

    }
}
