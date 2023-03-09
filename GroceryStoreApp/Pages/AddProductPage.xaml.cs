using GroceryStoreApp.Databases;
using Microsoft.Win32;
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

namespace GroceryStoreApp.Pages
{
    public partial class AddProductPage : Page
    {
        readonly GroceryStoreDatabasesEntities databasesEntities = new GroceryStoreDatabasesEntities();
        readonly OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Multiselect = false,
            Filter = "Images (*.JPG; *.PNG)| *.JPG;*.PNG"
        };
        public AddProductPage()
        {
            InitializeComponent();
        }

        byte[] photoProduct;

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

            if (VATTextBox.Text == null || VATTextBox.Text == "")
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

            if(ManufacturerComboBox.SelectedIndex == 0)
            {
                errors.AppendLine("Укажите производителя товара");
            }

            if (VATTextBox.Text == null || VATTextBox.Text == "")
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

            if (UnitComboBox.SelectedIndex == 0)
            {
                errors.AppendLine("Укажите единицу измерения товара");
            }

            if (WeightTextBox.Text == null || WeightTextBox.Text == "")
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

            if (WidthTextBox.Text == null || WidthTextBox.Text == "")
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

            if (HeightTextBox.Text == null || WidthTextBox.Text == "")
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

            if (photoProduct == null)
            {
                if (MessageBox.Show("Добавить товар без изображения?", "Внимание", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            if(DescriptionTextBox.Text == null|| DescriptionTextBox.Text == "")
            {
                if (MessageBox.Show("Добавить товар без описания?", "Внимание", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

            #endregion



            databasesEntities.Товар.Add(new Товар
            {
                Актикул = VendorCodeTextBox.Text,
                ШтрихКод = BarcodeCodeTextBox.Text,
                ЕдиницаИзмерения = UnitComboBox.SelectedItem as ЕдиницаИзмерения,
                Категория = CategoryComboBox.SelectedItem as Категория,
                Производитель = ManufacturerComboBox.SelectedItem as Производитель,
                Описание = DescriptionTextBox.Text,
                Количество = 0,
                НДС = Convert.ToByte(VATTextBox.Text),
                Фото = photoProduct,
                Наименование = NameTextBox.Text,
                Высота = Convert.ToInt16(WidthTextBox.Text),
                Ширина = Convert.ToInt16(HeightTextBox.Text),
                Цена = Convert.ToInt16(CostTextBox.Text),
                СрокГодности = Convert.ToInt16(ExpirationDateTextBox.Text),
                Вес = Convert.ToInt32(WeightTextBox.Text),
                Статус = true
            });
            try
            {
                databasesEntities.SaveChanges();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
                int pixelSizeImage = 800;

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


                    photoProduct = memory.ToArray();
                    PhotoProductImageBrush.ImageSource = bitmapImage;
                    memory.Dispose();
                }

            }
        }

        private void ClearInputFields_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var categoryItems = databasesEntities.Категория.ToList();
            categoryItems.Insert(0, new Категория
            {
                Наименование = "Выберите категорию",
                //Подкатегория = false,
            });

            CategoryComboBox.ItemsSource = categoryItems.ToList();
            CategoryComboBox.DisplayMemberPath = "Наименование";
            CategoryComboBox.SelectedIndex = 0;

            var manufacturerItems = databasesEntities.Производитель.ToList();
            manufacturerItems.Insert(0, new Производитель
            {
                Наименование = "Выберите производителя"
                
            });

            ManufacturerComboBox.ItemsSource = manufacturerItems.ToList();
            ManufacturerComboBox.DisplayMemberPath = "Наименование";
            ManufacturerComboBox.SelectedIndex = 0;

            var unitItems = databasesEntities.ЕдиницаИзмерения.ToList();
            unitItems.Insert(0, new ЕдиницаИзмерения
            {
                Наименование = "Выберите единицу измерения",
                Формат = false,

            });

            UnitComboBox.ItemsSource = unitItems.ToList();
            UnitComboBox.DisplayMemberPath = "Наименование";
            UnitComboBox.SelectedIndex = 0;
        }

        private void AddManufacturerButton_Click(object sender, RoutedEventArgs e)
        {
            //List<Товар> product = databasesEntities.Товар.ToList();
            //databasesEntities.Списание.Add(new Списание
            //{
            //    Товар = product,
            //});
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
    }
}
