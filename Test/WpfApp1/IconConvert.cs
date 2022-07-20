using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1;

public class IconConvert:IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        return new BitmapImage(new Uri("/" + value.ToString() + ".png"));
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}