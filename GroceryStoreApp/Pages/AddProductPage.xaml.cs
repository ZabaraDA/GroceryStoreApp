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
            databasesEntities.Товар.Add(new Товар
            {
                Актикул = VendorCodeTextBox.Text,
                ШтрихКод = BarcodeCodeTextBox.Text,
                ЕдиницаИзмерения = UnitComboBox.SelectedItem as ЕдиницаИзмерения,
                Категория = CategoryComboBox.SelectedItem as Категория,
                Производитель = ManufacturerComboBox.SelectedItem as Производитель,
                Количество = 0,
                НДС = Convert.ToByte(VATTextBox.Text),
                Фото = photoProduct,
                Наименование = NameTextBox.Text,
                Высота = Convert.ToInt16(WidthTextBox.Text),
                Ширина = Convert.ToInt16(HeightTextBox.Text),
                Цена = Convert.ToInt16(CostTextBox.Text),
                СрокГодности = Convert.ToInt16(ExpirationDateTextBox.Text),
                Вес = Convert.ToInt32(WeightTextBox.Text),
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

        }

        private void AddManufacturerButton_Click(object sender, RoutedEventArgs e)
        {

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
