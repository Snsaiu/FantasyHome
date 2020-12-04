﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace Gardener.Enums
{
    /// <summary>
    /// 异常状态码
    /// </summary>
    public enum ExceptionCode
    {
        /// <summary>
        /// 用户锁定
        /// </summary>
        [Description("用户锁定")]
        USER_LOCKED,
        /// <summary>
        /// 用户密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        USER_NAME_OR_PASSWORD_ERROR,
        /// <summary>
        /// 用户名重复
        /// </summary>
        [Description("用户名重复")]
        USER_NAME_REPEAT,
        /// <summary>
        /// 资源键值重复
        /// </summary>
        [Description("资源键值重复")]
        RESOURCE_KEY_REPEAT
    }
}
