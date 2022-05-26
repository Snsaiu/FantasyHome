using System.Globalization;
using FantasyHome.UI.Resources;

namespace FantasyHome.UI.Converters;

public class WeatherIconConverter:IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        string v = value.ToString();
        return WeatherIconFont.GetValueByKey(v);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}