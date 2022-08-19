using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using FantasyHomeCenter.DevicePluginInterface;

namespace TestPlugin
{
    [Export(typeof(DeviceControllerBase))]
    public class PluginTestExport:DeviceControllerBase
    {
        public override List<DeviceInputParameter> CreateInitInputParameters()
        {
           List<DeviceInputParameter> inputParameters = new List<DeviceInputParameter>();
           DeviceInputParameter inputParameter = new DeviceInputParameter("设置间隔", "1");
           inputParameters.Add(inputParameter);
           return   inputParameters;
        }

        public override BackgroundParam? BackgroundParam { get=> new BackgroundParam("时间累加","时间累加", "0/1 * * * * ?"); }
        protected override ControlUI GetDeskTopControlUi(DeviceMetaOutput initData)
        {
            return new DemoUserControl(initData);
        }

        public override string Topic { get=>"plugintestexport"; }
        public override List<DeviceInputParameter> CreateSetDeviceParameters()
        {
            DeviceInputParameter deviceInputParameter = new DeviceInputParameter();
            deviceInputParameter.Name = "设置间隔";
            deviceInputParameter.Value = "1";

            List<DeviceInputParameter> res = new List<DeviceInputParameter>();
            res.Add(deviceInputParameter);
            return res;
        }

        public override List<DeviceInputParameter> CreateGetDeviceParameters()
        { 
           
            return new List<DeviceInputParameter>();
        }

        public override string DeviceType { get=>"TestPlugin"; }
        public override string DeviceTypeVersion { get=>"v1.0"; }
        public override string Key { get=>"BB1E214B-BD73-BCA4-1F8B-D879EDA388EF"; }
        public override string Author { get=>"saiu"; }
        public override string Description { get=>"数值累加器测试插件"; }
        public override Task<CommandResult> InitAsync(List<DeviceInputParameter> input, string pluginPath)
        {

            var data = new CommandResult();
            data.Success = true;
            data.Data = new Dictionary<string, string>();
            data.Data.Add("当前数值","1");
            return Task.FromResult<CommandResult>(data);
        }

        protected override Task<CommandResult> SetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath)
        {
           var inputvalue=  input.First(x => x.Name == "设置间隔");
           string v= inputvalue.Value;
           TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
           var timespan= Convert.ToInt64(ts.TotalSeconds).ToString();
           var addTimeSpan = timespan + int.Parse(v);
           
           var data = new CommandResult();
           data.Success = true;
           data.Data = new Dictionary<string, string>();
           data.Data.Add("当前数值",addTimeSpan);
           return Task.FromResult<CommandResult>(data);

        }

        public override Task<CommandResult> GetDeviceStateAsync(List<DeviceInputParameter> input, string pluginPath)
        {
            
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var timespan= Convert.ToInt64(ts.TotalSeconds).ToString();
            var addTimeSpan = timespan;
           
            var data = new CommandResult();
            data.Success = true;
            data.Data = new Dictionary<string, string>();
            data.Data.Add("当前数值",addTimeSpan);
            return Task.FromResult<CommandResult>(data);
        }

        public override List<PropertyModel> GetDeviceProperties()
        {

            List<PropertyModel> list = new List<PropertyModel>();
            list.Add(new PropertyModel("当前数值"));
            return list;
        }
    }
}
