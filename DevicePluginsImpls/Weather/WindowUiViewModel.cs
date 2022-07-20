using CommunityToolkit.Mvvm.ComponentModel;

namespace Weather
{
    [ObservableObject]
    public partial class WindowUiViewModel
    {

        [ObservableProperty]
        private WeatherModel weatherModel;

        public WindowUiViewModel()
        {
            this.WeatherModel = new WeatherModel(); 
        }
    }
}