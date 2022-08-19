using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

using DevExpress.Xpf.WindowsUI;

using FantasyRoomDisplayDevice.EventAggregates;
using FantasyRoomDisplayDevice.Models;

using Prism.Events;

namespace FantasyRoomDisplayDevice.MqttMessageParse
{
    public class RoomAddParser:MqttMessageParseBase
    {
        private readonly ObservableCollection<HamburgerMenuNavigationButton> rooms;
        private readonly IEventAggregator eventAggregator;

        public RoomAddParser(string flag, MqttSendInfo data,
            ObservableCollection<HamburgerMenuNavigationButton> rooms,IEventAggregator eventAggregator) : base(flag, data)
        {
            this.rooms = rooms;
            this.eventAggregator = eventAggregator;
        }

        protected override string GetParserFlag()
        {
            return "fantasyhome-room-add";
        }

        protected override void Parse(MqttSendInfo data)
        {
            string roomName = data.Data["RoomName"];
            App.Current.Dispatcher.Invoke(() =>
            {

                this.rooms.Add(new HamburgerMenuNavigationButton()
                {
                    Content = roomName,
                    Command = new RelayCommand(() =>
                    {
                        this.eventAggregator.GetEvent<PageChageEventAggregater>().Publish(roomName);

                    })
                });

            });
    
        }
    }
}