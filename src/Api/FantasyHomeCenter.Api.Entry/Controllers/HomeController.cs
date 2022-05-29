// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyHomeCenter.Web.Entry.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [IgnoreAudit]
        public IActionResult Index()
        {
            return View();
        }
    }
}
