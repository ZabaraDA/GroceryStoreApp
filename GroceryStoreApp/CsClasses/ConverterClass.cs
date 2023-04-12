using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GroceryStoreApp.CsClasses
{
    public class RectangleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new Rect(0, 0, (double)values[0], (double)values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    [ValueConversion(typeof(decimal), typeof(decimal))]
    public class PriceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return null;

            Nullable<decimal> cost;
            Nullable<decimal> quantity;

            cost = values[0] as Nullable<decimal>;
            quantity = values[1] as Nullable<decimal>;

            if (cost == null || quantity == null)
                return null;
           
            return (cost * quantity);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    public class CubeCathetusAConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            Nullable<double> x;
            x = value as Nullable<int>;

            if (x == null)
            {
                return null;
            }

            x = x * 0.3;

            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class CubeCathetusBConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            Nullable<double> y;
            y = value as Nullable<int>;

            if (y == null)
            {
                return null;
            }

            y = y * 0.5;

            return y;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SideCubeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null|| values[2] == null)
            {
                return null;
            }

            Nullable<int> x;
            Nullable<int> y;
            Nullable<int> z;
            x = values[0] as Nullable<int>;
            y = values[1] as Nullable<int>;
            z = values[2] as Nullable<int>;
            //y = values as Nullable<int>;

            if ( x == null || y == null || z == null)
            {
                return null;
            }

            while(x > 100 || y > 100 || z > 100)
            {
                x = x / 2;
                y = y / 2;
                z = z / 2;
            }
           

            return x;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ByteArrayToBitmapImageConverter : IValueConverter
    {
        public BitmapImage ConvertByteArrayToBitMapImage(byte[] imageByteArray)
        {
            BitmapImage img = new BitmapImage();
            //img.BeginInit();
            using (MemoryStream memStream = new MemoryStream(imageByteArray))
            {
                img.StreamSource = memStream;
            }
            //img.EndInit();
            return img;
        }



        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            BitmapImage bitmapImage = new BitmapImage();

            using (MemoryStream memoryStream = new MemoryStream(value as byte[]))
            {
                
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
                //bitmapImage.Freeze();
                return bitmapImage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class ListBoxVisiblityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return null;

            Nullable<decimal> cost;
            Nullable<decimal> quantity;

            cost = values[0] as Nullable<decimal>;
            quantity = values[1] as Nullable<decimal>;

            if (cost == null || quantity == null)
                return null;

            return (cost * quantity);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
