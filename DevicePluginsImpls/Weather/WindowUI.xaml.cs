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
            res.Add("��������url", param.First(x => x.Name == "��������url").Value);
            res.Add("3������url", param.First(x => x.Name == "3������url").Value);
            res.Add("����", param.First(x => x.Name == "����").Value);
            res.Add("γ��", param.First(x => x.Name == "γ��").Value);
            res.Add("Key", param.First(x => x.Name == "Key").Value);
            return res;
        }

        public override void UpdateState(Dictionary<string, string> data)
        {
            this.WeatherModel.State = data["��������"];
            this.WeatherModel.Temperature = data["�����¶�"];
            this.WeatherModel.WindSpeed = data["����"];
            this.WeatherModel.Icon = data["ͼ��"];
           
            
            this.WeatherModel.FeatureWeatherModels.Add(new FeatureWeatherModel() {
                Icon = data["��������ͼ��"],
                Date = DateTime.Now.AddDays(1).Month + "-" + DateTime.Now.AddDays(1).Day
            });

            this.WeatherModel.FeatureWeatherModels.Add(new FeatureWeatherModel()
            {
                Icon = data["��������ͼ��"],
                Date = DateTime.Now.AddDays(2).Month + "-" + DateTime.Now.AddDays(2).Day
            });
        }
    }
}