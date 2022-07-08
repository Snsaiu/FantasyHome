using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FantasyRoomDisplayDevice.Models;

namespace FantasyRoomDisplayDevice.Views.Components
{
    public partial class AirControlComponent : UserControl
    {
        private AirControlModel airControlModel;
        public AirControlComponent()
        {
            
            InitializeComponent();
            this.airControlModel = new AirControlModel();
            this.airControlModel.RunModes.Add(new RunMode(){Name = "自动",Icon = "\ue92f"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "仅吹风",Icon = "\ue8ab"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "制冷",Icon = "\ue6ef"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "制热",Icon = "\ue753"});
            this.airControlModel.RunModes.Add(new RunMode(){Name = "抽湿",Icon = "\ue62d"});
            this.runModelsList.ItemsSource = this.airControlModel.RunModes;
        }

        private void WindSpeedHandle(object sender, TouchEventArgs e)
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