// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;

namespace FantasyHomeCenter.Attributes
{
    /// <summary>
    /// 操作码
    /// </summary>
    public class CodeAttribute : Attribute
    {
        /// <summary>
        /// 初始化一个<see cref="OperateCodeAttribute"/>类型的新实例
        /// </summary>
        public CodeAttribute(string code)
        {
            Code = code;
        }

        /// <summary>
        /// 获取 属性名称
        /// </summary>
        public string Code { get; private set; }
    }
}
