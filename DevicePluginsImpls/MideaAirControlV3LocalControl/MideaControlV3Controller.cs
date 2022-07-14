using System.Diagnostics;
using FantasyHomeCenter.DevicePluginInterface;
using Newtonsoft.Json;

namespace MideaAirControlV3LocalControl
{
    public class MideaControlV3Controller: IDeviceController
    {
     

        public string DeviceType { get=> "MideaControlV3Controller"; }
        public string DeviceTypeVersion { get=>"v3"; }
        public string Key { get=> "8E3BB04A-A0AF-454C-805A-E05AC067F9EA"; }
        public string Author { get=>"Saiu";  }
        public string Description { get=>"美的空调局域网控制器";  }

        public List<DeviceInputParameter> CreateGetDeviceParameters()
        {
            List<DeviceInputParameter> p = new List<DeviceInputParameter>();

            p.Add(new DeviceInputParameter("acip", true, "当前空调的ip地址"));
            p.Add(new DeviceInputParameter("acid", true, "当前空调的唯一id"));
            p.Add(new DeviceInputParameter("ack1", true, "当前空调的唯一key"));
            p.Add(new DeviceInputParameter("actoken", true, "当前空调的唯一token"));
            p.Add(new DeviceInputParameter("提示音", false, "设置获得提示音","1",new Dictionary<string, string>
            {
                {"打开","1"},
                {"关闭","0"},
            }));
            p.Add(new DeviceInputParameter("空调状态", false, "设置当前空调是否打开","0",new Dictionary<string, string>
            {
                {"打开","1"},
                {"关闭","0"},
            }));

            p.Add(new DeviceInputParameter("温度", false, "设置空调温度") { Value = "26" });
            p.Add(new DeviceInputParameter("模式", false, "设置空调工作模式","1",new Dictionary<string,string>
            {
                {"自动","1"},
                {"制冷","2"},
                {"干燥","3"},
                {"制热","4"},
                {"仅吹风","5"},

            }));

            return p;
        }

        public List<DeviceInputParameter> CreateInitInputParameters()
        {
            List<DeviceInputParameter> p = new List<DeviceInputParameter>();

     
            p.Add(new DeviceInputParameter("acip", true, "当前空调的ip地址"));
            p.Add(new DeviceInputParameter("acid", true, "当前空调的唯一id"));
            p.Add(new DeviceInputParameter("ack1", true, "当前空调的唯一key"));
            p.Add(new DeviceInputParameter("actoken", true, "当前空调的唯一token"));
            p.Add(new DeviceInputParameter("提示音", false, "设置获得提示音", "1", new Dictionary<string, string>
            {
                {"打开","1"},
                {"关闭","0"},
            }));
            p.Add(new DeviceInputParameter("空调状态", false, "设置当前空调是否打开", "0", new Dictionary<string, string>
            {
                {"打开","1"},
                {"关闭","0"},
            }));

            p.Add(new DeviceInputParameter("温度", false, "设置空调温度"){Value="26"});
            p.Add(new DeviceInputParameter("模式", false, "设置空调工作模式", "1", new Dictionary<string, string>
            {
                {"自动","1"},
                {"制冷","2"},
                {"干燥","3"},
                {"制热","4"},
                {"仅吹风","5"},

            }));

            return p;
        }

        public SyncResult SyncDevices(string content)
        {
            throw new NotImplementedException();
        }

        public string Topic { get=>"aircontrolv3"; }

        public List<DeviceInputParameter> CreateSetDeviceParameters()
        {
            List<DeviceInputParameter> p = new List<DeviceInputParameter>();

            p.Add(new DeviceInputParameter("acip", true, "当前空调的ip地址"));
            p.Add(new DeviceInputParameter("acid", true, "当前空调的唯一id"));
            p.Add(new DeviceInputParameter("ack1", true, "当前空调的唯一key"));
            p.Add(new DeviceInputParameter("actoken", true, "当前空调的唯一token"));
            p.Add(new DeviceInputParameter("提示音", false, "设置获得提示音", "1", new Dictionary<string, string>
            {
                {"打开","1"},
                {"关闭","0"},
            }));
            p.Add(new DeviceInputParameter("空调状态", false, "设置当前空调是否打开", "0", new Dictionary<string, string>
            {
                {"打开","1"},
                {"关闭","0"},
            }));

            p.Add(new DeviceInputParameter("温度", false, "设置空调温度") { Value = "26" });
            p.Add(new DeviceInputParameter("模式", false, "设置空调工作模式", "1", new Dictionary<string, string>
            {
                {"自动","1"},
                {"制冷","2"},
                {"干燥","3"},
                {"制热","4"},
                {"仅吹风","5"},

            }));

            return p;
        }

