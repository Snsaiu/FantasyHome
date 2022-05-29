﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel;

namespace FantasyHomeCenter.CodeGeneration.Dtos
{
    /// <summary>
    /// 实体属性信息
    /// </summary>
    [Description("实体属性信息")]
    public class EntityPropertyDto
    {

        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string DisplayName { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        [DisplayName("字段名称")]
        public string FieldName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [DisplayName("数据类型")]
        public string DataTypeName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        [DisplayName("数据类型")]
        public string DataTypeFullName { get; set; }

        /// <summary>
        /// 是否是可为NULL类型
        /// </summary>
        [DisplayName("是否是可为NULL类型")]
        public bool IsNullableType { get; set; }

    }
}
