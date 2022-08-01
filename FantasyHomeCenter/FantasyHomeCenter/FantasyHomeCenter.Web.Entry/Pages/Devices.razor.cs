using System.Diagnostics;
using AntDesign;
using FantasyHomeCenter.Application.DeviceCenter;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.Application.RoomCenter;
using FantasyHomeCenter.Application.RoomCenter.Dto;
using FantasyHomeCenter.Core.Enums;
using FantasyHomeCenter.DevicePluginInterface;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class Devices
{
    [Inject] private IDeviceService deviceService { get; set; }

    [Inject] private IDeviceTypeService deviceTypeService { get; set; }

    [Inject] private IRoomService roomService { get; set; }

    private PagedList<DeviceOutput> data = new PagedList<DeviceOutput>();

    private DeviceInput condition = new DeviceInput(1, 20);

    private ITable table;

    /// <summary>
    /// 是否显示设置状态的弹框
    /// </summary>
    private bool setStateVisible = false;

    /// <summary>
    /// 设置状态表单
    /// </summary>
    private Form<SetDeviceStateInput> setStateForm;

    /// <summary>
    /// 设置状态对话框等待
    /// </summary>
    private bool setStateLoading = false;

    /// <summary>
    /// 弹框
    /// </summary>
    [Inject]
    private MessageService messageService { get; set; }

    /// <summary>
    /// 添加设备弹框
    /// </summary>
    private bool addDeviceDialog = false;

    /// <summary>
    /// 添加新设备弹框form实例
    /// </summary>
    private Form<AddDeviceInput> addDeviceForm;

    /// <summary>
    /// 路由管理器
    /// </summary>
    [Inject]
    private NavigationManager navigationManager { get; set; }

    /// <summary>
    /// 添加新设备模型
    /// </summary>
    private AddDeviceInput addDeviceInputModel = new AddDeviceInput();

    /// <summary>
    /// 设置设备状态模型
    /// </summary>
    private SetDeviceStateInput setDeviceStateInputModel = new SetDeviceStateInput();

    /// <summary>
    /// 设备类型和房间集合
    /// </summary>
    private DeviceTypesAndRoomsOutput deviceTypesAndRoomsOutput = new DeviceTypesAndRoomsOutput();


    /// <summary>
    /// 添加新设备弹框请求等待进度
    /// </summary>
    private bool addDeviceLoading = false;

    protected async override Task OnInitializedAsync()
    {
        await this.pageChanged(null);
    }

    IEnumerable<DeviceOutput> selectedRows;

    public void RemoveSelection(string key)
    {
    }

    /// <summary>
    /// 请求列表
    /// </summary>
    /// <param name="typeid"></param>
    private async Task pageChanged(int? typeid)
    {
        var res = await this.deviceService.GetDevices(this.condition);

        if (res.Succeeded)
        {
            this.data = res.Data;
        }
        else
        {
            this.navigationManager.NavigateTo("/error");
        }
    }

    /// <summary>
    /// 打开添加设备对话框
    /// </summary>
    private async Task openAddDeviceDialog()
    {
        var res = await this.deviceService.GetDeviceTypesAndRoomsAsync();


        if (res.Succeeded)
        {
            this.deviceTypesAndRoomsOutput = res.Data;
            if (this.deviceTypesAndRoomsOutput.DeviceTypes.Count == 0)
            {
                await this.messageService.Error("当前没有设备类型可用，您需要先到设备类型页面添加设备类型！");
                return;
            }

            if (this.deviceTypesAndRoomsOutput.Rooms.Count == 0)
            {
                await this.messageService.Error("当前没有房间可用，您需要先到房间页面添加房间！");
                return;
            }

            this.deviceTypeSelectChangedHandle(this.deviceTypesAndRoomsOutput.DeviceTypes.First());
            this.addDeviceDialog = true;
        }
        else
        {
            await this.messageService.Error(res.Errors.ToString());
        }
    }

    /// <summary>
    /// 添加新设备 设备类型选择框选择改变处理
    /// </summary>
    /// <param name="devieType"></param>
    public async void deviceTypeSelectChangedHandle(DeviceTypeOutput deviceType)
    {
        this.addDeviceInputModel.Parameters = new List<DeviceConstCommandParamsOutput>();
        this.addDeviceInputModel.InitCommandParams = new();
        this.addDeviceInputModel.SetCommandParams = new();
        this.addDeviceInputModel.GetCommandParams = new();
        string key = deviceType.Key;
        var controllerRes = await this.deviceTypeService.GetDeviceControllerByKey(key);
        if (controllerRes.Succeeded)
        {
            var controller = controllerRes.Data;
            var initList = controller.CreateInitInputParameters();
            foreach (var item in initList)
            {
                if (item.ConstValue)
                {
                    var p = new DeviceConstCommandParamsOutput();
                    p.Name = item.Name;
                    p.Value = item.Value;
                    p.Type = CommandParameterType.Init;
                    if (item.ValueHasEnums)
                    {
                        p.List = item.ValueEnums.Select(x => new KeyValue<string, string>()
                        {
                            Key = x.Key,
                            Value = x.Value
                        }).ToList();

                        p.IsList = true;
                    }

                    this.addDeviceInputModel.InitCommandParams.Add(new KeyValue<string, string>() { Key = p.Name });
                    this.addDeviceInputModel.Parameters.Add(p);
                }
            }

            var setList = controller.CreateSetDeviceParameters();
            foreach (var item in setList)
            {
                if (item.ConstValue)
                {
                    var p = new DeviceConstCommandParamsOutput();
                    p.Type = CommandParameterType.Set;
                    p.Name = item.Name;
                    p.Value = item.Value;
                    if (item.ValueHasEnums)
                    {
                        p.List = item.ValueEnums.Select(x => new KeyValue<string, string>()
                        {
                            Key = x.Key,
                            Value = x.Value
                        }).ToList();
                        p.IsList = true;
                    }

                    this.addDeviceInputModel.SetCommandParams.Add(new KeyValue<string, string>() { Key = p.Name });
                    this.addDeviceInputModel.Parameters.Add(p);
                }
            }

            var getList = controller.CreateGetDeviceParameters();
            foreach (var item in getList)
            {
                if (item.ConstValue)
                {
                    var p = new DeviceConstCommandParamsOutput();
                    p.Type = CommandParameterType.Get;
                    p.Name = item.Name;
                    p.Value = item.Value;
                    if (item.ValueHasEnums)
                    {
                        p.List = item.ValueEnums.Select(x => new KeyValue<string, string>()
                        {
                            Key = x.Key,
                            Value = x.Value
                        }).ToList();

                        p.IsList = true;
                    }

                    this.addDeviceInputModel.GetCommandParams.Add(new KeyValue<string, string>() { Key = p.Name });
                    ;
                    this.addDeviceInputModel.Parameters.Add(p);
                }
            }
        }
        else
        {
            await this.messageService.Error(controllerRes.Errors.ToString());
        }
    }


    /// <summary>
    /// 设置设备状态
    /// </summary>
    /// <param name="context"></param>
    private async Task submitSetDeviceState(EditContext context)
    {
        
        this.setStateLoading = true;


        var parameters = new List<DeviceInputParameter>();

        foreach (var item in this.setDeviceStateInputModel.SetCommandConstParams)
        {
            parameters.Add(new DeviceInputParameter(item.Key,item.Value));
        }

        foreach (var item in this.setDeviceStateInputModel.Values)
        {
            parameters.Add(new DeviceInputParameter(item.Name,item.Value));
        }
        
        var res= await  this.setDeviceStateInputModel.Controller.SetDeviceStateWithNotifyAsync(this.setDeviceStateInputModel.DeviceName,parameters,
            this.setDeviceStateInputModel.PluginPath);
        if (res.Success)
        {
            await this.messageService.Success("设置成功!");
        }
        else
        {
            await this.messageService.Error(res.ErrorMessage);
          
        }
        this.setDeviceStateInputModel = new SetDeviceStateInput();
        this.setStateVisible = false;

    }

    /// <summary>
    ///  设置状态 按钮点击
    /// </summary>
    /// <param name="id"></param>
    private async Task setState(int id, int typeid, string deviceName, string room)
    {
        this.setStateVisible = true;
        this.setStateLoading = true;
        this.setDeviceStateInputModel = new SetDeviceStateInput();
        //获得当前状态
        var paramRes= await  this.deviceService.GetGetDeviceCommandParamsbyDeviceId(id);

        var p = new List<DeviceInputParameter>();

        foreach (var item in paramRes.Data)
        {
            p.Add(new DeviceInputParameter(item.Key,item.Value)); 
        }
        var controllerRes = await this.deviceTypeService.GetDeviceControllerById(typeid);
        if (controllerRes.Succeeded == false)
        {
            await this.messageService.Error(controllerRes.Errors.ToString());
            return;
        }

        this.setDeviceStateInputModel.Room = room;
        this.setDeviceStateInputModel.DeviceName=deviceName;
        //获得控制器的字段
        var controller = controllerRes.Data;
        this.setDeviceStateInputModel.Controller = controller;
        //获得插件路径
        var pluginPathRes = await this.deviceTypeService.GetDeviceTypePluginPathById(typeid);
        var deviceState=await  controller.GetDeviceStateAsync(p,pluginPathRes.Data);
        this.setDeviceStateInputModel.PluginPath = pluginPathRes.Data;
        
        if (deviceState.Success == false)
        {
            await this.messageService.Error(deviceState.ErrorMessage);
            return;
        }

        foreach (var item in deviceState.Data)
        {
            this.setDeviceStateInputModel.CurrentStates.Add(new KeyValue<string, string>()
            {
                Key = item.Key,
                Value = item.Value
            });
          
        }
        // 获得必填参数
       var SetCommandConstParamsRes= await  this.deviceService.GetSetDeviceCommandParamsByDeviceId(id);
       if (SetCommandConstParamsRes.Succeeded)
       {
           this.setDeviceStateInputModel.SetCommandConstParams = SetCommandConstParamsRes.Data;
       }
       
        StateHasChanged();
        
       
        var setlist = controller.CreateSetDeviceParameters();
        if (setlist != null)
        {
            this.setDeviceStateInputModel.Values.AddRange(setlist.Where(x => x.ConstValue == false).Select(x =>
                new DeviceConstCommandParamsOutput()
                {
                    Name = x.Name,
                    IsList = x.ValueHasEnums,
                    List = x.ValueEnums.Select(y => new KeyValue<string, string>()
                    {
                        Key = y.Key,
                        Value = y.Value
                    }).ToList(),
                    Value = x.Value,
                    Type = CommandParameterType.Init

                }).ToList());
        }


     
      this.setStateLoading = false;
    }

    /// <summary>
    /// 添加新设备请求
    /// </summary>
    /// <param name="context"></param>
    private async Task submitAddDeviceHandle(EditContext context)
    {
        this.addDeviceLoading = true;
        RESTfulResult<int> res = await this.deviceService.AddDeviceAsync(this.addDeviceInputModel);
        this.addDeviceLoading = false;
        if (res.Succeeded)
        {
            this.addDeviceInputModel = new AddDeviceInput();
            await pageChanged(null);
            await this.messageService.Success("创建设备完成!");
        }
        else
        {
            await this.messageService.Error(res.Errors.ToString());
        }
    }

    private async Task submitAddDeviceFail(EditContext context)
    {
     
    }

    /// <summary>
    /// 删除列表中某一行数据
    /// </summary>
    /// <param name="id"></param>
    private async Task deleteItem(int id)
    {
    }
}