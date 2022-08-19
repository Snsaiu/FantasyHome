using System.Collections.Generic;

namespace FantasyHomeCenter.DevicePluginInterface
{
    public class PublishData
    {

        public PublishData()
        {
            this.BeforeData = new Dictionary<string, string>();
            this.AfterData = new Dictionary<string, string>();
        }

        /// <summary>
        /// 插件key
        /// </summary>
        public string DevicePluginKey { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string  DeviceName { get; set; }


        /// <summary>
        /// 变换前的状态
        /// </summary>
        public Dictionary<string,string> BeforeData { get; set; }

        /// <summary>
        /// 变换后的状态
        /// </summary>
        public Dictionary<string, string> AfterData { get; set; }

    }
}