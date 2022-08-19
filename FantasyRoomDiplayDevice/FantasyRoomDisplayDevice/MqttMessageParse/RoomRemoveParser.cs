using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.Input;

using DevExpress.Xpf.WindowsUI;
using FantasyRoomDisplayDevice.EventAggregates;
using FantasyRoomDisplayDevice.Models;

using Prism.Events;

namespace FantasyRoomDisplayDevice.MqttMessageParse
{
    public class RoomRemoveParser: MqttMessageParseBase
    {
        private readonly ObservableCollection<HamburgerMenuNavigationButton> rooms;
        private readonly IEventAggregator eventAggregator;

        public RoomRemoveParser(string flag, MqttSendInfo data, ObservableCollection<HamburgerMenuNavigationButton> rooms, IEventAggregator eventAggregator) : base(flag, data)
        {
            this.rooms = rooms;
            this.eventAggregator = eventAggregator;
        }

        protected override string GetParserFlag()
        {
            return "fantasyhome-room-remove";
        }

        protected override void Parse(MqttSendInfo data)
        {
            string roomName = data.Data["RoomName"];
            App.Current.Dispatcher.Invoke(() =>
            {

               var room=this.rooms.FirstOrDefault(x=>x.Content.ToString()==roomName);
               if (room!=null)
               {
                   this.rooms.Remove(room);
               }

            });


        }
    }
}