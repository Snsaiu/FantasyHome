using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.DeviceCenter.Dto;

namespace FantasyHomeCenter.Application.DeviceCenter;

public interface IDeviceService
{
    /// <summary>
    /// 获得设备信息分页列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedList<DeviceOutput>> GetDevices(DeviceInput input);
}