namespace FantasyRoomDisplayDevice.Services
{
    public class TempConfigService
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Token { get; set; }

        public string UserName { get; set; }
        public string Pwd { get; set; }

        public string MqttHost { get; set; }
        public string MqttPort { get; set; }
    }
}