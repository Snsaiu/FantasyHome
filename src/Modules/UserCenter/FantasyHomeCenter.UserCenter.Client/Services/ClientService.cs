// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Authorization.Dtos;
using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.UserCenter.Dtos;
using FantasyHomeCenter.UserCenter.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyHomeCenter.UserCenter.Client.Services
{
    [ScopedService]
    public class ClientService : ClientServiceBase<ClientDto, Guid>, IClientService
    {
        public ClientService(IApiCaller apiCaller) : base(apiCaller, "client")
        {
        }
        public async Task<List<FunctionDto>> GetFunctions(Guid id)
        {
            return await apiCaller.GetAsync<List<FunctionDto>>($"{controller}/{id}/functions");
        }

        public async Task<TokenOutput> Login(ClientLoginInput input)
        {
            var result = await apiCaller.PostAsync<ClientLoginInput, TokenOutput>($"{controller}/login", input);
            return result;
        }

        public async Task<TokenOutput> RefreshToken(RefreshTokenInput input)
        {
            return await apiCaller.PostAsync<RefreshTokenInput, TokenOutput>($"{controller}/refresh-token", input);
        }
    }
}
