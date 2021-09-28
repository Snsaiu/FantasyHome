﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Gardener.Authentication.Core;
using Gardener.Authentication.Domains;
using Gardener.Authentication.Dtos;
using Gardener.EntityFramwork;
using Gardener.EntityFramwork.Dto;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Gardener.Authentication.Services
{
    /// <summary>
    /// 用户登录TOKEN服务
    /// </summary>
    [ApiDescriptionSettings("UserCenterServices")]
    public class LoginTokenService : ServiceBase<LoginToken, LoginTokenDto, Guid>, ILoginTokenService
    {
        private readonly IRepository<LoginToken> _repository;
        private readonly IJwtService _jwtBearerService;
        /// <summary>
        /// 用户登录TOKEN服务
        /// </summary>
        /// <param name="repository"></param>
        public LoginTokenService(IRepository<LoginToken> repository, IJwtService jwtBearerService) : base(repository)
        {
            _repository = repository;
            _jwtBearerService = jwtBearerService;
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

            IQueryable<LoginToken> queryable = _repository.AsQueryable(false)
                .Where(u => u.IsDeleted == false)
                .Where(expression);
            return await queryable
                .OrderConditions(request.OrderConditions.ToArray())
                .Select(x => x.Adapt<LoginTokenDto>())
                .ToPageAsync(request.PageIndex, request.PageSize);
        }
    }
}