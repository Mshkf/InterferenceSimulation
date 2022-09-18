using System;

namespace Meslin
{
    internal static class Parameters
    {
        public static double F = 10d, H = 10d, L = 15d, x = 20d;
        public static double lambda = 600 * Math.Pow(10, -7);
        public static double aDiff = 0d;
        public static double a => aStart + aDiff;
        public static bool firstCase => S2 > S1;
        public static double aStart => (firstCase) ?
            (x * (x2 - x1)) / (x2 + x1 - x) :
            (x2 - x) * (x1 - x2) / (x2 + x1 - x);
        public static double S1 => (L * L) / (L - F);
        public static double S2 => (L + x) * (L + x) / (L - F + x);
        public static double x1 => L * F / (L - F);
        public static double x2 => x + (F * (L + x)) / (L - F + x);
        public static double ymax => (firstCase) ?
            H * (x2 - x1 - a) / (x2 - x) :
            H * (x1 - x2) / (x2 + x1 - x);
        public static double dl(double y) => (firstCase) ?
            (y * y / 2) *
            (x1 * x1 / (L * a * a) + x1 / (a * a) + 1 / a -
            (x2 - x) * (x2 - x) / ((L + x) * (x2 - x1 - a) * (x2 - x1 - a)) -
            (x1 - x + a) / ((x2 - x1 - a) * (x2 - x1 - a))) :
            (y * y / 2) *
            ((x2 - x) * (x2 - x) / ((L + x) * a * a) + (x2 - x + a) / (a * a) -
            (x1 * x1) / (L * (x1 - x2 - a) * (x1 - x2 - a)) -
            (x2 + a) / ((x1 - x2 - a) * (x1 - x2 - a)));
        public static double I(double y) =>
            2 * (1 + Math.Cos(2 * Math.PI * dl(y) / lambda));
    }
}
