using System.Reflection;
using FantasyHome.UI.Resources;
using FantasyResultModel;
using FantasyResultModel.Impls;

namespace FantasyHome.UI.Utils;

public class Tools
{
    public static  readonly string WeatherKey="442fcedfd10d4c46acabe91529bc5d80";
      
    public static readonly string  CurrentWeatherApi="https://devapi.qweather.com/v7/weather/now?";

    public static readonly string MulitWeatherApi = "https://devapi.qweather.com/v7/weather/3d?";
    
    /// <summary>
    /// 获得当前位置
    /// </summary>
    /// <returns></returns>
    public static async Task<ResultBase<Location>> GetCurrentLocation()
    {
        try
        {
            Location location = await Geolocation.Default.GetLastKnownLocationAsync();
            return location != null
                ? new SuccessResultModel<Location>(location)
                : new ErrorResultModel<Location>("未知原因无法获得位置信息");
        }
        catch (Exception e)
        {
            return new ErrorResultModel<Location>(e.Message);
        }
    }
    
}

public class demo
{
    public string Name { get; set; }
}