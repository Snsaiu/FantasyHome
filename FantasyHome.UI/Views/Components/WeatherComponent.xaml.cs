using System.Windows.Input;
using FantasyHome.UI.Models;

namespace FantasyHome.UI.Views.Components;

public partial class WeatherComponent : ContentView
{
	public WeatherComponent()
	{
		InitializeComponent();
	}

    /// <summary>
    /// token 值
    /// </summary>
    public static readonly BindableProperty TokenProperty = BindableProperty.Create(
        "Token",
        typeof(string),
        typeof(WeatherComponent),
        ""
    );

    public string Token
    {
        get { return (string)GetValue(TokenProperty); }
        set { SetValue(TokenProperty, value); }
    }

    public static readonly BindableProperty AcIpProperty = BindableProperty.Create(
        "AcIp",
        typeof(string),
        typeof(WeatherComponent),
        ""
    );

    public string AcIp
    {
        get { return (string)GetValue(AcIpProperty); }
        set { SetValue(AcIpProperty, value); }
    }


    public static readonly BindableProperty AcIdProperty = BindableProperty.Create(
        "AcId",
        typeof(string),
        typeof(WeatherComponent),
        ""
    );

    public string AcId
    {
        get { return (string)GetValue(AcIdProperty); }
        set { SetValue(AcIdProperty, value); }
    }

    public static readonly BindableProperty AcK1Property = BindableProperty.Create(
        "AcK1",
        typeof(string),
        typeof(WeatherComponent),
        ""
    );

    public string AcK1
    {
        get { return (string)GetValue(AcK1Property); }
        set { SetValue(AcK1Property, value); }
    }


    #region ����
    public static readonly BindableProperty WeeksWeatherListProperty = BindableProperty.Create("WeeksWeatherList",
        typeof(IEnumerable<WeekWeatherListModel>), typeof(WeatherComponent));

    public IEnumerable<WeekWeatherListModel> WeeksWeatherList
    {
        get { return (IEnumerable<WeekWeatherListModel>)GetValue(WeeksWeatherListProperty); }
        set { SetValue(WeeksWeatherListProperty, value); }
    }


    public static readonly BindableProperty BarColorProperty = BindableProperty.Create("BarColor",
        typeof(Color), typeof(WeatherComponent));

    public Color BarColor
    {
        get { return (Color)GetValue(BarColorProperty); }
        set { SetValue(BarColorProperty, value); }
    }

    /// <summary>
    /// ��������ͼ��
    /// </summary>
    public static readonly BindableProperty CurrentDayIconProperty = BindableProperty.Create("CurrentDayIcon",
        typeof(ImageSource), typeof(WeatherComponent));

    public ImageSource CurrentDayIcon
    {
        get { return (ImageSource)GetValue(CurrentDayIconProperty); }
        set { SetValue(CurrentDayIconProperty, value); }
    }

    /// <summary>
    /// �����¶�
    /// </summary>
    public static readonly BindableProperty CurrentTemperatureProperty = BindableProperty.Create("CurrentTemperature",
        typeof(string), typeof(WeatherComponent));

    public string CurrentTemperature
    {
        get { return (string)GetValue(CurrentTemperatureProperty); }
        set { SetValue(CurrentTemperatureProperty, value); }
    }


    /// <summary>
    /// ������������
    /// </summary>
    public static readonly BindableProperty CurrentStateProperty = BindableProperty.Create("CurrentState",
        typeof(string), typeof(WeatherComponent));

    public string CurrentState
    {
        get { return (string)GetValue(CurrentStateProperty); }
        set { SetValue(CurrentStateProperty, value); }
    }

    /// <summary>
    /// ����������
    /// </summary>
    public static readonly BindableProperty CurrentRayProperty = BindableProperty.Create("CurrentRay",
        typeof(string), typeof(WeatherComponent));

    public string CurrentRay
    {
        get { return (string)GetValue(CurrentRayProperty); }
        set { SetValue(CurrentRayProperty, value); }
    }

    

    public static readonly BindableProperty TapCommandProperty = BindableProperty.Create("TapCommand",
        typeof(ICommand), typeof(WeatherComponent));

    public ICommand TapCommand
    {
        get { return  (ICommand)GetValue(TapCommandProperty); }
        set { SetValue(TapCommandProperty, value); }
    }
    #endregion

    private void Send()
    {

    }

}