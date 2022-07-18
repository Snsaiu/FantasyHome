using System.Collections.Generic;
using System.Threading.Tasks;
using FantasyHomeCenter.DevicePluginInterface;

namespace Weather
{
    public class WeatherExport:IDeviceController
    {
        public List<DeviceInputParameter> CreateInitInputParameters()
        {

            List<DeviceInputParameter> parameters = new List<DeviceInputParameter>();
            parameters.Add(new DeviceInputParameter("Url",true,"请求天气的地址"));
            parameters.Add(new DeviceInputParameter("Longitude",true,"经度"));
            parameters.Add(new DeviceInputParameter("Latitude",true,"纬度"));
            parameters.Add(new DeviceInputParameter("Key",true,"key"));
            
        }

        public ControlUI GetDeskTopControlUi(object initData)
        {
            throw new System.NotImplementedException();
        }

        public string Topic { get; }
        public List<DeviceInputParameter> CreateSetDeviceParameters()
        {
            throw new System.NotImplementedException();
        }

        public List<DeviceInputParameter> CreateGetDeviceParameters()
        {
            throw new System.NotImplementedException();
        }

        public string DeviceType { get; }
        public string DeviceTypeVersion { get; }
        public string Key { get; }
        public string Author { get; }
        public string Description { get; }
        public Task<CommandResult> InitAsync(List<DeviceInputParameter> input, string pluginPath)
        {
            throw new System.NotImplementedException();
        }

        public Task<CommandResult> SetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath)
        {
            throw new System.NotImplementedException();
        }

        public Task<CommandResult> GetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath)
        {
            throw new System.NotImplementedException();
        }
    }
}