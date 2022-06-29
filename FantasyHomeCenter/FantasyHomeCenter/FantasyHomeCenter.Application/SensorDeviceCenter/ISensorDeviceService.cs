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
}