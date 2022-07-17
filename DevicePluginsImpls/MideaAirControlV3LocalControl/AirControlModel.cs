using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MideaAirControlV3LocalControl
{
    [ObservableObject]
    public partial class AirControlModel
    {
        public AirControlModel()
        {
            this.RunModes = new ();
            
        }

        [ObservableProperty] public ObservableCollection<RunMode> runModes;

    }

    /// <summary>
    /// 运行模式
    /// </summary>
    [ObservableObject]
    public partial class RunMode
    {
        public RunMode()
        {
            this.State = false;
            this.IconPath = "pack://application:,,,/Resources/Fonts/#aircontrol";
        }
        public string Name { get; set; }
        [ObservableProperty] 
        private bool state;
        public string Icon { get; set; }
        public string IconPath { get; set; }

        public string GrpName { get; set; }

        public string Key { get; set; }
    }
}