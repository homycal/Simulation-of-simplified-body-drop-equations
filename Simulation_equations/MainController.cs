﻿using System;
using System.Collections.Generic;
using System.Text;
using View;
using Model;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace Controller
{
    class MainController
    {
        private const float CANVAS_PADDING = 30;
        private const float HALF_GRADUATION = 5;
        private const float MARGIN_SCALE = 30;
        private const float PRECISION_FACTOR = 0.045f;
        private const int CANVAS_MAIN = 0;
        private const int CANVAS_SPEED = 1;
        private const int CANVAS_ACCELERATION = 2;
        private const int CANVAS_ENERGY = 3;

        public void PlotEquation(List<Canvas> canvas, Equation equation)
        {
            foreach (Canvas c in canvas)
            {
                c.Children.Clear();
            }
            //MainCanvas
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;
            SolidColorBrush blueBrush = new SolidColorBrush();
            blueBrush.Color = Colors.Blue;
            SolidColorBrush greenBrush = new SolidColorBrush();
            greenBrush.Color = Colors.Green;
            float precisionDistance = equation.ZeroHeight.X * PRECISION_FACTOR;
            float precisionTime = equation.FlightTime * PRECISION_FACTOR;
            DrawOnCanvas(canvas[0], equation.GetPoints(precisionDistance), redBrush, equation, CANVAS_MAIN);
            //SpeedCanvas
            DrawOnCanvas(canvas[1], equation.GetPointsSpeedX(precisionTime), redBrush, equation, CANVAS_SPEED);
            DrawOnCanvas(canvas[1], equation.GetPointsSpeedZ(precisionTime), blueBrush, equation, CANVAS_SPEED);

            //AccelerationCanvas
            //DrawOnCanvas(canvas[2], equation.GetPointsAcceleration(precision), redBrush, equation);
            //EnergyCanvas
            DrawOnCanvas(canvas[3], equation.GetPointsTotalEnergy(precisionTime), redBrush, equation, CANVAS_ENERGY);
            DrawOnCanvas(canvas[3], equation.GetPointsKineticEnergy(precisionTime), blueBrush, equation, CANVAS_ENERGY);
            DrawOnCanvas(canvas[3], equation.GetPointsPotentialEnergy(precisionTime), greenBrush, equation, CANVAS_ENERGY);



        }
        private void DrawOnCanvas(Canvas canvas, List<Model.Point> points, SolidColorBrush brush, Equation equation, int canvasType)
        {
            float scaleX = 0;
            float scaleZ = 0;
            if(canvasType == CANVAS_MAIN)
            {
                scaleX = (float)(canvas.ActualWidth - CANVAS_PADDING - MARGIN_SCALE) / equation.ZeroHeight.X;
                scaleZ = (float)(canvas.ActualHeight - CANVAS_PADDING - MARGIN_SCALE) / equation.MaxHeight.Z;
            }
            else if (canvasType == CANVAS_SPEED)
            {
                scaleX = (float)((canvas.ActualWidth - CANVAS_PADDING - MARGIN_SCALE) /equation.FlightTime);
                scaleZ = (float)((canvas.ActualHeight - CANVAS_PADDING - MARGIN_SCALE) / equation.SpeedInit);
            }
            else if (canvasType == CANVAS_ENERGY)
            {
                scaleX = (float)((canvas.ActualWidth - CANVAS_PADDING - MARGIN_SCALE) / equation.FlightTime);
                scaleZ = (float)(canvas.ActualHeight - CANVAS_PADDING - MARGIN_SCALE) / equation.GetKineticEnergy(0);
            }


            DrawAxes(canvas, scaleX, scaleZ);
            Model.Point latest = null;
            foreach (Model.Point point in points)
            {
                if (latest != null)
                {
                    DrawLine(canvas, latest, point, brush, scaleX, scaleZ);
                }
                latest = point;
            }
            if (canvasType == CANVAS_MAIN)
            {
                DrawLine(canvas, latest, new Model.Point(equation.ZeroHeight.X, 0), brush, scaleX, scaleZ);
            }
        }

        private void DrawLine(Canvas canvas, Model.Point p1, Model.Point p2, SolidColorBrush brush, float scaleX, float scaleZ)
        {
            double invert = canvas.ActualHeight;
            Line l = new Line();
            l.X1 = (scaleX * p1.X) + CANVAS_PADDING;
            l.X2 = (scaleX * p2.X) + CANVAS_PADDING;
            l.Y1 = invert - CANVAS_PADDING - scaleZ * p1.Z;
            l.Y2 = invert - CANVAS_PADDING - scaleZ * p2.Z;
            l.StrokeThickness = 1;
            l.Stroke = brush;

            canvas.Children.Add(l);
        }

        private void DrawAxes(Canvas canvas, float scaleX, float scaleZ)
        {
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;

            //X-Axis
            Line xAxis = new Line();
            xAxis.X1 = 0;
            xAxis.X2 = canvas.ActualWidth;
            xAxis.Y1 = canvas.ActualHeight - CANVAS_PADDING;
            xAxis.Y2 = canvas.ActualHeight - CANVAS_PADDING;
            xAxis.StrokeThickness = 1;
            xAxis.Stroke = blackBrush;
            canvas.Children.Add(xAxis);

            //Z-Axis
            Line zAxis = new Line();
            zAxis.X1 = CANVAS_PADDING;
            zAxis.X2 = CANVAS_PADDING;
            zAxis.Y1 = 0;
            zAxis.Y2 = canvas.ActualHeight;
            zAxis.StrokeThickness = 1;
            zAxis.Stroke = blackBrush;
            canvas.Children.Add(zAxis);

            float factor = GetFactor((float)Math.Floor(100 / scaleX));
            if (factor == 0) factor = 1;

            float length = 0;

            //X-Axis graduations
            for (float i = CANVAS_PADDING; i < canvas.ActualWidth; i += factor * scaleX)
            {
                Line grad = new Line();
                grad.X1 = i;
                grad.X2 = i;
                grad.Y1 = canvas.ActualHeight - CANVAS_PADDING - HALF_GRADUATION;
                grad.Y2 = canvas.ActualHeight - CANVAS_PADDING + HALF_GRADUATION;
                grad.StrokeThickness = 1;
                grad.Stroke = blackBrush;
                canvas.Children.Add(grad);
                TextBlock num = new TextBlock();
                num.Text = length.ToString();
                num.Margin = new Thickness(i - HALF_GRADUATION, canvas.ActualHeight - CANVAS_PADDING + HALF_GRADUATION, 0, 0);
                canvas.Children.Add(num);
                length += factor;
            }

            factor = GetFactor((float)Math.Floor(100 / scaleZ));
            if (factor == 0) factor = 1;

            length = 0;
            //Z-Axis graduations
            for (float i = (float)canvas.ActualHeight - CANVAS_PADDING; i > 0; i -= factor * scaleZ)
            {
                Line grad = new Line();
                grad.X1 = CANVAS_PADDING - HALF_GRADUATION;
                grad.X2 = CANVAS_PADDING + HALF_GRADUATION;
                grad.Y1 = i;
                grad.Y2 = i;
                grad.StrokeThickness = 1;
                grad.Stroke = blackBrush;
                canvas.Children.Add(grad);
                TextBlock num = new TextBlock();
                num.Text = length.ToString();
                num.Margin = new Thickness(HALF_GRADUATION, i - 2 * HALF_GRADUATION, 0, 0);
                canvas.Children.Add(num);
                length += factor;
            }
        }

        private float GetFactor(float rawFactor)
        {
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
    }
}
