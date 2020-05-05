using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Model;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const float CANVAS_PADDING = 30;
        private const float HALF_GRADUATION = 5;
        private const float MARGIN_SCALE = 30;
        private const float PRECISION_FACTOR = 0.045f;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCalcul_Click_1(object sender, RoutedEventArgs e)
        {

            float textSpeed = float.Parse(TextBoxSpeed.Text);
            float textAngle = float.Parse(TextBoxAngle.Text);
            float textHeight = float.Parse(TextBoxHeight.Text);
            float textGravity = float.Parse(TextBoxGravity.Text);
            float textWeight = float.Parse(TextBoxWeight.Text);

            Equation equation = new Equation(textSpeed, textAngle, textGravity, textHeight);

            plotEquation(equation);

        }

        private void plotEquation(Equation equation)
        {
            canvasMainGraph.Children.Clear();
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;
            float precision = equation.getZeroHeight().X*PRECISION_FACTOR;
            float test = precision / equation.getZeroHeight().X;
            //float precision =getPrecision(equation);
            LinkedList<Model.Point> points = equation.getPoints(precision);
            float scaleX = (float)(canvasMainGraph.ActualWidth - CANVAS_PADDING - MARGIN_SCALE)/equation.getZeroHeight().X;
            float scaleZ = (float)(canvasMainGraph.ActualHeight - CANVAS_PADDING - MARGIN_SCALE) / equation.getMaxHeight().Z;

            drawAxes(canvasMainGraph, scaleX, scaleZ);
            Model.Point latest = null;
            foreach(Model.Point point in points)
            {
                if(latest != null)
                {
                    drawLine(latest, point, redBrush, scaleX, scaleZ);
                }
                latest = point;
            }
            drawLine(latest, new Model.Point(equation.getZeroHeight().X, 0), redBrush, scaleX, scaleZ);
        }

        private void drawLine(Model.Point p1, Model.Point p2, SolidColorBrush brush, float scaleX, float scaleZ)
        {
            double invert = canvasMainGraph.ActualHeight;
            Line l = new Line();
            l.X1 = (scaleX * p1.X)+CANVAS_PADDING;
            l.X2 = (scaleX * p2.X)+CANVAS_PADDING;
            l.Y1 = invert - CANVAS_PADDING - scaleZ * p1.Z;
            l.Y2 = invert - CANVAS_PADDING - scaleZ * p2.Z;
            l.StrokeThickness = 1;
            l.Stroke = brush;

            canvasMainGraph.Children.Add(l);
        }

        private void drawAxes(Canvas canvas, float scaleX, float scaleZ)
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

            float factor = getFactor((float)Math.Floor(100 / scaleX));
            if (factor == 0) factor = 1;

            float length = 0;

            //X-Axis graduations
            for (float i=CANVAS_PADDING; i < canvas.ActualWidth; i+= factor*scaleX)
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
                num.Margin = new Thickness(i-HALF_GRADUATION, canvas.ActualHeight - CANVAS_PADDING + HALF_GRADUATION, 0, 0);
                canvas.Children.Add(num);
                length+=factor;
            }

            factor = getFactor((float)Math.Floor(100 / scaleZ));
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
                num.Margin = new Thickness(HALF_GRADUATION , i- 2* HALF_GRADUATION, 0, 0 );
                canvas.Children.Add(num);
                length += factor;
            }
        }

        private float getFactor(float rawFactor)
        {
            string str = rawFactor.ToString();
            int digits = str.Length;
            if(digits==1)
            {
                return rawFactor;
            }
            else if (int.Parse(str[1].ToString()) < 5)
            {
                return (float)(int.Parse(str[0].ToString())*Math.Pow(10, digits-1));
            }
            else
            {
                return (float)((int.Parse(str[0].ToString()) + 1)*Math.Pow(10, digits-1));


            }
        }

        private void SliderSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxSpeed.Text = SliderSpeed.Value.ToString();
        }

        private void SliderAngle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxAngle.Text = SliderAngle.Value.ToString();
        }

        private void SliderHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxHeight.Text = SliderHeight.Value.ToString();
        }

        private void SliderGravity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxGravity.Text = SliderGravity.Value.ToString();
        }

        private void SliderWeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxWeight.Text = SliderWeight.Value.ToString();
        }
    }
}
