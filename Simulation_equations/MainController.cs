using System;
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
        public void PlotEquation(List<Canvas> canvas, Equation equation)
        {
            foreach (Canvas c in canvas)
            {
                c.Children.Clear();
            }
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;
            float precision = equation.ZeroHeight.X * PRECISION_FACTOR;
            LinkedList<Model.Point> points = equation.GetPoints(precision);
            DrawOnCanvas(canvas[0], points, redBrush, equation);

        }
        private void DrawOnCanvas(Canvas canvas, LinkedList<Model.Point> points, SolidColorBrush brush, Equation equation)
        {
            float scaleX = (float)(canvas.ActualWidth - CANVAS_PADDING - MARGIN_SCALE) / equation.ZeroHeight.X;
            float scaleZ = (float)(canvas.ActualHeight - CANVAS_PADDING - MARGIN_SCALE) / equation.MaxHeight.Z;

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
            DrawLine(canvas, latest, new Model.Point(equation.ZeroHeight.X, 0), brush, scaleX, scaleZ);
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
