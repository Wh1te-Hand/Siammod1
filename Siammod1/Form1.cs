using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Siammod1
{
    public partial class Form1 : Form
    {
        const long k = 20;
        const long NUMBER_OF_TRIALS = 55000;
        const long V_NUMBER = 150000;
        private double a, m, R, expectation, variance_num, RMS_num,period, aperiodicity;
        private double xmin, xmax, rvar, length;
        public Form1()
        {
            InitializeComponent();
            this.chart_graphic.ChartAreas[0].AxisX.Minimum = 0;
            this.chart_graphic.ChartAreas[0].AxisX.Maximum = 1;
        }

        private void textBox_a_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_a_KeyUp(object sender, KeyEventArgs e)
        {
            double N_number;
            String line = "";
            line = this.textBox_a.Text;
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    a = N_number;
                }
            }
            else
            {
                this.chart_graphic.Series[0].Points.Clear();
            }
        }
        private void textBox_R_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_R_KeyUp(object sender, KeyEventArgs e)
        {
            double N_number;
            String line = "";
            line = this.textBox_R.Text;
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    R = N_number;
                }
            }
            else
            {
                this.chart_graphic.Series[0].Points.Clear();
            }
        }

        private void textBox_m_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_m_KeyUp(object sender, KeyEventArgs e)
        {
            double N_number;
            String line = "";
            line = this.textBox_m.Text;
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    m = N_number;
                }
            }
            else
            {
                this.chart_graphic.Series[0].Points.Clear();
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            double [] mas=new double[NUMBER_OF_TRIALS];
            double Rres;
            double Rvar=0;
            expectation = 0;
            double expectation_old=0;
            variance_num = 0;
            RMS_num = 0;
            xmin = 0;
            xmax = 0;
            for (int i = 0; i < NUMBER_OF_TRIALS; i++){
                if (i == 0)
                {
                    Rres = (a * R) % m;
                }
                else {
                    Rres = (a * Rvar)% m;
                   // 
                   
                }
                xmin = check_min(Rres);
                xmax = check_max(Rres);                
                Rvar = Rres;
                mas[i] = Rres/m;
                expectation_old = expectation;
                expectation = expectation_old * (double)((i) / (double)(i + 1)) + (double)((mas[i]) / (double)(i + 1));
                variance_num = variance_num * ((i) / (double)(i + 1)) + (1 / (double)(i + 1)) * ((mas[i] - expectation_old) * (mas[i] - expectation_old));
            }


            Array.Sort(mas);
/*            for (int i = 0; i < NUMBER_OF_TRIALS; i++)
            {
                expectation += mas[i];
            }
            expectation = expectation / NUMBER_OF_TRIALS;

            for (int i = 0; i < NUMBER_OF_TRIALS; i++)
            {
                variance_num += (mas[i] - expectation) * (mas[i] - expectation);
            }
            variance_num = variance_num / NUMBER_OF_TRIALS;
*/
            this.math_average.Text = expectation.ToString();
            this.variance.Text=variance_num.ToString();


            rvar = xmax-xmin;
            length = (double)rvar / k;
            double[] gates = new double[k];
            long [] histograma_rates=new long[k];

            for (int i = 0; i < (k); i++)
            {
                gates[i] = length * (i+1);
            }

            long porog = 0;
            long count=0;
            for (int i = 0; i < NUMBER_OF_TRIALS; i++) {
                if (gates[porog] >= (mas[i] + xmin))
                {
                    histograma_rates[porog]++;
                }
                else {
                    porog++;
                    histograma_rates[porog]++; //отнесли число оказавшееся больше в следующий промежуток
                }
            }
            chart_graphic.Series[0].Points.Clear();
            for (int i = 0; i < k; i++) {
                chart_graphic.Series[0].Points.AddXY((length * (i + 1)) - 0.5 * length, histograma_rates[i]);
            }
            RMS_num = Math.Sqrt(variance_num);
            this.variance.Text = RMS_num.ToString();


        }
        private double check_min(double var) {
            if ((var/m) < xmin)
            {
                return (var / m);
            }
            else
            { 
            return xmin;
            }
        }

        private double check_max(double var) {
            if ((var / m) > xmax)
            {
                return (var / m);
            }
            else
            {
                return xmax;
            }
        }



        }

}
