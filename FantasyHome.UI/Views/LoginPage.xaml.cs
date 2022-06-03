using FantasyHome.UI.ViewModels;
using Microsoft.Maui.Controls;

namespace FantasyHome.UI.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageModel loginPageModel)
	{
		InitializeComponent();
		this.BindingContext = loginPageModel;
	}
	
	
}