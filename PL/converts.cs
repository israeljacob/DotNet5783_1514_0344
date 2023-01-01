using DocumentFormat.OpenXml.Office.CustomUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL;

public class convertsAdd :  IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        int id = (int)value;
        if (id!=0)
            return Visibility.Visible;
        else
           return Visibility.Hidden;

    }
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    { throw new NotImplementedException(); }
}


public class convertsUpdate : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        int id = (int)value;
        if (id != 0)
            return Visibility.Hidden;
        else
            return Visibility.Visible;
    }
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    { throw new NotImplementedException(); }
}
