using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyHomeCenter.DevicePluginInterface
{

    /// <summary>
    /// 控制设备
    /// </summary>
    public interface IDeviceController
    {

        /// <summary>
        /// 创建初始化参数
        /// </summary>
        /// <returns></returns>
        List<DeviceInputParameter> CreateInitInputParameters();

        /// <summary>
        /// 中央处理器主题
        /// </summary>
        string MasterTopic
        {
            get => "fantasyhome";
        }

        /// <summary>
        /// 控制面板的ui
        /// </summary>
        /// <param name="messageProcesser"></param>
        /// <returns></returns>
        Object GetDeskTopControlUi(MessageProcesser messageProcesser);

        /// <summary>
        /// 设备同步，可以在这个方法里面判断设备的状态是否真的被修改，如果被修改那么应该同步，否则应该返回实际的状态
        /// </summary>
        /// <param name="content">某一个客户端发送的数据</param>
        /// <returns>返回的字符串将会被同一个主题的设备接受并更新</returns>
        SyncResult SyncDevices(string content);

        /// <summary>
        /// mqtt同步的主题
        /// </summary>
        public string Topic { get; }

        /// <summary>
        /// 创建设置设备状态的命令参数
        /// </summary>
        /// <returns></returns>
        List<DeviceInputParameter> CreateSetDeviceParameters();

        /// <summary>
        /// 创建获得设备状态的命令参数
        /// </summary>
        /// <returns></returns>
        List<DeviceInputParameter> CreateGetDeviceParameters();

        /// <summary>
        /// 获得设备类型
        /// </summary>
        string DeviceType { get; }


        /// <summary>
        /// 设备类型版本
        /// </summary>
        string DeviceTypeVersion { get; }

        /// <summary>
        /// 该设备插件的唯一key
        /// </summary>
        string Key { get; }

        /// <summary>
        /// 作者
        /// </summary>
        string Author { get; }

        /// <summary>
        /// 表述信息
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 启动或者初始化调用
        /// </summary>
        /// <returns></returns>
        Task<CommandResult> InitAsync(List<DeviceInputParameter> input, string pluginPath);

        /// <summary>
        /// 设置设备状态
        /// </summary>
        /// <returns></returns>
        Task<CommandResult> SetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath);

        /// <summary>
        /// 获得用户状态
        /// </summary>
        /// <returns></returns>
        Task<CommandResult> GetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath);


    }
}