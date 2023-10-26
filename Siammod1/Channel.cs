using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siammod1
{
    public class Channel
    {
        double intensity1, intensity2;
        private double time_busy = 0;
        private double time_free = 0;
        private long number_request1 = 0;
        private long number_request2 = 0;
        Boolean employed = false;
        private Random rand = new Random();

        public Channel(double intensity1,double intensity2)
        {
            this.intensity1 = intensity1;
            this.intensity2 = intensity2;
        }

        public long Request1 { 
        get { return number_request1; }
        }

        public long Request2 { 
        get { return number_request2; }
        }
        public double timeWork
        {
            get => time_busy;
/*            set => time_busy = value;*/
        }
        public double timeFree
        {
            get => time_free;
/*            set => time_free = value;*/
        }

        public Boolean Employed
        { 
            get => employed;
            set => employed = value;
        }
        public double Solve()
        {
            double var;
            double result = 0;
            var = rand.NextDouble();
            if (var <= 0.3)
            {
                var = rand.NextDouble();
                result = ((-1) / intensity1) * Math.Log(var, Math.E);
                number_request1++;
            }
            else
            {
                var = rand.NextDouble();
                result = ((-1) / intensity2) * Math.Log(var, Math.E);
                number_request2++;
            }
            employed = true;
            return result;
        }

        public void calculate_time(double time) {
            if (employed)
            {
                time_busy += time;
            }
            else
            {
                time_free += time;
            }
        }
    }
}
