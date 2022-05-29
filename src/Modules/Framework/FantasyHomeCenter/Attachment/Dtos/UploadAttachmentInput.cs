// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using FantasyHomeCenter.Attachment.Enums;

namespace FantasyHomeCenter.Attachment.Dtos
{
    /// <summary>
    /// 上传附件
    /// </summary>
    public class UploadAttachmentInput
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        [MaxLength(64,ErrorMessage = "最大长度不能大于{1}")]
        public string BusinessId { get; set; }
        /// <summary>
        /// 附件业务类型
        /// </summary>
        [Required(ErrorMessage = "附件业务类型不能为空")]
        public AttachmentBusinessType BusinessType { get; set; }
    }
}
