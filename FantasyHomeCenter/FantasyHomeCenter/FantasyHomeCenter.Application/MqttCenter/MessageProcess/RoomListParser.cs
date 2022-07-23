using System.Collections.Generic;
using System.Linq;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.Application.RoomCenter;
using MQTTnet.Server;

using StackExchange.Profiling.Internal;

namespace FantasyHomeCenter.Application.MqttCenter.MessageProcess;

public class RoomListParser:MessageParserBase
{
    private readonly IRoomService roomService;
    private readonly MqttServerInstance mqttServer;

    public RoomListParser(string flag, MqttSendInfo data,IRoomService roomService, MqttServerInstance mqttServer) : base(flag, data)
    {
        this.roomService = roomService;
        this.mqttServer = mqttServer;
    }

    protected override string GetParserFlag()
    {
        return "room-list-receive";
    }

    protected override async void Parse(MqttSendInfo data)
    {
        var roomListRes = await roomService.GetListAsync();
        if (roomListRes.Succeeded)
        {
            List<string> rooms = roomListRes.Data.Select(x => x.RoomName).ToList();
            Dictionary<string, string> datas = new Dictionary<string, string>();
            datas["rooms"] = rooms.ToJson();

            await mqttServer.PublishAsync("fantasyhome-room-list", new MqttSendInfo
            {
                Topic = "fantasyhome-room-list",
                Data = datas
            });

        }
    }
}