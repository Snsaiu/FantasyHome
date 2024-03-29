using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyHomeCenter.DevicePluginInterface;
using Newtonsoft.Json;
using System.ComponentModel.Composition;
namespace Weather
{
    [Export(typeof(DeviceControllerBase))]
    public class WeatherExport:DeviceControllerBase
    {
        public override List<DeviceInputParameter> CreateInitInputParameters()
        {

            List<DeviceInputParameter> parameters = new List<DeviceInputParameter>();
            parameters.Add(new DeviceInputParameter("当天天气url",true,"请求今天天气的地址,形如http://demo或者http://192.179.2.3:9090"));
            parameters.Add(new DeviceInputParameter("3天天气url",true,"请求未来天气的地址,形如http://demo或者http://192.179.2.3:9090"));
            parameters.Add(new DeviceInputParameter("经度",true,"经度"));
            parameters.Add(new DeviceInputParameter("纬度",true,"纬度"));
            parameters.Add(new DeviceInputParameter("Key",true,"key"));
            return parameters;
        }

      

        protected override ControlUI GetDeskTopControlUi(DeviceMetaOutput initData)
        {
            return new WindowUI(initData);
        }

        public override string Topic { get=>"0F30C03D-45B2-D764-0632-592B78FBC9A1"; }
        public override List<DeviceInputParameter> CreateSetDeviceParameters()
        {
            List<DeviceInputParameter> parameters = new List<DeviceInputParameter>();
            parameters.Add(new DeviceInputParameter("当天天气url",true,"请求今天天气的地址,形如http://demo或者http://192.179.2.3:9090"));
            parameters.Add(new DeviceInputParameter("3天天气url",true,"请求未来天气的地址,形如http://demo或者http://192.179.2.3:9090"));
            parameters.Add(new DeviceInputParameter("经度",true,"经度"));
            parameters.Add(new DeviceInputParameter("纬度",true,"纬度"));
            parameters.Add(new DeviceInputParameter("Key",true,"key"));
            return parameters;
        }

        public override List<DeviceInputParameter> CreateGetDeviceParameters()
        {
            List<DeviceInputParameter> parameters = new List<DeviceInputParameter>();
            parameters.Add(new DeviceInputParameter("当天天气url",true,"请求今天天气的地址,形如http://demo或者http://192.179.2.3:9090"));
            parameters.Add(new DeviceInputParameter("3天天气url",true,"请求未来天气的地址,形如http://demo或者http://192.179.2.3:9090"));
            parameters.Add(new DeviceInputParameter("经度",true,"经度"));
            parameters.Add(new DeviceInputParameter("纬度",true,"纬度"));
            parameters.Add(new DeviceInputParameter("Key",true,"key"));
            return parameters;
        }

        public override string DeviceType { get=>"Weather"; }
        public override string DeviceTypeVersion { get=>"v1"; }
        public override string Key { get=>"0F30C03D-45B2-D764-0632-592B78FBC9A1"; }
        public override string Author { get=>"saiu"; }
        public override string Description { get=>"天气预报"; }

        public override BackgroundParam? BackgroundParam => new BackgroundParam("天气预报定时请求", "天气预报定时请求天气状况", "0 0/30 * * * ?");

        public override Task<CommandResult> InitAsync(List<DeviceInputParameter> input, string pluginPath)
        {


            return this.getWeather(input, pluginPath);


        }

        private  Task<CommandResult> getWeather(List<DeviceInputParameter> input, string pluginPath)
        {
            string url =
                $"{input.First(x => x.Name=="当天天气url").Value}location={input.First(x => x.Name=="经度").Value},{input.First(x => x.Name=="纬度").Value}&key={input.First(x=>x.Name=="Key").Value}"; 
            string currentWeatherRes = HttpRequest.GET(url);
            if (string.IsNullOrEmpty(currentWeatherRes))
            {
                return Task.FromResult(new CommandResult() { Success = false, ErrorMessage = "获得天气发生错误" });
            }

            var w = JsonConvert.DeserializeObject<CurrentWeatherModel>(currentWeatherRes);
            if (w.code!="200")
            {
                return Task.FromResult(new CommandResult() { Success = false, ErrorMessage = "获得天气发生错误" });
            }
            CommandResult cr = new CommandResult();
            cr.Success = true;
            cr.Data = new Dictionary<string, string>();
            cr.Data.Add("今日天气",w.now.text);
            cr.Data.Add("今日温度",w.now.temp);
            cr.Data.Add("湿度",w.now.humidity);
            cr.Data.Add("风速", w.now.windSpeed);
            cr.Data.Add("图标", w.now.icon);

            // 获得未来三天天气

            string threeurl = $"{input.First(x => x.Name == "3天天气url").Value}location={input.First(x => x.Name == "经度").Value},{input.First(x => x.Name == "纬度").Value}&key={input.First(x => x.Name == "Key").Value}";
            string theeday = HttpRequest.GET(threeurl);
            if (string.IsNullOrEmpty(theeday))
            {
                return Task.FromResult(new CommandResult() { Success = false, ErrorMessage = "获得未来天气发生错误" });
            }
            var models = JsonConvert.DeserializeObject<MulitWeatherModel>(theeday);
            if (models.code=="200")
            {
                cr.Data.Add("明天天气图标", models.daily[1].iconNight);
                cr.Data.Add("后天天气图标", models.daily[2].iconNight);

            }
            else
            {
                return Task.FromResult(new CommandResult() { Success = false, ErrorMessage = "获得未来天气发生错误" });
            }


            return Task.FromResult(cr);
        }

        protected override Task<CommandResult> SetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath)
        {
            return this.getWeather(input, pluginPath);
        }

        public override Task<CommandResult> GetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath)
        {
            return this.getWeather(input, pluginPath);
        }

        public override List<PropertyModel> GetDeviceProperties()
        {
           List<PropertyModel> result = new List<PropertyModel>();
            result.Add(new PropertyModel("今日天气"));
            result.Add(new PropertyModel("今日温度"));
            result.Add(new PropertyModel("湿度"));
            result.Add(new PropertyModel("风速"));
            result.Add(new PropertyModel("图标"));
            result.Add(new PropertyModel("明天天气图标"));
            result.Add(new PropertyModel("后天天气图标"));
            return result;
        }
    }
}