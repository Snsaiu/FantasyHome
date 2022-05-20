using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FantasyHome.UI.Models;

namespace FantasyHome.UI.ViewModels;

  public partial class MainPageModel :ObservableObject
{


    [ObservableProperty]
    private ObservableCollection<NotifyBarModel> notifyBarModels;

    [ObservableProperty]
    private ObservableCollection<WeekWeatherListModel> weekWeatherListModels;

	public  MainPageModel()
	{

        this.notifyBarModels = new ObservableCollection<NotifyBarModel>();
        this.getNotifyBarInfoList();
        this.WeekWeatherListModels = new ObservableCollection<WeekWeatherListModel>();
        this.getWeatherList();
    }

    private void getWeatherList()
    {
        this.WeekWeatherListModels.Add(new WeekWeatherListModel("one","6-2") );
        this.WeekWeatherListModels.Add(new WeekWeatherListModel("one","6-3") );
        this.WeekWeatherListModels.Add(new WeekWeatherListModel("one","6-4") );
        this.WeekWeatherListModels.Add(new WeekWeatherListModel("one","6-5") );
        this.WeekWeatherListModels.Add(new WeekWeatherListModel("one","6-6") );
        this.WeekWeatherListModels.Add(new WeekWeatherListModel("one","6-7") );
    }

    private void getNotifyBarInfoList()
    {
        this.notifyBarModels.Add(new NotifyBarModel { Id = "1", Title = "电风扇打开" });
        this.notifyBarModels.Add(new NotifyBarModel { Id = "2", Title = "电风扇打开1" });
        this.notifyBarModels.Add(new NotifyBarModel { Id = "3", Title = "电风扇打开2" });
    }

    
   [ICommand]
	private void  ShowWarngingLabel(object param)
    {

       App.Current.MainPage.DisplayAlert("详情", param.ToString(), "cancel");
    }

    [ICommand]
    private void CloseWarningLabel(object param)
    {
        string id = param.ToString();
        var item = this.NotifyBarModels.FirstOrDefault(x => x.Id == id);
        if (item != null)
        {
            this.NotifyBarModels.Remove(item);
        }
        else
        {
            throw new InvalidOperationException();
        }
    }




}