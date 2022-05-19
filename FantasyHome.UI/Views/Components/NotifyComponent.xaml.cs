using System.Windows.Input;

namespace FantasyHome.UI.Views.Components;

public partial class NotifyComponent : ContentView
{
	public NotifyComponent()
	{
		InitializeComponent();
	}

    #region Propertties
    

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create("Title", typeof(string), typeof(NotifyComponent), "",propertyChanged:TitleChanged);

    private static void TitleChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        var root= bindable as NotifyComponent;
        root.label.Text = (string)newvalue;
    }

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }


    public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create("BackgroundColor",
        typeof(Color), typeof(NotifyComponent),Colors.Orange,propertyChanged:BackgroundColorChanged);

    private static void BackgroundColorChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        var root = bindable as NotifyComponent;
        root.bg.BackgroundColor= (Color)newvalue;
    }

    public Color BackgroundColor
    {
        get { return (Color)GetValue(BackgroundProperty); }
        set { SetValue(BackgroundProperty, value); }
    }

    #endregion



    #region commands

    public static readonly BindableProperty ShowDetailCommandProperty =
        BindableProperty.Create("ShowDetailCommand", typeof(ICommand), typeof(NotifyComponent));

    public ICommand ShowDetailCommand
    {
        get { return (ICommand)GetValue(ShowDetailCommandProperty); }
        set   { SetValue(ShowDetailCommandProperty, value); }
    }




    #endregion
}