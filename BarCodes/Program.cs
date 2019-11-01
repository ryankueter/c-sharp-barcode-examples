/*
 * Author: Ryan Kueter
 * Copyright 2019 Ryan Kueter.
 */
using System;
using System.Drawing;

namespace BarCodes
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateBarCode("Test 123", @"C:\MyFolder\test.gif");
        }

        static void CreateBarCode(string text, string path)
        {
            // Get the font
            System.Drawing.Text.PrivateFontCollection privateFonts = new System.Drawing.Text.PrivateFontCollection();
            privateFonts.AddFontFile("barcode.ttf");
            Font fn = new System.Drawing.Font(privateFonts.Families[0], 72, FontStyle.Regular, GraphicsUnit.Point);

            // Measure the barcode on a temporary drawing surface:
            Bitmap bmTemp = new Bitmap(100, 100, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Graphics grTemp = Graphics.FromImage(bmTemp);
            SizeF sf = grTemp.MeasureString(text, fn);

            // Draw the barcode on a bitmap:
            Bitmap bm = new Bitmap(Convert.ToInt32(sf.Width), Convert.ToInt32(sf.Height), System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            
            Graphics gr = Graphics.FromImage(bm);
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
            gr.FillRectangle(Brushes.White, 0, 0, sf.Width, sf.Height);
            gr.DrawString(text, fn, Brushes.Black, 0, 0);

            //Stream the image back as a GIF:
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bm.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            bm.Save(path);
        }
    }
}
