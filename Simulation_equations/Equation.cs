using System;
using System.Collections.Generic;
using System.Text;

namespace Model
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
        private float speedX;
        public float SpeedX
        {
            get { return speedX; }
            set { speedX = value; }
        }
        private float speedZ;
        public float SpeedZ
        {
            get { return speedZ; }
            set { speedZ = value; }
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

        public Equation(float speedInit, float angle, float g, float h)
        {
            this.speedInit = speedInit;
            this.angle = angle;
            this.g = g;
            this.speedX = (float)(speedInit * Math.Cos(angle * Math.PI / 180));
            this.speedZ = (float)(speedInit * Math.Sin(angle * Math.PI / 180));
            this.a = (float)(-0.5 * (g / Math.Pow((double)speedX, 2)));
            this.b = (float)(speedZ / speedX);
            this.c = h;
        }

        public override string ToString()
        {
            return "-0.5 * (" + g + "/" + speedInit + "²) * x² * [1+tan²(" + angle + ")] + x * tan(" + angle + ")";
        }

        public float getHeight(float x)
        {
            return (float)(a * Math.Pow(x, 2) + b * x + c);
        }

        public Point getZeroHeight()
        {
            float delta = b * b - 4 * a * c;
            float s1 = (float)(-b - Math.Sqrt(delta)) / (2 * a);
            float s2 = (float)(-b + Math.Sqrt(delta)) / (2 * a);
            if (s1 > s2) return new Point(s1,0);
            else return new Point(s2, 0);
        }

        public Point getMaxHeight()
        {
            float x = -b / (2 * a);
            float z = getHeight(x);
            return new Point(x, z);
        }

        public Point getPosition(float time)
        {
            return new Point( speedX * time, (float)(-0.5*g*time*time)+speedZ*time);
        }

        public Point getSpeed(float time)
        {
            return new Point ( speedX, -g * time + speedZ );
        }

        public Point getAcceleration()
        {
            return new Point ( 0, -g );
        }

        public LinkedList<Point> getPoints(float precision)
        {
            LinkedList<Point> points = new LinkedList<Point>();
            float max = (float)Math.Ceiling(getZeroHeight().X);
            for(float i=0; i<max; i += precision)
            {
                points.AddLast(new Point(i, getHeight(i)));
            }
            return points;
        }
    }
}
