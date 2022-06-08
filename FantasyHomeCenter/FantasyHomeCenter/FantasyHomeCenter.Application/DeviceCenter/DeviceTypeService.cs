using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.Application.PluginCenter;
using FantasyHomeCenter.Core.Entities;
using FantasyHomeCenter.DevicePluginInterface;
using FantasyHomeCenter.EntityFramework.Core.PluginContext;
using Furion;
using Furion.DatabaseAccessor;
using Furion.FriendlyException;
using Furion.UnifyResult;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FantasyHomeCenter.Application.DeviceCenter;

public class DeviceTypeService:IDynamicApiController,ITransient,IDeviceTypeService
{
    private readonly IRepository<DeviceType> repository;
    private readonly IConfiguration configuration;
    private readonly IPluginService pluginService;

    public DeviceTypeService(IRepository<DeviceType> repository,IConfiguration configuration,IPluginService pluginService)
    {
        this.repository = repository;
        this.configuration = configuration;
        this.pluginService = pluginService;
    }
    public async Task<RESTfulResult<DeviceTypeOutput>> AddDeviceTypeAsync(AddDeviceTypeInput input)
    {
        bool exist= await  this.repository.AnyAsync(x => x.DeviceTypeName == input.DeviceTypeName);
         if (exist)
             return new RESTfulResult<DeviceTypeOutput>() { Succeeded = false, Errors = $"{input.DeviceTypeName}已经存在" };
         var devicetype= input.Adapt<DeviceType>();
         var res= await this.repository.InsertNowAsync(devicetype);
         if (res == null)
             Oops.Bah($"插入{input.DeviceTypeName}失败");
         var convert_res = res.Adapt<DeviceTypeOutput>();
         return new RESTfulResult<DeviceTypeOutput>() { Succeeded = true, Data = convert_res };
    }

    public async Task<RESTfulResult<PagedList<DeviceTypeOutput>>> GetListPageAsync(DeviceTypeInput input)
    {
        var where = this.repository.AsQueryable();
       var res=   await where.Select(x => new DeviceTypeOutput()
        {
            DeviceTypeName = x.DeviceTypeName,
            Id = x.Id,
            Key=x.Key,
            Version=x.Version,
            Author=x.Author,
            PluginDescription=x.PluginDescription
        }).ToPagedListAsync();
       return new RESTfulResult<PagedList<DeviceTypeOutput>>() { Succeeded = true, Data = res };
    }

    public async Task<RESTfulResult<int>> DeleteDeviceTypeList(List<int> inputs)
    {
        //未来需要判断如果类型中还有设备，那么不应该被删除
        var list= await this.repository.AsQueryable().Where(x => inputs.Any(y => y == x.Id)).ToListAsync();
        await this.repository.DeleteNowAsync(list);
        return new RESTfulResult<int>() { Succeeded = true };
    }

    public async Task<RESTfulResult<List<DeviceTypeOutput>>> GetListAsync()
    {
        var entities= await this.repository.Entities.ToListAsync();
        var res= entities.Adapt<List<DeviceTypeOutput>>();
        return new RESTfulResult<List<DeviceTypeOutput>>() { Succeeded = true, Data = res };
    }
    
