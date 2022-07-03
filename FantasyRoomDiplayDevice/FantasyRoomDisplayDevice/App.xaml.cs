using System.ComponentModel;
using FantasyRoomDisplayDevice.Views;
using Prism.Ioc;
using System.Windows;

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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
