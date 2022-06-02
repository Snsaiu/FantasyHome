using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using Furion.UnifyResult;

namespace FantasyHomeCenter.Application.DeviceCenter;

public interface IDeviceService
{
    /// <summary>
    /// 获得设备信息分页列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<RESTfulResult< PagedList<DeviceOutput>>> GetDevices(DeviceInput input);


    /// <summary>
    /// 获得所有的房间集合和设备类型集合
    /// </summary>
    /// <returns></returns>
    Task<RESTfulResult<DeviceTypesAndRoomsOutput>> GetDeviceTypesAndRoomsAsync();

    /// <summary>
    /// 添加新设备
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<RESTfulResult<int>> AddDeviceAsync(AddDeviceInput input);
}