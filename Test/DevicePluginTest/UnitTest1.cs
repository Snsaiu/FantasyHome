using FantasyHomeCenter.DevicePluginInterface;
using MideaAirControlV3LocalControl;

namespace DevicePluginTest
{
    public class UnitTest1
    {

        private IDeviceController controller;
        public UnitTest1()
        {
            this.controller = new MideaControlV3Controller();
        }

        [Fact]
        public async Task GetAirState()
        {

            
            // python .\example.py 0 192.168.0.100 188016488736458 11C0385DD86F48829E4D94FD93D9A3EA6034BCE22891457DA4F3CBB3A7904E71 CF15CCA3D5EDC796BFDBB694D3EC6EAD860FFFADBADA112DBAEFCB348F3AA54761CBCBA49CED1836F6D8014748A6EE5494523D61941DBF686EE9F8880C340090
         
            List<DeviceInputParameter> p=new List<DeviceInputParameter> ();
            p.Add(new DeviceInputParameter("acip", "192.168.0.100"));
            p.Add(new DeviceInputParameter("acid", "188016488736458"));
            p.Add(new DeviceInputParameter("ack1", "11C0385DD86F48829E4D94FD93D9A3EA6034BCE22891457DA4F3CBB3A7904E71"));
            p.Add(new DeviceInputParameter("actoken", "CF15CCA3D5EDC796BFDBB694D3EC6EAD860FFFADBADA112DBAEFCB348F3AA54761CBCBA49CED1836F6D8014748A6EE5494523D61941DBF686EE9F8880C340090"));

           var res=  await this.controller.GetDeviceStateAsync(p);
        }

        [Fact]
        public async Task SetAirState()
        {
            List<DeviceInputParameter> p = new List<DeviceInputParameter>();
            p.Add(new DeviceInputParameter("acip", "192.168.0.100"));
            p.Add(new DeviceInputParameter("acid", "188016488736458"));
            p.Add(new DeviceInputParameter("ack1", "11C0385DD86F48829E4D94FD93D9A3EA6034BCE22891457DA4F3CBB3A7904E71"));
            p.Add(new DeviceInputParameter("actoken", "CF15CCA3D5EDC796BFDBB694D3EC6EAD860FFFADBADA112DBAEFCB348F3AA54761CBCBA49CED1836F6D8014748A6EE5494523D61941DBF686EE9F8880C340090"));
            p.Add(new DeviceInputParameter("提示音", "1"));
            p.Add(new DeviceInputParameter("空调状态", "1"));
            p.Add(new DeviceInputParameter("温度", "18"));
            p.Add(new DeviceInputParameter("模式", "2"));

           var res=  await this.controller.SetDeviceStateAsync(p);
        }
    }
}