using System.Collections.Generic;

namespace FantasyHomeCenter.DevicePluginInterface
{
    /// <summary>
    /// 插件变化通知者
    /// </summary>
    public class PluginStateChangeNotification
    {


        private List<DeviceControllerBase> _devices;

        public PluginStateChangeNotification()
        {
            this._devices = new List<DeviceControllerBase>();
        }

        public List<DeviceControllerBase> GetDevices()
        {
            return this._devices;
        }

        public void AddPlugins(DeviceControllerBase device)
        {
            if (this._devices.Contains(device))
                return;

            this._devices.Add(device);
        }

        /// <summary>
        /// 消息分发
        /// </summary>
        /// <param name="data"></param>
        public void Notify(PublishData data)
        {
            // 有变化就会通知
        }

    }
}