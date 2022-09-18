using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Meslin.Parameters;


namespace Meslin
{
    internal class LensImage
    {
        int width, height;
        Bitmap Pole;
        Graphics PoleGraphics;
        Pen blackPen = new Pen(Color.Black);
        Brush blackBrush = Brushes.Black;
        public LensImage(int width, int height)
        {
            this.width = width;
            this.height = height;
            Pole = new Bitmap(width, height);
            PoleGraphics = Graphics.FromImage(Pole);
        }
        public Bitmap GetImage()
        {
            int x0 = 0;
            int y0 = height / 2;
            double xsize = width * 5 / (6 * Math.Max(S1, S2));
            double ysize = height / (H * 3);

            DrawSetup(x0, y0);
            DrawLenses(x0, y0, xsize, ysize);
            DrawScreen(x0, xsize);
            DrawLines(x0, y0, xsize, ysize);

            return Pole;
        }

        private void DrawLines(int x0, int y0, double xsize, double ysize)
        {
            PoleGraphics.DrawLine(blackPen, x0, y0,
                (int)(x0 + L * xsize), (int)(y0 - H * ysize));
            PoleGraphics.DrawLine(blackPen, (int)(x0 + L * xsize),
                (int)(y0 - H * ysize), (int)(x0 + S1 * xsize), y0);
            PoleGraphics.DrawLine(blackPen, x0, y0,
                (int)(x0 + (L + x) * xsize), (int)(y0 + H * ysize));
            PoleGraphics.DrawLine(blackPen, (int)(x0 + (L + x) * xsize), (int)(y0 + H * ysize),
                (int)(x0 + S2 * xsize), y0);
        }

        private void DrawScreen(int x0, double size)
        {
            PoleGraphics.DrawLine(blackPen, x0 + (int)((Math.Min(S1, S2) + a) * size)
                , 0, x0 + (int)((Math.Min(S1, S2) + a) * size), height);
        }

        private void DrawLenses(int x0, int y0, double xsize, double ysize)
        {
            PoleGraphics.DrawLine(blackPen, (int)(x0 + L * xsize)
                            , y0, (int)(x0 + L * xsize), (int)(y0 - H * ysize));
            PoleGraphics.DrawLine(blackPen, (int)(x0 + (L + x) * xsize)
                , y0, (int)(x0 + (L + x) * xsize), (int)(y0 + H * ysize));
        }

        private void DrawSetup(int x0, int y0)
        {
            PoleGraphics.Clear(Color.White);
            PoleGraphics.FillEllipse(blackBrush, x0 - 5, y0 - 5, 10, 10);
            PoleGraphics.DrawLine(blackPen, x0, y0, width, y0);
        }
    }
}
