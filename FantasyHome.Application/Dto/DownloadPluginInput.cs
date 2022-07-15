namespace FantasyHome.Application.Dto
{
    public class DownloadPluginInput:RequestBase
    {
        public string Key { get; set; }
        public string SavePath { get; set; }
    }
}