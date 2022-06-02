using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using Furion.UnifyResult;

namespace FantasyHomeCenter.Application.DeviceCenter;

public interface IDeviceTypeService
{

    /// <summary>
    /// 添加新的设备类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<RESTfulResult<DeviceTypeOutput>> AddDeviceTypeAsync(AddDeviceTypeInput input);

    /// <summary>
    /// 获得设备类型分页
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<RESTfulResult<PagedList<DeviceTypeOutput>>> GetListPageAsync(DeviceTypeInput input);

    /// <summary>
    /// 删除设备类型
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    Task<RESTfulResult<int>> DeleteDeviceTypeList(List<int> inputs);

    /// <summary>
    /// 获得所有的设备类型
    /// </summary>
    /// <returns></returns>
    Task<RESTfulResult<List<DeviceTypeOutput>>> GetListAsync();
}