    [HttpPost("/api/device-type/upload-plugin")]
    public async Task<RESTfulResult<AddDevicePluginOutput>> UploadFileAsync(List<IFormFile> files)
    {
        string pluginPath = this.configuration["PluginRootPath"];

        if (string.IsNullOrEmpty(pluginPath))
        {
            throw Oops.Bah("插件配置目录不存在");
        }
        //保存路径
        string savePath = "";
        if (pluginPath.StartsWith("./"))
        {
             savePath = Path.Combine(App.HostEnvironment.ContentRootPath, pluginPath.Replace(@"./",""));
        }
        else
        {
            savePath = pluginPath;
        }
       
         if(!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
         if (files.Sum(f=>f.Length)==0)
         {
             throw Oops.Oh("上传文件是空的");
         }

         var file = files.First();
          // 避免文件名重复，采用 GUID 生成
          string guidFolder = Path.Combine(savePath, Guid.NewGuid().ToString("N").Substring(1, 6));
          var filePath = guidFolder + Path.GetExtension(file.FileName); 
          using (var stream = System.IO.File.Create(filePath))
          {
              await file.CopyToAsync(stream);
          }
          
          //解压文件
        ZipFile.ExtractToDirectory(filePath,guidFolder,true);
        File.Delete(filePath);

        if (File.Exists(Path.Combine(guidFolder,"Config.txt"))==false)
        {
            return new RESTfulResult<AddDevicePluginOutput>() { Succeeded = false, Errors = "插件包内容不正确,缺少Config.txt配置文件" };
        }

        //获得配置文件中的内容
        string configContent= File.ReadAllText(Path.Combine(guidFolder, "Config.txt"));

        string[] configSplit = configContent.Split("\n");
        if (configSplit.Length != 2)
        {
            return new RESTfulResult<AddDevicePluginOutput>() { Succeeded = false, Errors = $"插件包配置文件内容不正确" };
        }

        string newKey = configSplit[1];
        //根据key判断是否已经存在该文件
        if (this.repository.AsEnumerable().Any(x => x.Key == newKey))
        {

          //  await this.pluginService.DeletePluginByKeyAsync(info.Key);
            //删除文件夹
            Directory.Delete(guidFolder, true);
            File.Delete(guidFolder + ".zip");
            return new RESTfulResult<AddDevicePluginOutput> { Succeeded = false, Errors = "已经存在该设备类型插件" };
        }

        if (!File.Exists(Path.Combine(guidFolder, configSplit[0].Trim() +".dll")))
        {
            return new RESTfulResult<AddDevicePluginOutput>() { Succeeded = false, Errors = $"插件包内容不正确,缺少{configContent}.dll文件" };
        }

        var pluginModelRes= await this.pluginService.AddPluginAsync(guidFolder, configSplit[0].Trim());
        if (pluginModelRes.Succeeded)
        {
            IDeviceController controller = pluginModelRes.Data;
            if (controller != null)
            {
                AddDevicePluginOutput info = new AddDevicePluginOutput();
                info.Author = controller.Author;
                info.Description = controller.Description;
                info.Key = controller.Key;
                info.Path = guidFolder;
                info.Version = controller.DeviceTypeVersion;
                info.PluginName = controller.DeviceType;
             

                return new RESTfulResult<AddDevicePluginOutput>() { Succeeded = true, Data = info };
            }
            else
            {

                return new RESTfulResult<AddDevicePluginOutput> { Succeeded = false, Errors = "插件解析失败！" };
            }
        }
        else
        {
            return new RESTfulResult<AddDevicePluginOutput>() { Succeeded = false, Errors = pluginModelRes.Errors };
        }
       
    }

    public async Task<RESTfulResult<IDeviceController>> GetDeviceControllerById(int id)
    {
        var entity= this.repository.AsEnumerable().FirstOrDefault(x => x.Id == id);
        if (entity == null)
            return await Task.FromResult(new RESTfulResult<IDeviceController>() { Errors = "未发现设备类型", Succeeded = false });

        return await this.GetDeviceControllerByKey(entity.Key);
    }

    public async Task<RESTfulResult<IDeviceController>> GetDeviceControllerByKey(string key)
    {
     
        var getRes= await this.pluginService.GetPluginByKeyAsync(key);
        if (getRes.Succeeded==false)
        {
           var find=  this.repository.AsEnumerable().FirstOrDefault(x => x.Key == key);
           if (find == null)
           {
               return new RESTfulResult<IDeviceController>(){Succeeded=false,Errors= $"未发现{key}对应的数据" };
              
           }
           else
           {
            return  await this.pluginService.AddPluginAsync(find.PluginPath, find.PluginName);
           }

        }
        else
        {
            return getRes;
        }

    }

    public async Task<RESTfulResult<string>> GetDeviceTypePluginPathById(int id)
    {
        return await Task.FromResult( new RESTfulResult<string>(){Succeeded=true, Data= this.repository.First(x => x.Id == id).PluginPath});
    }
}