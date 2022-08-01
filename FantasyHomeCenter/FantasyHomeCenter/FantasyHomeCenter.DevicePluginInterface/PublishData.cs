using System.Collections.Generic;

namespace FantasyHomeCenter.DevicePluginInterface
{
    public class PublishData
    {
        /// <summary>
        /// 插件key
        /// </summary>
        public string DevicePluginKey { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string  DeviceName { get; set; }
        
        /// <summary>
        /// 数据
        /// </summary>
        public Dictionary<string, string> Data { get; set; }

    }
}