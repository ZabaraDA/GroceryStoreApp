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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfCustomControlLibrary;
using GroceryStoreApp.Pages;
using System.Windows.Markup;

namespace GroceryStoreApp.Windows
{
    public partial class MenuWindow : Window
    {
        DoubleAnimation doubleAnimation = new DoubleAnimation();

        int quantity = 0;

        readonly List<(int access,Page page, string content, string geometryName)> navigationButtonList = new List<(int access, Page page, string content, string data)>()
        {
          (1,new AddUserPage(null), "Аккаунты","ProfilePathData"),
          (1,new DataOfUserPage(), "Сотрудники","ProfilePathData"),
          (1,new DataOfAddressPage(), "Адреса","ProfilePathData"),
          (3,new DataOfProductPage(), "Товары","ProductPathData"),
          (3,new AddProductPage(), "Добавить товар","AddProductPathData"),
          (3,new DataOfCategoriesPage(), "Категории","ProductPathData"),
          (3,new DataOfSupplyPage(), "Поставки","SupplyPathData"),

        };
        public MenuWindow()
        {

            InitializeComponent();

            //ExitPath.Data = Geometry.Parse(PathDataClass.exitData);
            //WindowStatePath.Data = Geometry.Parse(PathDataClass.fullScreenData);
            //HidePath.Data = Geometry.Parse(PathDataClass.hideData);
            StatusBarTextBlock.Text = "Главное меню - Приветствие";

            MenuFrame.Navigate(new WelcomePage());

            ButtonGenerator();
        }

        private void ButtonGenerator()
        {
            for (int i = 0; i < navigationButtonList.Count; i++)
            {
                NavigationButton navigationButton = new NavigationButton()
                {
                    Content = navigationButtonList[i].content,
                    Data = (Geometry)Application.Current.FindResource(navigationButtonList[i].geometryName),
                    Tag = quantity++,

                };
                 
                navigationButton.Click += Button_Click;
                ControlStackPanel.Children.Add(navigationButton);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var a = sender as NavigationButton;
            StatusBarTextBlock.Text = "Главное меню - " + navigationButtonList[(int)a.Tag].content;
            MenuFrame.Navigate(navigationButtonList[(int)a.Tag].page);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void CollapseButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void StateScreenButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
                StateScreenButton.Data = (Geometry)Application.Current.FindResource("CollapsePathData");
            }
            else
            {
                WindowState = WindowState.Normal;
                StateScreenButton.Data = (Geometry)Application.Current.FindResource("FullScreenPathData");
            }
        }
        private void ControlPanel_MouseEnter(object sender, MouseEventArgs e)
        {

            doubleAnimation.From = ControlGrid.ActualWidth;
            doubleAnimation.To = 210;
            doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
            doubleAnimation.EasingFunction = new QuadraticEase();
            ControlGrid.BeginAnimation(WidthProperty, doubleAnimation);
        }

        private void ControlPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            doubleAnimation.From = ControlGrid.ActualWidth;
            doubleAnimation.To = 70;
            doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
            doubleAnimation.EasingFunction = new QuadraticEase();
            ControlGrid.BeginAnimation(WidthProperty, doubleAnimation);
        }
    }
}
