using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationGlobals
{
    class LineRandom
    {
        Random helpGenerator = new Random();
        private double a;
        private double b;
        private double m;
        private double Xstart;
        private double lastRNGnumber;

        public LineRandom()
        {
            this.a = 16807;
            this.b = 0;
            this.m = (double)Math.Pow(2, 31) - 1;
            this.Xstart = helpGenerator.Next();
            this.lastRNGnumber = Xstart;
        }

        public double NextInt()
        {
            this.lastRNGnumber = (a * this.lastRNGnumber + b) % m;
            return this.lastRNGnumber;
        }

        public double NextDouble()
        {
            return (this.NextInt() / m);
        }

        public double NextInt(double minimum, double maximum)
        {
            return (this.NextInt() % (maximum - minimum + 1)) + minimum;
        }

        public double NextDouble(double minimum, double maximum)
        {
            return this.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
