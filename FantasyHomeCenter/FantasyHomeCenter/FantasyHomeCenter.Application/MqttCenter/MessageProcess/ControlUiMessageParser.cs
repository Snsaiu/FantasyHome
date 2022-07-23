using System.Collections.Generic;
using System.Text;

using FantasyHomeCenter.Application.DeviceCenter;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using MQTTnet;
using MQTTnet.Server;

using Newtonsoft.Json;

namespace FantasyHomeCenter.Application.MqttCenter.MessageProcess;

public class ControlUiMessageParser:MessageParserBase
{
    private readonly IDeviceService deviceService;
    private readonly IMqttServer mqttServerInstance;

    public ControlUiMessageParser(string flag, MqttSendInfo data,IDeviceService deviceService, IMqttServer mqttServerInstance) : base(flag, data)
    {
        this.deviceService = deviceService;
        this.mqttServerInstance = mqttServerInstance;
    }

    protected override string GetParserFlag()
    {
        return "fantasyhome-ui-receive";
    }

    protected override async void Parse(MqttSendInfo data)
    {
        if (string.IsNullOrEmpty(data.Topic) || string.IsNullOrEmpty(data.PluginKey) ||
            string.IsNullOrEmpty(data.DeviceName))
        {
            return;
        }
        RESTfulResult<Dictionary<string, string>> res = new RESTfulResult<Dictionary<string, string>>();
        if (data.CommandType == CommandType.Get)
        {
            res = this.deviceService.GetDeviceState(data);
        }
        else
        {
            res = this.deviceService.SetThenGetDeviceState(data);
        }

        MqttSendInfo sendinfo = new MqttSendInfo();
        if (res.Succeeded)
        {
            sendinfo.Success = true;
            sendinfo.Data = res.Data;
            sendinfo.DeviceName = data.DeviceName;
            sendinfo.PluginKey = data.PluginKey;
            sendinfo.Topic = data.Topic;
        }
        else
        {
            sendinfo.Success = false;
            sendinfo.ErrorMsg = res.Errors.ToString();
        }

        string sendcontent = JsonConvert.SerializeObject(sendinfo);
        //发送
       await this.mqttServerInstance.PublishAsync(new MqttApplicationMessage()
        {
            Topic = "fantasyhome-ui-update",
            Payload = Encoding.UTF8.GetBytes(sendcontent)
        });

    }
}