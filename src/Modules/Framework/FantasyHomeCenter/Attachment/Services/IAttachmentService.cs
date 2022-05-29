// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using FantasyHomeCenter.Attachment.Dtos;
using FantasyHomeCenter.Base;

namespace FantasyHomeCenter.Attachment.Services
{
    /// <summary>
    /// 附件服务接口
    /// </summary>
    public interface IAttachmentService : IServiceBase<AttachmentDto, Guid>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<UploadAttachmentOutput> Upload(UploadAttachmentInput input, IFormFile file);
    }
}