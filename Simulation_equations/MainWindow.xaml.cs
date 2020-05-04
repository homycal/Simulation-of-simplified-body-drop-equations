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
        private const float CANVAS_PADDING = 20;
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
            drawAxes(canvasMainGraph);
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;
            //float precision = equation.getZeroHeight()
            float precision = 1;
            LinkedList<Model.Point> points = equation.getPoints(precision);
            float scaleX = (float)(canvasMainGraph.RenderSize.Width - CANVAS_PADDING)/ points.Count;
            float scaleZ = (float)(canvasMainGraph.RenderSize.Height - CANVAS_PADDING) / points.Count;
            Model.Point latest = null;
            foreach(Model.Point point in points)
            {
                if(latest != null)
                {
                    drawLine(latest, point, redBrush, scaleX, scaleZ);
                }
                latest = point;
            }
            drawLine(latest, new Model.Point(equation.getZeroHeight(), 0), redBrush, scaleX, scaleZ);
        }

        private void drawLine(Model.Point p1, Model.Point p2, SolidColorBrush brush, float scaleX, float scaleZ)
        {
            double invert = canvasMainGraph.RenderSize.Height;
            Line l = new Line();
            l.X1 = (scaleX * p1.X)+CANVAS_PADDING;
            l.X2 = (scaleX * p2.X)+CANVAS_PADDING;
            l.Y1 = invert - CANVAS_PADDING - scaleZ * p1.Z;
            l.Y2 = invert - CANVAS_PADDING - scaleZ * p2.Z;
            l.StrokeThickness = 1;
            l.Stroke = brush;

            canvasMainGraph.Children.Add(l);
        }

        private void drawAxes(Canvas canvas)
        {
            SolidColorBrush blackBrush = new SolidColorBrush();
            blackBrush.Color = Colors.Black;


            //X-Axis
            Line xAxis = new Line();
            xAxis.X1 = CANVAS_PADDING;
            xAxis.X2 = CANVAS_PADDING;
            xAxis.Y1 = 0;
            xAxis.Y2 = canvas.RenderSize.Height;
            xAxis.StrokeThickness = 1;
            xAxis.Stroke = blackBrush;
            canvasMainGraph.Children.Add(xAxis);

            //Y-Axis
            Line yAxis = new Line();
            yAxis.X1 = 0;
            yAxis.X2 = canvas.RenderSize.Width;
            yAxis.Y1 = canvas.RenderSize.Height - CANVAS_PADDING;
            yAxis.Y2 = canvas.RenderSize.Height - CANVAS_PADDING;
            yAxis.StrokeThickness = 1;
            yAxis.Stroke = blackBrush;
            canvasMainGraph.Children.Add(yAxis);
        }
    }
}
