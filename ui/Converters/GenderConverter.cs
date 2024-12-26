using System;
using System.Globalization;

namespace ui.Converters;


public class GenderConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        
        return (int)value==1? "Male":"Female";

    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}