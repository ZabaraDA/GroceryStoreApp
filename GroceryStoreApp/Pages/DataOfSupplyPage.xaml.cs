using GroceryStoreApp.Databases;
using GroceryStoreApp.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    public partial class DataOfSupplyPage : Page
    {
        readonly GroceryStoreDatabasesEntities _databaseEntities = new GroceryStoreDatabasesEntities();

        DateTime month = DateTime.Now;

        private bool _handleSelection = false;

        private const string WARNING_DELYVERY_STATUS_COMPLETED_MESSAGE = "При подтверждение статуса \"Выполнен\"" +
                                                         "\n1.Ожидаемая дата поставки станет фактической.\n" +
                                                         "2.Все товары поставки будут зачислены на склад филиала" +
                                                         "\nЕсли данные поставки не соответствуют реальным," +
                                                         " то необходимо вручную отредектировать поставку и изменить статус" +
                                                         "\nИзменить статус?";
        private const string WARNING_DELYVERY_STATUS_CANCELED_MESSAGE = "111При подтверждение статуса \"Выполнен\"" +
                                                         "\n1.Ожидаемая дата поставки станет фактической.\n" +
                                                         "2.Все товары поставки будут зачислены на склад филиала" +
                                                         "\nЕсли данные поставки не соответствуют реальным," +
                                                         " то необходимо вручную отредектировать поставку и изменить статус" +
                                                         "\nИзменить статус?";
        private const string WARNING_DELYVERY_STATUS_SENT_MESSAGE = "Вы уверены что хотите измнить статус поставки на \"Отправлен\"?" +
                                                         "\nОтменить изменение статуса невозможно";
        private const string WARNING_DELYVERY_STATUS_NOT_SENT_MESSAGE = "Невозможно изменить статус поставки на " +
                                                         "\"Не отправлен\", если статус поствки \"Отправлен\"/\"Отменён\"/\"Выполнен\"";
        public DataOfSupplyPage()
        {
            InitializeComponent();
            CreateRangeYears();
            StatusSearchComboBox.SelectedIndex = 0;
            TypeSortComboBox.SelectedIndex = 0;
            UpdateSupplyList();

            List<Поставщик> supplierList = _databaseEntities.Поставщик.ToList();
            supplierList.Insert(0, new Поставщик
            {
                Наименование = "Все поставщики"
            });
            SupplierSearchComboBox.ItemsSource = supplierList.ToList();
            SupplierSearchComboBox.DisplayMemberPath = "Наименование";
            SupplierSearchComboBox.SelectedIndex = 0;
        }

        private void UpdateSupplyList()
        {
            var supplyList = _databaseEntities.Поставка.OrderByDescending(x => x.ДатаПоставки).ToList();

            int numberOfSupply = supplyList.Count();
            if (StatusSearchComboBox.SelectedIndex > 0)
            {
                supplyList = supplyList.Where(x => x.Статус == Convert.ToByte(StatusSearchComboBox.SelectedIndex - 1)).ToList();
            }
            if (YearSearchComboBox.SelectedIndex > 0)
            {
                supplyList = supplyList.Where(x => x.ДатаПоставки.Year.Equals(Convert.ToInt16(YearSearchComboBox.SelectedItem))).ToList();
            }
            if (SupplierSearchComboBox.SelectedIndex > 0)
            {
                supplyList = supplyList.Where(x => x.Поставщик.Equals(SupplierSearchComboBox.SelectedItem)).ToList();
            }
            if (DateOfCreationSearchComboBox.SelectedIndex > 0)
            {
                if (DateOfCreationSearchComboBox.SelectedIndex == 1)
                {
                    supplyList = supplyList.OrderBy(x => x.ДатаЗаявки).ToList();
                }
                if (DateOfCreationSearchComboBox.SelectedIndex == 2)
                {
                    supplyList = supplyList.OrderByDescending(x => x.ДатаЗаявки).ToList();
                }
            }
            if (MinimumDateOfCreationDatePicker.Text != null && MinimumDateOfCreationDatePicker.Text != "")
            {
                supplyList = supplyList.Where(x => x.ДатаЗаявки >= Convert.ToDateTime(MinimumDateOfCreationDatePicker.Text)).ToList();
            }
            if (MaximumDateOfCreationDatePicker.Text != null && MaximumDateOfCreationDatePicker.Text != "")
            {
                supplyList = supplyList.Where(x => x.ДатаЗаявки <= Convert.ToDateTime(MaximumDateOfCreationDatePicker.Text)).ToList();
            }
            if (DateOfSupplySearchComboBox.SelectedIndex > 0)
            {
                if (DateOfSupplySearchComboBox.SelectedIndex == 1)
                {
                    supplyList = supplyList.OrderBy(x => x.ДатаПоставки).ToList();
                }
                else
                {
                    supplyList = supplyList.OrderByDescending(x => x.ДатаПоставки).ToList();
                }
            }
            if (MinimumDateOfDeliveryDatePicker.Text != null && MinimumDateOfDeliveryDatePicker.Text != "")
            {
                supplyList = supplyList.Where(x => x.ДатаЗаявки >= Convert.ToDateTime(MinimumDateOfDeliveryDatePicker.Text)).ToList();
            }
            if (MaximumDateOfDeliveryDatePicker.Text != null && MaximumDateOfDeliveryDatePicker.Text != "")
            {
                supplyList = supplyList.Where(x => x.ДатаЗаявки <= Convert.ToDateTime(MaximumDateOfDeliveryDatePicker.Text)).ToList();
            }

            NumberOfSupplyTextBlock.Text = $"Выбрано {supplyList.Count} из {numberOfSupply} поставок";
            SupplyListView.ItemsSource = supplyList.ToList();
        }
        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (_handleSelection == true)
            {
                _handleSelection = false;
                if (comboBox.SelectedIndex == 3 && MessageBox.Show(WARNING_DELYVERY_STATUS_COMPLETED_MESSAGE, "Вы уверены", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    comboBox.SelectedItem = e.RemovedItems[0];
                    return;
                }
                else if (comboBox.SelectedIndex == 3)
                {
                    Поставка selectedSupply = comboBox.DataContext as Поставка;
                    List<ТоварПоставка> productSupplyList = selectedSupply.ТоварПоставка.ToList();
                    for (int i = 0; i < productSupplyList.Count; i++)
                    {
                        Товар currentProduct = productSupplyList[i].Товар;
                        currentProduct.Количество += productSupplyList[i].Количество;

                        ФилиалТовар subsidiaryProduct = currentProduct.ФилиалТовар.Where(x => x.КодФилиала.Equals(selectedSupply.КодФилиала)).FirstOrDefault();

                        if (subsidiaryProduct != null)
                        {
                            subsidiaryProduct.Количество += productSupplyList[i].Количество;
                        }
                        else
                        {
                            subsidiaryProduct = new ФилиалТовар()
                            {
                                Филиал = selectedSupply.Филиал,
                                Товар = currentProduct,
                                Количество = productSupplyList[i].Количество
                            };

                            DatabaseReferenceWindow window = new DatabaseReferenceWindow(subsidiaryProduct)
                            {
                                Owner = Window.GetWindow(this)
                            };

                            if (window.ShowDialog() == true)
                            {
                                subsidiaryProduct.Норма = window.NormalLimit;
                                subsidiaryProduct.МинимальныйЛимит = window.MinimumLimit;

                                _databaseEntities.ФилиалТовар.Add(subsidiaryProduct);
                            }
                            else
                            {
                                comboBox.SelectedItem = e.RemovedItems[0];
                                return;
                            }
                        }

                    }
                }
                else if (comboBox.SelectedIndex == 2 && MessageBox.Show(WARNING_DELYVERY_STATUS_CANCELED_MESSAGE, "Вы уверены", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    comboBox.SelectedItem = e.RemovedItems[0];
                    return;
                }
                else if (comboBox.SelectedIndex == 2)
                {
                    Поставка selectedSupply = comboBox.DataContext as Поставка;
                    if (comboBox.SelectedIndex == 2)
                    {
                        List<ТоварПоставка> productSupplyList = selectedSupply.ТоварПоставка.ToList();
                        for (int i = 0; i < productSupplyList.Count; i++)
                        {
                            productSupplyList[i].Остаток = 0;
                        }
                    }
                }
                else if (comboBox.SelectedIndex == 1 && MessageBox.Show(WARNING_DELYVERY_STATUS_SENT_MESSAGE, "Внимание", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    comboBox.SelectedItem = e.RemovedItems[0];
                    return;
                }
                else if (comboBox.SelectedIndex == 0)
                {
                    MessageBox.Show(WARNING_DELYVERY_STATUS_NOT_SENT_MESSAGE);
                    comboBox.SelectedItem = e.RemovedItems[0];
                    return;
                }
            }
            if (comboBox.SelectedIndex == 3 || comboBox.SelectedIndex == 2)
            {
                comboBox.IsEnabled = false;
            }

            ComboBoxItem comboBoxItem = comboBox.SelectedItem as ComboBoxItem;
            comboBox.Foreground = comboBoxItem.Foreground;

            try
            {
                _databaseEntities.SaveChanges();
            }
            catch (Exception exeption)
            {
                MessageBox.Show(exeption.Message);
            }
        }
        private void TypeSortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeSortComboBox.SelectedIndex == 0)
            {
                DeactivateSortingByDateOfCreation();
                DeactivateSortingByDateOfSupply();
            }
            else if (TypeSortComboBox.SelectedIndex == 1)
            {
                DeactivateSortingByDateOfSupply();
                DateOfCreationSearchComboBox.SelectedIndex = 0;
                DateOfCreationSortStackPanel.Visibility = Visibility.Visible;
            }
            else if (TypeSortComboBox.SelectedIndex == 2)
            {
                DeactivateSortingByDateOfCreation();
                DateOfSupplySearchComboBox.SelectedIndex = 0;
                DateOfSupplySortStackPanel.Visibility = Visibility.Visible;
            }
        }
        private void DeactivateSortingByDateOfCreation()
        {
            DateOfCreationSearchComboBox.SelectedIndex = 0;
            DateOfCreationSortStackPanel.Visibility = Visibility.Collapsed;
            MaximumDateOfCreationDatePicker.Text = null;
            MinimumDateOfCreationDatePicker.Text = null;
        }
        private void DeactivateSortingByDateOfSupply()
        {
            DateOfSupplySearchComboBox.SelectedIndex = 0;
            DateOfSupplySortStackPanel.Visibility = Visibility.Collapsed;
            MaximumDateOfDeliveryDatePicker.Text = null;
            MinimumDateOfDeliveryDatePicker.Text = null;
        }
        private void CreateRangeYears()
        {
            List<string> yearList = new List<string>();
            if (_databaseEntities.Поставка.Count() > 0)
            {
                DateTime firstDeliveryDate = _databaseEntities.Поставка.Min(x => x.ДатаПоставки);
                int timeRange = month.Year - firstDeliveryDate.Year;
                for (int i = 0; i < timeRange + 1; i++)
                {
                    yearList.Add((firstDeliveryDate.Year + i).ToString());
                }
            }
            yearList.Insert(0, "Все года");
            YearSearchComboBox.ItemsSource = yearList.ToList();
            YearSearchComboBox.SelectedIndex = 0;
        }
        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _handleSelection = false;
            UpdateSupplyList();
        }
        private void ViewSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Поставка selectedSupply = button.DataContext as Поставка;
            NavigationService.Navigate(new AddSupplyPage(selectedSupply,false));
        }

        private void DeleteSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Поставка selectedSupply = button.DataContext as Поставка;
            ТоварПоставка productSupplyList = selectedSupply.ТоварПоставка.Where(x => x.Остаток > 0).FirstOrDefault();
            if (productSupplyList != null)
            {
                MessageBox.Show("Невозможно удалить данные о поставке пока не реализованы все товарные позиции");
                return;
            }
            if (selectedSupply != null && MessageBox.Show($"Вы хотите удалить выбранную поставку\nКод поставки: {selectedSupply.Код}" +
                $"\nДата поставки: {selectedSupply.ДатаПоставки}" +
                $"\nПоставщик: {selectedSupply.Поставщик.Наименование}", "Внимание", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                _databaseEntities.ТоварПоставка.RemoveRange(selectedSupply.ТоварПоставка);
                _databaseEntities.Поставка.Remove(selectedSupply);
                try
                {
                    _databaseEntities.SaveChanges();
                    MessageBox.Show("Информация о поставке успешно удалена");
                    UpdateSupplyList();
                }
                catch (Exception exeption)
                {
                    MessageBox.Show(exeption.Message);
                }
            }
        }

        private void ChangeSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Поставка selectedSupply = button.DataContext as Поставка;
            if (selectedSupply.Статус == 2)
            {
                MessageBox.Show("Невозможно редактировать отменённую поставку");
            }
            else if (selectedSupply.Статус == 3)
            {
                MessageBox.Show("Невозможно редактировать выполненную поставку");
            }
            else
            {
                NavigationService.Navigate(new AddSupplyPage(selectedSupply,true));
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSupplyList();
        }
        void ComboBoxItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _handleSelection = true;
        }

        private void ComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void ComboBox_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _databaseEntities.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            _handleSelection = false;
            CreateRangeYears();
            UpdateSupplyList();

        }

        private void AddSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddSupplyPage(null,false));
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            _databaseEntities.SaveChanges();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            _databaseEntities.SaveChanges();
        }
    }
}
