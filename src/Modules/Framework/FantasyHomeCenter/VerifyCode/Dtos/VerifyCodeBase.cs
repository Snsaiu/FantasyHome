// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasyHomeCenter.VerifyCode.Enums;

namespace FantasyHomeCenter.VerifyCode.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class VerifyCodeBase
    {
        /// <summary>
        /// 验证码类型
        /// </summary>
        public abstract VerifyCodeTypeEnum VerifyCodeType { get; }
    }
}
