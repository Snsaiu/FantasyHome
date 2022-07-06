using System.Collections.Generic;
using System.Linq;
using FantasyHomeCenter.Application.SensorDeviceCenter.Dto;
using Furion.RemoteRequest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FantasyHomeCenter.Application.SensorDeviceCenter;

public class SensorDeviceService:IDynamicApiController,ISensorDeviceService,ITransient
{
    private readonly IDistributedCache distributedCache;

    public SensorDeviceService(IDistributedCache distributedCache)
    {
        this.distributedCache = distributedCache;
    }
    
    /// <summary>
    /// 健康检查
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Post("/check-health")]
    public RESTfulResult<HealthOutputDto> CheckHealth(HealthInputDto input)
    {
        List<HealthInputDto> sensorDevices = new List<HealthInputDto>();
        //todo
        string value= this.distributedCache.GetString("sensorDevices");
        if (value != null)
        {
          sensorDevices=  JsonConvert.DeserializeObject<List<HealthInputDto>>(value);
        }
        if (!sensorDevices.Any(x=>x.guid==input.guid))
        {
            sensorDevices.Add(input);
            
            this.distributedCache.SetString("sensorDevices",JsonConvert.SerializeObject(sensorDevices));
        }
        return new RESTfulResult<HealthOutputDto>();
    }

    [Post("/not-regist-scandevices")]
    public RESTfulResult<List<ScanDeviceOutputDto>> GetNotRegistScanDevices()
    {
      //todo 
      //根据id识别
      
      
      
      
      List<ScanDeviceOutputDto> list = new List<ScanDeviceOutputDto>();
      list.Add(new ScanDeviceOutputDto(){Guid = "12",DeviceName = "测试1",Ip = "192.168.0.102"});
      list.Add(new ScanDeviceOutputDto(){Guid = "123",DeviceName = "测试2",Ip = "192.168.0.103"});
      list.Add(new ScanDeviceOutputDto(){Guid = "144",DeviceName = "测试5",Ip = "192.168.0.107"});

      return new RESTfulResult<List<ScanDeviceOutputDto>>() { Succeeded = true, Data = list };


    }
}