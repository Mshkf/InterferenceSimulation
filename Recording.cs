using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Meslin.Parameters;

namespace Meslin
{
    internal class Recording
    {
        double bottom, top, step, y;
        public void SetParameters(string bottom, string top, string step,string y)
        {
            this.bottom = double.Parse(bottom);
            this.top = double.Parse(top);
            this.step = double.Parse(step);
            this.y = double.Parse(y);
        }

        public void SaveFile(ref double x)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Document (*.txt)|*.txt|All Files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog.FileName, false, Encoding.Unicode);
                for (x = bottom; x <= top; x += step)
                {
                    sw.WriteLine(x.ToString() + "\t" + I(y).ToString());
                }
                sw.Close();
            }
        }
    }
}
