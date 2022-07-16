using System;
using System.Collections.Generic;
using System.IO;
using FantasyHome.Application.Dto;
using FantasyHomeCenter.DevicePluginInterface;

namespace FantasyRoomDisplayDevice.Services
{
    public class TempConfigService
    {

        public TempConfigService()
        {
            this.Components = new List<ControlUI>();
        }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Token { get; set; }

        public string UserName { get; set; }
        public string Pwd { get; set; }

        public string MqttHost { get; set; }
        public string MqttPort { get; set; }

        public string MqttServiceTopic { get; set; }


        public List<ControlUI> Components { get; }

        public List<DevicePluginMetaOutput> DevicePluginMetaOutputs { get; set; }
        public string PluginFolder { get=> Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");  }
    }
}