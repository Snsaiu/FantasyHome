using System.Linq;
using FantasyHomeCenter.Core.Entities;
using FantasyHomeCenter.EntityFramework.Core.PluginContext;
using Furion.DatabaseAccessor;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyHomeCenter.Web.Core;

public static class Extensions
{

    public static void AddInitPluginService(this IServiceCollection services)
    {
        var provider= services.BuildServiceProvider();
        var repository = provider.GetService<IRepository<DeviceType>>();
        var pluginService= provider.GetService<IPluginService>();
        var deviceTypes= repository.AsQueryable().ToList();
        foreach (DeviceType item in deviceTypes)
        {
            if (string.IsNullOrEmpty(item.Key)==false)
            {
                pluginService.AddPluginAsync(item.PluginPath, item.PluginName);
            }
        }
    }
}