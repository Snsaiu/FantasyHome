using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace FantasyHome.UI.ViewModels;

  public partial class MainPageModel :ObservableObject
{
	public  MainPageModel()
	{

    }

   [ICommand]
	private void  showWarngingLabel()
    {

       App.Current.MainPage.DisplayAlert("hello", "info", "cancel");
    }


}