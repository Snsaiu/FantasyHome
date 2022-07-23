using DevExpress.Utils.About;
using FantasyHomeCenter.DevicePluginInterface;
using System.Linq;

using FantasyRoomDisplayDevice.Models;
using FantasyRoomDisplayDevice.Services;

namespace FantasyRoomDisplayDevice.MqttMessageParse
{
    public class ControlUIUpdateParser:MqttMessageParseBase
    {
        private readonly TempConfigService tempConfigService;

        public ControlUIUpdateParser(string flag, MqttSendInfo data,TempConfigService tempConfigService) : base(flag, data)
        {
            this.tempConfigService = tempConfigService;
        }

        protected override string GetParserFlag()
        {
            return "fantasyhome-ui-update";
        }

        protected override void Parse(MqttSendInfo data)
        {
            ControlUI device = this.tempConfigService.Components.FirstOrDefault(x => x.DeviceTypeKey == data.PluginKey && x.DeviceKey == data.DeviceName);
            if (device != null)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                  //  this.logger.Info($"开始更新设备组件{info.DeviceName}的数据");
                    device.UpdateState(data.Data);
                   // this.logger.Info($"更新设备组件{info.DeviceName}的数据已完成");
                });


            }
        }
    }
}