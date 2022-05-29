// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Cache;
using FantasyHomeCenter.VerifyCode.Core;
using FantasyHomeCenter.VerifyCode.Enums;
using System;
using System.Threading.Tasks;

namespace FantasyHomeCenter.VerifyCode.CacheStore
{
    /// <summary>
    /// 图片验证码数据库存储服务
    /// </summary>
    public class VerifyCodeCacheStoreService : IVerifyCodeStoreService
    {
        private readonly ICache _cache;
        private readonly string keyPre = "ImageVerifyCode:";

        public VerifyCodeCacheStoreService(ICache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <param name="code"></param>
        /// <param name="expire"></param>
        /// <returns></returns>
        public async Task Add(VerifyCodeTypeEnum verifyCodeType, string key, string code, TimeSpan expire)
        {
            await _cache.SetAsync(keyPre+ verifyCodeType + key, code, expire);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> GetCode(VerifyCodeTypeEnum verifyCodeType, string key)
        {
            return await _cache.GetStringAsync(keyPre + verifyCodeType + key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="verifyCodeType"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task Remove(VerifyCodeTypeEnum verifyCodeType, string key)
        {
            await _cache.RemoveAsync(keyPre + verifyCodeType + key);
        }
    }
}
