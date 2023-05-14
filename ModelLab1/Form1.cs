using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ModelLab1
{
    public partial class Form1 : Form
    {
        const double limit = 1;
        public Form1()
        {
            InitializeComponent();
            chart1.Series["Series1"]["PixelPointWidth"] = "14";
            chart1.ChartAreas[0].AxisX.Minimum = 0;
        }

        double Func(double l, double x)
        {
            return 2 - 2 * Math.Sqrt(1 - x);
        }

        double dplus(double[] num)
        {
            double[] d1 = new double[num.Length];
            int n = num.Length;
            for (int i = 0; i < num.Length; i++)
            {
                d1[i] = ((i+1) / (double)n) - num[i];
            }
            return d1.Max();

        }

        double dminus(double[] num)
        {
            double[] d2 = new double[num.Length];
            int n = num.Length;
            for (int i = 0; i < num.Length; i++)
            {
                d2[i] = num[i] - (i) / (double)n;
            }
            return d2.Max();

        }

        private void GenerateAndSolve_Click(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.Series.Add(new Series());
            Random r1 = new Random();

            const int amountOfNum = 1000, amountOfIntervals = 10;
            double[] nums = new double[amountOfNum];
            double leftMargin, rightMargin;
            int counter;
            for (int i = 0; i < amountOfNum; i++)
            {
                nums[i] = Func(1, r1.NextDouble());
            }
            double iterator = limit / amountOfIntervals;
            leftMargin = 0;
            for (rightMargin = iterator; rightMargin <= 2.1; rightMargin += iterator)
            {
                counter = 0;
                foreach (double i in nums)
                {
                    if (i < rightMargin && i >= leftMargin)
                        counter++;
                }

                chart1.Series[0].Points.AddXY(leftMargin, counter / (double)amountOfNum);
                leftMargin = rightMargin;
            }
            double d1max = dplus(nums);
            double d2max = dminus(nums);
            double d = 0;
            if (d1max > d2max)
            {
                d = d1max;
            }
            else { d = d2max; }
           double kvantil = Math.Sqrt(-0.5*Math.Log((1-0.995)/2));
            if (radioButton1.Checked == true && d <= kvantil)
            {
                label1.Text = "Гипотеза доказана";
            }
            else { label1.Text = "Гипотеза не доказана"; }
        }
    }
}