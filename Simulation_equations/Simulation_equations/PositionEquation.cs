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
            double speedX = (speedInit * Math.Cos(angle*Math.PI/180));
            double speedZ = (speedInit * Math.Sin(angle*Math.PI/180)) ;
            this.a = (float)(-0.5 * (g / Math.Pow((double)speedX, 2)));
            this.b =(float) (speedZ/speedX);
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

        public float getZeroHeight()
        {
            float delta = b * b - 4 * a * c;
            float s1 =(float) (-b - Math.Sqrt(delta)) / (2*a);
            float s2 =(float) (-b + Math.Sqrt(delta)) / (2*a);
            if (s1 > s2) return s1;
            else return s2;
        }
    }
}
