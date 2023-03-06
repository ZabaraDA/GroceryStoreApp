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
using GroceryStoreApp.CsClasses;
using System.Reflection.Emit;

namespace GroceryStoreApp.Pages
{
    public partial class AddCategoryPage : Page
    {
        GroceryStoreDatabasesEntities databasesEntities = new GroceryStoreDatabasesEntities();

        Категория _currentCategory;
        Группа _currentGroup;
        public AddCategoryPage(Группа selectedGroup, Категория selectedCategory,bool topLevel)
        {
            InitializeComponent();
            DataContext = _currentCategory;
            if (selectedCategory != null)
            {
                _currentCategory = selectedCategory;
            }
            else if(selectedGroup != null)
            {
                DataContext = _currentGroup = selectedGroup;
            }
        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            //photo =  PhotoImportClass.Import(800);
            PhotoCategoryImageBrush.ImageSource = PhotoImportClass.Import(800);
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
