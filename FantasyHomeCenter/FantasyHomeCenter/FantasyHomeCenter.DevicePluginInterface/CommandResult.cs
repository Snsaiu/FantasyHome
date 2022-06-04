namespace FantasyHomeCenter.DevicePluginInterface;

public sealed class CommandResult
{

    public Dictionary<string,string> Data { get; set; }
    public string SuccessMessage { get; set; }
    public string ErrorMessage { get; set; }
    public bool Success { get; set; }
}