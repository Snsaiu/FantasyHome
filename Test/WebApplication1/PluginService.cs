using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using FantasyHomeCenter.DevicePluginInterface;
using McMaster.NETCore.Plugins;

namespace WebApplication1
{
    public class PluginService
    {

        private string pluginFolder;

        private CompositionContainer container = null;
        
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<IDeviceController>> DevicesControllers { get; set; }

        public void LoadPlugins()
        {
           
            string path =
                "G:\\PROJECT\\VS\\FantasyHome\\DevicePluginsImpls\\MideaAirControlV3LocalControl\\bin\\Debug\\net5.0-windows";
            var loader = PluginLoader.CreateFromAssemblyFile(
                assemblyFile: Path.Combine(path, "MideaAirControlV3LocalControl" + ".dll"),
                sharedTypes: new[] { typeof(IDeviceController), typeof(IServiceCollection) },
                isUnloadable: true);
            var type = loader.LoadDefaultAssembly().GetTypes().First(t => typeof(IDeviceController).IsAssignableFrom(t) && !t.IsAbstract);
            IDeviceController controller = Activator.CreateInstance(type) as IDeviceController;

        }
        
        

        public PluginService()
        {
            //this.pluginFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");
            //if (Directory.Exists(pluginFolder)==false)
            //{
            //    Directory.CreateDirectory(this.pluginFolder);
            //}
        }
        
    }
}