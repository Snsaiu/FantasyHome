using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FantasyHome.Application;
using FantasyHome.Application.Dto;
using FantasyHome.Application.Impls;
using FantasyHome.GTool;

namespace FantasyRoomDisplayDevice.Services
{
    public class DeviceService:IDeviceService
    {
        private readonly TempConfigService tempConfigService;


        private IDeviceApplication deviceApplication;
        public DeviceService(TempConfigService tempConfigService)
        {
            this.deviceApplication = new DeviceApplication();
            this.tempConfigService = tempConfigService;
        }
        
        public ResultBase<List<DevicePluginMetaOutput>> DownloadPlugins()
        {
            if (Directory.Exists(this.tempConfigService.PluginFolder))
            {
                Directory.Delete(this.tempConfigService.PluginFolder,true);
            }

            Directory.CreateDirectory(this.tempConfigService.PluginFolder);
            
            // 获得集合
           var requestListRes=  this.deviceApplication.GetDeviceMetas(new DeviceMetaInput()
                { Host = this.tempConfigService.Host, Port = this.tempConfigService.Port });
           if (requestListRes.Succeeded==false)
           {
               return new ResultBase<List<DevicePluginMetaOutput>> { Succeeded = false, Errors = requestListRes.Errors };
           }

           if (requestListRes.Data!=null&& requestListRes.Data.Count!=0)
           {
               foreach (DevicePluginMetaOutput item in requestListRes.Data)
               {
                   string zip = Path.Combine(this.tempConfigService.PluginFolder,
                       Guid.NewGuid().ToString().Substring(1, 5) + ".zip");
                 var downloadRes=  this.deviceApplication.DownloadPlugin(new DownloadPluginInput()
                   {
                       Host = this.tempConfigService.Host,
                       Key = item.Key,
                       Port = this.tempConfigService.Port,
                       SavePath = zip,

                   });
                 if (downloadRes.Succeeded)
                 {

                   
                     if (File.Exists(zip))
                     {
                       string folder=  Path.Combine(this.tempConfigService.PluginFolder,
                             Guid.NewGuid().ToString().Substring(1, 5));
                       Directory.CreateDirectory(folder);
                         Tools.Unzip(zip,folder);
                         
                         File.Delete(zip);
                     }
                     else
                     {
                         return new ResultBase<List<DevicePluginMetaOutput>>() { Succeeded = false, Errors ="插件解压失败！没有找到文件!" };
                     }
                 }
                 else
                 {
                     return new ResultBase<List<DevicePluginMetaOutput>>() { Succeeded = false, Errors = downloadRes.Errors };
                 }
               }

               return new ResultBase<List<DevicePluginMetaOutput>>() { Succeeded = true, Data = requestListRes.Data };
           }
           else
           {
               return new ResultBase<List<DevicePluginMetaOutput>>() { Succeeded = false, Errors = "没有找到插件信息" };
           }
        }
    }
}