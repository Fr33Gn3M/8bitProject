namespace TH.ServerFramework.Utility
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;

    public static class GraphicsExtensions
    {
        public static void DrawImage(this Graphics source, Image image, double opacity)
        {
            var imgAttr = new ImageAttributes();
            float[][] colorMatrixArr = {new float[] {1, 0, 0, 0, 0},
                                               new float[] {0, 1, 0, 0, 0},
                                               new float[] {0, 0, 1, 0, 0},
                                              new float[] {0, 0, 0, (float)opacity, 0},
                                               new float[] {0, 0, 0, 0, 1}};
            var colorMatrix = new ColorMatrix(colorMatrixArr);
            imgAttr.SetColorMatrix(colorMatrix);
            source.DrawImage(image, new Rectangle(new Point(0, 0), image.Size), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imgAttr);
        }
    }

}

