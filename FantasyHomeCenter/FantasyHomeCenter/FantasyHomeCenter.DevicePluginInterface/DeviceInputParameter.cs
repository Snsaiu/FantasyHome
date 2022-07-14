using System.Collections.Generic;

namespace FantasyHomeCenter.DevicePluginInterface
{

    /// <summary>
    /// 输入参数
    /// </summary>
    public sealed class DeviceInputParameter
    {

        public DeviceInputParameter()
        {
            this.ValueHasEnums = false;
            this.ValueEnums = new Dictionary<string, string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="constValue">是否是常量</param>
        /// <param name="description">描述</param>
        /// <param name="value">参数值</param>
        public DeviceInputParameter(string name, bool constValue, string description, string defaultValue,
            Dictionary<string, string>? valueEnums)
        {
            if (valueEnums != null && valueEnums.Count != 0)
            {
                this.ValueEnums = valueEnums;
                this.ValueHasEnums = true;
            }
            else
            {
                this.ValueHasEnums = false;
                this.ValueEnums = new Dictionary<string, string>();
            }

            (Name, ConstValue, Description, Value) = (name, constValue, description, defaultValue);

        }

        public DeviceInputParameter(string name, bool constValue, string description)
        {
            this.ValueHasEnums = false;
            this.ValueEnums = new Dictionary<string, string>();
            (Name, ConstValue, Description) = (name, constValue, description);

        }


        public DeviceInputParameter(string name, bool constValue, string description,
            Dictionary<string, string>? valueEnums)
        {
            if (valueEnums != null && valueEnums.Count != 0)
            {
                this.ValueEnums = valueEnums;
                this.ValueHasEnums = true;
            }
            else
            {
                this.ValueHasEnums = false;
                this.ValueEnums = new Dictionary<string, string>();
            }

            (Name, ConstValue, Description) = (name, constValue, description);

        }

        /// <summary>
        /// 设置值的时候用此构造函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public DeviceInputParameter(string name, string value)
        {
            (Name, Value) = (name, value);
            this.ValueEnums = new Dictionary<string, string>();
        }

        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 参数是否含有枚举
        /// </summary>
        public bool ValueHasEnums { get; private set; }

        /// <summary>
        /// 是否是常量值
        /// </summary>
        public bool ConstValue { get; set; } = false;

        /// <summary>
        /// 参数枚举类型，通常会变成下拉单选
        /// </summary>
        public Dictionary<string, string> ValueEnums { get; set; }

        /// <summary>
        /// 参数描述信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string Value { get; set; }
    }
}