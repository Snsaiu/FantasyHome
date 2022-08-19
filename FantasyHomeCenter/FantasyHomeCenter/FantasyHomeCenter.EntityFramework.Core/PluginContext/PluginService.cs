using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.PluginCenter;
using FantasyHomeCenter.Core.Entities;
using FantasyHomeCenter.DevicePluginInterface;
using Furion;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.UnifyResult;
using McMaster.NETCore.Plugins;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FantasyHomeCenter.EntityFramework.Core.PluginContext;

/// <summary>
/// 插件配置目录
/// </summary>
public class PluginService : IPluginService, ISingleton
{
    private readonly IRepository<DeviceType> deviceRepository;

    private List<PluginModel> pluginModels = null;

   

    public PluginService()
    {
        this.deviceRepository = App.GetService<IRepository<DeviceType>>();
        this.pluginModels = new List<PluginModel>();
    }

  
    public Task<RESTfulResult<DeviceControllerBase>> DeletePluginByKeyAsync(string key)
    {
        PluginModel? item= this.pluginModels.Find(x => x.Key == key);
        if (item == null)
        {
            throw Oops.Oh("插件key" + key + "不存在了");
        }
        DeviceControllerBase dc = item.Controller;

        if (item.Loader.IsUnloadable)
        {
            item.Loader.Dispose();

        }
        else
        {
            return Task.FromResult(new RESTfulResult<DeviceControllerBase> { Succeeded = false,Errors="当前插件正在运行，无法卸载"});
        }

        System.IO.Directory.Delete(item.PluginPath, true);
        this.pluginModels.Remove(item);
        return Task.FromResult(new RESTfulResult<DeviceControllerBase> { Succeeded = true, Data = dc });
    }

    public async Task<RESTfulResult<DeviceControllerBase>> GetPluginByKeyAsync(string key)
    {
        PluginModel? item = this.pluginModels.Find(x => x.Key == key);
        
        
        if (item == null)
        {

            var deviceType = this.deviceRepository.AsQueryable().FirstOrDefault(x => x.Key == key);
            if (deviceType==null)
            {
                return await Task.FromResult(new RESTfulResult<DeviceControllerBase> { Succeeded = false }); 
            }
          
                if (string.IsNullOrEmpty(deviceType.Key)==false)
                {
                   return await  this.AddPluginAsync(deviceType.PluginPath, deviceType.PluginName);
                }
            
            
            
            
          
        }
        return await Task.FromResult(new RESTfulResult<DeviceControllerBase> { Succeeded = true, Data = item.Controller });
    }

    public Task<RESTfulResult<DeviceControllerBase>> AddPluginAsync(string path,string name)
    {
        if (this.pluginModels.Any(x=>x.PluginPath==path))
        {
            return Task.FromResult(new RESTfulResult<DeviceControllerBase>()
            {
                Succeeded = true,
               Data = this.pluginModels.First(x=>x.PluginPath==path).Controller
            });
        }

        try
        {
            PluginModel pm = new PluginModel();

            var loader = PluginLoader.CreateFromAssemblyFile(
                assemblyFile: Path.Combine(path, name + ".dll"),
                sharedTypes: new[] { typeof(DeviceControllerBase), typeof(IServiceCollection) },
                isUnloadable: true);
            pm.Loader = loader;


            var type = loader.LoadDefaultAssembly().GetTypes().First(t => typeof(DeviceControllerBase).IsAssignableFrom(t) && !t.IsAbstract);
            DeviceControllerBase controller = Activator.CreateInstance(type) as DeviceControllerBase;
            if (controller != null)
            {
                pm.Controller = controller;
                pm.Key = controller.Key;
                pm.PluginPath = path;
                this.pluginModels.Add(pm);
                return Task.FromResult(new RESTfulResult<DeviceControllerBase>
                    { Succeeded = true, Data = controller });
            }
            return Task.FromResult(new RESTfulResult<DeviceControllerBase> { Succeeded = false, Errors = $"插件{name}读取出错" });


        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
            
     
       
    }
}