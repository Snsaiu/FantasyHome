using System.Reflection;
using System.Runtime.Loader;
using FantasyHomeCenter.DevicePluginInterface;
using FantasyHomeCenter.EntityFramework.Core.PluginContext;
using Microsoft.Extensions.Configuration;


namespace DevicePluginTest;

public class PluginContextTest
{


    [Fact]
    public void test()
    {
        //IConfiguration config=null;
        //IPluginService ps = new PluginService(config);
        //ps.AddPluginAsync(
        //    @"G:\PROJECT\VS\FantasyHome\FantasyHomeCenter\FantasyHomeCenter\FantasyHomeCenter.Web.Entry\plugins\b7fd0e",
        //    "MideaAirControlV3LocalControl");
    }
    
    [Fact]
    public void loadPluinContext()
    {
        AssemblyLoadContext pluginLoadContext = new AssemblyLoadContext(@"G:\PROJECT\VS\FantasyHome\FantasyHomeCenter\FantasyHomeCenter\FantasyHomeCenter.Web.Entry\plugins\345404",true);
        var ass= pluginLoadContext.LoadFromAssemblyName(new AssemblyName("MideaAirControlV3LocalControl"));
        IDeviceController c=null;
        foreach (Type type in ass.GetTypes())
        {
            if (typeof(IDeviceController).IsAssignableFrom(type))
            {
                c = Activator.CreateInstance(type) as IDeviceController;
                if (c != null)
                {
                   string author= c.Author;
                }
            }
        }

       // c = null;
        pluginLoadContext.Unload();
      

    }
}