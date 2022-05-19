using FantasyHome.UI.ViewModels;

namespace FantasyHome.UI.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageModel mainPageModel)
	{
		InitializeComponent();
        this.BindingContext  = mainPageModel;
    }
}