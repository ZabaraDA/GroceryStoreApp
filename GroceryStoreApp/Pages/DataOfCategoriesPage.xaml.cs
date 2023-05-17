using GroceryStoreApp.Databases;
using GroceryStoreApp.Models.Databases;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroceryStoreApp.Pages
{
    public partial class DataOfCategoriesPage : Page
    {
        GroceryStoreDatabasesEntities databasesEntities = new GroceryStoreDatabasesEntities();
        public DataOfCategoriesPage()
        {
            InitializeComponent();
            var categoryItems = databasesEntities.Категория.ToList();

        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddCategoryPage());
        }

        private void AddGroupButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCategoryPage());
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCategoryPage());
        }

        private void ChangeCateryButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCategoryPage());
        }
    }
}
