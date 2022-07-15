using System.Collections.Generic;
using FantasyHomeCenter.Application.ControlDeviceCenter.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FantasyHomeCenter.Application.ControlDeviceCenter;

public interface IControlDeviceService
{
    /// <summary>
    /// 登录或者注册当前设备
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    RESTfulResult<RegistResultOutput> Regist(RegistMachineInput input);


    /// <summary>
    /// 获得插件以及插件下的设备集合
    /// </summary>
    /// <returns></returns>
    RESTfulResult<List<DevicePluginMetaOutput>> GetPluginsMeta();
/// <summary>
/// 下载插件
/// </summary>
IActionResult PluginDownload(string key );
}