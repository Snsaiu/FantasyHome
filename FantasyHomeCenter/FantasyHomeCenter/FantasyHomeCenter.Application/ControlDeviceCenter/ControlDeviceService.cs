using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using FantasyHome.GTool;
using FantasyHomeCenter.Application.ControlDeviceCenter.Dto;
using FantasyHomeCenter.Application.FamilyCenter;
using FantasyHomeCenter.Core.Entities;
using Furion;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FantasyHomeCenter.Application.ControlDeviceCenter;

public class ControlDeviceService : IControlDeviceService, IDynamicApiController, ITransient
{
    private readonly IRepository<Family> familyRepository;
    private readonly IConfiguration configuration;
    private readonly IRepository<UiDevice> uiDeviceRepository;
    private readonly IRepository<UiDeviceType> uiDeviceTypeRepository;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly IRepository<DeviceType> deviceTypeRepository;
    private readonly IRepository<Device> deviceRepository;
    private readonly IRepository<CommandConstParams> commandConstParamsRepository;


    public ControlDeviceService(IRepository<Family> familyRepository,
        IConfiguration configuration,
        IRepository<UiDevice> uiDeviceRepository,
        IRepository<UiDeviceType> uiDeviceTypeRepository,
        IHttpContextAccessor httpContextAccessor,
        IRepository<DeviceType> deviceTypeRepository,
        IRepository<Device> deviceRepository,
        IRepository<CommandConstParams> commandConstParamsRepository)
    {
        this.familyRepository = familyRepository;
        this.configuration = configuration;
        this.uiDeviceRepository = uiDeviceRepository;
        this.uiDeviceTypeRepository = uiDeviceTypeRepository;
        this.httpContextAccessor = httpContextAccessor;
        this.deviceTypeRepository = deviceTypeRepository;
        this.deviceRepository = deviceRepository;
        this.commandConstParamsRepository = commandConstParamsRepository;
    }

    [AllowAnonymous]
    public RESTfulResult<RegistResultOutput> Regist(RegistMachineInput input)
    {

        if (this.familyRepository.AsEnumerable().Any(x =>
                x.UserName == input.ValidateUserName && x.Password == input.ValidateUsePassword))
        {
            var user = this.familyRepository.AsEnumerable().First(x => x.UserName == input.ValidateUserName);
            var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
            {
                { ClaimTypes.Name, user.UserName },
                { ClaimTypes.Role, "family" }
            }, 20000); // 过期时间 1分钟,用于测试

           
            RESTfulResult<bool> updateMatchingRes = this.RegistControlDevice(input,user);
            if (updateMatchingRes.Succeeded==false)
            {
                return new RESTfulResult<RegistResultOutput>() { Succeeded = false, Errors = "注册设备服务器出现错误!" };
            }

            RegistResultOutput output = new RegistResultOutput();
            output.Token = accessToken;
            output.MqttServiceTopic = this.configuration["MqttService:Topic"];
            output.MqttService = this.httpContextAccessor.HttpContext.Request.Host.Host;
            output.Port = "1883";
            return new RESTfulResult<RegistResultOutput>() { Succeeded = true, Data = output };
        }
        else
        {
            return new RESTfulResult<RegistResultOutput>() { Succeeded = false, Errors = "账号或者密码错误" };
        }
    }
    
    [HttpGet, NonUnify]
    public IActionResult PluginDownload(string key)
    {
       

        try
        {
            var plugin = this.deviceTypeRepository.AsQueryable().First(x => x.Key == key);

            if (plugin.PluginPath.EndsWith(@"\"))
            {
                plugin.PluginPath= plugin.PluginPath.Substring(0, plugin.PluginPath.Length - 1);
            }

            string zipName = Path.GetFileNameWithoutExtension(plugin.PluginPath) + ".zip";
            string zipFile = Path.Combine(Directory.GetParent(plugin.PluginPath).FullName, zipName);
            if (File.Exists(zipFile) == false)
            {
                Tools.ZipFileDictory(plugin.PluginPath+@"\", zipFile);
            }


            return new FileStreamResult(new FileStream(zipFile, FileMode.Open), "application/octet-stream") { FileDownloadName = zipName };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
      
    }

    public RESTfulResult<List<DevicePluginMetaOutput>> GetPluginsMeta()
    {
        //获得所有的插件
        var devicetypes = this.deviceTypeRepository.Include(x => x.Devices).ThenInclude(x => x.ConstCommandParams)
            .Include(x => x.Devices).ThenInclude(x => x.Room)
            .Adapt<List<DevicePluginMetaOutput>>();
       return new RESTfulResult<List<DevicePluginMetaOutput>>() { Succeeded = true, Data = devicetypes };
    }

    private RESTfulResult<bool> RegistControlDevice(RegistMachineInput input,Family user)
    {
        // 判断当前用户是否存在该设备
        if (!this.uiDeviceRepository.Include(x=>x.Family).Where(x=>x.Family.Id==user.Id&&x.DeviceCode==input.MachineCode).Any())
        {
            // 判断是否存在该设备类型
            if (!this.uiDeviceTypeRepository.Any(x=>x.TypeName==input.DeviceType))
            {
                UiDeviceType type = new UiDeviceType();
                type.TypeName = input.DeviceType;
                this.uiDeviceTypeRepository.InsertNow(type);
            }

            var uitype = this.uiDeviceTypeRepository.First(x => x.TypeName == input.DeviceType);

            UiDevice uiDevice = new();
            uiDevice.Family = user;
            uiDevice.Ip = input.Ip;
            uiDevice.Name = input.MachineCode;
            uiDevice.Room = null;
            uiDevice.DeviceCode = input.MachineCode;
            uiDevice.UiDeviceType = uitype;

            this.uiDeviceRepository.InsertNow(uiDevice);
            return new RESTfulResult<bool>() { Succeeded = true };

        }
        else
        {
            var cur_device= this.uiDeviceRepository.Include(x => x.Family).First(x => x.Family.Id == user.Id&&input.MachineCode==x.DeviceCode);
            cur_device.Ip = input.Ip;
            this.uiDeviceRepository.UpdateNow(cur_device);
            return new RESTfulResult<bool>() { Succeeded = true };
        }
    }
}