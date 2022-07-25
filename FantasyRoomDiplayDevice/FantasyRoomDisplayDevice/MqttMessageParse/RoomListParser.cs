using System.Collections.Generic;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;

using DevExpress.Utils.About;
using DevExpress.Xpf.WindowsUI;

using FantasyRoomDisplayDevice.EventAggregates;
using FantasyRoomDisplayDevice.Models;
using FantasyRoomDisplayDevice.Services;

using Newtonsoft.Json;

using Prism.Events;

namespace FantasyRoomDisplayDevice.MqttMessageParse
{
    /// <summary>
    /// fantasyhome-room-list 消息处理
    /// </summary>
    public class RoomListParser: MqttMessageParseBase
    {
        private readonly ObservableCollection<HamburgerMenuNavigationButton> rooms;
        private readonly IEventAggregator eventAggregator;


        public RoomListParser(string flag, MqttSendInfo data, ObservableCollection<HamburgerMenuNavigationButton> rooms,IEventAggregator eventAggregator) : base(flag, data)
        {
            this.rooms = rooms;
            this.eventAggregator = eventAggregator;
        }

        protected override string GetParserFlag()
        {
            return "fantasyhome-room-list";
        }
        private void openComponentPage(string name)
        {

            this.eventAggregator.GetEvent<PageChageEventAggregater>().Publish(name);
        }


        protected override void Parse(MqttSendInfo data)
        {
            
            App.Current.Dispatcher.Invoke(() =>
            {
                this.rooms.Clear();

                this.rooms.Add(new HamburgerMenuNavigationButton()
                    {
                        Content = "主页",
                        IsSelected=true,
                        Command = new RelayCommand(() =>
                        {
                            this.openComponentPage("主页");

                        })
                    }
                );


                List<string> rooms = JsonConvert.DeserializeObject<List<string>>(data.Data["rooms"]);

                foreach (string room in rooms)
                {
                    this.rooms.Add(new HamburgerMenuNavigationButton()
                        {
                            Content = room,
                            Command = new RelayCommand(() =>
                            {
                                this.openComponentPage(room);

                            })
                        }
                    );
                }
                //将控件全部放到主页中

                this.eventAggregator.GetEvent<PageChageEventAggregater>().Publish("主页");


            });
            
 
        }
    }
}