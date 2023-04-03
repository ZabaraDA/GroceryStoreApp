using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
    public class QuantityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }

            Nullable<decimal> quantity;
            quantity = value as Nullable<decimal>;

            if (quantity == null)
            {
                return null;
            }

            string quantityString = quantity.ToString();

            for (int i = 0; i < quantityString.Length; i++)
            {
                if(quantityString[i] == ',')
                {
                    return quantityString;
                }
            }
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
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
