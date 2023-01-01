using DocumentFormat.OpenXml.Office.CustomUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PL;

public class zeroToContent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        int id = (int)value;
        if (id > 0)
            return "UPDATE";
        return "ADD";

    }
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    { throw new NotImplementedException(); }
}
[ValueConversion(typeof(int), typeof(bool))]
public class zeroToTrue : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        int id = (int)value;
        if (id > 0)
            return false;
        return true;

    }
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    { throw new NotImplementedException(); }
}

public class zeroToFalse : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        int id = (int)value;
        if (id > 0)
            return true;
        return false;
    }
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    { throw new NotImplementedException(); }
}
