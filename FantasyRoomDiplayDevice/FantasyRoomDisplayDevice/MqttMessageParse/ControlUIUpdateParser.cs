using DevExpress.Utils.About;
using FantasyHomeCenter.DevicePluginInterface;
using System.Linq;

using FantasyRoomDisplayDevice.Models;
using FantasyRoomDisplayDevice.Services;
using System.Collections.Generic;

namespace FantasyRoomDisplayDevice.MqttMessageParse
{
    public class ControlUIUpdateParser:MqttMessageParseBase
    {
        private readonly TempConfigService tempConfigService;
        

        public ControlUIUpdateParser(string flag, MqttSendInfo data,TempConfigService tempConfigService) : base(flag, data)
        {
            this.DeviceUiUpdate = true;
            this.tempConfigService = tempConfigService;
        }

        protected override string GetParserFlag()
        {
            return "";
        }

        protected override List<string> GetParseFlags()
        {
          return  this.tempConfigService.DeviceMqttTopicFilters.Select(x => x.Topic).ToList();
        }

        protected override void Parse(MqttSendInfo data)
        {
            ControlUI device = this.tempConfigService.Components.FirstOrDefault(x => x.DeviceTypeKey == data.PluginKey && x.DeviceKey == data.DeviceName);
            if (device != null)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    //this.logger.Info($"开始更新设备组件{info.DeviceName}的数据");
                    device.UpdateState(data: data.Data);
                   // this.logger.Info($"更新设备组件{info.DeviceName}的数据已完成");
                });


            }
        }
    }
}