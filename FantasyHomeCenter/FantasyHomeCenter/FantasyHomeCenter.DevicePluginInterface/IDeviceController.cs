namespace FantasyHomeCenter.DevicePluginInterface;

/// <summary>
/// 控制设备
/// </summary>
public interface IDeviceController
{

    /// <summary>
    /// 创建初始化参数
    /// </summary>
    /// <returns></returns>
   List<DeviceInputParameter> CreateInitInputParameters();




    /// <summary>
    /// 创建设置设备状态的命令参数
    /// </summary>
    /// <returns></returns>
    List<DeviceInputParameter> CreateSetDeviceParameters();

    /// <summary>
    /// 创建获得设备状态的命令参数
    /// </summary>
    /// <returns></returns>
    List<DeviceInputParameter> CreateGetDeviceParameters();

    /// <summary>
    /// 获得设备类型
    /// </summary>
    string DeviceType { get; }


    /// <summary>
    /// 设备类型版本
    /// </summary>
    string  DeviceTypeVersion{ get; }

    /// <summary>
    /// 该设备插件的唯一key
    /// </summary>
    string Key{ get; }

    /// <summary>
    /// 作者
    /// </summary>
    string Author{ get; }

    /// <summary>
    /// 表述信息
    /// </summary>
    string Description{ get; }

    /// <summary>
    /// 启动或者初始化调用
    /// </summary>
    /// <returns></returns>
    Task<CommandResult> InitAsync(List<DeviceInputParameter> input,string pluginPath);

    /// <summary>
    /// 设置设备状态
    /// </summary>
    /// <returns></returns>
    Task<CommandResult> SetDeviceStateAsync(List<DeviceInputParameter> input,string pluginPath);
    /// <summary>
    /// 获得用户状态
    /// </summary>
    /// <returns></returns>
    Task<CommandResult> GetDeviceStateAsync(List<DeviceInputParameter> input,string pluginPath);


}