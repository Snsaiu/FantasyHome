using System.Collections.Generic;
using FantasyHomeCenter.Application.SensorDeviceCenter.Dto;
using Furion.RemoteRequest;
using Microsoft.AspNetCore.Authorization;

namespace FantasyHomeCenter.Application.SensorDeviceCenter;

public class SensorDeviceService:IDynamicApiController,ISensorDeviceService,ITransient
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

    [Post("/not-regist-scandevices")]
    public RESTfulResult<List<ScanDeviceOutputDto>> GetNotRegistScanDevices()
    {
      //todo 

      List<ScanDeviceOutputDto> list = new List<ScanDeviceOutputDto>();
      list.Add(new ScanDeviceOutputDto(){Guid = "12",DeviceName = "测试1",Ip = "192.168.0.102"});
      list.Add(new ScanDeviceOutputDto(){Guid = "123",DeviceName = "测试2",Ip = "192.168.0.103"});
      list.Add(new ScanDeviceOutputDto(){Guid = "144",DeviceName = "测试5",Ip = "192.168.0.107"});

      return new RESTfulResult<List<ScanDeviceOutputDto>>() { Succeeded = true, Data = list };


    }
}