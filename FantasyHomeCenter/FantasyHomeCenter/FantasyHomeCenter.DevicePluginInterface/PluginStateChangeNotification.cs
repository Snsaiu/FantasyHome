using System.Collections.Generic;
using System.Linq;

namespace FantasyHomeCenter.DevicePluginInterface
{
    /// <summary>
    /// 插件变化通知者,属于单例
    /// </summary>
    public class PluginStateChangeNotification
    {


        private List<DeviceControllerBase> _devices;


        /// <summary>
        /// 自动化设备
        /// </summary>
        public List<AutomationTemp> AutomationTemps { get; set; }



        public PluginStateChangeNotification()
        {
            this._devices = new List<DeviceControllerBase>();
            this.AutomationTemps = new List<AutomationTemp>();
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
            foreach (var item in this.AutomationTemps)
            {

                ///是否可以执行
                bool canExec = false;
                //加入存在
              if( item.Triggers.Any(x=>x.DeviceName==data.DeviceName))
                {

                    foreach (AutomationTriggerTemp trigger in item.Triggers)
                    {
                        
                        if (data.BeforeData.ContainsKey(trigger.Property))
                        {
                            string beforeValue = data.BeforeData[trigger.Property];
                            string afterValue = data.AfterData[trigger.Property];
                            if (beforeValue == trigger.BeforeValue && afterValue == trigger.AfterValue)
                            {
                                canExec = true;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //触发其他设备
                    if (canExec)
                    {
                        foreach (var action in item.Actions)
                        {
                           // 获得插件
                           var controller= this._devices.First(x => x.Key == action.PluginTypeKey);

                           
                           //todo 需要修改模型，
                           controller.SetDeviceStateWithNotifyAsync(action.TargetDeviceName, action.SetParameters,
                               action.GetParameters, action.pluginPath);

                        }
                    }
                    
                }
            }
            
        }

    }
}