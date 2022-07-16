using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Xpf.Editors.Internal;
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
            this.airControlModel.RunModes.Add(new RunMode(){Name = "自动",Icon = "\ue92f",Key = "1"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "仅吹风",Icon = "\ue8ab",Key = "5"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "制冷",Icon = "\ue6ef",Key = "2"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "制热",Icon = "\ue753",Key = "4"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "干燥",Icon = "\ue62d",Key = "3"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "关闭",Icon = "\xe87a;",Key = "4"});
            this.runModelsList.ItemsSource = this.airControlModel.RunModes;




            this.tempreBar.EditValueChanged += (s, e) =>
            {
                change();
            };

        }


        private void change()
        {
            var param = this.deviceMetaOutput.ConstCommandParams.Where(x => x.Type == CommandParameterTypeOutput.Set).ToList();

            Dictionary<string, string> data = new();
            if (this.airControlModel.RunModes.First(x=>x.Name=="关闭").State)
            {
                data.Add("空调状态","0");
                data.Add("模式","1");
            }
            else
            {
                var mode = this.airControlModel.RunModes.Where(x => x.State == true).FirstOrDefault();
                data.Add("空调状态","1");
                data.Add("模式",mode.Key);
            }
          
            data.Add("acip", param.First(x => x.Name == "acip").Value);
            data.Add("acid", param.First(x => x.Name == "acid").Value);
            data.Add("ack1", param.First(x => x.Name == "ack1").Value);
            data.Add("actoken", param.First(x => x.Name == "actoken").Value);
            data.Add("提示音", "1");
            data.Add("温度", this.tempreBar.Value.ToString());

            MessageModel mm = new MessageModel();
            mm.CommandType = CommandType.Set;
            mm.Data = data;
            this.SendMessage(mm);
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
               var state=  data["空调状态"] == "False" ? false : true;
               if (state==false)
               {
                   foreach (var item in this.airControlModel.RunModes)
                   {
                       item.State = false;
                   }

                   this.airControlModel.RunModes.FirstOrDefault(x => x.Name == "关闭").State = true;
               }
               else
               {
                   if (data.ContainsKey("模式"))
                   {
                       string v = data["模式"];
                       foreach (var item in  this.airControlModel.RunModes)
                       {
                           item.State = false;
                       }

                       this.airControlModel.RunModes.FirstOrDefault(x => x.Name == v).State = true;
                   }
               }
            }

            if (data.ContainsKey("温度"))
            {
                this.tempreBar.Value = double.Parse(data["温度"]);
            }
          

            if (data.ContainsKey("扫风模式"))
            {
                this.swingBtn.Content = $"扫风模式:{data["扫风模式"]}";
            }

            if (data.ContainsKey("风速"))
            {
                this.windSpeedBtn.Content = $"风速:{data["风速"]}";
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

        private void ModeChangedHandle(object sender, RoutedEventArgs e)
        {
           change();
        }
    }
}