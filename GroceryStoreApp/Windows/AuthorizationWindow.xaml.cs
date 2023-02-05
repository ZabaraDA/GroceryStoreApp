using GroceryStoreApp.CsClasses;
using GroceryStoreApp.Windows;
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
using System.Windows.Threading;
using GroceryStoreApp.Databases;

namespace GroceryStoreApp
{
    public partial class AuthorizationWindow : Window
    {
        GroceryStoreDatabasesEntities databasesEntities = new GroceryStoreDatabasesEntities();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        bool incorrectly = false;
        int timerTick = 9;
        public AuthorizationWindow()
        {
            InitializeComponent();
            LoginTextBox.Text = "ad";
            PasswordBox.Password = "ad";

            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            TimerTextBlock.Text = timerTick.ToString();
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

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Аккаунт selectedAccount = databasesEntities.Аккаунт.Where(x => x.Логин.Equals(LoginTextBox.Text) && x.Пароль.Equals(PasswordBox.Password)).FirstOrDefault();

            if (selectedAccount == null)
            {
                LoginTextBox.Text = "";
                PasswordBox.Password = "";

                if (incorrectly == false)
                {
                    MessageBox.Show("Логин или пароль введены неверно: \n\n1) Проверьте правильность ввода\n\n2) Обратитесь к администратору", "Ошибка авторизации");
                    incorrectly = true;
                }
                else if (incorrectly == true)
                {
                    AuthorizationStackPanel.Visibility = Visibility.Hidden;
                    TimerStackPanel.Visibility = Visibility.Visible;
                    OpenButton.IsEnabled = false;
                    dispatcherTimer.Start();
                }

            }
            else
            {
                ParametersClass.SelectedAccount = selectedAccount;
                //ParametersClass.SelectedUser = databasesEntities.Сотрудник.Where(x => x.Код.Equals(selectedAccount.КодСотрудника)).FirstOrDefault();
                ParametersClass.SelectedUser = selectedAccount.Сотрудник;

                if (incorrectly == true)
                {                 
                    CaptchaWindow captchaWindow = new CaptchaWindow();
                    captchaWindow.ShowDialog();
                    this.Close();
                }
                else if (incorrectly == false)
                {
                    MenuWindow menuWindow = new MenuWindow();
                    menuWindow.Show();
                    this.Close();
                }
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password.Length > 0)
            {
                border2.Visibility = Visibility.Visible;
            }
            else
            {
                border2.Visibility = Visibility.Collapsed;
            }
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            TimerTextBlock.Text = timerTick.ToString();
            timerTick--;
            if (timerTick == 0)
            {
                ParametersClass.TimerStart = false;
                dispatcherTimer.Stop();
                timerTick = 9;
                TimerStackPanel.Visibility = Visibility.Hidden;
                AuthorizationStackPanel.Visibility = Visibility.Visible;

                TimerTextBlock.Text = timerTick.ToString();
                OpenButton.IsEnabled = true;
            }
        }
    }
}
