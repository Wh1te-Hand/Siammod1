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
        private double m_gauss = 0, d_gauss = 0;
        private double lambda_exponential = 0;
        private double lambda_gamma = 0;
        private long N_gamma = 0;
        private Boolean min_mod = false;
        private double a_triangle = 0, b_triangle = 0;
        private double a_simpson = 0, b_simpson = 0;
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
            const long V_NUMBER = 300000;
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



        private long find_aperiodicity(long period) {
            const long V_NUMBER = 300000;
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
            if (i3 > 0) { i3--; }
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

        private double find_min_mass(double[] mas) {
            double result = mas[0];
            for (int i = 1; i < mas.Length; i++)
            {
                if (result > mas[i])
                    result = mas[i];
            }
            return result;
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
        private double find_max_mass(double[] mas)
        {
            double result = mas[0];
            for (int i = 1; i < mas.Length; i++)
            {
                if (result < mas[i])
                    result = mas[i];
            }
            return result;
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


        private void button_calculate_gauss_Click(object sender, EventArgs e)
        {
            const long COUNT_OF_TRIALS = 50000;
            double[] mas_v = gauss_generate(m_gauss,d_gauss, COUNT_OF_TRIALS);

            if (mas_v.Length < 2)
            {
            }
            else
            {
                double min = find_min_mass(mas_v);
                double max = find_max_mass(mas_v);
                long[] mas_y = find_dots_to_histogramma(min, max, k, mas_v);
                double length = (max - min) / (double)k;
                chart_gauss.Series[0].Points.Clear();
                for (int i = 0; i < k; i++)
                {
                    chart_gauss.Series[0].Points.AddXY((min + length * (i + 1)) - 0.5 * length, (double)mas_y[i] / (double)COUNT_OF_TRIALS);
                }
                this.label_math_gauss.Text = (m_gauss).ToString();
                this.label_variance_gauss.Text = (d_gauss).ToString();
                this.label_RMS_gauss.Text = Math.Sqrt(d_gauss).ToString();
            }
        }

        private double[] gauss_generate(double m, double d, long count)
        {
            double[] mas = new double[count];
            Random rnd = new Random();
            double var;
            double x;
            if ( (m != 0) && (d != 0))
            {
                for (int i = 0; i < count; i++)
                {
                    double sum = 0;
                    for (int j = 0; j < 6; j++) {
                        sum +=  rnd.NextDouble(); 
                    }
                    x = m +d*Math.Sqrt(2)*(sum-3);
                    mas[i] = x;
                }
                Array.Sort(mas);// may be should to change
                return mas;
            }
            else
                return new double[1];
        }

        private void textBox_m_gauss_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_m_gauss_KeyUp(object sender, KeyEventArgs e)
        {
            double N_number;
            String line = "";
            line = this.textBox_m_gauss.Text;  //change
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    m_gauss = N_number; //change
                }
            }
            else
            {
                this.chart_gauss.Series[0].Points.Clear(); //change
            }
        
        }

        private void textBox_gauss_D_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void textBox_gauss_D_KeyUp(object sender, KeyEventArgs e)
        {
            double N_number;
            String line = "";
            line = this.textBox_gauss_D.Text;  //change
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    d_gauss = N_number; //change
                }
            }
            else
            {
                this.chart_gauss.Series[0].Points.Clear(); //change
            }
        
        }
        //---------------------------------------------lab_2_exponential---------------------------------
        private void button_calculate_exponential_Click(object sender, EventArgs e)
        {
            const long COUNT_OF_TRIALS = 50000;
            double[] mas_v = exponential_generate(lambda_exponential, COUNT_OF_TRIALS); //change
            if (mas_v.Length < 2)
            {
            }
            else
            {
                double min = find_min_mass(mas_v);
                double max = find_max_mass(mas_v);
                long[] mas_y = find_dots_to_histogramma(min, max, k, mas_v);
                double length = (max - min) / (double)k;
                chart_exponential.Series[0].Points.Clear();//change
                for (int i = 0; i < k; i++)
                {
                    chart_exponential.Series[0].Points.AddXY((min + length * (i + 1)) - 0.5 * length, (double)mas_y[i] / (double)COUNT_OF_TRIALS);
                }
                this.label_math_exponential.Text = (1/lambda_exponential).ToString();
                this.label_variance_exponential.Text = ((1 / lambda_exponential)* (1 / lambda_exponential)).ToString();
                this.label_RMS_exponential.Text = Math.Sqrt((1 / lambda_exponential) * (1 / lambda_exponential)).ToString();
            }
        }
        private double[] exponential_generate(double lambda, long count)
        {
            double[] mas = new double[count];
            Random rnd = new Random();
            double var;
            double x;
            if (lambda!=0)
            {
                for (int i = 0; i < count; i++)
                {                     
                    var = rnd.NextDouble();                    
                    x = ((-1)/lambda)*Math.Log(var,Math.E);
                    mas[i] = x;
                }
                Array.Sort(mas);// may be should to change
                return mas;
            }
            else
                return new double[1];
        }
        private void textBox_lambda_exponential_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_lambda_exponential_KeyUp(object sender, KeyEventArgs e)
        {
            double N_number;
            String line = "";
            line = this.textBox_lambda_exponential.Text;  //change
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    lambda_exponential = N_number; //change
                }
            }
            else
            {
                this.chart_exponential.Series[0].Points.Clear(); //change
            }
        }
        //-------------------------------------------------lab_2_gamma------------------------------------
        private void button_calculate_gamma_Click(object sender, EventArgs e)
        {
            const long COUNT_OF_TRIALS = 50000;
            double[] mas_v = gamma_generate(lambda_gamma,N_gamma, COUNT_OF_TRIALS); //change
            if (mas_v.Length < 2)
            {
            }
            else
            {
                double min = find_min_mass(mas_v);
                double max = find_max_mass(mas_v);
                long[] mas_y = find_dots_to_histogramma(min, max, k, mas_v);
                double length = (max - min) / (double)k;
                chart_gamma.Series[0].Points.Clear();//change
                for (int i = 0; i < k; i++)
                {
                    chart_gamma.Series[0].Points.AddXY((min + length * (i + 1)) - 0.5 * length, (double)mas_y[i] / (double)COUNT_OF_TRIALS);
                }
                this.label_math_gamma.Text = (N_gamma / lambda_gamma).ToString();
                this.label_variance_gamma.Text = (N_gamma*(1 / lambda_gamma) * (1 / lambda_gamma)).ToString();
                this.label_RMS_gamma.Text = Math.Sqrt(N_gamma*(1 / lambda_gamma) * (1 / lambda_gamma)).ToString();
            }
        }
        private double[] gamma_generate(double lambda,long N, long count)
        {
            double[] mas = new double[count];
            Random rnd = new Random();
            double var;
            double x;
            if ((lambda != 0)&&(N!=0))
            {
                for (int i = 0; i < count; i++)
                {
                    var = rnd.NextDouble();
                    for (int j = 1; j < N; j++) { 
                    var=var* rnd.NextDouble();
                    }
                    x = ((-1) / lambda) * Math.Log(var, Math.E);
                    mas[i] = x;
                }
                Array.Sort(mas);// may be should to change
                return mas;
            }
            else
                return new double[1];
        }

        private void textBox_lambda_gamma_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void textBox_lambda_gamma_KeyUp(object sender, KeyEventArgs e)
        {
            double N_number;
            String line = "";
            line = this.textBox_lambda_gamma.Text;  //change
            if (line != "")
            {
                N_number = double.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    lambda_gamma = N_number; //change
                }
            }
            else
            {
                this.chart_gamma.Series[0].Points.Clear(); //change
            }
        }

        private void textBox_N_gamma_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_N_gamma_KeyUp(object sender, KeyEventArgs e)
        {
            long N_number;
            String line = "";
            line = this.textBox_N_gamma.Text;  //change
            if (line != "")
            {
                N_number = long.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    N_gamma = N_number; //change
                }
            }
            else
            {
                this.chart_gamma.Series[0].Points.Clear(); //change
            }
        }
        //----------------------------------------------lab2_triangle---------------------------------------------
        private void button_calculate_triangle_Click(object sender, EventArgs e)
        {
            const long COUNT_OF_TRIALS = 50000;
            double[] mas_v = triangle_generate(a_triangle, b_triangle,min_mod, COUNT_OF_TRIALS); //change
            if (mas_v.Length < 2)
            {
            }
            else
            {
                double min = find_min_mass(mas_v);
                double max = find_max_mass(mas_v);
                long[] mas_y = find_dots_to_histogramma(min, max, k, mas_v);
                double length = (max - min) / (double)k;
               
                chart_triangle.Series[0].Points.Clear();//change
                for (int i = 0; i < k; i++)
                {
                    chart_triangle.Series[0].Points.AddXY((min + length * (i + 1)) - 0.5 * length, (double)mas_y[i] / (double)COUNT_OF_TRIALS);
                }
                double math = find_math_expectation(mas_v);
                double variance = find_variance(mas_v, math);
                this.label_math_triangle.Text = (math).ToString();
                this.label_variance_triangle.Text = (variance).ToString();
                this.label_RMS_triangle.Text = Math.Sqrt(variance).ToString();
            }
        }

        private double find_math_expectation(double[]mas) {
            double result = 0;
            for (int i = 0; i < mas.Length; i++){
                result += mas[i] / (double)mas.Length;
            }
            return result;
        }
        private double find_variance(double[] mas,double math) {
            double result = 0;
            for (int i = 0; i < mas.Length; i++) { 
            result+=(mas[i]-math)* (mas[i] - math)/(double)mas.Length;
            }
            return result;
        }
        private double[] triangle_generate(double a, double b,Boolean min_mod, long count)
        {
            double[] mas = new double[count];
            Random rnd = new Random();
            double var1,var2;
            double x;
            if ((a != 0) && (b != 0))
            {
                if (min_mod)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var1 = rnd.NextDouble();
                        var2 = rnd.NextDouble();
                        if (var1 < var2)
                        {
                            x = a + (b - a) * var2;
                        }
                        else { x = a + (b - a) * var1; }                        
                        mas[i] = x;
                    }
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        var1 = rnd.NextDouble();
                        var2 = rnd.NextDouble();
                        if (var1 > var2)
                        {
                            x = a + (b - a) * var2;
                        }
                        else { x = a + (b - a) * var1; }
                        mas[i] = x;
                    }
                }
                Array.Sort(mas);// may be should to change
                return mas;
            }
            else
                return new double[1];
        }

        private void textBox_a_triangle_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_a_triangle_KeyUp(object sender, KeyEventArgs e)
        {
            long N_number;
            String line = "";
            line = this.textBox_a_triangle.Text;  //change
            if (line != "")
            {
                N_number = long.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    a_triangle = N_number; //change
                }
            }
            else
            {
                this.chart_triangle.Series[0].Points.Clear(); //change
            }
        }

        private void textBox_b_triangle_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_b_triangle_KeyUp(object sender, KeyEventArgs e)
        {
            long N_number;
            String line = "";
            line = this.textBox_b_triangle.Text;  //change
            if (line != "")
            {
                N_number = long.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    b_triangle = N_number; //change
                }
            }
            else
            {
                this.chart_triangle.Series[0].Points.Clear(); //change
            }
        }

        private void checkBox_min_max_CheckedChanged(object sender, EventArgs e)
        {
            min_mod = !min_mod;
        }

        //--------------------------------------------------------lab2_simpson-------------------------------
        private void button_calculate_simpson_Click(object sender, EventArgs e)
        {
            const long COUNT_OF_TRIALS = 50000;
            double[] mas_v = simpson_generate(a_simpson, b_simpson, COUNT_OF_TRIALS);
            if (mas_v.Length < 2)
            {
            }
            else
            {
                double min = find_min_mass(mas_v);
                double max = find_max_mass(mas_v);
                long[] mas_y = find_dots_to_histogramma(min, max, k, mas_v);
                double length = (max - min) / (double)k;

                chart_simpson.Series[0].Points.Clear();//change
                for (int i = 0; i < k; i++)
                {
                    chart_simpson.Series[0].Points.AddXY((min + length * (i + 1)) - 0.5 * length, (double)mas_y[i] / (double)COUNT_OF_TRIALS);
                }
                double math = find_math_expectation(mas_v);
                double variance = find_variance(mas_v, math);
                this.label_math_simpson.Text = (math).ToString();
                this.label_variance_simpson.Text = (variance).ToString();
                this.label_RMS_simpson.Text = Math.Sqrt(variance).ToString();
            }
        }

        private double[] simpson_generate(double a, double b, long count)
        {
            double[] mas = new double[count];
            Random rnd = new Random();
            double var;
            double z, y;
            double x;
            if ((a < b) && (a != 0) && (b != 0))
            {
                for (int i = 0; i < count; i++)
                {
                    var = rnd.NextDouble();
                    z = a/ (double)2 + (b/ (double)2 - a/(double)2) * var;
                    var = rnd.NextDouble();
                    y = a / (double)2 + (b / (double)2 - a / (double)2) * var;
                    x = y + z;
                    mas[i] = x;
                }
                Array.Sort(mas);// may be should to change
                return mas;
            }
            else
                return new double[1];
        }

        private void textBox_a_simpson_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_a_simpson_KeyUp(object sender, KeyEventArgs e)
        {
            long N_number;
            String line = "";
            line = this.textBox_a_simpson.Text;  //change
            if (line != "")
            {
                N_number = long.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    a_simpson = N_number; //change
                }
            }
            else
            {
                this.chart_simpson.Series[0].Points.Clear(); //change
            }
        }

        private void textBox_b_simpson_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_b_simpson_KeyUp(object sender, KeyEventArgs e)
        {
            long N_number;
            String line = "";
            line = this.textBox_b_simpson.Text;  //change
            if (line != "")
            {
                N_number = long.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    b_simpson = N_number; //change
                }
            }
            else
            {
                this.chart_simpson.Series[0].Points.Clear(); //change
            }
        }

        //-----------------------------------------------------lab4-------------------------------------------------------
        long lab4_time =0;
        private void textBox_lab4_Time_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            String line = "";
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox_lab4_Time_KeyUp(object sender, KeyEventArgs e)
        {
            long N_number;
            String line = "";
            line = this.textBox_lab4_Time.Text;  //change
            if (line != "")
            {
                N_number = long.Parse(line);
                if (N_number > 0 && N_number != double.NaN)
                {
                    lab4_time = N_number; //change
                }
            }
            else
            {
                lab4_time=0; //change
            }

        }

        private void button_lab4_calculate_Click(object sender, EventArgs e)
        {
            const double INTENSITY_GENERATOR = 80;
            const double INTENSITY_CHANNEL11 = 20;
            const double INTENSITY_CHANNEL12 = 60;
            const double INTENSITY_CHANNEL21 = 40;
            const double INTENSITY_CHANNEL22 = 40;

            Generator_lab4 generator = new Generator_lab4(INTENSITY_GENERATOR);
            Channel channel1 = new Channel(INTENSITY_CHANNEL11, INTENSITY_CHANNEL12);
            Channel channel2 = new Channel(INTENSITY_CHANNEL21, INTENSITY_CHANNEL22);
            double total_time = 0;
            double time_source = 0;
            double time_channel1 = double.MaxValue;
            double time_channel2 = double.MaxValue;
            double time_before_event = 0;
            int state = 0;
           
            while (total_time<=lab4_time) {

                if (state == 0)
                {
                    time_source = generator.Tact();

                    if (!channel1.Employed)
                    {
                        time_channel1 = channel1.Solve();
                        generator.tactsUse++;
                    }
                    else if (!channel2.Employed)
                    {
                        time_channel2 = channel2.Solve();
                        generator.tactsUse++;
                    }
                    else {
                        generator.tactsRejected++;
                    }
                }
                else if (state == 1) 
                {
                    channel1.Employed = false;
                    time_channel1 = double.MaxValue;
                }
                else if (state == 2)
                {
                    channel2.Employed = false;
                    time_channel2 = double.MaxValue;
                }
                state=find_min_state(time_source,time_channel1,time_channel2);
                time_before_event = find_min_time(time_source, time_channel1, time_channel2);
                total_time += time_before_event;
                time_source-=time_before_event;
                time_channel1-= time_before_event;
                time_channel2-= time_before_event;

                channel1.calculate_time(time_before_event);
                channel2.calculate_time(time_before_event);
            }

            double lab4_A = (double)(channel1.Request1 + channel1.Request2 + channel2.Request1 + channel2.Request2) / total_time;
            double lab4_L1 = (channel1.timeWork) / total_time;
            double lab4_L2 = (channel2.timeWork) / total_time;
            double lab4_L = (channel1.timeWork+channel2.timeWork)/total_time;
            double lab4_W1 = total_time / (channel1.Request1 + channel1.Request2);
            double lab4_W2 = total_time / (channel2.Request1 + channel2.Request2);
            double lab4_W = total_time / (channel1.Request1 + channel1.Request2+ channel2.Request1 + channel2.Request2);

            this.label_lab4_A.Text =lab4_A.ToString();
            this.label_lab4_L.Text =lab4_L.ToString();
            this.label_lab4_L1.Text = lab4_L1.ToString();
            this.label_lab4_L2.Text = lab4_L2.ToString();
            this.label_lab4_W.Text = lab4_W.ToString();
            this.label_lab4_W1.Text = lab4_W1.ToString();
            this.label_lab4_W2.Text = lab4_W2.ToString();
        }

        private int find_min_state(double source, double channel1, double channel2) {
            if ((source <= channel1) && (source <= channel2))
            {
                return 0;
            }
            else if ((channel1 <= source) && (channel1 <= channel2)) 
            {
                return 1;
            }
            else { return 2; }
         }

        private double find_min_time(double source, double channel1, double channel2)
        {
            if ((source <= channel1) && (source <= channel2))
            {
                return source;
            }
            else if ((channel1 <= source) && (channel1 <= channel2))
            {
                return channel1;
            }
            else { return channel2; }
        }

    }
}
