﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

namespace Gardener.Enums
{
    /// <summary>
    /// 节所属上、中、下（一般主科安排前三节、复科和其他安排 中间考下，如果有晚自习，可以将晚自习的节次设置为2）
    /// </summary>
    public enum Frequency
    {
        /// <summary>
        /// 靠前
        /// </summary>
        Morning = 0,
        /// <summary>
        /// 中间
        /// </summary>
        Afternoon = 1,
        /// <summary>
        /// 靠下
        /// </summary>
        Night = 2,
    }
}
