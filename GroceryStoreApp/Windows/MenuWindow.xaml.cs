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
        private readonly DoubleAnimation _doubleAnimation = new DoubleAnimation();

        int _quantity = 0;

        readonly List<(int access,Page page, string content, string geometryName)> navigationButtonList = new List<(int access, Page page, string content, string data)>()
        {
          (1,new AddUserPage(null), "Аккаунты","ProfilePathData"),
          (1,new DataOfUserPage(), "Сотрудники","ProfilePathData"),
          (1,new DataOfAddressPage(), "Адреса","ProfilePathData"),
          (3,new DataOfProductPage(), "Товары","ProductPathData"),
          (3,new AddProductPage(), "Добавить товар","AddProductPathData"),
          (3,new DataOfCategoriesPage(), "Категории","ProductPathData"),
          (3,new DataOfSupplyPage(), "Поставки","SupplyPathData"),
          (3,new DataOfGroupsPage(), "Товары","ProductPathData"),

        };
        public MenuWindow()
        {

            InitializeComponent();
            StatusBarTextBlock.Text = "Главное меню - Приветствие";

            //MenuFrame.Navigate(new WelcomePage());

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
                    Tag = _quantity++,
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
                ResizeMode = ResizeMode.NoResize;  
                WindowState = WindowState.Maximized;
                
            }
            else
            {
                ResizeMode = ResizeMode.CanResize;
                WindowState = WindowState.Normal;
                
            }
        }
        private void ControlPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            _doubleAnimation.From = ControlGrid.ActualWidth;
            _doubleAnimation.To = 210;
            _doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
            _doubleAnimation.EasingFunction = new QuadraticEase();
            ControlGrid.BeginAnimation(WidthProperty, _doubleAnimation);
        }

        private void ControlPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            _doubleAnimation.From = ControlGrid.ActualWidth;
            _doubleAnimation.To = 70;
            _doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
            _doubleAnimation.EasingFunction = new QuadraticEase();
            ControlGrid.BeginAnimation(WidthProperty, _doubleAnimation);
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                ResizeMode = ResizeMode.CanResize;
                StateScreenButton.Data = (Geometry)Application.Current.FindResource("CollapsePathData");
            }
            else
            {
                
                StateScreenButton.Data = (Geometry)Application.Current.FindResource("FullScreenPathData");
            }
        }
    }
}
