using System.Collections.Generic;
using FantasyHome.Application;
using FantasyHome.Application.Dto;

namespace FantasyRoomDisplayDevice.Services
{
    public interface IDeviceService
    {
        ResultBase<List<DevicePluginMetaOutput>> DownloadPlugins();
    }
}