using System.Collections.Generic;
using FantasyHome.Application.Dto;

namespace FantasyHome.Application
{
    public interface IDeviceApplication
    {
        /// <summary>
        /// 下载插件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ResultBase<string> DownloadPlugin(DownloadPluginInput input);
        
        /// <summary>
        /// 获得插件信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        ResultBase<List<DevicePluginMetaOutput>> GetDeviceMetas(DeviceMetaInput input);
    }
}