using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;

namespace Weather
{
    [ObservableObject]
    public partial class WeatherModel
    {

        public WeatherModel()
        {
            this.FeatureWeatherModels = new ObservableCollection<FeatureWeatherModel>();

        }

        /// <summary>
        /// 天气状态
        /// </summary>
        [ObservableProperty]
        private string state;

        [ObservableProperty]
        private string icon;

        /// <summary>
        /// 风俗
        /// </summary>
        [ObservableProperty]
        private string windSpeed;

        /// <summary>
        /// 温度
        /// </summary>
        [ObservableProperty]
        private string temperature;

        [ObservableProperty]
        private ObservableCollection<FeatureWeatherModel> featureWeatherModels;
    }

    [ObservableObject]
    public partial class FeatureWeatherModel
    {
        [ObservableProperty]
        private string icon;
        [ObservableProperty]
        private string date;
       
    }

}
