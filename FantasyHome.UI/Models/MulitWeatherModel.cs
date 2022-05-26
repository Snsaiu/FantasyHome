namespace FantasyHome.UI.Models;

//如果好用，请收藏地址，帮忙分享。
public class DailyItem
{
    /// <summary>
    /// 
    /// </summary>
    public string fxDate { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string sunrise { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string sunset { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string moonrise { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string moonset { get; set; }
    /// <summary>
    /// 残月
    /// </summary>
    public string moonPhase { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string moonPhaseIcon { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string tempMax { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string tempMin { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string iconDay { get; set; }
    /// <summary>
    /// 雷阵雨
    /// </summary>
    public string textDay { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string iconNight { get; set; }
    /// <summary>
    /// 多云
    /// </summary>
    public string textNight { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string wind360Day { get; set; }
    /// <summary>
    /// 东南风
    /// </summary>
    public string windDirDay { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string windScaleDay { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string windSpeedDay { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string wind360Night { get; set; }
    /// <summary>
    /// 东南风
    /// </summary>
    public string windDirNight { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string windScaleNight { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string windSpeedNight { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string humidity { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string precip { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string pressure { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string vis { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string cloud { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string uvIndex { get; set; }
}



public class MulitWeatherModel
{
    /// <summary>
    /// 
    /// </summary>
    public string code { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string updateTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string fxLink { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List <DailyItem > daily { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Refer refer { get; set; }
}
