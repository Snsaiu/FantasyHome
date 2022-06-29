using FantasyHomeCenter.Application.SensorDeviceCenter.Dto;
using Furion.RemoteRequest;
using Microsoft.AspNetCore.Authorization;

namespace FantasyHomeCenter.Application.SensorDeviceCenter;

public class SensorDeviceService:IDynamicApiController,ISensorDeviceService
{
    /// <summary>
    /// 健康检查
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Post("/check-health")]
    public RESTfulResult<HealthOutputDto> CheckHealth(HealthInputDto input)
    {
        //todo
        
        return new RESTfulResult<HealthOutputDto>();
    }
}