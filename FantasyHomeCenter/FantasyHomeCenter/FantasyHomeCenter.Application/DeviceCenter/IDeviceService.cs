using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.DevicePluginInterface;

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
    /// 获得所有设备
    /// </summary>
    /// <returns></returns>
    RESTfulResult<List<DeviceOutput>> GetAllDevices();

    /// <summary>
    /// 添加新设备
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<RESTfulResult<int>> AddDeviceAsync(AddDeviceInput input);

    /// <summary>
    /// 根据id获得 获得命令参数
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<RESTfulResult<Dictionary<string, string>>> GetGetDeviceCommandParamsbyDeviceId(int id);

    /// <summary>
    /// 根据id获得设置命令参数
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<RESTfulResult<Dictionary<string, string>>> GetSetDeviceCommandParamsByDeviceId(int id);

    /// <summary>
    /// 根据设备名称获得get属性以及值
    /// </summary>
    /// <param name="deviceName"></param>
    /// <returns></returns>
    List<DeviceInputParameter> GetGetDeviceCommandParamsbyDeviceName(string deviceName);
    RESTfulResult<Dictionary<string,string>> GetDeviceState(MqttSendInfo info);
    RESTfulResult<Dictionary<string,string>> SetThenGetDeviceState(MqttSendInfo info);
    RESTfulResult<List<PropertyModel>> GetDeviceControllPropertiesByDeviceTypeId(int deviceTypeId);
}