﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Client.Base;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyHomeCenter.Client.Core
{
    /// <summary>
    /// SignalRClientManager
    /// </summary>
    public static class SystemNotificationExtension
    {
        /// <summary>
        /// SignalRClientManager
        /// </summary>
        /// <param name="builder"></param>
        public static void AddSignalRClientManager(this WebAssemblyHostBuilder builder) 
        {
            builder.Services.AddTransient<ISignalRClientBuilder, SignalRClientBuilder>();
            builder.Services.AddScoped<ISignalRClientManager, SignalRClientManager>();
        }

    }
}
