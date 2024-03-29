﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FantasyHomeCenter.Application.BackgroundTaskCenter;
using FantasyHomeCenter.Application.DeviceCenter;
using FantasyHomeCenter.Application.MqttCenter;
using FantasyHomeCenter.Application.MqttCenter.Dto;
using FantasyHomeCenter.Application.RoomCenter;
using FantasyHomeCenter.Core.Entities;
using FantasyHomeCenter.Core.Enums;
using FantasyHomeCenter.DevicePluginInterface;
using FantasyHomeCenter.EntityFramework.Core.PluginContext;

using Furion;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Furion.TaskScheduler;
using Furion.UnifyResult;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using StackExchange.Profiling.Internal;

namespace FantasyHomeCenter.Web.Core;

public static class Extensions
{

    public static void AddInitPluginService(this IServiceCollection services)
    {
        var provider= services.BuildServiceProvider();
        var repository = provider.GetService<IRepository<DeviceType>>();
        var pluginService= provider.GetService<IPluginService>();
        var deviceTypes= repository.AsQueryable().ToList();
        foreach (DeviceType item in deviceTypes)
        {
            if (string.IsNullOrEmpty(item.Key)==false)
            {
                pluginService.AddPluginAsync(item.PluginPath, item.PluginName);
            }
        }
    }

    public static async void  AddBackgroundTaskService(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        IRepository<Device> deviceRepository = provider.GetService<IRepository<Device>>();
        IRepository <DeviceType> deviceTypeRepository=provider.GetService<IRepository<DeviceType>>();

        PluginStateChangeNotification notify=provider.GetService<PluginStateChangeNotification>();
     
        var deviceTypeService = provider.GetService<IDeviceTypeService>();
      
        //获得所有插件

        var deviceTypes = deviceTypeRepository.AsQueryable().Include(x=>x.Devices).ThenInclude(x=>x.ConstCommandParams) .ToList();

        foreach (DeviceType deviceType in deviceTypes)
        {
            var deviceControllRes = await deviceTypeService.GetDeviceControllerById(deviceType.Id);

            if (!deviceControllRes.Succeeded)
            {
                Console.WriteLine($"加载插件{deviceType.Key}失败");
                return;

            }

           
            var controller = deviceControllRes.Data;

            notify.AddPlugins(controller);
            controller.Regist(notify);
            

            foreach (Device device in deviceType.Devices)
            {
                if (controller.BackgroundParam != null)
                {

                    try
                    {

                  
                    SpareTime.Do(controller.BackgroundParam.Time, async (time, count)  =>
                    {

                        try
                        {

                      
                       await Scoped.Create(async (_, scope) =>
                        {
                            var ser = scope.ServiceProvider;
                            System.Console.WriteLine(device.Name + "___" + controller.BackgroundParam.TaskName + "::任务执行" + count + "次");
                            var getlist = device.ConstCommandParams.Where(x => x.Type == CommandParameterType.Get).ToList();
                            List<DeviceInputParameter> inputs = new List<DeviceInputParameter>();

                            if (getlist != null)
                            {
                                foreach (var item in getlist)
                                {
                                    inputs.Add(new DeviceInputParameter(item.Name, item.Value));
                                }
                            }
                            var getRes = await controller.GetDeviceStateAsync(inputs, deviceType.PluginPath);
                            if (getRes.Success)
                            {
                                MqttSendInfo info = new MqttSendInfo();
                                info.Data = getRes.Data;

                                info.DeviceName = device.Name;
                                info.Success = true;
                                info.PluginKey = deviceType.Key;
                                info.Topic=deviceType.Key;
                                //mqtt 发送
                                MqttServerInstance mqttServerInstance = ser.GetService<MqttServerInstance>();
                                Console.WriteLine($"定时信息:{device.Name}::{JsonConvert.SerializeObject(getRes.Data)}");
                                await mqttServerInstance.PublishAsync(info.Topic, info);

                            }
                            else
                            {
                                System.Console.WriteLine(device.Name + "___" + controller.BackgroundParam.TaskName + "::任务执行发生错误" + getRes.ErrorMessage);
                            }



                        });
                        }
                        catch (Exception e)
                        {
                           
                        }



                    }, device.Name + "___" + controller.BackgroundParam.TaskName, controller.BackgroundParam.Description);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }

            }



        }
    }




    public  static  void AddMqttServiceAsync(this IServiceCollection services)
    {
        
        var provider= services.BuildServiceProvider();
       var mqtt=  provider.GetService<MqttServerInstance>();
       mqtt.StartAsync();
    }

    /// <summary>
    /// 添加自动化服务
    /// </summary>
    /// <param name="services"></param>
    public static void AddAutomationTaskAsync(this IServiceCollection services)
    {
        var provider= services.BuildServiceProvider();
        var automationService = provider.GetService<IBackgroundTaskService>();
        
        automationService.RigAutomatioinTask();
        
    }


    public static async void AddSendRoomToControlDevice(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        IRoomService roomService=provider.GetService<IRoomService>();
        MqttServerInstance mqttServerInstance=provider.GetService<MqttServerInstance> ();

       var roomListRes=  await roomService.GetListAsync ();
       if (roomListRes.Succeeded)
       {
          List<string> rooms=roomListRes.Data.Select(x=>x.RoomName).ToList();
           Dictionary<string, string> data = new Dictionary<string, string>();
           data["rooms"] = rooms.ToJson();
          await mqttServerInstance.PublishAsync("fantasyhome-room-list", new MqttSendInfo
           {
               Topic="fantasyhome-room-list",
               Data= data
           });

       }
       else
       {

       }
    }
}