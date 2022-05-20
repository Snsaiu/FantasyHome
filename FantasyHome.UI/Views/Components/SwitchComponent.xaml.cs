using System.Windows.Input;

namespace FantasyHome.UI.Views.Components;

public partial class SwitchComponent : ContentView
{
	public SwitchComponent()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty BarColorProperty = BindableProperty.Create("BarColor",
        typeof(Color), typeof(WeatherComponent));

    public Color BarColor
    {
        get { return (Color)GetValue(BarColorProperty); }
        set { SetValue(BarColorProperty, value); }
    }

    public static readonly BindableProperty IconProperty =
        BindableProperty.Create("Icon", typeof(ImageSource), typeof(SwitchComponent));

    public ImageSource Icon
    {
        get { return (ImageSource)GetValue(IconProperty); }
        set { SetValue(IconProperty, value); }
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create("Title", typeof(string), typeof(SwitchComponent));

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(IconProperty, value); }
    }

    public static readonly BindableProperty TouchCommandProperty =
        BindableProperty.Create("TouchCommand", typeof(ICommand), typeof(SwitchComponent));

    public ICommand TouchCommand
    {
        get { return (ICommand)GetValue(TouchCommandProperty); }
        set { SetValue(TouchCommandProperty, value); }
    }



}