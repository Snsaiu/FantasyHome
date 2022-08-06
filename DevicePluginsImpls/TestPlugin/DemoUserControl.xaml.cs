using System.Collections.Generic;
using System.Windows;

using FantasyHomeCenter.DevicePluginInterface;

namespace TestPlugin
{
    public partial class DemoUserControl : ControlUI
    {
        // public DemoUserControl(DeviceMetaOutput deviceMetaOutput)
        // {
        //     InitializeComponent();
        // }


        public DemoUserControl(DeviceMetaOutput deviceMetaOutput) : base(deviceMetaOutput)
        {
            InitializeComponent();
            this.roomLabel.Content = deviceMetaOutput.Room.RoomName;
            this.timeLabel.Content = "0";
           
        }

        public override Dictionary<string, string> BuildInitRequest()
        {
            var dic = new Dictionary<string, string>();
            dic.Add("设置间隔",this.step.ToString());
            return dic;
        }

        private int step=2;
        
        
        public override void UpdateState(Dictionary<string, string> data)
        {

            this.timeLabel.Content = data["当前数值"];
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.step += 5;
            var dic = new Dictionary<string, string>();
            dic.Add("设置间隔",this.step.ToString());
            this.SendMessage(new MessageModel()
            {
                CommandType = CommandType.Set,Data = dic
                
            });
            
        }
    }

   
    
}