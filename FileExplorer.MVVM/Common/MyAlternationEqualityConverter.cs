using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FileExplorer.MVVM
{
    public class MyAlternationEqualityConverter : IMultiValueConverter
  {
    #region Implementation of IMultiValueConverter
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values != null && values.Length == 2 &&
            values[0] is int && values[1] is int)
        {
            bool retval = Equals((int)values[0], (int)values[1] + 1);
            return retval;
        }
        return DependencyProperty.UnsetValue;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
    #endregion
} 
}
