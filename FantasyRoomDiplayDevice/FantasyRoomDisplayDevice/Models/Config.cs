namespace FantasyRoomDisplayDevice.Models
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ApiServer
    {
        public string Host { get; set; }
        public string Port { get; set; }
    }

    public class Logging
    {
        public LogLevel LogLevel { get; set; }
    }

    public class LogLevel
    {
        public string Default { get; set; }
        public string System { get; set; }
        public string Microsoft { get; set; }
    }

    public class MqttServer
    {
        public string Host { get; set; }
        public string Port { get; set; }
    }

    public class Config
    {
        public Logging Logging { get; set; }
        public MqttServer MqttServer { get; set; }
        public ApiServer ApiServer { get; set; }

        public UserInfo UserInfo { get; set; }
    }

    public class UserInfo
    {
        public string UserName { get; set; }
        public string Pwd { get; set; }
    }


}