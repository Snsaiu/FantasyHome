using System;
using System.Collections.Generic;
using System.IO;
using FantasyHome.Application.Dto;
using Newtonsoft.Json;
using RestSharp;

namespace FantasyHome.Application.Impls
{
    public class DeviceApplication:IDeviceApplication
    {
        public ResultBase<string> DownloadPlugin(DownloadPluginInput input)
        {
            try
            {
                var client = new RestClient($"http://{input.Host}:{input.Port}/api/control-device/plugin-download/{input.Key}");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                byte[] byteArray = client.DownloadData(request);
           

             
                using (FileStream fs = new FileStream(input.SavePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    fs.Close();
                }

                return new ResultBase<string>() { Succeeded = true };
            }
            catch (Exception e)
            {
                return new ResultBase<string>() { Succeeded = false,Errors = e.Message};
            }
      
        }

        public ResultBase<List<DevicePluginMetaOutput>> GetDeviceMetas(DeviceMetaInput input)
        {
            var client = new RestClient($"http://{input.Host}:{input.Port}/api/control-device/plugins-meta");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return JsonConvert.DeserializeObject<ResultBase<List<DevicePluginMetaOutput>>>(response.Content); 
        }
    }
}