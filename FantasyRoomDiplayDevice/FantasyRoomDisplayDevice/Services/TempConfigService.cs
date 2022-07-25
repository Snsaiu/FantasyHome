using System;
using System.Collections.Generic;
using System.IO;
using FantasyHome.Application.Dto;
using FantasyHomeCenter.DevicePluginInterface;
using MQTTnet.Packets;

namespace FantasyRoomDisplayDevice.Services
{
    public class TempConfigService
    {

        public TempConfigService()
        {
            this.Components = new List<ControlUI>();
            this.DeviceMqttTopicFilters = new List<MqttTopicFilter>();
        }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Token { get; set; }

        public string UserName { get; set; }
        public string Pwd { get; set; }

        public string MqttHost { get; set; }
        public string MqttPort { get; set; }

        public string MqttServiceTopic { get; set; }

        /// <summary>
        /// 设备的主题，每个插件的主题
        /// </summary>
        public List<MqttTopicFilter> DeviceMqttTopicFilters { get;  }

        /// <summary>
        /// 所有的主题，包括插件和系统自带
        /// </summary>
        public List<MqttTopicFilter> MqttTopicFilters { get; set; }


        public List<ControlUI> Components { get; }

        public List<DevicePluginMetaOutput> DevicePluginMetaOutputs { get; set; }
        public string PluginFolder { get=> Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");  }
    }
}