        public async Task<CommandResult> GetDeviceStateAsync(List<DeviceInputParameter> input,string pluginPath)
        {

            foreach (DeviceInputParameter p in input)
            {
                if (p.ConstValue&&string.IsNullOrEmpty(p.Value))
                {
                    return await Task.FromResult(new CommandResult { Success = false, ErrorMessage = $"{p.Name}不能为空!" });
                }
            }

            string param =
                $"0 {input.First(x => x.Name == "acip").Value} {input.First(x => x.Name == "acid").Value} {input.First(x => x.Name == "ack1").Value} {input.First(x => x.Name == "actoken").Value}";

            ProcessStartInfo startinfo = new ProcessStartInfo(Path.Combine(pluginPath ,"mideaair.exe"),param);
            startinfo.CreateNoWindow = true;
            startinfo.RedirectStandardOutput = true;
            startinfo.RedirectStandardError = true;
            var pp= Process.Start(startinfo);
           await  pp.WaitForExitAsync();
           string content= await pp.StandardOutput.ReadToEndAsync();
           string error=await pp.StandardError.ReadToEndAsync();
           if (string.IsNullOrEmpty(error))
           {
               var model= JsonConvert.DeserializeObject<AirModel>(content);

               Dictionary<string, string> res = new Dictionary<string, string>
               {
                   { "空调状态", model.power_state },
                   { "提示音", model.prompt_tone },
                   { "温度", model.target_temperature },
                   { "模式", model.operational_mode },
                   { "风速", model.fan_speed },
                   { "扫风模式", model.swing_mode },
                   { "节能模式", model.eco_mode },
                   { "强劲模式", model.turbo_mode },
                   { "室内温度", model.indoor_temperature },
                   { "室外温度", model.outdoor_temperature },

               };

               return new CommandResult() { Success = true, Data = res };
            }
           return await Task.FromResult(new CommandResult { Success = false, ErrorMessage = error });

        }

        public async Task<CommandResult> InitAsync(List<DeviceInputParameter> input, string pluginPath)
        {
            foreach (DeviceInputParameter p in input)
            {
                if (p.ConstValue && string.IsNullOrEmpty(p.Value))
                {
                    return await Task.FromResult(new CommandResult { Success = false, ErrorMessage = $"{p.Name}不能为空!" });
                }
            }

            string param =
                $"0 {input.First(x => x.Name == "acip").Value} {input.First(x => x.Name == "acid").Value} {input.First(x => x.Name == "ack1").Value} {input.First(x => x.Name == "actoken").Value}";

            ProcessStartInfo startinfo = new ProcessStartInfo(Path.Combine(pluginPath, "mideaair.exe"), param);
            startinfo.CreateNoWindow = true;
            startinfo.RedirectStandardOutput = true;
            startinfo.RedirectStandardError = true;
            var pp = Process.Start(startinfo);
            await pp.WaitForExitAsync();
            string content = await pp.StandardOutput.ReadToEndAsync();
            string error = await pp.StandardError.ReadToEndAsync();
            if (string.IsNullOrEmpty(error))
            {
                var model = JsonConvert.DeserializeObject<AirModel>(content);
           
                Dictionary<string, string> res = new Dictionary<string, string>
                {
                    { "空调状态", model.power_state },
                    { "提示音", model.prompt_tone },
                    { "温度", model.target_temperature },
                    { "模式", model.operational_mode },
                    { "风速", model.fan_speed },
                    { "扫风模式", model.swing_mode },
                    { "节能模式", model.eco_mode },
                    { "强劲模式", model.turbo_mode },
                    { "室内温度", model.indoor_temperature },
                    { "室外温度", model.outdoor_temperature },

                };

                return new CommandResult() { Success = true, Data = res };
            }
            return await Task.FromResult(new CommandResult { Success = false, ErrorMessage = error });

        }

        public async Task<CommandResult> SetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath)
        {
            foreach (DeviceInputParameter p in input)
            {
                if (p.ConstValue && string.IsNullOrEmpty(p.Value))
                {
                    return await Task.FromResult(new CommandResult { Success = false, ErrorMessage = $"{p.Name}不能为空!" });
                }
            }

            string param =
                $"1 {input.First(x => x.Name == "acip").Value} " +
                $"{input.First(x => x.Name == "acid").Value} " +
                $"{input.First(x => x.Name == "ack1").Value} " +
                $"{input.First(x => x.Name == "actoken").Value} " +
                $"{input.First(x=>x.Name== "提示音").Value} " +
                $"{input.First(x => x.Name == "空调状态").Value} "+
                $"{input.First(x => x.Name == "温度").Value} "+
                $"{input.First(x => x.Name == "模式").Value} ";

            ProcessStartInfo startinfo = new ProcessStartInfo(Path.Combine(pluginPath, "mideaair.exe"), param);
            startinfo.CreateNoWindow = true;
            startinfo.RedirectStandardOutput = true;
            startinfo.RedirectStandardError = true;
            var pp = Process.Start(startinfo);
            await pp.WaitForExitAsync();
            string content = await pp.StandardOutput.ReadToEndAsync();
            string error = await pp.StandardError.ReadToEndAsync();
            if (string.IsNullOrEmpty(error))
            {
                var model = JsonConvert.DeserializeObject<AirModel>(content);

                Dictionary<string, string> res = new Dictionary<string, string>
                {
                    { "空调状态", model.power_state },
                    { "提示音", model.prompt_tone },
                    { "温度", model.target_temperature },
                    { "模式", model.operational_mode },
                    { "风速", model.fan_speed },
                    { "扫风模式", model.swing_mode },
                    { "节能模式", model.eco_mode },
                    { "强劲模式", model.turbo_mode },
                    { "室内温度", model.indoor_temperature },
                    { "室外温度", model.outdoor_temperature },

                };

                return new CommandResult() { Success = true, Data = res };
            }
            return await Task.FromResult(new CommandResult { Success = false, ErrorMessage = error });

        }
    }
}