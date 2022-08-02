using AntDesign;

using FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;
using FantasyHomeCenter.Application.DeviceCenter;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.DevicePluginInterface;

using Furion.UnifyResult;

using Microsoft.AspNetCore.Components;

using StackExchange.Profiling.Internal;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class Automation
{

    #region 字段
    private int count = 1;

    /// <summary>
    /// 所有设备
    /// </summary>
    private List<DeviceOutput> devices = new List<DeviceOutput>();

    /// <summary>
    /// 所选设备的属性
    /// </summary>
    private List<PropertyModel> propertyModels = new List<PropertyModel>();

    /// <summary>
    /// 设备服务
    /// </summary>
    [Inject]
    private IDeviceService deviceService { get; set; }

    /// <summary>
    /// 消息服务
    /// </summary>
    [Inject]
    private MessageService messageService { get; set; }

    /// <summary>
    /// 显示添加新触发器弹框
    /// </summary>
    private bool addNewVisiable = false;

    /// <summary>
    /// 添加新的触发器等待
    /// </summary>
    private bool addNewLoading = false;

    /// <summary>
    /// 触发器类型
    /// </summary>
    private List<string> triggerType = new List<string>()
    {
        "数字状态触发器","状态触发器","时间规律触发器","区域触发器","坐标触发器","模板触发器","时间触发器"
    };

    /// <summary>
    /// 新增触发器输入模型
    /// </summary>
    private AutomationInput automationInput = new AutomationInput();


    /// <summary>
    /// 添加新的自动化表单
    /// </summary>
    private Form<AutomationInput> addNewForm;

    #endregion


    #region 方法

    /// <summary>
    /// 打开添加新自动化弹框
    /// </summary>
    private void openAddNewDialog()
    {
        
        this.addNewVisiable = true;
        //获得所有列表
        this.getDevices();

    }

    /// <summary>
    /// 获得所有设备列表
    /// </summary>
    /// <returns></returns>
    private void getDevices()
    {
       
        var res= this.deviceService.GetAllDevices();

        if (res.Succeeded)
            this.devices= res.Data;
        else
            this.messageService.Error(res.Errors.ToString());
       

    }

    /// <summary>
    /// 设备选择改变
    /// </summary>
    /// <param name="input"></param>
    private void deviceSelectChangedHanld(DeviceOutput input)
    {
        RESTfulResult<List<PropertyModel>> res= this.deviceService.GetDeviceControllPropertiesByDeviceTypeId(input.DeviceTypeId);

        this.propertyModels = res.Data;
    
    }

    /// <summary>
    /// 设备属性改变
    /// </summary>
    /// <param name="x"></param>
    private void propertyChangedHanle(PropertyModel x)
    {
        this.automationInput.From = "";
        this.automationInput.To = "";

    }


    #endregion



}