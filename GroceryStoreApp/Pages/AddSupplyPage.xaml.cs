﻿using GroceryStoreApp.Databases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;
using WpfCustomControlLibrary;
using static System.Net.Mime.MediaTypeNames;

namespace GroceryStoreApp.Pages
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public ЕдиницаИзмерения Unit { get; set; }
        public Product(int id, string name, int quantity, ЕдиницаИзмерения unit, decimal cost, decimal price)
        {
            ID = id;
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Cost = cost;
            Price = cost * quantity;
        }
        public void PriceUpdate()
        {
            Price = Cost * Quantity;

        }
        public void AddOneProduct()
        {
            Quantity += 1;
            //PriceUpdate();
        }
        public void RemoveOneProduct()
        {
            if (Quantity > 1)
            {
                Quantity -= 1;
                //PriceUpdate();
            }
        }
        public Product()
        {

        }
    }


    public partial class AddSupplyPage : Page
    {
        public ObservableCollection<Product> productList { get; set; } = new ObservableCollection<Product>();
        readonly GroceryStoreDatabasesEntities databaseEntities = new GroceryStoreDatabasesEntities();

        //List<Product> productList = new List<Product>();
        List<Товар> supplyProductList = new List<Товар>();
        public AddSupplyPage()
        {

            InitializeComponent();
            this.DataContext = this;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Группа> groupList = databaseEntities.Группа.ToList();
            groupList.Insert(0, new Группа
            {
                Наименование = "Все группы"
            });
            GroupSearchComboBox.ItemsSource = groupList.ToList();
            GroupSearchComboBox.DisplayMemberPath = "Наименование";
            GroupSearchComboBox.SelectedIndex = 0;

            List<Производитель> manufacturerList = databaseEntities.Производитель.ToList();
            manufacturerList.Insert(0, new Производитель
            {
                Наименование = "Все производители"
            });
            ManufacturerSearchComboBox.ItemsSource = manufacturerList.ToList();
            ManufacturerSearchComboBox.DisplayMemberPath = "Наименование";
            ManufacturerSearchComboBox.SelectedIndex = 0;

            StatusSearchComboBox.SelectedIndex = 0;
            CostSearchComboBox.SelectedIndex = 0;
            WeightSearchComboBox.SelectedIndex = 0;
            TypeSortComboBox.SelectedIndex = 0;
            PhotoSearchComboBox.SelectedIndex = 0;

            UpdateProductList();

        }

        private void UpdateProductList()
        {
            List<Товар> productList = databaseEntities.Товар.ToList();

            NumberOfProductTextBlock.Text = productList.Count().ToString();

            if (supplyProductList.Count > 0)
            {
                productList = productList.Except(supplyProductList).ToList();
            }

            if (NameSearchTextBox.Text != null && NameSearchTextBox.Text != "")
            {
                productList = productList.Where(x => x.Наименование.ToLower().Contains(NameSearchTextBox.Text.ToLower())).ToList();
            }
            if (ManufacturerSearchComboBox.SelectedIndex > 0)
            {
                productList = productList.Where(x => x.Производитель.Equals(ManufacturerSearchComboBox.SelectedItem)).ToList();
            }
            if (GroupSearchComboBox.SelectedIndex > 0)
            {
                productList = productList.Where(x => x.Категория.Группа.Equals(GroupSearchComboBox.SelectedItem)).ToList();
            }
            if (CategorySearchComboBox.SelectedIndex > 0)
            {
                productList = productList.Where(x => x.Категория.Equals(CategorySearchComboBox.SelectedItem)).ToList();
            }
            if (StatusSearchComboBox.SelectedIndex > 0)
            {
                if (StatusSearchComboBox.SelectedIndex == 1)
                {
                    productList = productList.Where(x => x.Статус.Equals(true)).ToList();
                }
                else if (StatusSearchComboBox.SelectedIndex == 2)
                {
                    productList = productList.Where(x => x.Статус.Equals(false)).ToList();
                }
            }
            if (CostSearchComboBox.SelectedIndex > 0)
            {
                if (CostSearchComboBox.SelectedIndex == 1)
                {
                    productList = productList.OrderBy(x => x.Цена).ToList();
                }
                if (CostSearchComboBox.SelectedIndex == 2)
                {
                    productList = productList.OrderByDescending(x => x.Цена).ToList();
                }
            }
            if (MinimumCostTextBox.Text != null && MinimumCostTextBox.Text != "")
            {
                productList = productList.Where(x => x.Цена > Convert.ToDecimal(MinimumCostTextBox.Text)).ToList();
            }
            if (MaximumCostTextBox.Text != null && MaximumCostTextBox.Text != "")
            {
                productList = productList.Where(x => x.Цена < Convert.ToDecimal(MaximumCostTextBox.Text)).ToList();
            }
            if (PhotoSearchComboBox.SelectedIndex > 0)
            {
                if (PhotoSearchComboBox.SelectedIndex == 1)
                {
                    productList = productList.Where(x => x.Фото != null).ToList();
                }
                if (PhotoSearchComboBox.SelectedIndex == 2)
                {
                    productList = productList.Where(x => x.Фото == null).ToList();
                }
            }

            FilterNumberOfProductTextBlock.Text = productList.Count().ToString();
            ProductListView.ItemsSource = productList.ToList();
        }

        private void AddProductToDeliveryButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is Товар selectedProduct)
            {
                supplyProductList.Add(selectedProduct);
                productList.Add(new Product
                {
                    ID = selectedProduct.Код,
                    Name = selectedProduct.Наименование,
                    Quantity = 1,
                    Cost = selectedProduct.Цена,
                    Unit = selectedProduct.ЕдиницаИзмерения,
                    Price = 1 * selectedProduct.Цена
                });
                //SupplyOfProductListView.ItemsSource = supplyProductList.ToList();
                //SupplyOfProductListView.ItemsSource = productList1.ToList();
                UpdateProductList();
            }
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            List<Control> children = ProductEditStackPanel.Children.OfType<Control>().ToList();
            foreach (Control control in children)
            {
                if (control as TextBox != null)
                {
                    var textBox = control as TextBox;
                    textBox.Text = null;
                }
                else if (control as ComboBox != null)
                {
                    var comboBox = control as ComboBox;
                    comboBox.SelectedIndex = 0;
                }
            }
        }
        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProductList();
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProductList();
        }
        private void GroupSearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            UpdateCategoryList();
            UpdateProductList();
        }
        private void UpdateCategoryList()
        {
            List<Категория> categoryList = new List<Категория>();
            if (GroupSearchComboBox.SelectedIndex > 0)
            {
                categoryList = databaseEntities.Категория.ToList();
                categoryList = categoryList.Where(x => x.Группа.Equals(GroupSearchComboBox.SelectedItem)).ToList();
                CategorySearchComboBox.SelectedIndex = 0;
                CategorySearchComboBox.IsEnabled = true;
            }
            else
            {
                CategorySearchComboBox.SelectedIndex = 0;
                CategorySearchComboBox.IsEnabled = false;
            }

            categoryList.Insert(0, new Категория
            {
                Наименование = "Все категории"
            });
            CategorySearchComboBox.ItemsSource = categoryList.ToList();
            CategorySearchComboBox.DisplayMemberPath = "Наименование";
            CategorySearchComboBox.SelectedIndex = 0;
        }
        private void TypeSortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeSortComboBox.SelectedIndex < 1)
            {
                DeactivateSortingByCost();
                DeactivateSortingByWeight();
            }
            else if (TypeSortComboBox.SelectedIndex == 1)
            {
                DeactivateSortingByCost();
                WeightSearchComboBox.SelectedIndex = 0;
                WeightSortStackPanel.Visibility = Visibility.Visible;
            }
            else if (TypeSortComboBox.SelectedIndex == 2)
            {
                DeactivateSortingByWeight();
                CostSearchComboBox.SelectedIndex = 0;
                CostSortStackPanel.Visibility = Visibility.Visible;
            }
        }
        private void DeactivateSortingByCost()
        {
            CostSearchComboBox.SelectedIndex = 0;
            CostSortStackPanel.Visibility = Visibility.Collapsed;
            MaximumCostTextBox.Text = null;
            MinimumCostTextBox.Text = null;
        }
        private void DeactivateSortingByWeight()
        {
            WeightSearchComboBox.SelectedIndex = 0;
            WeightSortStackPanel.Visibility = Visibility.Collapsed;
            MaximumWeightTextBox.Text = null;
            MinimumWeightTextBox.Text = null;
        }
        private void ViewProductInDeliveryButton_Click(object sender, RoutedEventArgs e)
        {
            SupplyOfProductListView.Visibility = Visibility.Visible;
            ProductListView.Visibility = Visibility.Collapsed;

            Panel.SetZIndex(ViewProductInDeliveryButton, 1);
            Panel.SetZIndex(ViewProductListButton, 0);
            ViewProductInDeliveryButton.BorderThickness = new Thickness(3, 3, 3, 0);
            ViewProductInDeliveryButton.BorderBrush = Brushes.Gray;
            ViewProductInDeliveryButton.Background = Brushes.White;

            ViewProductListButton.BorderThickness = new Thickness(0, 0, 0, 3);
            ViewProductListButton.BorderBrush = null;
            ViewProductListButton.Background = Brushes.Gray;
        }
        private void ViewProductListButton_Click(object sender, RoutedEventArgs e)
        {
            ProductListView.Visibility = Visibility.Visible;
            SupplyOfProductListView.Visibility = Visibility.Collapsed;

            Panel.SetZIndex(ViewProductListButton, 1);
            Panel.SetZIndex(ViewProductInDeliveryButton, 0);

            ViewProductListButton.BorderThickness = new Thickness(3, 3, 3, 0);
            ViewProductListButton.BorderBrush = Brushes.Gray;
            ViewProductListButton.Background = Brushes.White;

            ViewProductInDeliveryButton.BorderThickness = new Thickness(0, 0, 0, 3);
            ViewProductInDeliveryButton.BorderBrush = null;
            ViewProductInDeliveryButton.Background = Brushes.Gray;
        }
        private void AddSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            databaseEntities.Поставка.Add(new Поставка
            {
                Код = databaseEntities.Поставка.Count(),
                ДатаЗаявки = DateTime.Now,
                ДатаПоставки = DateTime.Now,
                КодПоставщика = 1,
                Статус = 0,
                Шаблон = false,
                КодСклада = 4,
                КодФилиала = 2,

            });
            for (int i = 0; i < productList.Count; i++)
            {
                productList[i].PriceUpdate();
                databaseEntities.ТоварПоставка.Add(new ТоварПоставка
                {
                    КодПоставки = databaseEntities.Поставка.Count(),
                    КодТовара = productList[i].ID,
                    Количество = productList[i].Quantity,
                    Цена = productList[i].Price,
                    Остаток = productList[i].Quantity,

                });
            }
            try
            {
                databaseEntities.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AddOneProductButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Product product = button.DataContext as Product;
            product.AddOneProduct();
            //product.PriceUpdate();
            //button.DataContext = product;

            SupplyOfProductListView.Items.Refresh();

        }

        private void RemoveOneProductButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Product product = button.DataContext as Product;
            product.RemoveOneProduct();
            SupplyOfProductListView.Items.Refresh();
        }

        private void QuantityProductTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.D0 && e.Key <= Key.D9 || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9 || e.Key == Key.Back)
            {
                e.Handled = false;
            }
            else if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && e.Key == Key.V)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void QuantityProductTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (sender as TextBox);
            if (textBox.Text == null || textBox.Text == "" || Convert.ToDecimal(textBox.Text) < 1)
            {
                textBox.Text = "1";
            }
            if (textBox.DataContext is Product selectedProduct)
            {
                selectedProduct.PriceUpdate();
            }
        }
        protected void SelectCurrentItem(object sender, KeyboardFocusChangedEventArgs e) //event
        {
            ListViewItem item = (ListViewItem)sender;
            item.IsSelected = true;
        }
    }
}
