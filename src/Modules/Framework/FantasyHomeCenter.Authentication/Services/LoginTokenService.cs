﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using FantasyHomeCenter.Authentication.Domains;
using FantasyHomeCenter.Authentication.Dtos;
using FantasyHomeCenter.Base;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FantasyHomeCenter;

namespace FantasyHomeCenter.Authentication.Services
{
    /// <summary>
    /// 用户登录TOKEN服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class LoginTokenService : ServiceBase<LoginToken, LoginTokenDto, Guid>, ILoginTokenService
    {
        private readonly IRepository<LoginToken> _loginTokenRepository;
        /// <summary>
        /// 用户登录TOKEN服务
        /// </summary>
        /// <param name="repository"></param>
        public LoginTokenService(IRepository<LoginToken> repository) : base(repository)
        {
            _loginTokenRepository = repository;
        }
        
        /// <summary>
        /// 搜索
        /// </summary>
        /// <remarks>
        /// 搜索数据
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public override async Task<PagedList<LoginTokenDto>> Search(PageRequest request)
        {
            IDynamicFilterService filterService = App.GetService<IDynamicFilterService>();
            Expression<Func<LoginToken, bool>> expression = filterService.GetExpression<LoginToken>(request.FilterGroups);

            IQueryable<LoginToken> queryable = _loginTokenRepository.AsQueryable(false)
                .Where(u => u.IsDeleted == false)
                .Where(expression);
            return await queryable
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<LoginTokenDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }
    }
}
