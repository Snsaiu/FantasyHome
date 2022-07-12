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

            string x = "<!DOCTYPE html>\n<html lang=\"en\">\n\n<head>\n    <meta charset=\"UTF-8\">\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\n    <title>Fantasy Home</title>\n</head>\n\n<body>\n    <div style=\"text-align:center;\">\n        <div>\n            <h1>fantasy home 设备配置</h1>\n        </div>\n        <div>\n            <form action=\"/config\" method=\"post\">\n                Wi-FI名称:<br>\n                <input type=\"text\" name=\"wifiName\" value=\"\">\n                <br>\n                Wi-Fi密码:<br>\n                <input type=\"text\" name=\"wifiPwd\" value=\"\">\n                <br><br>\n                服务器地址:<br>\n                <input type=\"text\" name=\"serviceAddress\" value=\"\">\n                <br><br>\n                设备命名:<br>\n                <input type=\"text\" name=\"deviceName\" value=\"\">\n                <br><br>\n                <input type=\"submit\" value=\"Submit\">\n            </form>\n\n        </div>\n    </div>\n\n</body>\n\n</html>";
            
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
            p.Add(new DeviceInputParameter("��ʾ��", "1"));
            p.Add(new DeviceInputParameter("�յ�״̬", "1"));
            p.Add(new DeviceInputParameter("�¶�", "25"));
            p.Add(new DeviceInputParameter("ģʽ", "1"));

           var res=  await this.controller.SetDeviceStateAsync(p);
        }
    }
}