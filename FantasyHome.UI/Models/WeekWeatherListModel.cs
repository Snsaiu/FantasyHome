using CommunityToolkit.Mvvm.ComponentModel;

namespace FantasyHome.UI.Models;

public partial class WeekWeatherListModel : ObservableObject
{
    public WeekWeatherListModel(string icon, string date)
    {
        this.icon = icon;
        this.date = date;
    }

    public WeekWeatherListModel()
    {

    }

    [ObservableProperty] private string icon;
    [ObservableProperty] private string date;
}
