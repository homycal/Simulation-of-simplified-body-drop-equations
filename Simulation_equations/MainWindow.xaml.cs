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
            Title = "Simulation of simplified body drop equations";
            controller = new MainController(this);
            canvas.Add(CanvasMainGraph);
            canvas.Add(CanvasSpeed);
            canvas.Add(CanvasAcceleration);
            canvas.Add(CanvasEnergy);
            SolidColorBrush redBrush = new SolidColorBrush();
            redBrush.Color = Colors.Red;
            TextBlockError.Foreground = redBrush;

        }
        /// <summary>
        /// Action when the button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCalcul_Click_1(object sender, RoutedEventArgs e)
        {

            GetValues();
        }
        /// <summary>
        /// Get values form TextBoxes
        /// </summary>
        private void GetValues()
        {
            try
            {
                textSpeed = CheckValue(SliderSpeed, TextBoxSpeed);
                textAngle = CheckValue(SliderAngle, TextBoxAngle);
                textHeight = CheckValue(SliderHeight, TextBoxHeight);
                textGravity = CheckValue(SliderGravity, TextBoxGravity);
                textWeight = CheckValue(SliderWeight, TextBoxWeight);
                equation = new Equation(textSpeed, textAngle, textGravity, textHeight, textWeight);
                controller.PlotEquation(canvas, equation);
                TextBlockEquationDesc.Text = equation.ToString();
                TextBlockError.Text = "";
            }
            catch(Exception e)
            {
                TextBlockError.Text = e.Message;

            }


        }
        /// <summary>
        /// Check if values in TextBoxes are fair
        /// Associate the value of a Slider and a Textbox
        /// </summary>
        /// <param name="slider"></param>
        /// <param name="textBox"></param>
        /// <returns></returns>
        private float CheckValue(Slider slider, TextBox textBox)
        {
            float value = float.Parse(textBox.Text);
            if (value > slider.Maximum)
            {
                value = (float)slider.Maximum;
            }
            if (value < slider.Minimum)
            {
                value = (float)slider.Minimum;
            }
            textBox.Text = value.ToString();
            slider.Value = value;
            return value;
        }
        /// <summary>
        /// Action when the value of the slider SliderSpeed has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxSpeed.Text = Math.Round(SliderSpeed.Value,2).ToString();
        }
        /// <summary>
        /// Action when the value of the slider SliderAngle has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderAngle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxAngle.Text = Math.Round(SliderAngle.Value,2).ToString();
        }
        /// <summary>
        /// Action when the value of the slider SliderHeight has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxHeight.Text = Math.Round(SliderHeight.Value,2).ToString();
        }
        /// <summary>
        /// Action when the value of the slider SliderGravity has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderGravity_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxGravity.Text = Math.Round(SliderGravity.Value,2).ToString();
        }
        /// <summary>
        /// Action when the value of the slider SliderWeight has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SliderWeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TextBoxWeight.Text = Math.Round(SliderWeight.Value,2).ToString();
        }
        /// <summary>
        /// Action when the grid size has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if(equation != null)
            {
                GetValues();

            }
        }
        /// <summary>
        /// Action when the TextBoxSpeed text has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (equation != null)
            {
                GetValues();
            }
        }
        /// Action when the TextBoxAngle text has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxAngle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (equation != null)
            {
                GetValues();
            }
        }
        /// Action when the TextBoxHeight text has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (equation != null)
            {
                GetValues();
            }
        }
        /// Action when the TextBoxGravity text has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxGravity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (equation != null)
            {
                GetValues();
            }
        }
        /// Action when the TextBoxWeight text has changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (equation != null)
            {
                GetValues();
            }
        }
        /// Action when there is a click on CanvasMainGraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
