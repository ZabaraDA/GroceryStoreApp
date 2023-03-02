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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroceryStoreApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для DataOfCategoriesPage.xaml
    /// </summary>
    public partial class DataOfCategoriesPage : Page
    {
        GroceryStoreDatabasesEntities databasesEntities = new GroceryStoreDatabasesEntities();
        public DataOfCategoriesPage()
        {
            InitializeComponent();
            var categoryItems = databasesEntities.Категория.ToList();
            //categoryItems.Insert(databasesEntities.Категория.Count(), new Категория
            //{
            //    Наименование = "Добавить категорию",
                
                
            //});
            Категория категория = new Категория();
            категория.Наименование = "Добавить категорию";
            
            ListViewItem item = new ListViewItem();
            item.DataContext = категория;
            item.MouseDown += Page_MouseDown;

            CategoryListView.ItemsSource = categoryItems.ToList();
            CategoryListView.Items.Add(item);
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
