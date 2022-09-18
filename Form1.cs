using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meslin
{
    public partial class Form1 : Form
    {
        LensImage lensImage;
        InterferenceImage interferenceImage;
        MyTrackbar myTrackbar;
        Recording recording;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lensImage = new LensImage(pictureBox1.Width, pictureBox1.Height);
            interferenceImage = new InterferenceImage(pictureBox2.Width, pictureBox2.Height);
            myTrackbar = new MyTrackbar(ADiffTrackBar.Maximum, ADiffTrackBar.Minimum);
            ADiffTrackBar.Value = myTrackbar.GetValue(ADiffTrackBar.Value);
            recording = new Recording();
            UpdateScreen();
        }

        private void UpdateScreen()
        {
            aTextBox.Text = Parameters.a.ToString();
            ymaxTextBox.Text = Parameters.ymax.ToString();
            ADiffTrackBar.Value = myTrackbar.GetValue(ADiffTrackBar.Value);
            pictureBox1.Image = lensImage.GetImage();
            pictureBox2.Image = interferenceImage.GetImage();
        }

        private void FTextBox_TextChanged(object sender, EventArgs e)
        {
            TryChange(ref Parameters.F, FTextBox.Text);
            Parameters.aDiff = 0d;
        }

        private void TryChange(ref double a, string text)
        {
            if (double.TryParse(text, out double b))
            {
                a = double.Parse(text);
                UpdateScreen();
            }
        }

        private void LTextBox_TextChanged(object sender, EventArgs e)
        {
            TryChange(ref Parameters.L, LTextBox.Text);
            Parameters.aDiff = 0d;
        }

        private void xTextBox_TextChanged(object sender, EventArgs e)
        {
            TryChange(ref Parameters.x, xTextBox.Text);
            Parameters.aDiff = 0d;
        }

        private void HTextBox_TextChanged(object sender, EventArgs e)
        {
            TryChange(ref Parameters.H, HTextBox.Text);
            Parameters.aDiff = 0d;
        }

        private void lambdaTextBox_TextChanged(object sender, EventArgs e)
        {
            if (double.TryParse(lambdaTextBox.Text, out double b))
            {
                Parameters.lambda = double.Parse(lambdaTextBox.Text) * Math.Pow(10, -7);
                UpdateScreen();
            }
            Parameters.aDiff = 0d;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            interferenceImage.AdjustScale();
            ScaleTextBox.Text = interferenceImage.scale.ToString();
            UpdateScreen();
        }

        private void ScaleTextBox_TextChanged(object sender, EventArgs e)
        {
            TryChange(ref interferenceImage.scale, ScaleTextBox.Text);
        }

        private void ADiffTrackBar_Scroll(object sender, EventArgs e)
        {
            myTrackbar.SetValue(ADiffTrackBar.Value);
            UpdateScreen();
        }


        private void RecordButton_Click(object sender, EventArgs e)
        {
            recording.SetParameters(BottomTextBox1.Text,
                TopTextBox.Text, StepTextBox.Text, yTextBox.Text);
            switch (ParCcomboBox.SelectedIndex)
            {
                case 0:
                    recording.SaveFile(ref Parameters.F);
                    break;
                case 1:
                    recording.SaveFile(ref Parameters.L);
                    break;
                case 2:
                    recording.SaveFile(ref Parameters.x);
                    break;
                case 3:
                    recording.SaveFile(ref Parameters.lambda);
                    break;
                default:
                    break;
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
