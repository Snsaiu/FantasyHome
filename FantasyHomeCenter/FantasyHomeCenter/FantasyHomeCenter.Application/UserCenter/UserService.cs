using System.Collections.Generic;
using System.Security.Claims;
using FantasyHomeCenter.Application.UserCenter.Dto;
using Furion.DataEncryption;
using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Authorization;

namespace FantasyHomeCenter.Application.UserCenter;

public class UserService:IDynamicApiController,IUserService,ITransient
{
    [AllowAnonymous]
    public LoginUserOutput Author(LoginUserInput input)
    { 
        
        // 到数据库中查询
        
        var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
        {
            { ClaimTypes.PrimarySid, 1 },
            { ClaimTypes.Name, "saiu" },
            {ClaimTypes.Role,"admin"}
        }, 100); // 过期时间 1分钟,用于测试

        return new LoginUserOutput() { Token = accessToken };
        // 下面设置 headers["access-token"] 即可登录. 这里无需重复设置
        //_httpContextAccessor.HttpContext.SigninToSwagger(accessToken);

        // 默认30天,可指定第二个参数设置有效期
        // var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken);
        // if (_httpContextAccessor.HttpContext != null)
        // {
        //     _httpContextAccessor.HttpContext.Response.Headers["access-token"] = accessToken;
        //     _httpContextAccessor.HttpContext.Response.Headers["x-access-token"] = refreshToken;
        //     // 指定公开的key,这样 axios才能取到值
        //     _httpContextAccessor.HttpContext.Response.Headers["Access-Control-Expose-Headers"] =
        //         "access-token, x-access-token";
        // }
        //
        // return !string.IsNullOrEmpty(accessToken);

    }
}