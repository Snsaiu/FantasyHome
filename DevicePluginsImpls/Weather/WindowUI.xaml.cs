using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

using CommunityToolkit.Mvvm.ComponentModel;

using FantasyHomeCenter.DevicePluginInterface;

namespace Weather
{

    [ObservableObject]
    public partial class WindowUI : ControlUI
    {
        [ObservableProperty]
        private WeatherModel weatherModel;
       
        public WindowUI(DeviceMetaOutput initData) : base(initData)
        {
            InitializeComponent();
           
            this.DataContext = this;
            this.WeatherModel = new WeatherModel();
        }

        public override Dictionary<string, string> BuildInitRequest()
        {
            var param = this.deviceMetaOutput.ConstCommandParams.Where(x => x.Type == CommandParameterTypeOutput.Get).ToList();

            var res = new Dictionary<string, string>();
            res.Add("当天天气url", param.First(x => x.Name == "当天天气url").Value);
            res.Add("3天天气url", param.First(x => x.Name == "3天天气url").Value);
            res.Add("经度", param.First(x => x.Name == "经度").Value);
            res.Add("纬度", param.First(x => x.Name == "纬度").Value);
            res.Add("Key", param.First(x => x.Name == "Key").Value);
            return res;
        }

        public override void UpdateState(Dictionary<string, string> data)
        {
            this.WeatherModel.State = data["今日天气"];
            this.WeatherModel.Temperature = data["今日温度"];
            this.WeatherModel.WindSpeed = data["风速"];
            this.WeatherModel.Icon = data["图标"];
           
            
            this.WeatherModel.FeatureWeatherModels.Add(new FeatureWeatherModel() {
                Icon = data["明天天气图标"],
                Date = DateTime.Now.AddDays(1).Month + "-" + DateTime.Now.AddDays(1).Day
            });

            this.WeatherModel.FeatureWeatherModels.Add(new FeatureWeatherModel()
            {
                Icon = data["后天天气图标"],
                Date = DateTime.Now.AddDays(2).Month + "-" + DateTime.Now.AddDays(2).Day
            });
        }
    }
}