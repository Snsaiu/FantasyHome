using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FantasyHome.UI.Models;
using FantasyHome.UI.Utils;
using FantasyResultModel;

namespace FantasyHome.UI.ViewModels;

  public partial class MainPageModel :ObservableObject
{


    [ObservableProperty]
    private ObservableCollection<NotifyBarModel> notifyBarModels;

    [ObservableProperty]
    private ObservableCollection<WeekWeatherListModel> weekWeatherListModels;

    [ObservableProperty]
    private bool keTingLightState=false;

    [ObservableProperty]
    private CurrentWeatherModel currentWeather = new CurrentWeatherModel();
    private readonly IConnectivity connectivity;
    private readonly Tools tools;

    public MainPageModel(IConnectivity connectivity,Tools tools)
	{
        this.connectivity = connectivity;
        this.tools = tools;
        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
             Shell.Current.DisplayAlert("No connectivity!",
                $"Please check internet and try again.", "OK");
            return;
        }

        this.KeTingLightState = true;
        this.notifyBarModels = new ObservableCollection<NotifyBarModel>();
        this.getNotifyBarInfoList();
       
        this.getCurrentWeather();
        this.getMulitWeather();

    }
    [ICommand]
    private async Task RefreshWeather()
    {
        await this.getCurrentWeather();
        await this.getMulitWeather();
    }

    private async Task getMulitWeather()
    {
        this.WeekWeatherListModels = new ObservableCollection<WeekWeatherListModel>();

        var currentLocationRes =await this.tools.GetCurrentLocationAsync();
        if (currentLocationRes.Ok)
        {
            string url =
                $"{Tools.MulitWeatherApi}location={currentLocationRes.Data.Longitude},{currentLocationRes.Data.Latitude}&key={Tools.WeatherKey}";
            ResultBase<MulitWeatherModel> currentWeatherRes = HttpRequest<MulitWeatherModel>.GET(url);
            if (currentWeatherRes.Ok)
            {
                if (currentWeatherRes.Data.daily!=null)
                {
                    currentWeatherRes.Data.daily.RemoveAt(0);
                    
                    foreach (var item in currentWeatherRes.Data.daily)
                    {
                        var d= Convert.ToDateTime(item.fxDate);
                        string date = d.Month.ToString() + "-" + d.Day.ToString();
                        this.WeekWeatherListModels.Add(new WeekWeatherListModel(item.iconDay, date));
                    }
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("警告", currentWeatherRes.ErrorMsg, "确定");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("警告", currentLocationRes.ErrorMsg, "确定");
        }
    }

    private async Task getCurrentWeather()
    {
        var currentLocationRes =await this.tools.GetCurrentLocationAsync();
        if (currentLocationRes.Ok)
        {
            string url =
                $"{Tools.CurrentWeatherApi}location={currentLocationRes.Data.Longitude},{currentLocationRes.Data.Latitude}&key={Tools.WeatherKey}";
            ResultBase<CurrentWeatherModel> currentWeatherRes = HttpRequest<CurrentWeatherModel>.GET(url);
            if (currentWeatherRes.Ok)
            {
                this.CurrentWeather = currentWeatherRes.Data;
            }
            else
            {
                await Shell.Current.DisplayAlert("警告", currentWeatherRes.ErrorMsg, "确定");
            }
        }
        else
        {
           await Shell.Current.DisplayAlert("警告", currentLocationRes.ErrorMsg, "确定");
        }
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
        this.notifyBarModels.Add(new NotifyBarModel { Id = "3", Title = "电风扇打24242开2" });
    }

    
   [ICommand]
	private void  ShowWarngingLabel(object param)
    {

       App.Current.MainPage.DisplayAlert("详情", param.ToString(), "cancel");
    }

    [ICommand]
    private void ReplaceKeTingLight()
    {
        this.KeTingLightState = !this.KeTingLightState;
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