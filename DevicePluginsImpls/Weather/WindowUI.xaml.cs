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

        private WindowUiViewModel windowUiViewModel;
       
        public WindowUI(DeviceMetaOutput initData) : base(initData)
        {
            InitializeComponent();
            windowUiViewModel= new WindowUiViewModel();
            this.DataContext = this.windowUiViewModel;
           
        }

        public override Dictionary<string, string> BuildInitRequest()
        {
            var param = this.deviceMetaOutput.ConstCommandParams.Where(x => x.Type == CommandParameterTypeOutput.Get).ToList();

            var res = new Dictionary<string,string>();
            res.Add("��������url", param.First(x => x.Name == "��������url").Value);
            res.Add("3������url", param.First(x => x.Name == "3������url").Value);
            res.Add("����", param.First(x => x.Name == "����").Value);
            res.Add("γ��", param.First(x => x.Name == "γ��").Value);
            res.Add("Key", param.First(x => x.Name == "Key").Value);
            return res;
        }

        public override void UpdateState(Dictionary<string, string> data)
        {
            this.windowUiViewModel.WeatherModel.FeatureWeatherModels.Clear();
            this.windowUiViewModel.WeatherModel.State = data["��������"];
            this.windowUiViewModel.WeatherModel.Temperature ="�¶�:"+ data["�����¶�"]+"��";
            this.windowUiViewModel.WeatherModel.WindSpeed = "����:"+data["����"]+"��";
            this.windowUiViewModel.WeatherModel.Icon ="./Icons/" +data["ͼ��"]+".svg";
           
            
            this.windowUiViewModel.WeatherModel.FeatureWeatherModels.Add(new FeatureWeatherModel() {
                Icon = "./Icons/" + data["��������ͼ��"] + ".svg",
                Date = DateTime.Now.AddDays(1).Month + "-" + DateTime.Now.AddDays(1).Day
            });

            this.windowUiViewModel.WeatherModel.FeatureWeatherModels.Add(new FeatureWeatherModel()
            {
                Icon = "./Icons/" + data["��������ͼ��"] + ".svg",
            Date = DateTime.Now.AddDays(2).Month + "-" + DateTime.Now.AddDays(2).Day
            });
        }
    }
}