using GroceryStoreApp.Databases;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
    public partial class DataOfGroupsPage : Page
    {
        GroceryStoreDatabasesEntities _databasesEntities = new GroceryStoreDatabasesEntities();
        public DataOfGroupsPage()
        {
            InitializeComponent();
            //UpdateProductList();
        }

        private void UpdateProductList()
        {
            var groupList = _databasesEntities.Группа.ToList();

            if (!string.IsNullOrEmpty(NameSearchTextBox.Text))
            {
                groupList = groupList.Where(x => x.Наименование.ToLower().Contains(NameSearchTextBox.Text.ToLower())).ToList();
            }

            GroupListView.ItemsSource = groupList.ToList();
        }
        private void ViewProductButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new FormProductPage((sender as Button).DataContext as Товар));
        }

        private void ChangeProductButton_Click(object sender, RoutedEventArgs e)
        {
            Товар selectedProduct = (sender as Button).DataContext as Товар;
            if(selectedProduct != null)
            {
                NavigationService.Navigate(new AddProductPage());
            }
        }

        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeGroupButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NameSearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProductList();
        }
    }
}
