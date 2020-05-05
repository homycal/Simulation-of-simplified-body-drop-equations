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
        }
        private float b;
        public float B
        {
            get { return b; }
        }
        private float c;
        public float C
        {
            get { return c; }
        }
        private float angle;
        public float Angle
        {
            get { return angle; }
        }

        private float speedInit;
        public float SpeedInit
        {
            get { return speedInit; }
        }
        private float speedX;
        public float SpeedX
        {
            get { return speedX; }
        }
        private float speedZ;
        public float SpeedZ
        {
            get { return speedZ; }
        }

        private float g;
        public float G
        {
            get { return g; }
        }

        private float h;
        public float H
        {
            get { return H; }
        }

        private Point maxHeight;
        public Point MaxHeight
        {
            get { return maxHeight; }
        }

        private float flightTime;
        public float FlightTime
        {
            get { return flightTime; }
        }

        private Point zeroHeight;
        public Point ZeroHeight
        {
            get { return zeroHeight; }
        }

        private Point acceleration;
        public Point Acceleration
        {
            get { return acceleration; }
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
            float x = -b / (2 * a);
            float z = GetHeight(x);
            maxHeight = new Point(x, z);
            flightTime = speedZ / (-g);
            float delta = b * b - 4 * a * c;
            float s1 = (float)(-b - Math.Sqrt(delta)) / (2 * a);
            float s2 = (float)(-b + Math.Sqrt(delta)) / (2 * a);
            if (s1 > s2) zeroHeight = new Point(s1, 0);
            else zeroHeight = new Point(s2, 0);
            acceleration = new Point(0, -g);
        }

        public override string ToString()
        {
            return "-0.5 * (" + g + "/" + speedInit + "²) * x² * [1+tan²(" + angle + ")] + x * tan(" + angle + ")";
        }

        public float GetHeight(float x)
        {
            return (float)(a * Math.Pow(x, 2) + b * x + c);
        }


        public Point GetPosition(float time)
        {
            return new Point( speedX * time, (float)(-0.5*g*time*time)+speedZ*time);
        }

        public Point GetSpeed(float time)
        {
            return new Point ( speedX, -g * time + speedZ );
        }

        public LinkedList<Point> GetPoints(float precision)
        {
            LinkedList<Point> points = new LinkedList<Point>();
            float max = (float)Math.Ceiling(ZeroHeight.X);
            for(float i=0; i<max; i += precision)
            {
                points.AddLast(new Point(i, GetHeight(i)));
            }
            return points;
        }

        public LinkedList<Point> GetPointsSpeedX(float precision)
        {
            LinkedList<Point> points = new LinkedList<Point>();
            float max = (float)Math.Ceiling(flightTime);
            for (float t = 0; t < max; t += precision)
            {
                points.AddLast(new Point(t, GetSpeed(t).X));
            }

            return points;
        }

        public LinkedList<Point> GetPointsSpeedZ(float precision)
        {
            LinkedList<Point> points = new LinkedList<Point>();
            float max = (float)Math.Ceiling(flightTime);
            for (float t = 0; t < max; t += precision)
            {
                points.AddLast(new Point(t, GetSpeed(t).Z));
            }

            return points;
        }

        public LinkedList<Point> GetPointsAcceleration(float precision)
        {
            LinkedList<Point> points = new LinkedList<Point>();
            float max = (float)Math.Ceiling(flightTime);
            for (float t = 0; t < max; t += precision)
            {
                points.AddLast(new Point(t, acceleration.X));
            }

            return points;
        }
    }
}
