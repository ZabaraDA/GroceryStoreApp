using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
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
using GroceryStoreApp.Databases;
using GroceryStoreApp.Pages;
using GroceryStoreApp.Windows;
using Microsoft.Win32;

namespace GroceryStoreApp.Pages
{
    public partial class AddUserPage : Page
    {
        readonly GroceryStoreDatabasesEntities databasesEntities = new GroceryStoreDatabasesEntities();
        readonly OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Multiselect = false,
            Filter = "Images (*.JPG; *.PNG)| *.JPG;*.PNG"
        };

        Сотрудник currentUser = new Сотрудник();
        Адрес currentAddress = new Адрес();
        Аккаунт currentAccount = new Аккаунт();

        //byte[] photoProfile;

        bool addWithAccount = false;
        public AddUserPage(Сотрудник selectedUser)
        {
            InitializeComponent();

            if (selectedUser != null)
            {
                currentUser = selectedUser;
                //currentAddress = selectedUser.Адрес;
                //if(selectedUser.Аккаунт != null)
                //{
                //    AccountStackPanel.Visibility = Visibility.Visible;
                //    addWithAccount = true;
                //    currentAccount = selectedUser.Аккаунт.FirstOrDefault();
                //}    
                //currentAddress = selectedUser.Адрес;
                if (selectedUser.Адрес != null)
                {
                    RegionComboBox.IsEnabled = true;
                    TypeLocalityComboBox.IsEnabled = true;
                    LocalityComboBox.IsEnabled = true;
                    StreetComboBox.IsEnabled = true;
                }
                
                
            }
            DataContext = currentUser;

            //AddressInfoStackPanel.DataContext = currentAddress;

            GenderComboBox.SelectedIndex = 0;
            FamilyStatusComboBox.SelectedIndex = 0;


            var typeLocalityItems = databasesEntities.ТипНаселённогоПункта.ToList();
            typeLocalityItems.Insert(0, new ТипНаселённогоПункта
            {
                Наименование = "Все населённые пункты"
            });
            TypeLocalityComboBox.DisplayMemberPath = "Наименование";
            TypeLocalityComboBox.ItemsSource = typeLocalityItems.ToList();

            var localityItems = databasesEntities.НаселённыйПункт.ToList();
            localityItems.Insert(0, new НаселённыйПункт
            {
                Наименование = "Укажите населённый пункт"
            });

            LocalityComboBox.ItemsSource = localityItems.ToList();

            //LocalityComboBox.SelectedIndex = 0;

            var professionItems = databasesEntities.Должность.ToList();
            professionItems.Insert(0, new Должность
            {
                Наименование = "Укажите должность"
            });
            ProfessionComboBox.ItemsSource = professionItems.ToList();
            ProfessionComboBox.DisplayMemberPath = "Наименование";
            ProfessionComboBox.SelectedIndex = 0;

            var subsidiaryItems = databasesEntities.Филиал.ToList();
            subsidiaryItems.Insert(0, new Филиал
            {
                Наименование = "Укажите филиал"

            });
            SubsidiaryComboBox.ItemsSource = subsidiaryItems.ToList();
            SubsidiaryComboBox.DisplayMemberPath = "Наименование";
            SubsidiaryComboBox.SelectedIndex = 0;


            var regionItems = databasesEntities.Регион.ToList();
            regionItems.Insert(0, new Регион
            {
                Наименование = "Укажите регион"
            });
            RegionComboBox.ItemsSource = regionItems.ToList();
            RegionComboBox.DisplayMemberPath = "Наименование";
            RegionComboBox.SelectedIndex = 0;

            var streetItems = databasesEntities.Улица.ToList();
            streetItems.Insert(0, new Улица
            {
                Наименование = "Укажите улицу"
            });
            StreetComboBox.ItemsSource = streetItems.ToList();
            StreetComboBox.DisplayMemberPath = "Наименование";
            //StreetComboBox.SelectedIndex = 0;

            LocalityComboBox.DisplayMemberPath = "Наименование";


        }

        private void AddPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(openFileDialog.FileName);
                bitmapImage.EndInit();

                CroppedBitmap croppedBitmap;
                int pixelSizeImage = 100;

