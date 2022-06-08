using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.DevicePluginInterface;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Http;

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

    Task<RESTfulResult<AddDevicePluginOutput>> UploadFileAsync(List<IFormFile> files);

    /// <summary>
    /// 根据主键获得控制器
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<RESTfulResult<IDeviceController>> GetDeviceControllerById(int id);

    /// <summary>
    /// 根据key获得插件控制器
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<RESTfulResult<IDeviceController>> GetDeviceControllerByKey(string key);

    /// <summary>
    /// 根据主键获得插件目录
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetDeviceTypePluginPathById(int id);
}