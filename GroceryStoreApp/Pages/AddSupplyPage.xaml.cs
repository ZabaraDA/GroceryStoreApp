using GroceryStoreApp.Databases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Principal;
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


namespace GroceryStoreApp.Pages
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public decimal Bulk { get; set; }
        public ЕдиницаИзмерения Unit { get; set; }
        public Product(int id, string name, int quantity, ЕдиницаИзмерения unit, decimal cost, decimal price, decimal weight, decimal bulk)
        {
            ID = id;
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Cost = cost;
            Price = cost * quantity;
            Weight = weight;
            Bulk = bulk;
        }
        public void PriceUpdate()
        {
            Price = Cost * Quantity;
        }
        public void BulkUpdate()
        {
            Bulk = Weight * Quantity;
        }
        public void AddOneProduct()
        {
            Quantity += 1;
        }
        public void RemoveOneProduct()
        {
            if (Quantity > 1)
            {
                Quantity -= 1;
            }
        }
        public Product()
        {

        }
    }
    public class TotalSupplyValue : INotifyPropertyChanged
    {
        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged("Amount"); }
        }
        private decimal _weight;
        public decimal Weight
        {
            get { return _weight; }
            set { _weight = value; OnPropertyChanged("Weight"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public partial class AddSupplyPage : Page
    {
        public ObservableCollection<Product> ProductList { get; set; } = new ObservableCollection<Product>();
        public List<Склад> WarehouseList { get; set; } = new List<Склад>();
        readonly GroceryStoreDatabasesEntities databaseEntities = new GroceryStoreDatabasesEntities();

        List<Товар> supplyProductList = new List<Товар>();
        public TotalSupplyValue TotalSupplyValue { get; set; } = new TotalSupplyValue();

        private bool _handleSelection = true;
        public AddSupplyPage()
        {
            InitializeComponent();
            this.DataContext = this;
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DateOfCreationDatePicker.Text = DateTime.Now.ToString();
            DateOfArrivalDatePicker.Text = DateTime.Now.ToString();

            List<Филиал> subsidiaryList = databaseEntities.Филиал.ToList();
            subsidiaryList.Insert(0, new Филиал
            {
                Наименование = "Выберите филиал"
            });
            SubsidiaryComboBox.ItemsSource = subsidiaryList.ToList();
            SubsidiaryComboBox.DisplayMemberPath = "Наименование";
            SubsidiaryComboBox.SelectedIndex = 0;

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
        }
        private void UpdateProductList()
        {
            if (WarehouseComboBox.SelectedIndex < 1)
            {
                return;
            }
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
                ProductList.Add(new Product
                {
                    ID = selectedProduct.Код,
                    Name = selectedProduct.Наименование,
                    Quantity = 1,
                    Cost = selectedProduct.Цена,
                    Unit = selectedProduct.ЕдиницаИзмерения,
                    Price = 1 * selectedProduct.Цена,
                    Weight = (decimal)selectedProduct.Вес,
                    Bulk = (decimal)(1 * selectedProduct.Вес)
                });
                TotalSupplyValue.Amount += selectedProduct.Цена;
                TotalSupplyValue.Weight += (decimal)selectedProduct.Вес;

                UpdateProductList();
            }
        }
        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFilter();
        }
        private void ClearFilter()
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
                CategorySearchComboBox.IsEnabled = true;
            }
            else
            {
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
            ReminderTextBlockVisiblity();

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
            ReminderTextBlockVisiblity();

            Panel.SetZIndex(ViewProductListButton, 1);
            Panel.SetZIndex(ViewProductInDeliveryButton, 0);

            ViewProductListButton.BorderThickness = new Thickness(3, 3, 3, 0);
            ViewProductListButton.BorderBrush = Brushes.Gray;
            ViewProductListButton.Background = Brushes.White;

            ViewProductInDeliveryButton.BorderThickness = new Thickness(0, 0, 0, 3);
            ViewProductInDeliveryButton.BorderBrush = null;
            ViewProductInDeliveryButton.Background = Brushes.Gray;
        }
        private void ReminderTextBlockVisiblity()
        {
            if (ProductListView.Items.Count < 1 && ProductListView.Visibility == Visibility.Visible)
            {
                ProductReminderTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                ProductReminderTextBlock.Visibility = Visibility.Collapsed;
            }
            if (SupplyOfProductListView.Items.Count < 1 && SupplyOfProductListView.Visibility == Visibility.Visible)
            {
                SupplyReminderTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                SupplyReminderTextBlock.Visibility = Visibility.Collapsed;
            }

        }
        private void AddSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            #region SupplyValidation
            StringBuilder errors = new StringBuilder();

            if (DateOfCreationDatePicker.Text == null || DateOfCreationDatePicker.Text == "")
            {
                errors.AppendLine("Укажите дату формирования поставки");
            }
            if (DateOfArrivalDatePicker.Text == null || DateOfArrivalDatePicker.Text == "")
            {
                errors.AppendLine("Укажите ожидаемую дату прибытия поставки");
            }
            if (errors.Length == 0 && Convert.ToDateTime(DateOfCreationDatePicker.Text) > Convert.ToDateTime(DateOfArrivalDatePicker.Text))
            {
                errors.AppendLine("Дата прибытия не может быть раньше даты создания поставки");
            }
            if (SubsidiaryComboBox.SelectedIndex < 1)
            {
                errors.AppendLine("Укажите место прибытия поставки(филиал)");
            }
            if (WarehouseComboBox.SelectedIndex < 1)
            {
                errors.AppendLine("Укажите склад отправки");
            }
            if (SupplierComboBox.SelectedIndex < 1)
            {
                errors.AppendLine("Укажите поставщика");
            }
            if (ProductList.Count < 1)
            {
                errors.AppendLine("Добавьте хотябы один товар в поставку");
            }
            #endregion
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            databaseEntities.Поставка.Add(new Поставка
            {
                Код = databaseEntities.Поставка.Count(),
                ДатаЗаявки = Convert.ToDateTime(DateOfCreationDatePicker.Text),
                ДатаПоставки = Convert.ToDateTime(DateOfArrivalDatePicker.Text),
                Статус = 0,
                Шаблон = false,
                Поставщик = SupplierComboBox.SelectedItem as Поставщик,
                Склад = WarehouseComboBox.SelectedItem as Склад,
                Филиал = SubsidiaryComboBox.SelectedItem as Филиал,
                Вес = TotalSupplyValue.Weight,
                Цена = TotalSupplyValue.Amount,

            });
            for (int i = 0; i < ProductList.Count; i++)
            {
                ProductList[i].PriceUpdate();
                databaseEntities.ТоварПоставка.Add(new ТоварПоставка
                {
                    КодПоставки = databaseEntities.Поставка.Count(),
                    КодТовара = ProductList[i].ID,
                    Количество = ProductList[i].Quantity,
                    Цена = ProductList[i].Price,
                    Остаток = ProductList[i].Quantity,
                    Вес = ProductList[i].Bulk,
                });
            }
            try
            {
                databaseEntities.SaveChanges();
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
        }
        private void AddOneProductButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Product selectedProduct = button.DataContext as Product;
            selectedProduct.AddOneProduct();
            TotalSupplyValue.Amount += selectedProduct.Cost;
            TotalSupplyValue.Weight += selectedProduct.Weight;
            SupplyOfProductListView.Items.Refresh();

        }
        private void RemoveOneProductButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Product selectedProduct = button.DataContext as Product;
            if (selectedProduct.Quantity > 1)
            {
                selectedProduct.RemoveOneProduct();
                TotalSupplyValue.Amount -= selectedProduct.Cost;
                TotalSupplyValue.Weight -= selectedProduct.Weight;
            }
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
                TotalSupplyValue.Amount -= selectedProduct.Price;
                TotalSupplyValue.Weight -= selectedProduct.Bulk;
                selectedProduct.PriceUpdate();
                selectedProduct.BulkUpdate();
                TotalSupplyValue.Amount += selectedProduct.Price;
                TotalSupplyValue.Weight += selectedProduct.Bulk;
            }
        }
        private void WarehouseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_handleSelection == true)
            {
                if (ProductList.Count > 0 && MessageBox.Show("1111При изменении будут сброшены товары в поставке", "Вы уверены", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    ComboBox comboBox = (ComboBox)sender;
                    _handleSelection = false;
                    comboBox.SelectedItem = e.RemovedItems[0];
                    return;
                }
                else
                {
                    ClearSupplyData();
                }
            }
            _handleSelection = true;
            List<Поставщик> supplierList = new List<Поставщик>();
            if (WarehouseComboBox.SelectedIndex > 0)
            {
                var selectedWarehouse = WarehouseComboBox.SelectedItem as Склад;
                supplierList = selectedWarehouse.Поставщик.ToList();
                SupplierComboBox.IsEnabled = true;
                ProductEditStackPanel.IsEnabled = true;
                UpdateProductList();
                ProductListView.IsEnabled = true;
                ProductListView.Items.Refresh();
            }
            else
            {
                ClearFilter();
                ProductListView.ItemsSource = null;
                SupplierComboBox.IsEnabled = false;
                ProductEditStackPanel.IsEnabled = false;
            }
            ReminderTextBlockVisiblity();
            supplierList.Insert(0, new Поставщик
            {
                Наименование = "Выберите поставщика"
            });
            SupplierComboBox.ItemsSource = supplierList.ToList();
            SupplierComboBox.DisplayMemberPath = "Наименование";
            SupplierComboBox.SelectedIndex = 0;
        }
        private void SubsidiaryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_handleSelection == true)
            {
                if (ProductList.Count > 0 && MessageBox.Show("1111При изменении будут сброшены товары в поставке", "Вы уверены", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    ComboBox combo = (ComboBox)sender;
                    _handleSelection = false;
                    combo.SelectedItem = e.RemovedItems[0];
                    return;
                }
                else
                {
                    ClearSupplyData();
                }
            }
            else
            {
                _handleSelection = true;
                return;
            }
            if (SubsidiaryComboBox.SelectedIndex > 0)
            {
                var selectedSubsidiary = SubsidiaryComboBox.SelectedItem as Филиал;
                WarehouseList = selectedSubsidiary.Склад.ToList();
                WarehouseComboBox.IsEnabled = true;
            }
            else
            {
                WarehouseComboBox.IsEnabled = false;
            }
            WarehouseList.Insert(0, new Склад
            {
                Наименование = "Выберите склад"
            });
            WarehouseComboBox.ItemsSource = WarehouseList.ToList();
            WarehouseComboBox.DisplayMemberPath = "Наименование";
            WarehouseComboBox.SelectedIndex = 0;

        }

        private void ClearSupplyData()
        {
            ProductList.Clear();
            supplyProductList.Clear();
            TotalSupplyValue.Amount = 0;
            TotalSupplyValue.Weight = 0;
        }
        private void ExcludeProductButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Product selectedProduct = button.DataContext as Product;
            ProductList.Remove(selectedProduct);
            selectedProduct.PriceUpdate();
            selectedProduct.BulkUpdate();
            TotalSupplyValue.Amount -= selectedProduct.Price;
            TotalSupplyValue.Weight -= selectedProduct.Bulk;
            supplyProductList.Remove(supplyProductList.Where(x => x.Код.Equals(selectedProduct.ID)).FirstOrDefault());
            UpdateProductList();
        }
       
    }
}
