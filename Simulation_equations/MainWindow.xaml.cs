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
        public MainWindow()
        {
            InitializeComponent();
        }

        private MainController controller = new MainController();
        private void ButtonCalcul_Click_1(object sender, RoutedEventArgs e)
        {

            float textSpeed = float.Parse(TextBoxSpeed.Text);
            float textAngle = float.Parse(TextBoxAngle.Text);
            float textHeight = float.Parse(TextBoxHeight.Text);
            float textGravity = float.Parse(TextBoxGravity.Text);
            float textWeight = float.Parse(TextBoxWeight.Text);
            //TODO: Check errors if a letter is entered
            Equation equation = new Equation(textSpeed, textAngle, textGravity, textHeight, textWeight);

            List<Canvas> canvas = new List<Canvas>();
            canvas.Add(CanvasMainGraph);
            canvas.Add(CanvasSpeed);
            canvas.Add(CanvasAcceleration);
            canvas.Add(CanvasEnergy);

            controller.PlotEquation(canvas, equation);
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
