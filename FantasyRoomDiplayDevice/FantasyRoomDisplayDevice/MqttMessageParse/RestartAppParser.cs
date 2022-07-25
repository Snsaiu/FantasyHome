using System.Diagnostics;
using System.Windows;
using FantasyRoomDisplayDevice.Models;

namespace FantasyRoomDisplayDevice.MqttMessageParse
{
    public class RestartAppParser:MqttMessageParseBase
    {
        public RestartAppParser(string flag, MqttSendInfo data) : base(flag, data)
        {
        }

        protected override string GetParserFlag()
        {
            return "fantasyhome-restart";
        }

        protected override void Parse(MqttSendInfo data)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Process p = new Process();
                p.StartInfo.FileName = System.AppDomain.CurrentDomain.BaseDirectory + "FantasyRoomDisplayDevice.exe";
                p.StartInfo.UseShellExecute = false;
                p.Start();
                Application.Current.Shutdown();

            });
     
        }
    }
}