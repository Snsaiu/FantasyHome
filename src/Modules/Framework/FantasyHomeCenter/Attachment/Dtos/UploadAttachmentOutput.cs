// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;

namespace FantasyHomeCenter.Attachment.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public class UploadAttachmentOutput
    {
        /// <summary>
        /// 文件地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid Id { get; set; }
    }
}
