using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Meslin.Parameters;

namespace Meslin
{
    internal class MyTrackbar
    {
        int max, min, stepscount;
        public MyTrackbar(int max, int min)
        {
            this.max = max;
            this.min = min;
            stepscount = max - min + 1;
        }
        public int GetValue(int value)
        {
            double stepsvalue = (Math.Max(S1, S2) - Math.Min(S1, S2)) / stepscount;
            int result = (int)(a / stepsvalue);
            return (result >= min && result <= max) ? result : value;
        }
        public void SetValue(int steps)
        {
            double stepsvalue = (Math.Max(S1, S2) - Math.Min(S1, S2)) / stepscount;
            aDiff = stepsvalue * steps - aStart;
        }
    }
}
