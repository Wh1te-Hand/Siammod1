using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Siammod1
{
    public class Generator_lab4
    {
        double intensity=80;
        double number_use = 0;
        double number_reject = 0;
        private Random rand = new Random();

        public Generator_lab4(double intensity)
        {
            this.intensity=intensity;
        }
 
        public double tactsUse
        {
            get => number_use;
            set=> number_use = value;
        }
        public double tactsRejected
        {
            get => number_reject;
            set => number_reject = value;
        }
        public Double Tact()
        {           
            double var;
            var = rand.NextDouble();
            return ((-1) / intensity) * Math.Log(var, Math.E);
        }

     /*   public void Calculate_tacts()
        {
            if (state == 2)
            {
                number_use++;
            }
            else if (state == 1)
            {
                number_use++;
            }
            
        }*/
    }
}
