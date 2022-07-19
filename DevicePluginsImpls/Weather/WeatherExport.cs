using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyHomeCenter.DevicePluginInterface;
using Newtonsoft.Json;

namespace Weather
{
    public class WeatherExport:IDeviceController
    {
        public List<DeviceInputParameter> CreateInitInputParameters()
        {

            List<DeviceInputParameter> parameters = new List<DeviceInputParameter>();
            parameters.Add(new DeviceInputParameter("CurrentDatyWeatherUrl",true,"请求今天天气的地址,形如http://demo或者http://192.179.2.3:9090"));
            parameters.Add(new DeviceInputParameter("FiveDatyWeatherUrl",true,"请求未来天气的地址,形如http://demo或者http://192.179.2.3:9090"));
            parameters.Add(new DeviceInputParameter("Longitude",true,"经度"));
            parameters.Add(new DeviceInputParameter("Latitude",true,"纬度"));
            parameters.Add(new DeviceInputParameter("Key",true,"key"));
            return parameters;
        }

        public ControlUI GetDeskTopControlUi(object initData)
        {
            var data = JsonConvert.DeserializeObject<DeviceMetaOutput>(initData.ToString());
            return new WindowUI(data);
        }

        public string Topic { get=>"0F30C03D-45B2-D764-0632-592B78FBC9A1"; }
        public List<DeviceInputParameter> CreateSetDeviceParameters()
        {
            return new List<DeviceInputParameter>();
        }

        public List<DeviceInputParameter> CreateGetDeviceParameters()
        {
            return new List<DeviceInputParameter>();
        }

        public string DeviceType { get=>"Weather"; }
        public string DeviceTypeVersion { get=>"v1"; }
        public string Key { get=>"0F30C03D-45B2-D764-0632-592B78FBC9A1"; }
        public string Author { get=>"saiu"; }
        public string Description { get=>"天气预报"; }
        public Task<CommandResult> InitAsync(List<DeviceInputParameter> input, string pluginPath)
        {


            return this.getWeather(input, pluginPath);


        }

        private Task<CommandResult> getWeather(List<DeviceInputParameter> input, string pluginPath)
        {
            string url =
                $"{input.First(x => x.Name=="CurrentDatyWeatherUrl").Value}location={input.First(x => x.Name=="Longitude").Value},{input.First(x => x.Name=="Latitude").Value}&key={input.First(x=>x.Name=="Key").Value}"; 
            string currentWeatherRes = HttpRequest.GET(url);
            if (string.IsNullOrEmpty(currentWeatherRes))
            {
                return Task.FromResult(new CommandResult() { Success = false, ErrorMessage = "获得天气发生错误" });
            }

            var w = JsonConvert.DeserializeObject<CurrentWeatherModel>(currentWeatherRes);
            CommandResult cr = new CommandResult();
            cr.Success = true;
            cr.Data = new Dictionary<string, string>();
            cr.Data.Add("今日天气",w.now.text);
            cr.Data.Add("今日温度",w.now.temp);
            cr.Data.Add("紫外线",w.now.humidity);

            return Task.FromResult(cr);
        }

        public Task<CommandResult> SetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath)
        {
            return this.getWeather(input, pluginPath);
        }

        public Task<CommandResult> GetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath)
        {
            return this.getWeather(input, pluginPath);
        }
    }
}