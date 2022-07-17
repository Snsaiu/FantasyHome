using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using DevExpress.Xpf.Editors.Internal;
using FantasyHomeCenter.DevicePluginInterface;

namespace MideaAirControlV3LocalControl
{
    [ObservableObject]
    public partial class AirControlComponent : ControlUI
    {


        [ObservableProperty]
        private AirControlModel airControlModel;
        public AirControlComponent( DeviceMetaOutput deviceMetaOutput):base(deviceMetaOutput)
        {
           
            InitializeComponent();

            this.roomNameLabel.Content = this.deviceMetaOutput.Room.RoomName;



            this.AirControlModel = new AirControlModel();
            this.AirControlModel.RunModes.Add(new RunMode(){Name = "自动",Icon = "\ue92f",Key = "1",GrpName= this.deviceMetaOutput.Name });
            this.AirControlModel.RunModes.Add(new RunMode(){Name = "仅吹风",Icon = "\ue8ab",Key = "5", GrpName = this.deviceMetaOutput.Name });
            this.AirControlModel.RunModes.Add(new RunMode(){Name = "制冷",Icon = "\ue6ef",Key = "2", GrpName = this.deviceMetaOutput.Name });
            this.AirControlModel.RunModes.Add(new RunMode(){Name = "制热",Icon = "\ue753",Key = "4", GrpName = this.deviceMetaOutput.Name });
            this.AirControlModel.RunModes.Add(new RunMode(){Name = "干燥",Icon = "\ue62d",Key = "3", GrpName = this.deviceMetaOutput.Name });
            this.AirControlModel.RunModes.Add(new RunMode(){Name = "关闭",Icon = "\xe87a",Key = "0", GrpName = this.deviceMetaOutput.Name });
            this.runModelsList.ItemsSource = this.AirControlModel.RunModes;



            this.openTemplateBtn.Click += (s, e) =>
            {
                ShowWindSpeedDialog spd = new ShowWindSpeedDialog("10");
                spd.SelectedChangedEvent += (wind) =>
                {
                    this.currentwind = wind;
                    this.openTemplateBtn.Content = "温度:" + this.currentwind + "℃";
                    Task.Run(() =>
                    {
                    
                      Thread.Sleep(500);

                    }).GetAwaiter().OnCompleted(() =>
                    {
                        spd.Close();
                        if (  this.AirControlModel.RunModes.First(x=>x.Name=="关闭").State==true)
                        {
                            foreach (var item in this.AirControlModel.RunModes)
                            {
                                item.State = false;
                            }

                            this.AirControlModel.RunModes.First(x => x.Name == "自动").State = true;

                        }
                        change();
                    });
               
                };
                spd.ShowDialog();


            };

         

        }


        private void change()
        {
            var param = this.deviceMetaOutput.ConstCommandParams.Where(x => x.Type == CommandParameterTypeOutput.Set).ToList();
        
            Dictionary<string, string> data = new();
            if (this.AirControlModel.RunModes.First(x=>x.Name=="关闭").State)
            {
                data.Add("空调状态","0");
                data.Add("模式","1");
            }
            else
            {
                var mode = this.AirControlModel.RunModes.Where(x => x.State == true).FirstOrDefault();
                data.Add("空调状态","1");
                data.Add("模式",mode.Key);
            }
          
            data.Add("acip", param.First(x => x.Name == "acip").Value);
            data.Add("acid", param.First(x => x.Name == "acid").Value);
            data.Add("ack1", param.First(x => x.Name == "ack1").Value);
            data.Add("actoken", param.First(x => x.Name == "actoken").Value);
            data.Add("提示音", "1");
            data.Add("温度", this.currentwind);

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

                       var x = this.airControlModel.RunModes.FirstOrDefault(x => x.Name == v);
                       x.State = true;
                     
                   }
               }
            }

            if (data.ContainsKey("温度"))
            {
              
                this.openTemplateBtn.Content = "温度:" + data["温度"] + "℃";
                this.currentwind = data["温度"];
            }

 

            if (data.ContainsKey("扫风模式"))
            {
                this.swingBtn.Content = $"扫风模式:{data["扫风模式"]}";
            }

            if (data.ContainsKey("风速"))
            {
                this.windSpeedBtn.Content = $"风速:{data["风速"]}";
            }

            if (data.ContainsKey("室内温度"))
            {
                this.roomNameLabel.Content = this.deviceMetaOutput.Room.RoomName + " : " + data["室内温度"];
            }
          
        }

        private string currentwind = "";

    
        private void ModeChangedHandle(object sender, RoutedEventArgs e)
        {
           change();
        }
    }
}