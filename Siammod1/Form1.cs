using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.XPath;

namespace Siammod1
{
    public partial class Form1 : Form
    {
        private struct PointD {
            public double X;
            public double Y;
        }
        const long k = 20;
        const long NUMBER_OF_TRIALS = 55000;
        private long period, aperiodicity;
        private double a, m, R, expectation, variance_num, RMS_num;
        private double xmin, xmax, rvar, length;
        private double a_uniform=0, b_uniform=0;
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
            long indirect = 0;
            double expectation_old = 0;
            Boolean flag_indirect=true;
            expectation = 0;

            variance_num = 0;
            RMS_num = 0;
            xmin = 0;
            xmax = 0;
            period = 0;
            aperiodicity = 0;
            for (int i = 0; i < NUMBER_OF_TRIALS; i++){
                if (i == 0)
                {
                    Rres = (a * R) % m;
                }
                else {
                    Rres = (a * Rvar)% m;                  
                }
                xmin = check_min(Rres);
                xmax = check_max(Rres);
                if (flag_indirect)                {
                    if (((Rvar / m) * (Rvar / m) + (Rres / m) * (Rres / m)) < 1) {
                        indirect++;
                    }
                    flag_indirect = false;  
                }
                else { 
                flag_indirect=true;
                }
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
            this.indirect_try.Text = ((double)((indirect*2)/ (double)NUMBER_OF_TRIALS)).ToString();


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
                chart_graphic.Series[0].Points.AddXY((length * (i + 1)) - 0.5 * length, (double)histograma_rates[i]/ (double)NUMBER_OF_TRIALS);
            }

            RMS_num = Math.Sqrt(variance_num);
            this.RMS.Text = RMS_num.ToString();

            period = find_period();
            aperiodicity = find_aperiodicity(period);

            this.indirect_period.Text = period.ToString();
            this.indirect_length.Text=aperiodicity.ToString();

        }

        private long find_period() {
            const long V_NUMBER = 150000;
            double Rres = 0;
            double Rvar = 0;
            double x_v=0;
            long i1=0, i2=0;
            for (int i = 0; i < V_NUMBER; i++)
            {
                if (i == 0)
                {
                    Rres = (a * R) % m;
                }
                else
                {
                    Rres = (a * x_v) % m;
                }
                x_v = Rres;
            }

            for (int i = 0; i < V_NUMBER; i++)
            {
                if (i2 == 0)
                {
                    if (i == 0)
                    {
                        Rres = (a * R) % m;
                    }
                    else
                    {
                        Rres = (a * Rvar) % m;
                    }
                    Rvar = Rres;
                    if (Rres == x_v && i1 == 0)
                    {
                        i1 = i;
                    }
                    else
                        if (Rres == x_v) {
                        i2 = i;
                    }
                }
            }

            return (i2-i1);
        }

        private void textBox_m_gauss_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox_m_gauss_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox_gauss_D_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox_gauss_D_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private long find_aperiodicity(long period) {
            const long V_NUMBER = 150000;
            double xp = 0;
            double x0 = R;
            double Rres=0;
            Boolean flag=true;
            long i3 = 0;
            for (int i = 0; i < period; i++) {
                if (i == 0)
                {
                    Rres = (a * R) % m;
                }
                else
                {
                    Rres = (a * Rres) % m;
                }
            }
            xp = Rres;

            while (flag) {
                if (x0 != xp)
                {
                    if (i3 == 0)
                    {
                        x0 = (a * R) % m;
                        xp = (a * xp) % m;
                        
                    }
                    else
                    {
                        x0 = (a * x0) % m;
                        xp = (a * xp) % m;
                    }
                    i3++;
                }
                else { 
                flag= false;
                }
            }

            return (period+i3);
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

        private long[] find_dots_to_histogramma(double a, double b, long count,double[] mas_start) {
           
            double length;
            length = (double)(b-a) /count;

            double[] gates = new double[count];
            long[] histograma_rates = new long[count];
            
            for (int i = 0; i < (count); i++)
            {
                gates[i] = a+length * (i + 1);
            }

            long porog = 0;
            long counter = 0;
            for (int i = 0; i <mas_start.Length ; i++)
            {
                if (gates[porog] >= (mas_start[i]))
                {
                    histograma_rates[porog]++;
                }
                else
                {
                    porog++;
                    histograma_rates[porog]++; //отнесли число оказавшееся больше в следующий промежуток
                }
            }
            return histograma_rates;
        }
        

      //-------------------------------------------------lab_2_uniform---------------------------------------------------  
        private double[] uniform_generate(double a,double b,long count) {
            double[] mas = new double[count];
            Random rnd=new Random();
            double var;
            double x;
            if ((a < b) && (a != 0) && (b != 0))
            {
                for (int i = 0; i < count; i++)
                {
                    var = rnd.NextDouble();
                    x = a + (b - a) * var;
                    mas[i] = x;
                }
                Array.Sort(mas);// may be should to change
               // mas.OrderBy(ms => ms.X);
                return mas;
            }
            else
                return new double[1];
        }

        private void button_calculate_uniform_Click(object sender, EventArgs e)
        {
            const long COUNT_OF_TRIALS = 50000;
            double[] mas_v = uniform_generate(a_uniform, b_uniform, COUNT_OF_TRIALS);
          
            if (mas_v.Length < 2)
            {
            }
            else {
                long[]mas_y=find_dots_to_histogramma(a_uniform, b_uniform, k, mas_v);
                double length = (b_uniform - a_uniform )/(double)k;
                chart_uniform.Series[0].Points.Clear();
                for (int i = 0; i < k; i++)
                {
                    chart_uniform.Series[0].Points.AddXY((a_uniform+ length* (i + 1)) - 0.5 * length, (double)mas_y[i] / (double)COUNT_OF_TRIALS);
                }
                this.label_math_uniform.Text = ((a_uniform + b_uniform) / (double)2).ToString();
                this.label_variance_uniform.Text = ((b_uniform - a_uniform) * (b_uniform - a_uniform) / (double)12).ToString();
                this.label_RMS_uniform.Text = Math.Sqrt(((b_uniform - a_uniform) * (b_uniform - a_uniform) / (double)12)).ToString();
            }

        }
        private void textBox_a_uniform_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_a_uniform_KeyUp(object sender, KeyEventArgs e)
        {
            double N_number;
            String line = "";
            line = this.textBox_a_uniform.Text;
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                   a_uniform  = N_number;
                }
            }
            else
            {
                this.chart_uniform.Series[0].Points.Clear();
            }
        }

        private void textBox_b_uniform_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_b_uniform_KeyUp(object sender, KeyEventArgs e)
        {
            double N_number;
            String line = "";
            line = this.textBox_b_uniform.Text;  //change
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    b_uniform = N_number; //change
                }
            }
            else
            {
                this.chart_uniform.Series[0].Points.Clear(); //change
            }
        }
        //----------------------------------------lab_2_gauss------------------------------------



    }

}
