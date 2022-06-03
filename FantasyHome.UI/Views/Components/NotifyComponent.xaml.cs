using System.Windows.Input;
using Microsoft.Maui.Controls;

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


    public static readonly BindableProperty BarColorProperty = BindableProperty.Create("BarColor",
        typeof(Color), typeof(NotifyComponent),Colors.Orange,propertyChanged: BarColorChanged);

    private static void BarColorChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        var root = bindable as NotifyComponent;
        root.bg.BackgroundColor = (Color)newvalue;
    }

    public Color BarColor
    {
        get { return (Color)GetValue(BarColorProperty); }
        set { SetValue(BarColorProperty, value); }
    }

    #endregion



    #region commands

    /// <summary>
    ///点击小眼镜命令
    /// 
    /// </summary>
    public static readonly BindableProperty ShowDetailCommandProperty =
        BindableProperty.Create("ShowDetailCommand", typeof(ICommand), typeof(NotifyComponent));

    public ICommand ShowDetailCommand
    {
        get { return (ICommand)GetValue(ShowDetailCommandProperty); }
        set   { SetValue(ShowDetailCommandProperty, value); }
    }

    public static readonly BindableProperty ShowDetailCommandParameterProperty =
        BindableProperty.Create("ShowDetailCommandParameter", typeof(object), typeof(NotifyComponent));

    public object ShowDetailCommandParameter
    {
        get { return (object)GetValue(ShowDetailCommandParameterProperty); }
        set { SetValue(ShowDetailCommandParameterProperty, value); }
    }
    /// <summary>
    /// 点击关闭按钮事件
    /// </summary>
    public static readonly BindableProperty CloseCommandProperty =
         BindableProperty.Create("CloseCommand", typeof(ICommand), typeof(NotifyComponent));

    public ICommand CloseCommand
    {
        get { return (ICommand)GetValue(CloseCommandProperty); }
        set { SetValue(CloseCommandProperty, value); }
    }


    public static readonly BindableProperty CloseCommandParameterProperty =
        BindableProperty.Create("CloseCommandParameter", typeof(object), typeof(NotifyComponent));

    public object CloseCommandParameter
    {
        get { return (object)GetValue(CloseCommandParameterProperty); }
        set { SetValue(CloseCommandParameterProperty, value); }
    }



    #endregion
}