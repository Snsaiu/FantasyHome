using System.Threading.Tasks;
using FantasyHomeCenter.DevicePluginInterface;
using Furion.UnifyResult;

namespace FantasyHomeCenter.EntityFramework.Core.PluginContext;

public interface  IPluginService
{

    /// <summary>
    /// 读取并存储到内存中
    /// </summary>
    /// <param name="pluginName"></param>
    /// <returns></returns>
    Task<RESTfulResult<DeviceControllerBase>> AddPluginAsync(string path, string name);

    /// <summary>
    /// 根据key获得插件
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<RESTfulResult<DeviceControllerBase>> GetPluginByKeyAsync(string key);

    /// <summary>
    /// 根据key删除插件
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<RESTfulResult<DeviceControllerBase>> DeletePluginByKeyAsync(string key);
}