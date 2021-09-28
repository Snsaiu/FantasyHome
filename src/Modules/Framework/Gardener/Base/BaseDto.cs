﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace Gardener.Base
{
    /// <summary>
    /// dto基础类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseDto<TKey>: BaseDto
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        [DisplayName("编号")]
        public TKey Id { get; set; }
       
    }

    /// <summary>
    /// dto基础类
    /// </summary>
    public class BaseDto
    {
        /// <summary>
        /// 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }
        /// <summary>
        /// 是否逻辑删除
        /// </summary>
        [DisplayName("是否删除")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [DisplayName("创建时间")]
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [DisplayName("更新时间")]
        public DateTimeOffset? UpdatedTime { get; set; }
    }
}
