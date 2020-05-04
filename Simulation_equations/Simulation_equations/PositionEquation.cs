using System;
using System.Collections.Generic;
using System.Text;

namespace Simulation_equations
{
    class PositionEquation : Equation
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
        private float angle;
        public float Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        private float speedInit;
        public float SpeedInit
        {
            get { return speedInit; }
            set { speedInit = value; }
        }

        private float g;
        public float G
        {
            get { return g; }
            set { g = value; }
        }

        private float h;
        public float H
        {
            get { return H; }
            set { H = value; }
        }

        public PositionEquation(float speedInit, float angle, float g, float h)
        {
            this.speedInit = speedInit;
            this.angle = angle;
            this.g = g;
            float speedX = (float)(speedInit * Math.Cos(angle));
            float speedZ = (float)(speedInit * Math.Sin(angle));
            this.a = (float)(-0.5 * (g / Math.Pow((double)speedX, 2)));
            this.b = speedZ/speedX;
            this.c = h;
        }

        public override string ToString()
        {
            return "-0.5 * ("+g+"/"+speedInit+"²) * x² * [1+tan²("+angle+")] + x * tan("+angle+")";
        }

        public Equation getDerivatedEquation()
        {
            throw new NotImplementedException();
        }

        public float getHeight(float x)
        {
            return (float)(this.a * Math.Pow(x, 2) + this.b * x + this.c);
        }
    }
}
