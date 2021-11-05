﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Base;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Email.Dtos
{
    /// <summary>
    /// 邮件模板信息
    /// </summary>
    [Description("邮件模板信息")]
    public class EmailTemplateDto : BaseDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [MaxLength(30, ErrorMessage = "最大长度不能大于{1}"), Required(ErrorMessage = "不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [DisplayName("备注")]
        [MaxLength(500, ErrorMessage = "最大长度不能大于{1}")]
        public string Remark { get; set; }
        /// <summary>
        /// 模板
        /// </summary>
        [DisplayName("模板")]
        [MaxLength(5000, ErrorMessage = "最大长度不能大于{1}")]
        public string Template { get; set; }
        /// <summary>
        /// 例子
        /// </summary>
        [DisplayName("例子")]
        [MaxLength(1000, ErrorMessage = "最大长度不能大于{1}")]
        public string Example { get; set; }
    }
}
