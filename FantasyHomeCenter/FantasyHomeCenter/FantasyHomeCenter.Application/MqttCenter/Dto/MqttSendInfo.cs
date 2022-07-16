using System.Collections.Generic;

namespace FantasyHomeCenter.Application.MqttCenter.Dto;

public class MqttSendInfo
{
    public MqttSendInfo()
    {
        this.Data = new();
    }

    public string PluginKey { get; set; }

    public string DeviceName { get; set; }

    public CommandType CommandType { get; set; }

    public string Topic { get; set; }

    public Dictionary<string,string> Data { get; set; }

    public bool Success { get; set; }

    public string ErrorMsg { get; set; }
}

public enum CommandType
{
    Set,
    Get
}