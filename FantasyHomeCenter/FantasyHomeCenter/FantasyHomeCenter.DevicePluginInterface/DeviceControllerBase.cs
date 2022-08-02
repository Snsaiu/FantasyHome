using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FantasyHomeCenter.DevicePluginInterface
{

   // public delegate void PublishDataDelegate(PublishData data);

    /// <summary>
    /// 控制设备
    /// </summary>
    public abstract class DeviceControllerBase
    {
        /// <summary>
        /// 当数据被设置后回调函数
        /// </summary>
       // public event PublishDataDelegate PublishDataEvent;

        /// <summary>
        /// 创建初始化参数
        /// </summary>
        /// <returns></returns>
        public abstract List<DeviceInputParameter> CreateInitInputParameters();

        /// <summary>
        /// 插件状态变化通知者
        /// </summary>
        private PluginStateChangeNotification pluginStateChangeNotification;

        /// <summary>
        /// 注册通知者
        /// </summary>
        /// <param name="pluginStateChangeNotification"></param>
        public void Regist(PluginStateChangeNotification pluginStateChangeNotification)
        {
            this.pluginStateChangeNotification= pluginStateChangeNotification;
        }

        /// <summary>
        /// 注销通知者
        /// </summary>
        public void UnRegist()
        {
            this.pluginStateChangeNotification = null;
        }

        /// <summary>
        /// 中央处理器主题
        /// </summary>
       public string MasterTopic
        {
            get => "fantasyhome";
        }

        /// <summary>
        /// 设置是否开启定时任务和配置定时任务参数
        /// </summary>
        public abstract BackgroundParam? BackgroundParam { get;  }


        public ControlUI GetControlUi(object initData)
        {
            string data= initData.ToString();

           var obj=  JsonConvert.DeserializeObject<DeviceMetaOutput>(data);

            return this.GetDeskTopControlUi(obj);
        }

        /// <summary>
        /// 控制面板的ui
        /// </summary>
        /// <param name="messageProcesser"></param>
        /// <returns></returns>
       protected abstract  ControlUI GetDeskTopControlUi(DeviceMetaOutput initData);

        // /// <summary>
        // /// 设备同步，可以在这个方法里面判断设备的状态是否真的被修改，如果被修改那么应该同步，否则应该返回实际的状态
        // /// </summary>
        // /// <param name="content">某一个客户端发送的数据</param>
        // /// <returns>返回的字符串将会被同一个主题的设备接受并更新</returns>
        // SyncResult SyncDevices(string content);

        /// <summary>
        /// mqtt同步的主题
        /// </summary>
        public abstract string Topic { get; }

        /// <summary>
        /// 创建设置设备状态的命令参数
        /// </summary>
        /// <returns></returns>
        public abstract List<DeviceInputParameter> CreateSetDeviceParameters();

        /// <summary>
        /// 创建获得设备状态的命令参数
        /// </summary>
        /// <returns></returns>
        public abstract List<DeviceInputParameter> CreateGetDeviceParameters();

        /// <summary>
        /// 获得设备类型 ,必须设置为项目的名称
        /// </summary>
        public abstract string DeviceType { get; }


        /// <summary>
        /// 设备类型版本
        /// </summary>
        public abstract string DeviceTypeVersion { get; }

        /// <summary>
        /// 该设备插件的唯一key
        /// </summary>
        public abstract string Key { get; }

        /// <summary>
        /// 作者
        /// </summary>
        public abstract string Author { get; }

        /// <summary>
        /// 表述信息
        /// </summary>
        public abstract string Description { get; }

      

        /// <summary>
        /// 启动或者初始化调用
        /// </summary>
        /// <returns></returns>
        public abstract Task<CommandResult> InitAsync(List<DeviceInputParameter> input, string pluginPath);

        public async Task<CommandResult> SetDeviceStateWithNotifyAsync(string deviceName, List<DeviceInputParameter> input, string pluginPath)
        {
            var command= await this.SetDeviceStateAsync(input,pluginPath);
            if (command.Success)
            {
                if (this.pluginStateChangeNotification != null)
                {
                    PublishData pd = new PublishData();
                    pd.Data = command.Data;
                    pd.DeviceName = deviceName;
                    pd.DevicePluginKey = this.Key;
                    this.pluginStateChangeNotification.Notify(pd);
                }
            }
            return command;
        }

        /// <summary>
        /// 设置设备状态
        /// </summary>
        /// <returns></returns>
        protected abstract Task<CommandResult> SetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath);

        /// <summary>
        /// 获得用户状态
        /// </summary>
        /// <returns></returns>
        public abstract Task<CommandResult> GetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath);

        /// <summary>
        /// 获得设备状态的属性值
        /// </summary>
        /// <returns></returns>
        public abstract List<PropertyModel> GetDeviceProperties();


    }
}