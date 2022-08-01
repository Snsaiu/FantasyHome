using System.Reflection;
using System.Runtime.Loader;
using FantasyHomeCenter.DevicePluginInterface;
using McMaster.NETCore.Plugins;

namespace FantasyHomeCenter.Application.PluginCenter;

public class PluginModel
{
    public PluginLoader Loader { get; set; }

    public DeviceControllerBase Controller { get; set; }

    public string Key { get; set; }

    public string PluginPath{ get; set; }
}