                if (bitmapImage.PixelWidth > bitmapImage.PixelHeight)
                {
                    int widthPoint = (bitmapImage.PixelWidth - bitmapImage.PixelHeight) / 2;
                    croppedBitmap = new CroppedBitmap(bitmapImage, new Int32Rect(widthPoint, 0, bitmapImage.PixelHeight, bitmapImage.PixelHeight));
                }
                else if (bitmapImage.PixelWidth < bitmapImage.PixelHeight)
                {
                    int heightPoint = (bitmapImage.PixelHeight - bitmapImage.PixelWidth) / 2;
                    croppedBitmap = new CroppedBitmap(bitmapImage, new Int32Rect(0, heightPoint, bitmapImage.PixelWidth, bitmapImage.PixelWidth));
                }
                else
                {
                    croppedBitmap = new CroppedBitmap(bitmapImage, new Int32Rect(0, 0, (int)(bitmapImage.PixelWidth), (int)(bitmapImage.PixelWidth)));
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    JpegBitmapEncoder bitmapEncoder = new JpegBitmapEncoder();
                    bitmapEncoder.Frames.Add(BitmapFrame.Create(croppedBitmap));
                    bitmapEncoder.Save(memoryStream);

                    bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memoryStream;

                    bitmapImage.DecodePixelHeight = pixelSizeImage;
                    bitmapImage.DecodePixelWidth = pixelSizeImage;
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    bitmapImage.EndInit();

                    MemoryStream memory = new MemoryStream();
                    bitmapEncoder = new JpegBitmapEncoder();
                    bitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    bitmapEncoder.Save(memory);
                    

                    currentUser.Фото = memory.ToArray();
                    PhotoProfileImageBrush.ImageSource = bitmapImage;
                    memory.Dispose();
                }

            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            #region validation
            StringBuilder errors = new StringBuilder();
            if (NameTextBox.Text == "" || NameTextBox.Text == null)
            {
                errors.AppendLine("Введите имя");
            }
            if (SurnameTextBox.Text == "" || SurnameTextBox.Text == null)
            {
                errors.AppendLine("Введите фамилию");
            }
            if (PatronymicTextBox.Text == "" || PatronymicTextBox.Text == null)
            {
                errors.AppendLine("Введите отчество");
            }
            if (ProfessionComboBox.SelectedIndex == 0)
            {
                errors.AppendLine("Укажите должность");
            }
            if (GenderComboBox.SelectedIndex == 0)
            {
                errors.AppendLine("Укажите пол");
            }
            if (DOBDatePicker.Text == "" || DOBDatePicker.Text == null)
            {
                errors.AppendLine("Укажите дату рождения");
            }
            if (FamilyStatusComboBox.SelectedIndex == 0)
            {
                errors.AppendLine("Укажите семейное положение");
            }
            if (INNTextBox.Text == "" || INNTextBox.Text == null)
            {
                errors.AppendLine("Введите ИНН");
            }
            else
            {
                for (int i = 0; i < INNTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(INNTextBox.Text[i]))
                    {
                        errors.AppendLine("Номер ИНН должен состоять из цифр");
                        break;
                    }
                }
            }

            if (OMSTextBox.Text == "" || OMSTextBox.Text == null)
            {
                errors.AppendLine("Введите ОМС");
            }
            else
            {
                for (int i = 0; i < OMSTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(OMSTextBox.Text[i]))
                    {
                        errors.AppendLine("Номер ОМС должен состоять из цифр");
                        break;
                    }
                }
            }
            if (SNILSTextBox.Text == "" || SNILSTextBox.Text == null)
            {
                errors.AppendLine("Введите СНИЛС");
            }
            else
            {
                for (int i = 0; i < SNILSTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(SNILSTextBox.Text[i]))
                    {
                        errors.AppendLine("Номер СНИЛС должен состоять из цифр");
                        break;
                    }
                }
            }

            if (PhoneTextBox.Text == "" || PhoneTextBox.Text == null)
            {
                errors.AppendLine("Введите номер телефона");
            }
            else
            {
                for (int i = 0; i < PhoneTextBox.Text.Length; i++)
                {
                    if (!Char.IsDigit(PhoneTextBox.Text[i]))
                    {
                        errors.AppendLine("Номер телефона должен состоять из цифр");
                        break;
                    }
                }
            }
            if (EmploymentDatePicker.Text == "" || EmploymentDatePicker.Text == null)
            {
                errors.AppendLine("Укажите дату трудоустройства");
            }
            if (EmailTextBox.Text == "" || EmailTextBox.Text == null)
            {
                errors.AppendLine("Введите электронную почту");
            }
            if (RegionComboBox.SelectedIndex == 0)
            {
                errors.AppendLine("Укажите регион");
            }
            if (LocalityComboBox.SelectedIndex == 0)
            {
                errors.AppendLine("Укажите населённый пункт");
            }
            if (StreetComboBox.SelectedIndex == 0)
            {
                errors.AppendLine("Укажите улицу");
            }
            if (HouseTextBox.Text == "" || HouseTextBox.Text == null)
            {
                errors.AppendLine("Введите номер дома");
            }
            #endregion 

            if (addWithAccount == true)
            {
                if (AccessComboBox.SelectedIndex == 0)
                {
                    errors.AppendLine("Укажите уровень доступа");
                }
                if (LoginTextBox.Text == "" || LoginTextBox.Text == null)
                {
                    errors.AppendLine("Введите логин");
                }
                else if (LoginTextBox.Text.Length < 8)
                {
                    errors.AppendLine("Логин должен содержать не менее 8 символов");
                }
                if (PasswordBox.Password == "" || PasswordBox.Password == null)
                {
                    errors.AppendLine("Введите пароль");
                }
                else if (PasswordBox.Password.Length < 8)
                {
                    errors.AppendLine("Пароль должен содержать не менее 8 символов");
                }
                else if (PasswordBox.Password != DoublePasswordBox.Password)
                {
                    errors.AppendLine("Значения паролей не совпадают");
                }
            }

            if (PhotoProfileImageBrush.ImageSource == null)
            {
                if (MessageBox.Show("Вы уверены", "Внимание", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    return;
                }
            }


            if (errors.Length == 0)
            {

                //if (addWithAccount == true)
                //{


                //}
                //if (currentAddress.Код == 0)
                //{
                //databasesEntities.Адрес.AddOrUpdate(currentAddress);
                //currentUser.Адрес = currentAddress;
                //}
                databasesEntities.Адрес.Add(new Адрес
                {
                    Код = databasesEntities.Адрес.Count(),
                    КодРегиона = databasesEntities.Регион.Where(x => x.Наименование.Equals(RegionComboBox.Text)).FirstOrDefault().Код,
                    КодНаселённогоПункта = databasesEntities.НаселённыйПункт.Where(x => x.Наименование.Equals(LocalityComboBox.Text)).FirstOrDefault().Код,
                    КодУлицы = databasesEntities.Улица.Where(x => x.Наименование.Equals(StreetComboBox.Text)).FirstOrDefault().Код,
                    Дом = Convert.ToInt16(HouseTextBox.Text),
                    ТипАдреса = false,
                    //НаселённыйПункт = LocalityComboBox.SelectedItem as НаселённыйПункт,
                    //Улица = StreetComboBox.SelectedItem is Улица,
                });
                currentUser.КодАдресаСотрудника = databasesEntities.Адрес.Count();
                databasesEntities.Сотрудник.AddOrUpdate(currentUser);


                try
                {
                    databasesEntities.SaveChanges();
                    //NavigationService.GoBack();
                    NavigationService.Refresh();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show(errors.ToString());
            }

        }

        private void ClearInputFields_Click(object sender, RoutedEventArgs e)
        {
            foreach (TextBox textBox in FullNameStackPanel.Children)
            {
                textBox.Text = null;
            }

            List<Control> children = UserInfoStackPanel.Children.OfType<Control>().ToList();
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
                else if (control as DatePicker != null)
                {
                    var datePicker = control as DatePicker;
                    datePicker.Text = null;
                }
            }


            //photoProfile = null;

        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (addWithAccount == false)
            {
                addWithAccount = true;
                AccountStackPanel.Visibility = Visibility.Visible;
                NoAccountStackPanel.Visibility = Visibility.Collapsed;
                AddAccountButton.Content = "Отменить добавление аккаунта";

            }
            else if (addWithAccount == true)
            {
                addWithAccount = false;
                AccountStackPanel.Visibility = Visibility.Collapsed;
                NoAccountStackPanel.Visibility = Visibility.Visible;
                AddAccountButton.Content = "Добавить аккаунт";
            }
        }


        private void RegionComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (RegionComboBox.SelectedIndex > 0)
            {
                TypeLocalityComboBox.IsEnabled = true;
                var typeLocalityItems = databasesEntities.ТипНаселённогоПункта.ToList();
                typeLocalityItems.Insert(0, new ТипНаселённогоПункта
                {
                    Наименование = "Все населённые пункты"
                });
                TypeLocalityComboBox.DisplayMemberPath = "Наименование";
                TypeLocalityComboBox.ItemsSource = typeLocalityItems.ToList();
                TypeLocalityComboBox.SelectedIndex = 0;
            }
            else
            {
                TypeLocalityComboBox.IsEnabled = false;
                LocalityComboBox.IsEnabled = false;
                StreetComboBox.IsEnabled = false;
            }
        }

        private void TypeLocalityComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (TypeLocalityComboBox.SelectedIndex > 0)
            {
                LocalityComboBox.IsEnabled = true;
                var localityItems = databasesEntities.НаселённыйПункт.ToList().Where(x => x.Регион.Equals(RegionComboBox.SelectedItem) && x.Тип.Equals(TypeLocalityComboBox.SelectedIndex)).ToList();
                localityItems.Insert(0, new НаселённыйПункт
                {
                    Наименование = "Укажите населённый пункт"
                });

                LocalityComboBox.ItemsSource = localityItems.ToList();
                LocalityComboBox.SelectedIndex = 0;
            }
            else
            {
                LocalityComboBox.IsEnabled = false;
                StreetComboBox.IsEnabled = false;
            }
        }

        private void LocalityComboBox_DropDownClosed(object sender, EventArgs e)
        {

            if (LocalityComboBox.SelectedIndex > 0)
            {
                var streetItems = databasesEntities.Улица.ToList().Where(x => x.НаселённыйПункт.Equals(LocalityComboBox.SelectedItem)).ToList();
                StreetComboBox.IsEnabled = true;
                streetItems.Insert(0, new Улица
                {
                    Наименование = "Укажите улицу"
                });
                StreetComboBox.ItemsSource = streetItems.ToList();
                StreetComboBox.DisplayMemberPath = "Наименование";
                StreetComboBox.SelectedIndex = 0;
            }
            else
            {
                StreetComboBox.IsEnabled = false;
            }
        }

        private void RegionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var regionItems = databasesEntities.Регион.Where(x => x.Наименование.ToLower().Contains(RegionTextBox.Text.ToLower())).ToList();
            regionItems.Insert(0, new Регион
            {
                Наименование = "Укажите регион"
            });

            RegionComboBox.ItemsSource = regionItems.ToList();
            RegionComboBox.DisplayMemberPath = "Наименование";
            RegionComboBox.SelectedIndex = 0;

            TypeLocalityComboBox.IsEnabled = false;
            LocalityComboBox.IsEnabled = false;
            StreetComboBox.IsEnabled = false;
        }
    }
}




//databasesEntities.Сотрудник.Add(new Сотрудник
//{
//    Имя = NameTextBox.Text,
//    Фамилия = SurnameTextBox.Text,
//    Отчество = PatronymicTextBox.Text,
//    ДатаРождения = Convert.ToDateTime(DOBDatePicker.Text),
//    ДатаТрудоустройства = Convert.ToDateTime(EmploymentDatePicker.Text),
//    КодДолжности = ProfessionComboBox.SelectedIndex,
//    ИНН = INNTextBox.Text,
//    ОМС = OMSTextBox.Text,
//    СНИЛС = SNILSTextBox.Text,
//    Почта = EmailTextBox.Text,
//    Телефон = PhoneTextBox.Text,
//    Фото = photoProfile,
//    Пол = Convert.ToBoolean(GenderComboBox.SelectedIndex),
//    СемейноеПоложение = Convert.ToBoolean(FamilyStatusComboBox.SelectedIndex),

//});
//databasesEntities.Аккаунт.Add(new Аккаунт
//{
//    КодСотрудника = databasesEntities.Сотрудник.Count(),
//    Логин = LoginTextBox.Text,
//    Пароль = PasswordBox.Password,
//    УровеньДоступа = (byte)AccessComboBox.SelectedIndex
//});