using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FantasyHome.UI.Models;

namespace FantasyHome.UI.ViewModels;

  public partial class MainPageModel :ObservableObject
{


    [ObservableProperty]
    private ObservableCollection<NotifyBarModel> notifyBarModels;

	public  MainPageModel()
	{

        this.notifyBarModels = new ObservableCollection<NotifyBarModel>();
        this.getNotifyBarInfoList();
    }


    private void getNotifyBarInfoList()
    {
        this.notifyBarModels.Add(new NotifyBarModel { Id = "1", Title = "电风扇打开" });
        this.notifyBarModels.Add(new NotifyBarModel { Id = "2", Title = "电风扇打开1" });
        this.notifyBarModels.Add(new NotifyBarModel { Id = "3", Title = "电风扇打开2" });
    }

    
   [ICommand]
	private void  ShowWarngingLabel()
    {

       App.Current.MainPage.DisplayAlert("hello", "info", "cancel");
    }


}