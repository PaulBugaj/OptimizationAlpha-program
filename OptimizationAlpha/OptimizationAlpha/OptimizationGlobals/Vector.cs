using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimizationGlobals
{
    public enum StandardAxis { X, Y, Z };

    class Vector
    {
        public List<double> Values { get; set; }

        public Vector()
        {
            this.Values = new List<double>();
        }

        public Vector(int size)
        {
            this.Values = new List<double>();
            for(int i=0; i<size;i++)
            {
                this.Values.Add(0);
                this.Values[i] = 0;
            }
        }

        public Vector(List<double> values)
        {
            this.Values = new List<double>(values);
        }

        public Vector(Vector vector)
        {
            this.Values = new List<double>(vector.Values);
        }
    }
}
