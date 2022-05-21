using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FantasyHome.UI.Views.Components;

public partial class SwitchComponent : ContentView
{
    
    
    
	public SwitchComponent()
	{
		InitializeComponent();

        this.Loaded += (s, e) =>
        {
            this.icon.HorizontalOptions = LayoutOptions.Center;
           
            if (this.State)
            {

                this.bar.BackgroundColor = this.OnBarColor;
                
                this.icon.Source = this.OnIcon;
            }
            else
            {
                this.bar.BackgroundColor = this.OffBarColor;
                this.icon.Source = this.OffIcon;
            }
        };


    }

    public static readonly BindableProperty StateProperty = BindableProperty.Create("State", typeof(bool),
        typeof(WeatherComponent), defaultValue: false,propertyChanged:StateChanged);

    private static void StateChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        bool state = (bool)newvalue;
        SwitchComponent wc = bindable as SwitchComponent;
        if (state)
        {
            wc.bar.BackgroundColor = wc.OnBarColor;
            wc.icon.Source = wc.OnIcon;
        }
        else
        {
            wc.bar.BackgroundColor = wc.OffBarColor;
            wc.icon.Source = wc.OffIcon;
        }

    }

    public bool State
    {
        get { return (bool)GetValue(StateProperty); }
        set{ SetValue(StateProperty,value);}
    }

    public static readonly BindableProperty OnBarColorProperty = BindableProperty.Create("OnBarColor",
        typeof(Color),
        typeof(SwitchComponent),
        Colors.Green
    );

    public Color OnBarColor
    {
        get { return (Color)GetValue(OnBarColorProperty); }
        set{SetValue(OnBarColorProperty,value);}
    }

    public static readonly BindableProperty OffBarColorProperty = BindableProperty.Create("OffBarColor",
        typeof(Color),
        typeof(SwitchComponent),
        Colors.Grey
    );

    public Color OffBarColor
    {
        get { return (Color)GetValue(OffBarColorProperty); }
        set{SetValue(OffBarColorProperty,value);}
    }
    
    
    // public static readonly BindableProperty BarColorProperty = BindableProperty.Create("BarColor",
    //     typeof(Color), typeof(WeatherComponent));
    //
    // public Color BarColor
    // {
    //     get { return (Color)GetValue(BarColorProperty); }
    //     set { SetValue(BarColorProperty, value); }
    // }

    public static readonly BindableProperty OnIconProperty =
        BindableProperty.Create("OnIcon", typeof(ImageSource), typeof(SwitchComponent));

    public ImageSource OnIcon
    {
        get { return (ImageSource)GetValue(OnIconProperty); }
        set { SetValue(OnIconProperty, value); }
    }

    public static readonly BindableProperty OffIconProperty =
        BindableProperty.Create("OffIcon", typeof(ImageSource), typeof(SwitchComponent));

    public ImageSource OffIcon
    {
        get { return (ImageSource)GetValue(OffIconProperty); }
        set { SetValue(OffIconProperty, value); }
    }

    
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create("Title", typeof(string), typeof(SwitchComponent),"");

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public static readonly BindableProperty TouchCommandProperty =
        BindableProperty.Create("TouchCommand", typeof(ICommand), typeof(SwitchComponent));

    public ICommand TouchCommand
    {
        get { return (ICommand)GetValue(TouchCommandProperty); }
        set { SetValue(TouchCommandProperty, value); }
    }



}