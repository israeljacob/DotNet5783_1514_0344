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
public class zeroToTrue : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        int id = (int)value;
        if (id != 0)
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

public class zeroToHidden : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        int id = (int)value;
        if (id==0)
            return Visibility.Hidden;
        return Visibility.Visible;
    }
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    { throw new NotImplementedException(); }
}

public class zeroToVisible : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        int id = (int)value;
        if (id != 0)
            return Visibility.Hidden;
        return Visibility.Visible;
    }
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    { throw new NotImplementedException(); }
}

public class trueToVisible : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        bool flag = (bool)value;
        if (!flag)
            return Visibility.Hidden;
        return Visibility.Visible;
    }
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    { throw new NotImplementedException(); }
}

public class windowToBool : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        string winName = (string)value;
        if (winName == "ProductListWindow")
            return false;
        return true;
    }
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    { throw new NotImplementedException(); }
}
public class windowToVisibile : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        string winName = (string)value;
        if (winName == "ProductListWindow")
            return Visibility.Visible;
        return Visibility.Hidden;
    }
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    { throw new NotImplementedException(); }
}

public class windowToHidden : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        string winName = (string)value;
        if (winName == "ProductListWindow")
            return Visibility.Hidden;
        return Visibility.Visible;
    }
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    { throw new NotImplementedException(); }
}