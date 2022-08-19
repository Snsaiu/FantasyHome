using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FantasyHomeCenter.DevicePluginInterface;
using Newtonsoft.Json;

namespace MideaAirControlV3LocalControl
{
    [Export(typeof(DeviceControllerBase))]
    public class MideaControlV3Controller: DeviceControllerBase
    {
     

        public override string DeviceType { get=> "MideaAirControlV3LocalControl"; }
        public override string DeviceTypeVersion { get=>"v3"; }
        public override string Key { get=> "8E3BB04A-A0AF-454C-805A-E05AC067F9EA"; }
        public override string Author { get=>"Saiu";  }
        public override string Description { get=>"美的空调局域网控制器";  }

        public override List<DeviceInputParameter> CreateGetDeviceParameters()
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

        public override List<DeviceInputParameter> CreateInitInputParameters()
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

        public override BackgroundParam? BackgroundParam { get=>new BackgroundParam("空调状态检测","空调状态检测任务", "*/30 * * * * *"); }

      

        protected override ControlUI GetDeskTopControlUi(DeviceMetaOutput initData)
        {
            return new AirControlComponent(initData);
        }

        public override string Topic { get=>"aircontrolv3"; }

        public override List<DeviceInputParameter> CreateSetDeviceParameters()
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

        public override async Task<CommandResult> GetDeviceStateAsync(List<DeviceInputParameter> input,string pluginPath)
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
           await  pp?.WaitForExitAsync();
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

        public override async Task<CommandResult> InitAsync(List<DeviceInputParameter> input, string pluginPath)
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

        protected override async Task<CommandResult> SetDeviceStateAsync( List<DeviceInputParameter> input, string pluginPath)
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

        public override List<PropertyModel> GetDeviceProperties()
        {
            List<PropertyModel> res = new List<PropertyModel>();
            res.Add(new PropertyModel("空调状态", new List<string>() { "True", "False" }));
            res.Add(new PropertyModel("提示音", new List<string>() { "True", "False" }));
            res.Add(new PropertyModel("温度"));
            res.Add(new PropertyModel("模式",new List<string>() { "自动","制冷","干燥","制热","仅吹风"}));
            res.Add(new PropertyModel("风速"));
            res.Add(new PropertyModel("室内温度"));
            return res;
        }
    }
}