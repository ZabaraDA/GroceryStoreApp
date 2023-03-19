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

namespace GroceryStoreApp.Pages
{
    public partial class DataOfSupplyPage : Page
    {
        readonly GroceryStoreDatabasesEntities databaseEntities = new GroceryStoreDatabasesEntities();

        DateTime month = DateTime.Now;

        
        public DataOfSupplyPage()
        {
            InitializeComponent();
            CreateRangeYears();
            TypeSortComboBox.SelectedIndex = 0;
            UpdateSupplyList();
        }

        private void UpdateSupplyList()
        {
            var supplyList = databaseEntities.Поставка.OrderByDescending(x=> x.ДатаПоставки).ToList();

            if(YearSearchComboBox.SelectedIndex > 0)
            {
                supplyList = supplyList.Where(x => x.ДатаПоставки.Year.Equals(Convert.ToInt16(YearSearchComboBox.SelectedItem))).ToList();
            }

            SupplyListView.ItemsSource = supplyList.ToList();
        }
        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                ComboBoxItem comboBoxItem = comboBox.SelectedItem as ComboBoxItem;
                comboBox.Foreground = comboBoxItem.Foreground;
            }
        }

        private void TypeSortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TypeSortComboBox.SelectedIndex < 1)
            {
                DeactivateSortingByDateOfCreation();
                DeactivateSortingByDateOfSupply();
            }
            else if (TypeSortComboBox.SelectedIndex == 1)
            {
                DeactivateSortingByDateOfCreation();
                DateOfCreationSearchComboBox.SelectedIndex = 0;
                DateOfCreationSortStackPanel.Visibility = Visibility.Visible;
            }
            else if (TypeSortComboBox.SelectedIndex == 2)
            {
                DeactivateSortingByDateOfSupply();
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
            DateTime firstDeliveryDate = databaseEntities.Поставка.Min(x => x.ДатаПоставки);
            int timeRange = month.Year - firstDeliveryDate.Year;
            List<string> year = new List<string>();
            for (int i = 0; i < timeRange + 1; i++)
            {
                year.Add((firstDeliveryDate.Year + i).ToString());
            }
            year.Insert(0, "Все года");
            
            YearSearchComboBox.ItemsSource = year.ToList();
            YearSearchComboBox.SelectedIndex = 0;
        }

        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSupplyList();
        }

        private void ViewSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteSupplyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeSupplyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSupplyList();
        }
    }
}
