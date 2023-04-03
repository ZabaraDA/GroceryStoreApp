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
    public sealed class AP : DependencyObject
    {
        public static bool GetIsChecked(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCheckedProperty);
        }

        public static void SetIsChecked(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCheckedProperty, value);
        }
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.RegisterAttached("IsChecked", typeof(bool), typeof(AP), new UIPropertyMetadata(false));
    }
    public partial class DataOfGroupsPage : Page
    {
        GroceryStoreDatabasesEntities _databasesEntities = new GroceryStoreDatabasesEntities();
        public DataOfGroupsPage()
        {
            InitializeComponent();
            GroupListView.ItemsSource = _databasesEntities.Группа.ToList();
        }

        private void border_Click(object sender, RoutedEventArgs e)
        {
            var a = sender as Button;
            var b = a.Parent as Grid;
            var c = b.Parent as ListViewItem;
            
        }
    }
}
