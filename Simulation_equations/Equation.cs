using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        private float weight;
        public float Weight
        {
            get { return weight; }
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


        public Equation(float speedInit, float angle, float g, float h, float weight)
        {
            this.speedInit = speedInit;
            this.angle = angle;
            this.g = g;
            this.weight = weight;
            this.h = h;

            speedX = (float)(speedInit * Math.Cos(angle * Math.PI / 180));
            speedZ = (float)(speedInit * Math.Sin(angle * Math.PI / 180));

            a = (float)(-0.5 * (g / Math.Pow(speedX, 2)));
            b = (speedZ / speedX);
            c = h;

            float x = -b / (2 * a);
            float z = GetHeight(x);
            maxHeight = new Point(x, z);

            float delta = b * b - 4 * a * c;
            float s1 = (float)(-b - Math.Sqrt(delta)) / (2 * a);
            float s2 = (float)(-b + Math.Sqrt(delta)) / (2 * a);
            if (s1 > s2) zeroHeight = new Point(s1, 0);
            else zeroHeight = new Point(s2, 0);

            delta = (float)(speedZ * speedZ - 4 * -g * 0.5 * h);
            s1 = (float)(-speedZ - Math.Sqrt(delta)) / (-g);
            s2 = (float)(-speedZ + Math.Sqrt(delta)) / (-g);
            if (s1 > s2) flightTime = s1;
            else flightTime = s2;

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

        public float GetKineticEnergy(float time)
        {
            Point speed = GetSpeed(time);
            return (float)(0.5 * weight * (Math.Pow(speed.X, 2) + Math.Pow(speed.Z, 2)));
        }

        public float GetPotentialEnergy(float time)
        {
            return weight * g * GetPosition(time).Z;
        }

        public float GetTotalEnergy(float time)
        {
            return GetKineticEnergy(time) + GetPotentialEnergy(time);
        }

        public List<Point> GetPoints(float precision)
        {
            List<Point> points = new List<Point>();
            float max = (float)Math.Ceiling(ZeroHeight.X);
            for(float i=0; i<max; i += precision)
            {
                points.Add(new Point(i, GetHeight(i)));
            }
            return points;
        }

        public List<Point> GetPointsSpeedX(float precision)
        {
            List<Point> points = new List<Point>();
            float max = flightTime;
            for (float t = 0; t < max; t += precision)
            {
                points.Add(new Point(t, GetSpeed(t).X));
            }

            return points;
        }

        public List<Point> GetPointsSpeedZ(float precision)
        {
            List<Point> points = new List<Point>();
            float max = flightTime;
            for (float t = 0; t < max; t += precision)
            {
                points.Add(new Point(t, GetSpeed(t).Z));
            }

            return points;
        }

        public List<Point> GetPointsAcceleration(float precision)
        {
            List<Point> points = new List<Point>();
            float max = flightTime;
            for (float t = 0; t < max; t += precision)
            {
                points.Add(new Point(t, acceleration.Z));
            }

            return points;
        }

        public List<Point> GetPointsKineticEnergy(float precision)
        {
            List<Point> points = new List<Point>();
            float max = flightTime;
            for (float t = 0; t < max; t += precision)
            {
                points.Add(new Point(t, GetKineticEnergy(t)));
            }

            return points;
        }

        public List<Point> GetPointsPotentialEnergy(float precision)
        {
            List<Point> points = new List<Point>();
            float max = flightTime;
            for (float t = 0; t < max; t += precision)
            {
                points.Add(new Point(t, GetPotentialEnergy(t)));
            }

            return points;
        }

        public List<Point> GetPointsTotalEnergy(float precision)
        {
            List<Point> points = new List<Point>();
            float max = flightTime;
            for (float t = 0; t < max; t += precision)
            {
                points.Add(new Point(t, GetTotalEnergy(t)));
            }

            return points;
        }

        public List<Point> GetPointsTotalEnergy(float precision, List<Point> potentialEnergy, List<Point> kineticEnergy)
        {
            List<Point> points = new List<Point>();
            int i = 0;
            for(float t =0; t< flightTime; t += precision)
            {
                points.Add(new Point(t, potentialEnergy[i].Z + kineticEnergy[i].Z));
                i++;
            }
            return points;
        }
    }
}
