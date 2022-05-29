// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.NotificationSystem.Domains;
using Furion.DatabaseAccessor;
using FantasyHomeCenter;
using FantasyHomeCenter.NotificationSystem.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FantasyHomeCenter.NotificationSystem.Services
{
    /// <summary>
    /// 公告服务
    /// </summary>
    [ApiDescriptionSettings("NotificationSystem")]
    public class AnnouncementService : ServiceBase<Announcement, AnnouncementDto>, IAnnouncementService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        public AnnouncementService(IRepository<Announcement> repository) : base(repository)
        {
        }
    }
}
