using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Meslin.Parameters;

namespace Meslin
{
    internal class InterferenceImage
    {
        public double scale = 40000;
        int width, height;
        int numberOfRings = 40;
        Bitmap Pole;
        Graphics PoleGraphics;
        Brush orangeBrush = Brushes.Orange, whiteBrush = Brushes.White;
        public InterferenceImage(int width, int height)
        {
            this.width = width;
            this.height = height;
            Pole = new Bitmap(width, height);
            PoleGraphics = Graphics.FromImage(Pole);
        }
        public Bitmap GetImage()
        {
            float x0 = width / 2;
            float y0 = (firstCase) ? 0 : height;

            PoleGraphics.Clear(Color.White);
            var yArray = CalculateYArray();

            for (int i = yArray.Length - 1; i > -1; i--)
            {
                if (i % 2 == 0)
                    PoleGraphics.FillEllipse(whiteBrush, x0 - (float)(yArray[i] * scale / 2),
                        y0 - (float)(yArray[i] * scale / 2), (float)(yArray[i] * scale),
                        (float)(yArray[i] * scale));
                else
                    PoleGraphics.FillEllipse(orangeBrush, x0 - (float)(yArray[i] * scale / 2),
                        y0 - (float)(yArray[i] * scale / 2), (float)(yArray[i] * scale),
                        (float)(yArray[i] * scale));
            }

            return Pole;
        }

        public void AdjustScale()
        {
            var yArray = CalculateYArray();
            scale = (float)(2 * height / yArray[yArray.Length - 1]);
        }

        private double[] CalculateYArray()
        {
            var yArray = new double[numberOfRings];
            double y = 0;
            double l = 0;
            double multiplier = (firstCase) ? //here for performance inprovement
                Math.Abs(2 / (
                x1 * x1 / (L * a * a) + x1 / (a * a) + 1 / a -
                (x2 - x) * (x2 - x) / ((L + x) * (x2 - x1 - a) * (x2 - x1 - a)) -
                (x1 - x + a) / ((x2 - x1 - a) * (x2 - x1 - a))
                )) :
                Math.Abs(2 / (
                (x2 - x) * (x2 - x) / ((L + x) * a * a) + (x2 - x + a) / (a * a) -
                (x1 * x1) / (L * (x1 - x2 - a) * (x1 - x2 - a)) -
                (x2 + a) / ((x1 - x2 - a) * (x1 - x2 - a))
                ));

            for (int i = 0; i < yArray.Length; i++)
            {
                yArray[i] = y;
                l += lambda / 2;
                y = Math.Sqrt(l * multiplier);
            }
            return yArray;
        }
    }
}
