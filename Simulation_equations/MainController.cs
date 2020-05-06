using System;
using System.Collections.Generic;
using System.Text;
using View;
using Model;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input.Manipulations;

namespace Controller
{
    class MainController
    {
        private const float CANVAS_PADDING = 40;
        private const float HALF_GRADUATION = 5;
        private const float MARGIN_SCALE = 30;
        private const float PRECISION_FACTOR = 0.045f;
        SolidColorBrush redBrush;
        SolidColorBrush blueBrush;
        SolidColorBrush greenBrush;
        SolidColorBrush blackBrush;
        public MainController()
        {
            redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;
            blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.Blue;
            greenBrush = new SolidColorBrush();
            greenBrush.Color = Colors.Green;
            blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;
        }

        public void PlotEquation(List<Canvas> canvas, Equation equation)
        {
            foreach (Canvas c in canvas)
            {
                c.Children.Clear();
            }
            //MainCanvas
            float precisionDistance = (float)Math.Round(equation.ZeroHeight.X * PRECISION_FACTOR,3);
            float precisionTime =(float) Math.Round(equation.FlightTime * PRECISION_FACTOR,3);
            DrawMainCanvas(canvas[0], precisionDistance, equation);
            //SpeedCanvas
            DrawSpeedCanvas(canvas[1], precisionTime, equation);
            //AccelerationCanvas
            DrawAccelerationCanvas(canvas[2], precisionTime, equation);
            //EnergyCanvas
            DrawEnergiesCanvas(canvas[3], precisionTime, equation);
            
        }
        public void DrawMainCanvas(Canvas canvas, float precision, Equation equation)
        {
            float scaleX = (float)(canvas.ActualWidth - CANVAS_PADDING - MARGIN_SCALE) / equation.ZeroHeight.X;
            float scaleZ = (float)(canvas.ActualHeight - CANVAS_PADDING - MARGIN_SCALE) / equation.MaxHeight.Z;
            List<Model.Point> points = equation.GetPoints(precision);
            scaleX = CheckScales(scaleX);
            scaleZ = CheckScales(scaleZ);

            //X-Axis
            Line xAxis = new Line();
            xAxis.X1 = 0;
            xAxis.X2 = canvas.ActualWidth;
            xAxis.Y1 = canvas.ActualHeight - CANVAS_PADDING;
            xAxis.Y2 = canvas.ActualHeight - CANVAS_PADDING;
            DrawLine(canvas, xAxis, blackBrush);

            float factor = GetFactor(scaleX);
            float length = 0;

            //X-Axis graduations
            for (float i = CANVAS_PADDING; i < canvas.ActualWidth; i += factor * scaleX)
            {
                Line grad = new Line();
                grad.X1 = i;
                grad.X2 = i;
                grad.Y1 = canvas.ActualHeight - CANVAS_PADDING - HALF_GRADUATION;
                grad.Y2 = canvas.ActualHeight - CANVAS_PADDING + HALF_GRADUATION;
                DrawLine(canvas, grad, blackBrush);
                TextBlock num = new TextBlock();
                num.Text = length.ToString();
                num.Margin = new Thickness(i - HALF_GRADUATION, canvas.ActualHeight - CANVAS_PADDING + HALF_GRADUATION, 0, 0);
                canvas.Children.Add(num);
                length += factor;
            }

            //Z-Axis
            Line zAxis = new Line();
            zAxis.X1 = CANVAS_PADDING;
            zAxis.X2 = CANVAS_PADDING;
            zAxis.Y1 = 0;
            zAxis.Y2 = canvas.ActualHeight;
            DrawLine(canvas, zAxis, blackBrush);
            factor = GetFactor(scaleZ);
            length = 0;
            //Z-Axis graduations
            for (float i = (float)canvas.ActualHeight - CANVAS_PADDING; i > 0; i -= factor * scaleZ)
            {
                Line grad = new Line();
                grad.X1 = CANVAS_PADDING - HALF_GRADUATION;
                grad.X2 = CANVAS_PADDING + HALF_GRADUATION;
                grad.Y1 = i;
                grad.Y2 = i;
                DrawLine(canvas, grad, blackBrush);
                TextBlock num = new TextBlock();
                num.Text = length.ToString();
                num.Margin = new Thickness(HALF_GRADUATION, i - 2 * HALF_GRADUATION, 0, 0);
                canvas.Children.Add(num);
                length += factor;
            }

            //Units
            TextBlock unitX = new TextBlock();
            unitX.Text = "x (m)";
            unitX.Margin = new Thickness(canvas.ActualWidth - CANVAS_PADDING, canvas.ActualHeight -CANVAS_PADDING - 4*HALF_GRADUATION, 0, 0);
            canvas.Children.Add(unitX);
            TextBlock unitZ = new TextBlock();
            unitZ.Text = "z (m)";
            unitZ.Margin = new Thickness(CANVAS_PADDING + 2*HALF_GRADUATION, HALF_GRADUATION, 0, 0);
            canvas.Children.Add(unitZ);

            Model.Point latest = null;
            points.Add(equation.ZeroHeight);
            foreach (Model.Point point in points)
            {
                if (latest != null)
                {
                    DrawMainLine(canvas, redBrush, latest, point, scaleX, scaleZ);
                }
                latest = point;
            }
        }
        public void DrawSpeedCanvas(Canvas canvas, float precision, Equation equation)
        {
            List<Model.Point> pointsSpeedX = equation.GetPointsSpeedX(precision);
            List<Model.Point> pointsSpeedZ = equation.GetPointsSpeedZ(precision);

            float scaleX = (float)((canvas.ActualWidth - CANVAS_PADDING - MARGIN_SCALE) / equation.FlightTime);
            float scaleZ;
            float sZ = -equation.GetSpeed(equation.FlightTime).Z;
            float sX = equation.SpeedX;
            if (sZ > sX)
            {
                scaleZ = (float)((canvas.ActualHeight / 2 - MARGIN_SCALE) / sZ);
            }
            else
            {
                scaleZ = (float)((canvas.ActualHeight / 2 - MARGIN_SCALE) / sX);
            }

            scaleX = CheckScales(scaleX);
            scaleZ = CheckScales(scaleZ);

            //X-Axis
            Line xAxis = new Line();
            xAxis.X1 = 0;
            xAxis.X2 = canvas.ActualWidth;
            xAxis.Y1 = canvas.ActualHeight / 2;
            xAxis.Y2 = canvas.ActualHeight / 2;
            DrawLine(canvas, xAxis, blackBrush);

            float factor = GetFactor(scaleX);
            float length = 0;

            //X-Axis graduations
            for (float i = CANVAS_PADDING; i < canvas.ActualWidth; i += factor * scaleX)
            {
                Line grad = new Line();
                grad.X1 = i;
                grad.X2 = i;
                grad.Y1 = canvas.ActualHeight / 2 - HALF_GRADUATION;
                grad.Y2 = canvas.ActualHeight / 2 + HALF_GRADUATION;
                DrawLine(canvas, grad, blackBrush);
                TextBlock num = new TextBlock();
                num.Text = length.ToString();
                num.Margin = new Thickness(i - HALF_GRADUATION, canvas.ActualHeight / 2 + HALF_GRADUATION, 0, 0);
                canvas.Children.Add(num);
                length += factor;
            }

            //Z-Axis
            Line zAxis = new Line();
            zAxis.X1 = CANVAS_PADDING;
            zAxis.X2 = CANVAS_PADDING;
            zAxis.Y1 = 0;
            zAxis.Y2 = canvas.ActualHeight;
            DrawLine(canvas, zAxis, blackBrush);

            factor = GetFactor(scaleZ);
            length = 0;
            //Z-Axis graduations
            for (float i = (float)canvas.ActualHeight / 2; i > 0; i -= factor * scaleZ)
            {
                Line grad = new Line();
                grad.X1 = CANVAS_PADDING - HALF_GRADUATION;
                grad.X2 = CANVAS_PADDING + HALF_GRADUATION;
                grad.Y1 = i;
                grad.Y2 = i;
                DrawLine(canvas, grad, blackBrush);
                TextBlock num = new TextBlock();
                num.Text = length.ToString();
                num.Margin = new Thickness(HALF_GRADUATION, i - 2 * HALF_GRADUATION, 0, 0);
                canvas.Children.Add(num);
                length += factor;
            }
            length = 0;
            for (float i = (float)canvas.ActualHeight / 2; i < (float)canvas.ActualHeight; i += factor * scaleZ)
            {
                Line grad = new Line();
                grad.X1 = CANVAS_PADDING - HALF_GRADUATION;
                grad.X2 = CANVAS_PADDING + HALF_GRADUATION;
                grad.Y1 = i;
                grad.Y2 = i;
                DrawLine(canvas, grad, blackBrush);
                TextBlock num = new TextBlock();
                num.Text = length.ToString();
                num.Margin = new Thickness(HALF_GRADUATION, i - 2 * HALF_GRADUATION, 0, 0);
                canvas.Children.Add(num);
                length -= factor;
            }

            Model.Point latestX = null;
            Model.Point latestZ = null;
            for(int i=0; i<pointsSpeedX.Count; i++)
            {
                if (latestX != null)
                {
                    DrawSpeedLine(canvas, redBrush, latestX, pointsSpeedX[i], scaleX, scaleZ);

                }
                latestX = pointsSpeedX[i];

                if (latestZ != null)
                {
                    DrawSpeedLine(canvas, blueBrush, latestZ, pointsSpeedZ[i], scaleX, scaleZ);
                }
                latestZ = pointsSpeedZ[i];
            }
        }
        public void DrawAccelerationCanvas(Canvas canvas, float precision, Equation equation)
        {
            List<Model.Point> points = equation.GetPointsAcceleration(precision);
            float scaleX = (float)((canvas.ActualWidth - CANVAS_PADDING - MARGIN_SCALE) / equation.FlightTime);
            float scaleZ = (float)((canvas.ActualHeight - CANVAS_PADDING - MARGIN_SCALE) / (-equation.Acceleration.Z));

            scaleX = CheckScales(scaleX);
            scaleZ = CheckScales(scaleZ);

            //X-Axis
            Line xAxis = new Line();
            xAxis.X1 = 0;
            xAxis.X2 = canvas.ActualWidth;
            xAxis.Y1 = CANVAS_PADDING;
            xAxis.Y2 = CANVAS_PADDING;
            DrawLine(canvas, xAxis, blackBrush);

            float factor = GetFactor(scaleX);
            float length = 0;

            //X-Axis graduations
            for (float i = CANVAS_PADDING; i < canvas.ActualWidth; i += factor * scaleX)
            {
                Line grad = new Line();
                grad.X1 = i;
                grad.X2 = i;
                grad.Y1 = CANVAS_PADDING - HALF_GRADUATION;
                grad.Y2 = CANVAS_PADDING + HALF_GRADUATION;
                DrawLine(canvas, grad, blackBrush);

                TextBlock num = new TextBlock();
                num.Text = length.ToString();
                num.Margin = new Thickness(i - HALF_GRADUATION, CANVAS_PADDING + HALF_GRADUATION, 0, 0);
                canvas.Children.Add(num);
                length += factor;
            }

            //Z-Axis
            Line zAxis = new Line();
            zAxis.X1 = CANVAS_PADDING;
            zAxis.X2 = CANVAS_PADDING;
            zAxis.Y1 = 0;
            zAxis.Y2 = canvas.ActualHeight;
            DrawLine(canvas, zAxis, blackBrush);


            factor = GetFactor(scaleZ);
            length = 0;
            //Z-Axis graduations
            for (float i = CANVAS_PADDING; i < canvas.ActualHeight; i += factor * scaleZ)
            {
                Line grad = new Line();
                grad.X1 = CANVAS_PADDING - HALF_GRADUATION;
                grad.X2 = CANVAS_PADDING + HALF_GRADUATION;
                grad.Y1 = i;
                grad.Y2 = i;
                DrawLine(canvas, grad, blackBrush);
                TextBlock num = new TextBlock();
                num.Text = length.ToString();
                num.Margin = new Thickness(HALF_GRADUATION, i - 2 * HALF_GRADUATION, 0, 0);
                canvas.Children.Add(num);
                length += factor;
            }

            Model.Point latest = null;
            foreach (Model.Point point in points)
            {
                if (latest != null)
                {
                    DrawAccelerationLine(canvas, redBrush, latest, point, scaleX, scaleZ);
                }
                latest = point;
            }

        }
        public void DrawEnergiesCanvas(Canvas canvas, float precision, Equation equation)
        {
            List<Model.Point> pointsKinetic = equation.GetPointsKineticEnergy(precision);
            List<Model.Point> pointsPotential = equation.GetPointsPotentialEnergy(precision);

            float scaleX = (float)((canvas.ActualWidth - CANVAS_PADDING - MARGIN_SCALE) / equation.FlightTime);
            float scaleZ = (float)(canvas.ActualHeight - CANVAS_PADDING - MARGIN_SCALE) / equation.GetTotalEnergy(0);

            scaleX = CheckScales(scaleX);
            scaleZ = CheckScales(scaleZ);

            //X-Axis
            Line xAxis = new Line();
            xAxis.X1 = 0;
            xAxis.X2 = canvas.ActualWidth;
            xAxis.Y1 = canvas.ActualHeight - CANVAS_PADDING;
            xAxis.Y2 = canvas.ActualHeight - CANVAS_PADDING;
            DrawLine(canvas, xAxis, blackBrush);

            float factor = GetFactor(scaleX);
            float length = 0;

            //X-Axis graduations
            for (float i = CANVAS_PADDING; i < canvas.ActualWidth; i += factor * scaleX)
            {
                Line grad = new Line();
                grad.X1 = i;
                grad.X2 = i;
                grad.Y1 = canvas.ActualHeight - CANVAS_PADDING - HALF_GRADUATION;
                grad.Y2 = canvas.ActualHeight - CANVAS_PADDING + HALF_GRADUATION;
                DrawLine(canvas, grad, blackBrush);
                TextBlock num = new TextBlock();
                num.Text = length.ToString();
                num.Margin = new Thickness(i - HALF_GRADUATION, canvas.ActualHeight - CANVAS_PADDING + HALF_GRADUATION, 0, 0);
                canvas.Children.Add(num);
                length += factor;
            }

            //Z-Axis
            Line zAxis = new Line();
            zAxis.X1 = CANVAS_PADDING;
            zAxis.X2 = CANVAS_PADDING;
            zAxis.Y1 = 0;
            zAxis.Y2 = canvas.ActualHeight;
            DrawLine(canvas, zAxis, blackBrush);

            factor = GetFactor(scaleZ);
            length = 0;
            //Z-Axis graduations
            for (float i = (float)canvas.ActualHeight - CANVAS_PADDING; i > 0; i -= factor * scaleZ)
            {
                Line grad = new Line();
                grad.X1 = CANVAS_PADDING - HALF_GRADUATION;
                grad.X2 = CANVAS_PADDING + HALF_GRADUATION;
                grad.Y1 = i;
                grad.Y2 = i;
                DrawLine(canvas, grad, blackBrush);
                TextBlock num = new TextBlock();
                num.Text = length.ToString();
                num.Margin = new Thickness(HALF_GRADUATION, i - 2 * HALF_GRADUATION, 0, 0);
                canvas.Children.Add(num);
                length += factor;
            }

            Model.Point latestK = null;
            Model.Point latestP = null;
            Model.Point latestT = null;
            for (int i=0; i<pointsKinetic.Count; i++)
            {
                if (latestK != null)
                {
                    DrawEnergyLine(canvas, blueBrush, latestK, pointsKinetic[i], scaleX, scaleZ);
                }
                latestK = pointsKinetic[i];
                if (latestP != null)
                {
                    DrawEnergyLine(canvas, greenBrush, latestP, pointsPotential[i], scaleX, scaleZ);
                }
                latestP = pointsPotential[i];
                if (latestT != null)
                {
                    DrawEnergyLine(canvas, redBrush, latestT, new Model.Point(pointsKinetic[i].X, pointsKinetic[i].Z + pointsPotential[i].Z), scaleX, scaleZ);
                }
                latestT = new Model.Point(pointsKinetic[i].X, pointsKinetic[i].Z + pointsPotential[i].Z);
            }
        }
        private float GetFactor(float scale)
        {
            float rawFactor = (float)Math.Floor(100 / scale);
            if(rawFactor == 0)
            {
                return 1;
            }
            string str = rawFactor.ToString();
            int digits = str.Length;
            if (digits == 1)
            {
                return rawFactor;
            }
            else if (int.Parse(str[1].ToString()) < 5)
            {
                return (float)(int.Parse(str[0].ToString()) * Math.Pow(10, digits - 1));
            }
            else
            {
                return (float)((int.Parse(str[0].ToString()) + 1) * Math.Pow(10, digits - 1));
            }
        }
        private float CheckScales(float scale)
        {
            if (float.IsNaN(scale) || float.IsInfinity(scale))
            {
                return 1;
            }
            else
            {
                return scale;
            }
        }
        private void DrawLine(Canvas canvas, Line axis, SolidColorBrush brush)
        {
            axis.StrokeThickness = 1;
            axis.Stroke = brush;
            canvas.Children.Add(axis);
        }
        private void DrawMainLine(Canvas canvas, SolidColorBrush brush, Model.Point latest, Model.Point next, float scaleX, float scaleZ)
        {
            double invert = canvas.ActualHeight;
            Line l = new Line();
            l.X1 = (scaleX * latest.X) + CANVAS_PADDING;
            l.X2 = (scaleX * next.X) + CANVAS_PADDING;
            l.Y1 = invert - CANVAS_PADDING - scaleZ * latest.Z;
            l.Y2 = invert - CANVAS_PADDING - scaleZ * next.Z;
            DrawLine(canvas, l, brush);
        }
        private void DrawSpeedLine(Canvas canvas, SolidColorBrush brush, Model.Point latest, Model.Point next, float scaleX, float scaleZ)
        {
            double invert = canvas.ActualHeight;
            Line l = new Line();
            l.X1 = (scaleX * latest.X) + CANVAS_PADDING;
            l.X2 = (scaleX * next.X) + CANVAS_PADDING;
            l.Y1 = invert - canvas.ActualHeight / 2 - scaleZ * latest.Z;
            l.Y2 = invert - canvas.ActualHeight / 2 - scaleZ * next.Z;
            DrawLine(canvas, l, brush);
        }
        private void DrawAccelerationLine(Canvas canvas, SolidColorBrush brush, Model.Point latest, Model.Point next, float scaleX, float scaleZ)
        {

            Line l = new Line();
            l.X1 = (scaleX * latest.X) + CANVAS_PADDING;
            l.X2 = (scaleX * next.X) + CANVAS_PADDING;
            l.Y1 = -(scaleZ * latest.Z) + CANVAS_PADDING;
            l.Y2 = -(scaleZ * next.Z) + CANVAS_PADDING;
            DrawLine(canvas, l, brush);
        }
        private void DrawEnergyLine(Canvas canvas, SolidColorBrush brush, Model.Point latest, Model.Point next, float scaleX, float scaleZ)
        {
            double invert = canvas.ActualHeight;
            Line l = new Line();
            l.X1 = (scaleX * latest.X) + CANVAS_PADDING;
            l.X2 = (scaleX * next.X) + CANVAS_PADDING;
            l.Y1 = invert - CANVAS_PADDING - scaleZ * latest.Z;
            l.Y2 = invert - CANVAS_PADDING - scaleZ * next.Z;
            DrawLine(canvas, l, brush);
        }
    }
}
