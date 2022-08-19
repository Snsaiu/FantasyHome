using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyHomeCenter.DevicePluginInterface
{
    /// <summary>
    /// 设备的状态
    /// </summary>
    public class DeviceState
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string  DeviceName { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }
    }
}
