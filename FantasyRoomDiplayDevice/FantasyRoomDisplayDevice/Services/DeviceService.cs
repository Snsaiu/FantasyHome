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
        private readonly ILogger logger;


        private IDeviceApplication deviceApplication;
        public DeviceService(TempConfigService tempConfigService,ILogger logger)
        {
            this.deviceApplication = new DeviceApplication();
            this.tempConfigService = tempConfigService;
            this.logger = logger;
        }
        
        public ResultBase<List<DevicePluginMetaOutput>> DownloadPlugins()
        {
            if (Directory.Exists(this.tempConfigService.PluginFolder))
            {
                this.logger.Info($"存在{this.tempConfigService.PluginFolder},对此文件夹进行移除");
                Directory.Delete(this.tempConfigService.PluginFolder,true);
                this.logger.Info($"移除{this.tempConfigService.PluginFolder}文件夹");
            }

            Directory.CreateDirectory(this.tempConfigService.PluginFolder);
            this.logger.Info($"创建文件夹{this.tempConfigService.PluginFolder}");
            
            // 获得集合
            this.logger.Info("向服务器请求以获得设备插件列表");
           var requestListRes=  this.deviceApplication.GetDeviceMetas(new DeviceMetaInput()
                { Host = this.tempConfigService.Host, Port = this.tempConfigService.Port });
           if (requestListRes.Succeeded==false)
           {
               this.logger.Error($"向服务器获得插件列表发生错误:{requestListRes.Errors.ToString()}");
               return new ResultBase<List<DevicePluginMetaOutput>> { Succeeded = false, Errors = requestListRes.Errors };
           }
           this.logger.Info("向服务器请求以获得设备插件列表成功");

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

                     this.logger.Info($"下载{item.Key}插件完成，保存到{zip}路径");
                   
                     if (File.Exists(zip))
                     {
                         this.logger.Info($"开始解压插件{zip}");
                       string folder=  Path.Combine(this.tempConfigService.PluginFolder,
                             Guid.NewGuid().ToString().Substring(1, 5));
                       Directory.CreateDirectory(folder);
                         Tools.Unzip(zip,folder);
                      
                         File.Delete(zip);
                         this.logger.Info($"解压插件{zip}完成,删除现在原文件{zip}");
                     }
                     else
                     {
                         this.logger.Error($"插件解压失败！没有找到文件!");
                         return new ResultBase<List<DevicePluginMetaOutput>>() { Succeeded = false, Errors ="插件解压失败！没有找到文件!" };
                     }
                 }
                 else
                 {
                     this.logger.Error($"下载插件{item.Key}失败,错误消息:{downloadRes.Errors.ToString()}");
                     return new ResultBase<List<DevicePluginMetaOutput>>() { Succeeded = false, Errors = downloadRes.Errors };
                 }
               }

               this.logger.Info("下载插件完成");
               return new ResultBase<List<DevicePluginMetaOutput>>() { Succeeded = true, Data = requestListRes.Data };
           }
           else
           {
               this.logger.Error("没有找到插件信息");
               return new ResultBase<List<DevicePluginMetaOutput>>() { Succeeded = false, Errors = "没有找到插件信息" };
           }
        }
    }
}