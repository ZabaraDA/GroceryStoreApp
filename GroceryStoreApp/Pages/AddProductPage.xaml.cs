using GroceryStoreApp.CsClasses;
//using GroceryStoreApp.Databases;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GroceryStoreApp;

namespace GroceryStoreApp.Pages
{
    public class Cube
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public double CathetusA { get; set; }
        public double CathetusB { get; set; }
        public int MaxSize { get; set; }
        public Cube()
        {
            CathetusB = Z * 0.5;
            CathetusA = Z * 0.3;
        }
        public void CalculateCathetusA()
        {
            CathetusA = Z * 0.3;
        }
        public void CalculateCathetusB()
        {
            CathetusB = Z * 0.5;
        }

    }
    
    public partial class AddProductPage : Page
    {
        //private Товар _currentProduct = new Товар();

        //private readonly GroceryStoreDatabasesEntities _databasesEntities = new GroceryStoreDatabasesEntities();
        public AddProductPage()
        {
            InitializeComponent();
            //if (selectedProduct != null)
            //{
            //    _currentProduct = selectedProduct;
            //}
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            #region validation
            if (BarcodeCodeTextBox.Text == null || BarcodeCodeTextBox.Text == "")
            {
                errors.AppendLine("Поле штрих код не может быть пустым");
            }
            else
            {
                for (int i = 0; i < BarcodeCodeTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(BarcodeCodeTextBox.Text[i]))
                    {
                        errors.AppendLine(" Штрих код должен состоять из цифр");
                        break;
                    }
                }
            }

            if (VendorCodeTextBox.Text == null || VendorCodeTextBox.Text == "")
            {
                errors.AppendLine("Поле актикул не может быть пустым");
            }

            if (NameTextBox.Text == null || NameTextBox.Text == "")
            {
                errors.AppendLine("Поле наименование не может быть пустым");
            }

            if (CostTextBox.Text == null || CostTextBox.Text == "")
            {
                errors.AppendLine("Поле стоимость не может быть пустым");
            }
            else
            {
                for (int i = 0; i < CostTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(CostTextBox.Text[i]))
                    {
                        errors.AppendLine("Стоимость должна состоять из цифр");
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(CostTextBox.Text))
            {
                errors.AppendLine("Поле стоимость не может быть пустым");
            }
            else
            {
                for (int i = 0; i < CostTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(CostTextBox.Text[i]))
                    {
                        errors.AppendLine("Стоимость должна состоять из цифр");
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(VATTextBox.Text))
            {
                errors.AppendLine("Поле стоимость не может быть пустым");
            }
            else
            {
                for (int i = 0; i < VATTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(VATTextBox.Text[i]))
                    {
                        errors.AppendLine("Стоимость должна состоять из цифр");
                        break;
                    }
                }
            }

            if (CategoryComboBox.SelectedIndex == 0)
            {
                errors.AppendLine("Укажите категорию товара");
            }

            if (ManufacturerComboBox.SelectedIndex == 0)
            {
                errors.AppendLine("Укажите производителя товара");
            }

            if (UnitComboBox.SelectedIndex == 0)
            {
                errors.AppendLine("Укажите единицу измерения товара");
            }

            if (string.IsNullOrEmpty(WeightTextBox.Text))
            {
                errors.AppendLine("Поле стоимость не может быть пустым");
            }
            else
            {
                for (int i = 0; i < WeightTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(WeightTextBox.Text[i]))
                    {
                        errors.AppendLine("Вес должен состоять из цифр");
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(WidthTextBox.Text))
            {
                errors.AppendLine("Поле ширина не может быть пустым");
            }
            else
            {
                for (int i = 0; i < WidthTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(WidthTextBox.Text[i]))
                    {
                        errors.AppendLine("Ширина должна состоять из цифр");
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(HeightTextBox.Text))
            {
                errors.AppendLine("Поле ширина не может быть пустым");
            }
            else
            {
                for (int i = 0; i < HeightTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(HeightTextBox.Text[i]))
                    {
                        errors.AppendLine("Ширина должна состоять из цифр");
                        break;
                    }
                }
            }

            //if (_currentProduct.Фото == null)
            //{
            //    if (MessageBox.Show("Добавить товар без изображения?", "Внимание", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            //    {
            //        return;
            //    }
            //}

            if (string.IsNullOrEmpty(DescriptionTextBox.Text))
            {
                if (MessageBox.Show("Добавить товар без описания?", "Внимание", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            #endregion
            //databasesEntities.Товар.Add(new Товар
            //{
            //    Артикул = VendorCodeTextBox.Text,
            //    ШтрихКод = BarcodeCodeTextBox.Text,
            //    ЕдиницаИзмерения = UnitComboBox.SelectedItem as ЕдиницаИзмерения,
            //    Категория = CategoryComboBox.SelectedItem as Категория,
            //    Производитель = ManufacturerComboBox.SelectedItem as Производитель,
            //    Описание = DescriptionTextBox.Text,
            //    Количество = 0,
            //    НДС = Convert.ToByte(VATTextBox.Text),
            //    Фото = photoProduct,
            //    Наименование = NameTextBox.Text,
            //    Высота = Convert.ToInt16(WidthTextBox.Text),
            //    Ширина = Convert.ToInt16(HeightTextBox.Text),
            //    Цена = Convert.ToInt16(CostTextBox.Text),
            //    СрокГодности = Convert.ToInt16(ExpirationDateTextBox.Text),
            //    Вес = Convert.ToInt32(WeightTextBox.Text),
            //    Статус = true
            //});
            //_currentProduct.Фото = _photoProduct;
            //_databasesEntities.Товар.AddOrUpdate(_currentProduct);
            //try
            //{
            //    _databasesEntities.SaveChanges();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            //_currentProduct.Фото = PhotoImportClass.ImportToByte(800);
        }

        private void ClearInputFields_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void Page_Loaded(object sender, RoutedEventArgs e)
        //{
        //    var categoryItems = _databasesEntities.Категория.ToList();
        //    categoryItems.Insert(0, new Категория
        //    {
        //        Наименование = "Выберите категорию",
        //    });

        //    CategoryComboBox.ItemsSource = categoryItems.ToList();
        //    CategoryComboBox.DisplayMemberPath = "Наименование";
        //    CategoryComboBox.SelectedIndex = 0;

        //    var manufacturerItems = _databasesEntities.Производитель.ToList();
        //    manufacturerItems.Insert(0, new Производитель
        //    {
        //        Наименование = "Выберите производителя"

        //    });
        //    ManufacturerComboBox.ItemsSource = manufacturerItems.ToList();
        //    ManufacturerComboBox.DisplayMemberPath = "Наименование";
        //    ManufacturerComboBox.SelectedIndex = 0;

        //    var unitItems = _databasesEntities.ЕдиницаИзмерения.ToList();
        //    unitItems.Insert(0, new ЕдиницаИзмерения
        //    {
        //        Наименование = "Выберите единицу измерения",
        //        Формат = false,

        //    });

        //    UnitComboBox.ItemsSource = unitItems.ToList();
        //    UnitComboBox.DisplayMemberPath = "Наименование";
        //    UnitComboBox.SelectedIndex = 0;

        //    //DataContext = _currentProduct;
        //}

        private void AddManufacturerButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GoBackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearInputFieldsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBoxWithRemoved_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Cube.CalculateCathetusB();
            //Cube.CalculateCathetusA();
        }
    }
}

