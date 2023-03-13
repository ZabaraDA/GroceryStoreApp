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

        int[] year;
        public DataOfSupplyPage()
        {
            InitializeComponent();
            CreateRangeYears();
            SupplyListView.ItemsSource = databaseEntities.Поставка.ToList();
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
            year = new int[timeRange + 1];
            for (int i = 0; i < year.Length; i++)
            {
                year[i] = firstDeliveryDate.Year + i;
            }
            YearSearchComboBox.ItemsSource = year;
            YearSearchComboBox.SelectedIndex = 0;
        }

        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
    }
}
