﻿using Gardener.Client.Services;
using Gardener.Application.Dtos;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Gardener.Client
{
    /// <summary>
    /// 自定义验证状态提供器
    /// </summary>
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthorizeService authorizeService;
        private IAuthenticationStateManager authenticationStateManager;
        private ILogger logger;
        public CustomAuthenticationStateProvider(IAuthorizeService authorizeService,
            IAuthenticationStateManager authenticationStateManager, ILogger logger)
        {
            this.authorizeService = authorizeService;
            this.authenticationStateManager = authenticationStateManager;
            authenticationStateManager.SetNotifyAuthenticationStateChangedAction(Refresh);
            this.logger = logger;
        }
        /// <summary>
        /// 刷新页面后会执行
        /// </summary>
        /// <returns></returns>
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            AuthenticationState authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            try
            {
                var user =await authenticationStateManager.RefreshUser();
                authenticationState = CreateAuthenticationState(user);
                return authenticationState;
            }
            catch (Exception ex)
            {
                await logger.Error("用户信息获取失败,请重新登陆。",ex:ex);
                return authenticationState;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticationState"></param>
        public void Refresh()
        {
            base.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
        /// <summary>
        /// 根据 userdto 创建 AuthenticationState
        /// </summary>
        /// <returns></returns>
        private AuthenticationState CreateAuthenticationState(UserDto currentUser)
        {
            if (currentUser == null) return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, currentUser.NickName ?? currentUser.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString()));
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "apiauth"));
            return new AuthenticationState(authenticatedUser);
        }
    }
}