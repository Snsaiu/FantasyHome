using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using FantasyHomeCenter.DevicePluginInterface;

namespace FantasyRoomDisplayDevice.Services
{
    public class PluginService
    {

        private string pluginFolder;

        private CompositionContainer container = null;
        
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<DeviceControllerBase>> DevicesControllers { get; set; }

        public void LoadPlugins()
        {
            var catalog = new AggregateCatalog();
            foreach (var item in Directory.GetDirectories(this.pluginFolder))
            {
                catalog.Catalogs.Add(new DirectoryCatalog(item));
            }

            this.container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
        
        

        public PluginService()
        {
            this.pluginFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins");
            if (Directory.Exists(pluginFolder)==false)
            {
                Directory.CreateDirectory(this.pluginFolder);
            }
        }
        
    }
}