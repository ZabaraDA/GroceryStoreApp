using System;
using System.Collections.Generic;
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
using GroceryStoreApp.Databases;
using GroceryStoreApp.Pages;
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
        readonly ImageSource photo;

        byte[] photoProfile;

        bool addWithAccount = false;
        public AddUserPage()
        {
            InitializeComponent();

            photo = PhotoProfileImageBrush.ImageSource;

            GenderComboBox.SelectedIndex = 0;
            FamilyStatusComboBox.SelectedIndex = 0;
            TypeLocalityComboBox.SelectedIndex = 0;

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
            StreetComboBox.SelectedIndex = 0;

            var localityItems = databasesEntities.НаселённыйПункт.ToList();
            localityItems.Insert(0, new НаселённыйПункт
            {
                Наименование = "Укажите населённый пункт"
            });
            LocalityComboBox.ItemsSource = localityItems.ToList();
            LocalityComboBox.DisplayMemberPath = "Наименование";
            LocalityComboBox.SelectedIndex = 0;
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
                int pixelSizeImage = 200;

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

                    bitmapEncoder = new JpegBitmapEncoder();
                    bitmapEncoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    bitmapEncoder.Save(memoryStream);

                    photoProfile = memoryStream.ToArray();
                }
                PhotoProfileImageBrush.ImageSource = bitmapImage;
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
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

            //if (SubsidiaryComboBox.SelectedIndex == 0)
            //{
            //    errors.AppendLine("Укажите филиал");
            //}
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

            if (photoProfile == null)
            {
                if (MessageBox.Show("Вы уверены", "Внимание", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    return;
                }
            }


            if (errors.Length == 0)
            {
                databasesEntities.Адрес.Add(new Адрес
                {
                    КодРегиона = databasesEntities.Регион.Where(x => x.Наименование.Equals(RegionComboBox.Text)).FirstOrDefault().Код,
                    КодНаселённогоПункта = databasesEntities.НаселённыйПункт.Where(x => x.Наименование.Equals(LocalityComboBox.Text)).FirstOrDefault().Код,
                    КодУлицы = databasesEntities.Улица.Where(x => x.Наименование.Equals(StreetComboBox.Text)).FirstOrDefault().Код,
                    Дом = Convert.ToInt16(HouseTextBox.Text),
                    ТипАдреса = false,
                    //НаселённыйПункт = LocalityComboBox.SelectedItem as НаселённыйПункт,
                    //Улица = StreetComboBox.SelectedItem as Улица,
                });

                databasesEntities.Сотрудник.Add(new Сотрудник
                {
                    Имя = NameTextBox.Text,
                    Фамилия = SurnameTextBox.Text,
                    Отчество = PatronymicTextBox.Text,
                    ДатаРождения = Convert.ToDateTime(DOBDatePicker.Text),
                    ДатаТрудоустройства = Convert.ToDateTime(EmploymentDatePicker.Text),
                    КодДолжности = ProfessionComboBox.SelectedIndex,
                    ИНН = INNTextBox.Text,
                    ОМС = OMSTextBox.Text,
                    СНИЛС = SNILSTextBox.Text,
                    Почта = EmailTextBox.Text,
                    Телефон = PhoneTextBox.Text,
                    Фото = photoProfile,
                    Пол = Convert.ToBoolean(GenderComboBox.SelectedIndex),
                    СемейноеПоложение = Convert.ToBoolean(FamilyStatusComboBox.SelectedIndex),

                });
                if (addWithAccount == true)
                {
                    databasesEntities.Аккаунт.Add(new Аккаунт
                    {
                        КодСотрудника = databasesEntities.Сотрудник.Count(),
                        Логин = LoginTextBox.Text,
                        Пароль = PasswordBox.Password,
                        УровеньДоступа = (byte)AccessComboBox.SelectedIndex
                    });
                }


                try
                {
                    databasesEntities.SaveChanges();
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

            PhotoProfileImageBrush.ImageSource = photo;
            photoProfile = null;

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
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
                var localityItems = databasesEntities.НаселённыйПункт.ToList();
            if (RegionComboBox.SelectedIndex > 0)
            {
                localityItems = localityItems.Where(x => x.Регион.Equals(RegionComboBox.SelectedItem)).ToList();
            }
                localityItems.Insert(0, new НаселённыйПункт
                {
                    Наименование = "Укажите населённый пункт"
                });
                LocalityComboBox.ItemsSource = localityItems.ToList();
                LocalityComboBox.DisplayMemberPath = "Наименование";
                LocalityComboBox.SelectedIndex = 0;
                var streetItems = databasesEntities.Улица.ToList();

            if(LocalityComboBox.SelectedIndex > 0)
            {
                streetItems = streetItems.Where(x => x.НаселённыйПункт.Equals(LocalityComboBox.SelectedItem)).ToList();
            }
                streetItems.Insert(0, new Улица
                {
                    Наименование = "Укажите улицу"
                });
                StreetComboBox.ItemsSource = streetItems.ToList();
                StreetComboBox.DisplayMemberPath = "Наименование";
                StreetComboBox.SelectedIndex = 0;
        }
    }
}
