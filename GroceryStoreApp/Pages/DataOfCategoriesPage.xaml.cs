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

            Image image = new Image()
            {
                Source = new BitmapImage(new Uri("/Images/plus.png", UriKind.Relative)),
            };
            TextBlock textBlock = new TextBlock()
            {
                Text = "Добавить категорию"
            };
            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(image);
            stackPanel.Children.Add(textBlock); 

            ListViewItem item = new ListViewItem();
            item.MouseDoubleClick += Page_MouseDown;
            item.Width = 200;
            item.Content = stackPanel;

            CompositeCollection compositeCollection = new CompositeCollection();
            compositeCollection.Add(categoryItems);
            //compositeCollection.Add(item);

            CategoryListView.Items.Add(categoryItems);
            CategoryListView.Items.Add(item);
        }

        private void Page_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddCategoryPage(null, null, false));
        }

        private void AddGroupButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCategoryPage(null,null,true));
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCategoryPage(null,null,false));
        }

        private void ChangeCateryButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCategoryPage(null, null, false));
        }
    }
}
