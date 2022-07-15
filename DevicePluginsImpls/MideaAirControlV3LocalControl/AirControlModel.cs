using System.Collections.Generic;

namespace MideaAirControlV3LocalControl
{
    public class AirControlModel
    {
        public AirControlModel()
        {
            this.RunModes = new();
            
        }

        public List<RunMode> RunModes { get; set; }
        
    }

    /// <summary>
    /// 运行模式
    /// </summary>
    public class RunMode
    {
        public RunMode()
        {
            this.State = false;
            this.IconPath = "pack://application:,,,/Resources/Fonts/#aircontrol";
        }
        public string Name { get; set; }
        public bool State { get; set; }
        public string Icon { get; set; }
        public string IconPath { get; set; }
    }
}