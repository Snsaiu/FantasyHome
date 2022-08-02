using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyHomeCenter.DevicePluginInterface
{
    /// <summary>
    /// 设备状态 属性模型
    /// </summary>
    public class PropertyModel
    {

        public PropertyModel()
        {

        }

        public PropertyModel(string propertyName)
        {
            PropertyName = propertyName;
            PropertyType = PropertyType.变量;
        }

        public PropertyModel(string propertyName,  List<string> enums)
        {
            PropertyName = propertyName;
            PropertyType = PropertyType.枚举;
            Enums = enums;
        }

        public string PropertyName { get; set; }

        public PropertyType PropertyType { get; set; }

        public List<string> Enums { get; set; }

    }

    public enum PropertyType
    {
    
        变量,
        枚举,
    }
}
