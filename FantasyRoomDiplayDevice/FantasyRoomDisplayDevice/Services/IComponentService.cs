using System.Collections.Generic;
using FantasyHome.Application;
using FantasyHomeCenter.DevicePluginInterface;

namespace FantasyRoomDisplayDevice.Services
{
    /// <summary>
    /// 组件服务
    /// </summary>
    public interface IComponentService
    {
        /// <summary>
        /// 解析插件并获得控件
        /// </summary>
        /// <returns></returns>
        ResultBase<List<ControlUI>> ParsePluginAndGetControlUI();
    }
}