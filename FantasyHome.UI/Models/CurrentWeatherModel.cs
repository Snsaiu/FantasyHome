namespace FantasyHome.UI.Models;

/// <summary>
/// 天气模型
/// </summary>
public class CurrentWeatherModel
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
    public Now now { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Refer refer { get; set; }
}

//如果好用，请收藏地址，帮忙分享。
public class Now
{
    /// <summary>
    /// 
    /// </summary>
    public string obsTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string temp { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string feelsLike { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string icon { get; set; }
    /// <summary>
    /// 阴
    /// </summary>
    public string text { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string wind360 { get; set; }
    /// <summary>
    /// 东北风
    /// </summary>
    public string windDir { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string windScale { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string windSpeed { get; set; }
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
    public string dew { get; set; }
}

public class Refer
{
    /// <summary>
    /// 
    /// </summary>
    public List <string > sources { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List <string > license { get; set; }
}


