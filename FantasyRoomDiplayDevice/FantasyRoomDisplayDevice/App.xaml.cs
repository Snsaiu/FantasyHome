﻿using System.ComponentModel;
using System.Configuration;
using System.IO;
using FantasyRoomDisplayDevice.Views;
using Prism.Ioc;
using System.Windows;
using FantasyRoomDisplayDevice.Services;
using FantasyRoomDisplayDevice.ViewModels;
using Microsoft.Extensions.Configuration;

namespace FantasyRoomDisplayDevice
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        public IConfiguration Configuration { get; private set; }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            containerRegistry.RegisterSingleton<IConfiguration>(() => this.Configuration);
            containerRegistry.Register<IConfigWriter, ConfigWriter>();
            containerRegistry.RegisterSingleton<TempConfigService>(() => new TempConfigService());
            
            containerRegistry.RegisterSingleton<PluginService>(() => new PluginService());
            containerRegistry.Register<ICommonService, CommonService>();
            containerRegistry.Register<IDeviceService, DeviceService>();
            containerRegistry.RegisterSingleton<MqttService>();
            containerRegistry.RegisterForNavigation<Login, LoginViewModel>();
            containerRegistry.RegisterForNavigation<Home, HomeViewModel>();
            containerRegistry.RegisterForNavigation<HomeComponent, HomeComponentViewModel>();
       






        }
    }
}
