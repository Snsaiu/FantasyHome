﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Gardener.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Application.Dtos
{
    /// <summary>
    /// 附件
    /// </summary>
    public class AttachmentDto : BaseDto<Guid>
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        [Required(ErrorMessage = "业务Id不能为空")]
        [DisplayName("业务编号")]
        public string BusinessId { get; set; }
        /// <summary>
        /// 附件业务类型
        /// </summary>
        [Required(ErrorMessage = "附件业务类型不能为空")]
        [DisplayName("业务类型")]
        public AttachmentBusinessType? BusinessType { get; set; }
        /// <summary>
        /// 上传的文件类型
        /// </summary>
        [Required(ErrorMessage = "文件类型不能为空")]
        [DisplayName("类型")]
        public AttachmentFileType? FileType { get; set; }
        /// <summary>
        /// 原始类型
        /// </summary>
        [DisplayName("原始类型")]
        public string ContentType { get; set; }
        /// <summary>
        /// 文件大小 byte
        /// </summary>
        [DisplayName("大小")]
        public long Size { get; set; }
        /// <summary>
        /// 存储地址 无name
        /// </summary>
        [Required, MaxLength(200)]
        [DisplayName("路径")]
        public string Path { get; set; }
        
        /// <summary>
        /// 文件名称 随机生成
        /// </summary>
        [Required, MaxLength(100)]
        [DisplayName("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 原始文件名
        /// </summary>
        [MaxLength(100)]
        [DisplayName("原始名称")]
        public string OriginalName { get; set; }
        /// <summary>
        /// 访问地址
        /// </summary>
        [Required, MaxLength(200)]
        [DisplayName("访问地址")]
        public string Url { get; set; }
        /// <summary>
        /// 后缀
        /// .jpg
        /// </summary>
        [Required, MaxLength(20)]
        [DisplayName("后缀")]
        public string Suffix { get; set; }
        /// <summary>
        /// 更新日期
        /// </summary>
        [DisplayName("更新时间")]
        public DateTimeOffset? UpdatedTime { get; set; }
    }
}