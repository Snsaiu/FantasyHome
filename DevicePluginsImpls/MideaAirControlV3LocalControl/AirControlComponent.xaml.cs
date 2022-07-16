using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FantasyHomeCenter.DevicePluginInterface;

namespace MideaAirControlV3LocalControl
{
    public partial class AirControlComponent : ControlUI
    {
      
        private AirControlModel airControlModel;
        public AirControlComponent( DeviceMetaOutput deviceMetaOutput):base(deviceMetaOutput)
        {
           
            InitializeComponent();

            this.roomNameLabel.Content = this.deviceMetaOutput.Room.RoomName;
     
            this.airControlModel = new AirControlModel();
            this.airControlModel.RunModes.Add(new RunMode(){Name = "自动",Icon = "\ue92f"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "仅吹风",Icon = "\ue8ab"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "制冷",Icon = "\ue6ef"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "制热",Icon = "\ue753"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "抽湿",Icon = "\ue62d"});
            this.runModelsList.ItemsSource = this.airControlModel.RunModes;
            
            var param = this.deviceMetaOutput.ConstCommandParams.Where(x => x.Type == CommandParameterTypeOutput.Set).ToList();

            this.windSpeedBtn.Click += (s, e) =>
            {
                bool? state = this.windSpeedBtn.IsChecked;
                Dictionary<string, string> data = new();
                data.Add("空调状态",state==true?"1":"0");
                data.Add("acip",param.First(x=>x.Name=="acip").Value);
                data.Add("acid",param.First(x=>x.Name=="acid").Value);
                data.Add("ack1",param.First(x=>x.Name=="ack1").Value);
                data.Add("actoken",param.First(x=>x.Name=="actoken").Value);
                MessageModel mm = new MessageModel();
                mm.CommandType = CommandType.Set;
                mm.Data = data;
                this.SendMessage(mm);
            };
        }



        public override Dictionary<string, string> BuildInitRequest()
        {
            var param = this.deviceMetaOutput.ConstCommandParams.Where(x => x.Type == CommandParameterTypeOutput.Get).ToList();
            
            var res= new Dictionary<string, string>();
            res.Add("acip",param.First(x=>x.Name=="acip").Value);
            res.Add("acid",param.First(x=>x.Name=="acid").Value);
            res.Add("ack1",param.First(x=>x.Name=="ack1").Value);
            res.Add("actoken",param.First(x=>x.Name=="actoken").Value);
            return res;
        }

        public override void UpdateState(Dictionary<string, string> data)
        {
            if (data.ContainsKey("空调状态"))
            {
                this.switchBtn.IsChecked = data["空调状态"] == "False" ? true : false;
            }
            
        }

        private void WindSpeedHandle(object sender, object e)
        {
            ShowWindSpeedDialog spd = new ShowWindSpeedDialog("10");
            spd.SelectedChangedEvent += (wind) =>
            {
                Task.Run(() =>
                {
                    Thread.Sleep(500);
                   
                }).GetAwaiter().OnCompleted(() =>
                {
                    spd.Close();
                });
               
            };
            spd.ShowDialog();
        }
    }
}