﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Gardener.Authentication.Domains;
using Gardener.Authentication.Dtos;
using Gardener.Authentication.Enums;
using Gardener.Authentication.Options;
using Gardener.Base;
using Gardener.Enums;
using Gardener.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Furion;
using Microsoft.AspNetCore.Http;

namespace Gardener.Authentication.Core
{
    /// <summary>
    /// JwtBearer服务
    /// </summary>
    public class JwtBearerService : IJwtService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();

        private readonly JWTOptions jWTOptions;

        /// <summary>
        /// 初始化声明
        /// </summary>
        /// <param name="jWTOptions"></param>
        public JwtBearerService(IOptions<JWTOptions> jWTOptions)
        {
            this.jWTOptions = jWTOptions.Value;
        }

        /// <summary>
        /// 创建token
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<JsonWebToken> CreateToken(Identity identity)
        {
            // New Token
            var (accessToken, accessTokenExpires) = CreateToken(identity, JwtTokenType.AccessToken);
            var (refreshToken, refreshTokenExpires) = CreateToken(identity,JwtTokenType.RefreshToken);
            //存储refreshToken
            //写入刷新token
            await Db.GetRepository<LoginToken>().InsertAsync(new LoginToken()
            {
                IdentityId=identity.Id,
                IdentityName=identity.Name,
                IdentityGivenName=identity.GivenName,
                IdentityType=identity.IdentityType,
                LoginId = identity.LoginId,
                LoginClientType = identity.LoginClientType,
                Value = refreshToken,
                EndTime = refreshTokenExpires,
                CreatedTime = DateTimeOffset.Now,
                Ip=App.HttpContext?.GetRemoteIpAddressToIPv4()

            });
            return new JsonWebToken()
            {
                AccessToken = accessToken,
                AccessTokenExpires = accessTokenExpires.ToUnixTimeSeconds(),
                RefreshToken = refreshToken,
                RefreshTokenExpires = refreshTokenExpires.ToUnixTimeSeconds()
            };
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="oldRefreshToken"></param>
        /// <returns></returns>
        public async Task<JsonWebToken> RefreshToken(string oldRefreshToken)
        {
            Identity identity = ReadToken(oldRefreshToken);
            IRepository<LoginToken> repository= Db.GetRepository<LoginToken>();

            LoginToken loginToken = repository.AsQueryable(false).Where(x =>
            x.IsDeleted == false
            && x.IsLocked == false 
            && x.IdentityId.Equals(identity.Id) 
            && x.IdentityType.Equals(identity.IdentityType)
            && x.LoginId.Equals(identity.LoginId)).OrderByDescending(x => x.EndTime).FirstOrDefault();
            
            //异常token检测
            if (loginToken == null || loginToken.Value != oldRefreshToken || loginToken.EndTime <= DateTimeOffset.Now)
            {
                //token删除
                if (loginToken != null)
                {
                    await repository.FakeDeleteNowByKeyAsync(loginToken.Id);
                }
                throw Oops.Oh(ExceptionCode.REFRESHTOKEN_NO_EXIST_OR_EXPIRE);
            }
            var jwtOpt= GetJWTSettingsOptions(identity.IdentityType);
            //设置非绝对过期，生成新的刷新token
            if (!jwtOpt.IsRefreshAbsoluteExpired)
            {
                var (refreshToken, refreshTokenExpires) = CreateToken(identity, JwtTokenType.RefreshToken);
                //更新刷新token
                loginToken.Value = refreshToken;
                loginToken.EndTime = refreshTokenExpires;
                loginToken.UpdatedTime= DateTimeOffset.Now;
                await repository.UpdateIncludeAsync(loginToken, new string[] { nameof(LoginToken.Value), nameof(LoginToken.EndTime), nameof(LoginToken.UpdatedTime) });
            }
            // New Token
            var (accessToken, accessTokenExpires) = CreateToken(identity, JwtTokenType.AccessToken);
            return new JsonWebToken()
            {
                AccessToken = accessToken,
                AccessTokenExpires = accessTokenExpires.ToUnixTimeSeconds(),
                RefreshToken = loginToken.Value,
                RefreshTokenExpires = loginToken.EndTime.ToUnixTimeSeconds()
            };
        }

        /// <summary>
        /// 移除token自动刷新
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public async Task<bool> RemoveRefreshToken(Identity identity)
        {
            IRepository<LoginToken> repository = Db.GetRepository<LoginToken>();
            var refreshTokens = await repository.AsQueryable(false).Where(x => x.IsDeleted == false && x.IsLocked == false && x.IdentityId.Equals(identity.Id) && x.IdentityType.Equals(identity.IdentityType) && x.LoginId.Equals(identity.LoginId)).ToListAsync();
            await refreshTokens.ForEachAsync(async x => await repository.FakeDeleteByKeyAsync(x.Id));
            
            return true;
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="jwtTokenType"></param>
        /// <returns></returns>
        private (string, DateTimeOffset) CreateToken(Identity identity, JwtTokenType jwtTokenType)
        {
            Claim[] claims =
                {
                new Claim(ClaimTypes.NameIdentifier, identity.Id),
                new Claim(ClaimTypes.GivenName, identity.GivenName),
                new Claim(ClaimTypes.Name, identity.Name),
                new Claim(AuthKeyConstants.IdentityType, identity.IdentityType.ToString()),
                new Claim(AuthKeyConstants.ClientIdKeyName, identity.LoginId),
                new Claim(AuthKeyConstants.ClientTypeKeyName, identity.LoginClientType.ToString()),
                new Claim(AuthKeyConstants.TokenTypeKey, jwtTokenType.ToString())
            };
            return CreateToken(claims, GetJWTSettingsOptions(identity.IdentityType), jwtTokenType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="identityType"></param>
        /// <returns></returns>
        private JWTSettingsOptions GetJWTSettingsOptions(IdentityType identityType)
        {
            if (jWTOptions.Settings != null && jWTOptions.Settings.ContainsKey(identityType))
            {
                JWTSettingsOptions jWTSettings = jWTOptions.Settings.GetValueOrDefault(identityType);
                return jWTSettings;
            }
            else
            {
                throw Oops.Oh(identityType+" JWTSettings is no find");
            }
        }
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="jwtOpt"></param>
        /// <param name="jwtTokenType"></param>
        /// <returns></returns>
        private (string, DateTimeOffset) CreateToken(IEnumerable<Claim> claims, JWTSettingsOptions jwtOpt, JwtTokenType jwtTokenType)
        {
            DateTimeOffset expires;
            DateTimeOffset now = DateTimeOffset.Now;
            string issuerSigningKey = jwtOpt.IssuerSigningKey;
           
            if (jwtTokenType.Equals(JwtTokenType.RefreshToken))
            {
                expires = now.AddMinutes(jwtOpt.RefreshExpireMins);
            }
            else 
            {
                //默认5分钟
                double minutes = jwtOpt.ExpiredTime.HasValue ? jwtOpt.ExpiredTime.Value : 5; 
                expires = now.AddMinutes(minutes);
            }
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey));
            SigningCredentials credentials = new SigningCredentials(key, jwtOpt.Algorithm);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Audience = jwtOpt.ValidAudience,
                Issuer = jwtOpt.ValidIssuer,
                SigningCredentials = credentials,
                NotBefore = now.DateTime,
                IssuedAt = now.DateTime,
                Expires = expires.DateTime
            };
            SecurityToken token = _tokenHandler.CreateToken(descriptor);
            string accessToken = _tokenHandler.WriteToken(token);
            return (accessToken, expires);
        }

        /// <summary>
        /// 从token中读取数据
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        private Identity ReadToken(string tokenStr)
        {
            
            JwtSecurityToken jwtSecurityToken = _tokenHandler.ReadJwtToken(tokenStr);
            Claim identityTypeCla=jwtSecurityToken.Claims.FirstOrDefault(x => x.Type.Equals(AuthKeyConstants.IdentityType));
            if (identityTypeCla == null)
            {
                throw Oops.Oh(ExceptionCode.TOKEN_INVALID);
            }
            IdentityType identityType = Enum.Parse<IdentityType>(identityTypeCla.Value);
            JWTSettingsOptions jWTSettingsOptions= GetJWTSettingsOptions(identityType);

            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidIssuer = jWTSettingsOptions.ValidIssuer,
                ValidAudience = jWTSettingsOptions.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTSettingsOptions.IssuerSigningKey))
            };
            ClaimsPrincipal principal = _tokenHandler.ValidateToken(tokenStr, parameters, out _);

            Identity identity = new Identity();

            identity.Id= principal.FindFirstValue(ClaimTypes.NameIdentifier);
            identity.Name = principal.FindFirstValue(ClaimTypes.Name);
            identity.GivenName = principal.FindFirstValue(ClaimTypes.GivenName);
            string loginClientType =principal.FindFirstValue(AuthKeyConstants.ClientTypeKeyName);
            identity.LoginClientType = Enum.Parse<LoginClientType>(loginClientType);
            identity.IdentityType = identityType;
            identity.LoginId =principal.FindFirstValue(AuthKeyConstants.ClientIdKeyName);
            
            return identity;
        }

    }

}