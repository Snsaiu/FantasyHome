// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;
using FantasyHomeCenter.Email.Dtos;

namespace FantasyHomeCenter.Email.Services
{
    /// <summary>
    /// 邮件服务
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> Send(SendEmailInputDto input);
    }
}
