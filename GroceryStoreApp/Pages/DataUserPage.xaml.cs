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
    public partial class DataUserPage : Page
    {
        GroceryStoreDatabasesEntities databasesEntities = new GroceryStoreDatabasesEntities();

        public DataUserPage()
        {
            InitializeComponent();

            var itemsProfessionList = databasesEntities.Должность.ToList();
            itemsProfessionList.Insert(0, new Должность
            {
                Наименование = "Все должности"
            });
            ProfessionSearchComboBox.ItemsSource = itemsProfessionList.ToList();
            ProfessionSearchComboBox.DisplayMemberPath = "Наименование";
            ProfessionSearchComboBox.SelectedIndex = 0;

            FamilyStatusSearchComboBox.SelectedIndex = 0;
            
            GenderSearchComboBox.SelectedIndex = 0;
            AccountSearchComboBox.SelectedIndex = 0;
            SearchUserDataUpdate();

        }

        private void SearchUserDataUpdate()
        {
            var itemUsers = databasesEntities.Сотрудник.ToList();
            int numberOfUsers = databasesEntities.Сотрудник.Count();
            NumberOfUsersTextBlock.Text = numberOfUsers.ToString();

            if (NameSearchTextBox.Text != "" && NameSearchTextBox.Text != null)
            {
                itemUsers = itemUsers.Where(x => x.Фамилия.ToLower().Contains(NameSearchTextBox.Text.ToLower()) || x.Имя.ToLower().Contains(NameSearchTextBox.Text.ToLower()) || x.Отчество.ToLower().Contains(NameSearchTextBox.Text.ToLower())).ToList();
            }
            if (FamilyStatusSearchComboBox.SelectedIndex > 0)
            {
                if (FamilyStatusSearchComboBox.SelectedIndex == 1)
                {
                    itemUsers = itemUsers.Where(x => x.СемейноеПоложение.Equals(true)).ToList();
                }
                else if (FamilyStatusSearchComboBox.SelectedIndex == 2)
                {
                    itemUsers = itemUsers.Where(x => x.СемейноеПоложение.Equals(false)).ToList();
                }
            }
            if (ProfessionSearchComboBox.SelectedIndex > 0)
            {
                itemUsers = itemUsers.Where(x => x.Должность.Equals(ProfessionSearchComboBox.SelectedItem)).ToList();
            }
            if (GenderSearchComboBox.SelectedIndex > 0)
            {
                if (GenderSearchComboBox.SelectedIndex == 1)
                {
                    itemUsers = itemUsers.Where(x => x.Пол.Equals(true)).ToList();
                }
                else if (GenderSearchComboBox.SelectedIndex == 2)
                {
                    itemUsers = itemUsers.Where(x => x.Пол.Equals(false)).ToList();
                }
            }
            if (AccountSearchComboBox.SelectedIndex > 0)
            {
                int[] idUsers = databasesEntities.Аккаунт.Select(x => x.КодСотрудника).ToArray();
                List<Сотрудник> r = new List<Сотрудник>();
                for (int i = 0; i < idUsers.Length; i++)
                {
                    r.Add(itemUsers.Where(x => x.Код != idUsers[i]).FirstOrDefault());
                }
                if (AccountSearchComboBox.SelectedIndex == 1)
                {
                    itemUsers = itemUsers.Except(r).ToList();

                }
                else if (AccountSearchComboBox.SelectedIndex == 2)
                {
                    itemUsers = r.ToList();
                }
            }

            numberOfUsers = itemUsers.Count();
            FilterNumberOfUserTextBlock.Text = numberOfUsers.ToString();
            UserListView.ItemsSource = itemUsers.ToList();
        }
        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchUserDataUpdate();
            UserGrid.Visibility = Visibility.Collapsed;
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchUserDataUpdate();
            UserGrid.Visibility = Visibility.Collapsed;
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddUserPage(null));
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserListView.SelectedItem is Сотрудник userItem)
            {
                var lp = databasesEntities.Аккаунт.Where(x => x.КодСотрудника.Equals(userItem.Код)).FirstOrDefault();
                if (lp == null)
                {
                    databasesEntities.Сотрудник.Remove(userItem);
                    databasesEntities.SaveChanges();
                    SearchUserDataUpdate();
                }
                else
                {
                    MessageBox.Show("У сотрудника есть связанный аккаунт");
                }
            }

        }

        private void ChangeParametersButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = UserListView.SelectedItem as Сотрудник;
            NavigationService.Navigate(new AddUserPage(selectedUser));
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            ProfessionSearchComboBox.SelectedIndex = 0;
            NameSearchTextBox.Text = "";
            FamilyStatusSearchComboBox.SelectedIndex = 0;
            GenderSearchComboBox.SelectedIndex = 0;
            AccountSearchComboBox.SelectedIndex = 0;
        }

        private void UserListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserGrid.DataContext = UserListView.SelectedItem;
            UserGrid.Visibility = Visibility.Visible;
        }

        private void HidePanelButton_Click(object sender, RoutedEventArgs e)
        {
            UserGrid.Visibility = Visibility.Collapsed;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            SearchUserDataUpdate();
            //databasesEntities.ChangeTracker.Entries().ToList().ForEach(x =>x.Reload());
        }
    }
}
