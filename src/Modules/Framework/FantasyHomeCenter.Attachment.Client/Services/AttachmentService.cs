// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Attachment.Dtos;
using FantasyHomeCenter.Attachment.Services;
using FantasyHomeCenter.Base;
using FantasyHomeCenter.Client.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyHomeCenter.Attachment.Client.Services
{
    [ScopedService]
    public class AttachmentService : ClientServiceBase<AttachmentDto, Guid>, IAttachmentService
    {

        public AttachmentService(IApiCaller apiCaller) : base(apiCaller, "attachment")
        {
        }

        public async Task<Base.PagedList<AttachmentDto>> Search(int? businessType, int? fileType, string businessId, string order = "desc", int pageIndex = 1, int pageSize = 10)
        {
            IDictionary<string, object> pramas = new Dictionary<string, object>()
            {
                {"businessType",businessType },
                {"fileType",fileType },
                {"order",order }
            };
            return await apiCaller.GetAsync<PagedList<AttachmentDto>>($"{controller}/search/{pageIndex}/{pageSize}", pramas);
        }

        public Task<UploadAttachmentOutput> Upload(UploadAttachmentInput input, IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
