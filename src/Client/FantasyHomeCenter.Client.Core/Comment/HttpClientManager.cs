// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Client.Base;
using System.Net.Http;
using System.Net.Http.Headers;

namespace FantasyHomeCenter.Client.Core
{
    [ScopedService]
    public  class HttpClientManager
    {
        private readonly HttpClient httpClient;

        public HttpClientManager(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public void SetClientAuthorization(string token)
        {
            //httpClient.Authenticator = new JwtAuthenticator(token);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}
