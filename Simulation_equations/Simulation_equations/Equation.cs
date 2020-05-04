using System;
using System.Collections.Generic;
using System.Text;

namespace Simulation_equations
{
    class Equation
    {
        private float a;
        public float A
        {
            get { return a; }
            set { a = value; }
        }
        private float b;
        public float B
        {
            get { return b; }
            set { b = value; }
        }
        private float c;
        public float C
        {
            get { return c; }
            set { c = value; }
        }

        public Equation(float a, float b, float c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public override string ToString()
        {
            return $"{a} * x² + {b} * x + {c}";
        }
    }
}
