// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.VerifyCode.Dtos;
using FantasyHomeCenter.VerifyCode.Services;
using System.Threading.Tasks;

namespace FantasyHomeCenter.VerifyCode.Client.Services
{
    [ScopedService]
    public class EmailVerifyCodeService : IEmailVerifyCodeService
    {
        private readonly static string controller = "email-verify-code";
        private readonly IApiCaller apiCaller;
        public EmailVerifyCodeService(IApiCaller apiCaller)
        {
            this.apiCaller = apiCaller;
        }

        public async Task<EmailVerifyCodeOutput> Create(EmailVerifyCodeInput input)
        {
            return await apiCaller.PostAsync<VerifyCodeInput, EmailVerifyCodeOutput>($"{controller}", input);
        }

        public async Task<bool> Remove(string key)
        {
            return await apiCaller.DeleteAsync<bool>($"{controller}/{key}");
        }

        public async Task<bool> Verify(EmailVerifyCodeCheckInput verifyCodeInput)
        {
            return await apiCaller.PostAsync<EmailVerifyCodeCheckInput, bool>($"{controller}/verify", verifyCodeInput);
        }
    }
}
