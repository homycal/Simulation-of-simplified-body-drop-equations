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
using Controller;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainController controller;
        private Equation equation;
        private List<Canvas> canvas = new List<Canvas>();
        private float textSpeed;
        private float textAngle;
        private float textHeight;
        private float textGravity;
        private float textWeight;
        public MainWindow()
        {
            InitializeComponent();
            controller = new MainController(this);
            canvas.Add(CanvasMainGraph);
            canvas.Add(CanvasSpeed);
            canvas.Add(CanvasAcceleration);
            canvas.Add(CanvasEnergy);
        }

        private void ButtonCalcul_Click_1(object sender, RoutedEventArgs e)
        {

            GetValues();
        }

        private void GetValues()
        {
            textSpeed = float.Parse(TextBoxSpeed.Text);
            textAngle = float.Parse(TextBoxAngle.Text);
            textHeight = float.Parse(TextBoxHeight.Text);
            textGravity = float.Parse(TextBoxGravity.Text);
            textWeight = float.Parse(TextBoxWeight.Text);
            //TODO: Check errors if a letter is entered
            equation = new Equation(textSpeed, textAngle, textGravity, textHeight, textWeight);
            controller.PlotEquation(canvas, equation);
            TextBlockEquationDesc.Text = equation.ToString();

        }
        private void SliderSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxSpeed.Text = Math.Round(SliderSpeed.Value,2).ToString();
        }

        private void SliderAngle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxAngle.Text = Math.Round(SliderAngle.Value,2).ToString();
        }

        private void SliderHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxHeight.Text = Math.Round(SliderHeight.Value,2).ToString();
        }

        private void SliderGravity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxGravity.Text = Math.Round(SliderGravity.Value,2).ToString();
        }

        private void SliderWeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxWeight.Text = Math.Round(SliderWeight.Value,2).ToString();
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(equation != null)
            {
                GetValues();

            }
        }

        private void TextBoxSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (equation != null)
            {
                GetValues();
            }
        }

        private void TextBoxAngle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (equation != null)
            {
                GetValues();
            }
        }

        private void TextBoxHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (equation != null)
            {
                GetValues();
            }
        }

        private void TextBoxGravity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (equation != null)
            {
                GetValues();
            }
        }

        private void TextBoxWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (equation != null)
            {
                GetValues();
            }
        }

        private void CanvasMainGraph_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (equation != null)
            {
                controller.SetCoordText(e.GetPosition(CanvasMainGraph), CanvasMainGraph);
                controller.DrawPointerLine(e.GetPosition(CanvasMainGraph), CanvasMainGraph);
            }
        }
    }
}
