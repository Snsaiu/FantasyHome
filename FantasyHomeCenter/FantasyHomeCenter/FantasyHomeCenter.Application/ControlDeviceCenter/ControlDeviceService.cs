using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FantasyHomeCenter.Application.ControlDeviceCenter.Dto;
using FantasyHomeCenter.Application.FamilyCenter;
using FantasyHomeCenter.Core.Entities;
using Furion.DatabaseAccessor;
using Furion.DataEncryption;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace FantasyHomeCenter.Application.ControlDeviceCenter;

public class ControlDeviceService : IControlDeviceService, IDynamicApiController, ITransient
{
    private readonly IRepository<Family> familyRepository;
    private readonly IConfiguration configuration;


    public ControlDeviceService(IRepository<Family> familyRepository, IConfiguration configuration)
    {
        this.familyRepository = familyRepository;
        this.configuration = configuration;
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

            RegistResultOutput output = new RegistResultOutput();
            output.Token = accessToken;
            output.MqttService = this.configuration.GetSection("MqttService:Host").Value;
            output.Port = this.configuration.GetSection("MqttService:NotifyPort").Value;
            return new RESTfulResult<RegistResultOutput>() { Succeeded = true, Data = output };
        }
        else
        {
            return new RESTfulResult<RegistResultOutput>() { Succeeded = false, Errors = "账号或者密码错误" };
        }
    }
}