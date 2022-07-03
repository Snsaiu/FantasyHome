using System.Collections.Generic;
using FantasyHomeCenter.Application.SensorDeviceCenter.Dto;

namespace FantasyHomeCenter.Application.SensorDeviceCenter;

public interface ISensorDeviceService
{
    /// <summary>
    /// 健康检查
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    RESTfulResult<HealthOutputDto> CheckHealth(HealthInputDto input);

    /// <summary>
    /// 获得没有注册的被扫描到的设备
    /// </summary>
    /// <returns></returns>
    
    RESTfulResult<List<ScanDeviceOutputDto>> GetNotRegistScanDevices();
}