using System.Collections.Generic;

namespace FantasyHomeCenter.DevicePluginInterface
{
    public class MessageModel
    {
  
        public Dictionary<string, string> Data { get; set; }
        public CommandType CommandType { get; set; }

    }

    public enum CommandType
    {
        Set,
        Get
    }
}