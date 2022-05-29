// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;

namespace FantasyHomeCenter.Attributes
{
    /// <summary>
    /// Enum 转换集合时忽略
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class IgnoreOnConvertToMapAttribute : Attribute
    {
    }
}
