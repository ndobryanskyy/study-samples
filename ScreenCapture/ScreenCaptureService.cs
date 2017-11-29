using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows;

namespace ScreenCapture
{
    public class ScreenCaptureService
    {
        public Bitmap TakeScreenSnapshot()
        {
            var screenWidth = (int) SystemParameters.PrimaryScreenWidth;
            var screenHeight = (int)SystemParameters.PrimaryScreenHeight;

            var bitmap = new Bitmap(
                screenWidth, 
                screenHeight,
                PixelFormat.Format32bppArgb);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                float textWidth = 200;
                float textHeight = 50;

                g.DrawString(DateTime.Today.ToString("D"),
                    new Font("Tahoma", 16), Brushes.DarkRed, new RectangleF(screenWidth - textWidth - 50, screenHeight - textHeight - 50, textWidth, textHeight));

                g.Flush();
            }

            return bitmap;
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr onj);
    }
}