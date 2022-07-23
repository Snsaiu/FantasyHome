using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Xpf.WindowsUI;
using FantasyRoomDisplayDevice.Models;

namespace FantasyRoomDisplayDevice.MqttMessageParse
{
    public class RoomAddParser:MqttMessageParseBase
    {
        public RoomAddParser(string flag, MqttSendInfo data,
            ObservableCollection<HamburgerMenuNavigationButton> hamburgerMenuNavigationButtons) : base(flag, data)
        {
        }

        protected override string GetParserFlag()
        {
            return "fantasyhome-room-add";
        }

        protected override void Parse(MqttSendInfo data)
        {
            throw new System.NotImplementedException();
        }
    }
}