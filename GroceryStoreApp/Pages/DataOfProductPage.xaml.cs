using GroceryStoreApp.Databases;
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

namespace GroceryStoreApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для DataProductPage.xaml
    /// </summary>
    public partial class DataOfProductPage : Page
    {
        GroceryStoreDatabasesEntities databasesEntities = new GroceryStoreDatabasesEntities();
        public DataOfProductPage()
        {
            InitializeComponent();
            FilterProduct();
        }
        private void FilterProduct()
        {
            ProductListView.ItemsSource = databasesEntities.Товар.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ViewProductButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new FormProductPage((sender as Button).DataContext as Товар));
        }

        private void ChangeProductButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
