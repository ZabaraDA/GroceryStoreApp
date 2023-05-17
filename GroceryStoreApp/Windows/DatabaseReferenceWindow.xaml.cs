using GroceryStoreApp.Databases;
using GroceryStoreApp.Models.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GroceryStoreApp.Windows
{
    public partial class DatabaseReferenceWindow : Window
    {
        public DatabaseReferenceWindow(ФилиалТовар currentProductSubsidiary)
        {
            InitializeComponent();
            if(currentProductSubsidiary != null)
            {
                DataContext = currentProductSubsidiary;
            }

        }

        
        public decimal MinimumLimit
        {
            get { return (decimal)GetValue(MinimumLimitProperty); }
            set { SetValue(MinimumLimitProperty, value); }
        }
        public static readonly DependencyProperty MinimumLimitProperty =
            DependencyProperty.Register("MinimumLimit", typeof(decimal), typeof(DatabaseReferenceWindow), new FrameworkPropertyMetadata(0.0M));
        public decimal NormalLimit
        {
            get { return (decimal)GetValue(NormalLimitProperty); }
            set { SetValue(NormalLimitProperty, value); }
        }
        public static readonly DependencyProperty NormalLimitProperty =
            DependencyProperty.Register("NormalLimit", typeof(decimal), typeof(DatabaseReferenceWindow), new FrameworkPropertyMetadata(0.0M));

        private void CancelAddingLimitButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("При отмене заполнения формы данные о поставке не будут изменены, а статус поставки примет исходное значение. Отменить изменения?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DialogResult = false;
                this.Close();
            }
        }

        private void AddLimitToSubsidiaryButton_Click(object sender, RoutedEventArgs e)
        {
            #region LimitValidation

            StringBuilder errors = new StringBuilder();

            if(MinimumLimitTextBox.Text == "" || MinimumLimitTextBox.Text == null)
            {
                errors.AppendLine("Введите минимальный лимит");
            }
            else
            {
                for (int i = 0; i < MinimumLimitTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(MinimumLimitTextBox.Text[i]))
                    {
                        errors.AppendLine("Минимальный лимит должен состоять из цифр");
                        break;
                    }
                }
            }
            if (NormalLimitTextBox.Text == "" || NormalLimitTextBox.Text == null)
            {
                errors.AppendLine("Введите норму товара в филиале");
            }
            else
            {
                for (int i = 0; i < NormalLimitTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(NormalLimitTextBox.Text[i]))
                    {
                        errors.AppendLine("Лимит должен состоять из цифр");
                        break;
                    }
                }
            }
            #endregion
            if(errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            MinimumLimit = Convert.ToDecimal(MinimumLimitTextBox.Text);
            NormalLimit = Convert.ToDecimal(NormalLimitTextBox.Text);
            DialogResult = true;
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
