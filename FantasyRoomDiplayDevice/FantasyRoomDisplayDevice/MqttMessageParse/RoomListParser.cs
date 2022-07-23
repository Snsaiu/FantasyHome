using System.Collections.Generic;
using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;

using DevExpress.Utils.About;
using DevExpress.Xpf.WindowsUI;
using FantasyRoomDisplayDevice.Models;

using Newtonsoft.Json;

namespace FantasyRoomDisplayDevice.MqttMessageParse
{
    /// <summary>
    /// fantasyhome-room-list 消息处理
    /// </summary>
    public class RoomListParser: MqttMessageParseBase
    {
        private readonly ObservableCollection<HamburgerMenuNavigationButton> rooms;

        public RoomListParser(string flag, MqttSendInfo data, ObservableCollection<HamburgerMenuNavigationButton> rooms) : base(flag, data)
        {
            this.rooms = rooms;
        }

        protected override string GetParserFlag()
        {
            return "fantasyhome-room-list";
        }
        private void openComponentPage(string name)
        {

            switch (name)
            {
                case "主页":
                    //  this.regionManager.RequestNavigate("ItemRegion",nameof(HomeComponentViewModel));
                    break;
                default:
                    break;
            }
        }


        protected override void Parse(MqttSendInfo data)
        {
            
            App.Current.Dispatcher.Invoke(() =>
            {
                this.rooms.Clear();

                this.rooms.Add(new HamburgerMenuNavigationButton()
                    {
                        Content = "主页",
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



            });
            
 
        }
    }
}