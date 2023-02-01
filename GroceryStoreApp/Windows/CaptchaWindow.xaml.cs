using GroceryStoreApp.CsClasses;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Shapes;

namespace GroceryStoreApp.Windows
{
    public partial class CaptchaWindow : Window
    {
        Random randomСoordinates = new Random();
       
        List<(int x, int y)> coordinatesList = new List<(int x, int y)>();

        string captchaContentString; 
        bool correctCoordinatesBool = false; 
                                             
        string symbolString;
        int xCoordinates;
        int yCoordinates; 
        int distant = 20;
        int numberSymbols = 5;

        public CaptchaWindow()
        {
            InitializeComponent();
            CreateCaptcha();
        }


        private void CreateCaptcha() 
        {
            CaptchaGrid.Children.Clear();
            coordinatesList.Add((randomСoordinates.Next(350), randomСoordinates.Next(350))); 

            Ellipse ellipse = new Ellipse(); 
            ellipse.Fill = System.Windows.Media.Brushes.Black; 

            ellipse.Height = 20;
            ellipse.Width = 20;

            ellipse.HorizontalAlignment = HorizontalAlignment.Left;
            ellipse.VerticalAlignment = VerticalAlignment.Top;

            ellipse.RenderTransform = new TranslateTransform(coordinatesList[0].x - 10, coordinatesList[0].y - 10);
            CaptchaGrid.Children.Add(ellipse);

            for (int i = 0; i < numberSymbols; i++) 
            {
                symbolString = ""; 
                captchaContentString += symbolString += (char)randomСoordinates.Next(97, 122);
              
                

                Line directionLine = new Line(); 
                
                directionLine.Stroke = System.Windows.Media.Brushes.Black;

                directionLine.X1 = coordinatesList[i].x;
                directionLine.Y1 = coordinatesList[i].y;
               
                correctCoordinatesBool = false;
                while (correctCoordinatesBool != true) 
                {
                    xCoordinates = randomСoordinates.Next(350);
                    for (int j = 0; j < coordinatesList.Count;)
                    {

                        if (xCoordinates + distant < coordinatesList[j].x || xCoordinates - distant > coordinatesList[j].x)
                        {
                            j++;
                            correctCoordinatesBool = true;
                        }
                        else
                        {
                            correctCoordinatesBool = false;
                            break;
                        }
                    }
                }
                correctCoordinatesBool = false;
                while (correctCoordinatesBool != true) 
                {
                    yCoordinates = randomСoordinates.Next(350);
                    for (int j = 0; j < coordinatesList.Count;)
                    {

                        if (yCoordinates + distant < coordinatesList[j].y || yCoordinates - distant > coordinatesList[j].y)
                        {
                            j++;
                            correctCoordinatesBool = true;
                        }
                        else
                        {
                            correctCoordinatesBool = false;
                            break;
                        }
                    }
                }
                coordinatesList.Add((xCoordinates, yCoordinates)); 

                directionLine.X2 = coordinatesList[i + 1].x;
                directionLine.Y2 = coordinatesList[i + 1].y;

                FormattedText formattedText = new FormattedText( 
                    symbolString, 
                    CultureInfo.GetCultureInfo("en-us"), 
                    FlowDirection.LeftToRight, 
                    new Typeface(new System.Windows.Media.FontFamily(),  
                    FontStyles.Italic,
                    FontWeights.Bold, 
                    FontStretches.Normal), 
                    24, Brushes.Black, 
                    VisualTreeHelper.GetDpi(this).PixelsPerDip);

                Geometry symbolGeometry = formattedText.BuildGeometry(new System.Windows.Point(directionLine.X1, directionLine.Y1)); 

                var symbolPath = new Path();
                symbolPath.Stroke = System.Windows.Media.Brushes.Black;
                symbolPath.Fill = System.Windows.Media.Brushes.MediumSlateBlue;
                symbolPath.Data = symbolGeometry; 

                CaptchaGrid.Children.Add(symbolPath);  
                CaptchaGrid.Children.Add(directionLine);
            }
            CaptchaGrid.Children.RemoveAt(numberSymbols*2); 
            coordinatesList.Clear();
        }

        private void CaptchaButton_Click(object sender, RoutedEventArgs e) 
        {
            if (CaptchaTextBox.Text == captchaContentString)
            {
                MenuWindow menuWindow = new MenuWindow();
                menuWindow.Show();
                this.Close();

            }
            else
            {
                ParametersClass.TimerStart = true;
                AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                authorizationWindow.Show(); 
                
                this.Close();
            }
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ChangeCaptchaButton_Click(object sender, RoutedEventArgs e)
        {
            CreateCaptcha();
        }
    }
}
