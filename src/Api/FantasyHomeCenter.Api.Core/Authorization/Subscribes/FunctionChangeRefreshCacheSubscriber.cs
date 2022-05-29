﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.EventBus;
using FantasyHomeCenter.Authorization.Core;
using FantasyHomeCenter.Authorization.Dtos;
using FantasyHomeCenter.Base;
using FantasyHomeCenter.Cache;
using FantasyHomeCenter.EventBus;
using FantasyHomeCenter.UserCenter.Impl.Domains;
using Mapster;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FantasyHomeCenter.Api.Core.Authorization.Subscribes
{
    /// <summary>
    /// 功能点变化刷新接口点缓存
    /// </summary>
    public class FunctionChangeRefreshCacheSubscriber : IEventSubscriber, ISingleton
    {
        private readonly ICache cache;
        private readonly IApiEndpointQueryService apiEndpointQueryService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="apiEndpointQueryService"></param>
        public FunctionChangeRefreshCacheSubscriber( ICache cache, IApiEndpointQueryService apiEndpointQueryService)
        {
            this.cache = cache;
            this.apiEndpointQueryService = apiEndpointQueryService;
        }

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventType.EntityOperate) + nameof(Function) + nameof(EntityOperateType.Delete))]
        [EventSubscribe(nameof(EventType.EntityOperate) + nameof(Function) + nameof(EntityOperateType.FakeDelete))]
        public async Task Delete(EventHandlerExecutingContext context)   
        {
            IEventSource eventSource = context.Source;

            IRepository<Function> repository = Db.GetRepository<Function>();
            Guid id = (Guid)eventSource.Payload;
            //移除IApiEndpointQueryService 的 function的缓存
            Function function = await repository.FindAsync(id);
            await apiEndpointQueryService.ClearApiEndpointCacheKey(function.Adapt<ApiEndpoint>());
        }

        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventType.EntityOperate) + nameof(Function) + nameof(EntityOperateType.Deletes))]
        [EventSubscribe(nameof(EventType.EntityOperate) + nameof(Function) + nameof(EntityOperateType.FakeDeletes))]
        public async Task Deletes(EventHandlerExecutingContext context)   
        {
            IEventSource eventSource = context.Source;

            IRepository<Function> repository = Db.GetRepository<Function>();
            IEnumerable<Guid> ids = (IEnumerable<Guid>)eventSource.Payload;
            foreach (Guid id in ids)
            {
                //移除IApiEndpointQueryService 的 function的缓存
                Function function = await repository.FindAsync(id);
                await apiEndpointQueryService.ClearApiEndpointCacheKey(function.Adapt<ApiEndpoint>());
            }
            
        }
        /// <summary>
        /// 功能点变化
        /// </summary>
        /// <param name="context"></param>
        [EventSubscribe(nameof(EventType.EntityOperate) + nameof(Function) + nameof(EntityOperateType.Update))]
        [EventSubscribe(nameof(EventType.EntityOperate) + nameof(Function) + nameof(EntityOperateType.Lock))]
        public async Task Update(EventHandlerExecutingContext context)   
        {
            IEventSource eventSource = context.Source;

            Function function = (Function)eventSource.Payload;
            IApiEndpointQueryService apiEndpointQueryService = App.GetService<IApiEndpointQueryService>();
            await apiEndpointQueryService.ClearApiEndpointCacheKey(function.Adapt<ApiEndpoint>());
        }
    }
}
