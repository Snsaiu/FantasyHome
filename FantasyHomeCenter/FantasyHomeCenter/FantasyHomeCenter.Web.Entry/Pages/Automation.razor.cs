using System.Windows;

using AntDesign;

using FantasyHomeCenter.Application.BackgroundTaskCenter;
using FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;
using FantasyHomeCenter.Application.DeviceCenter;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.DevicePluginInterface;

using Furion.UnifyResult;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using StackExchange.Profiling.Internal;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class Automation
{

    #region 字段


    [Inject]
    private IDeviceTypeService deviceTypeService{ get; set; }

    [Inject]
    private PluginStateChangeNotification pluginStateChangeNotification { get; set; }

    /// <summary>
    /// 所有设备
    /// </summary>
    private List<DeviceOutput> devices = new List<DeviceOutput>();


    /// <summary>
    /// 后台任务
    /// </summary>
    [Inject]
    private IBackgroundTaskService backgroundTaskService{ get; set; }

    /// <summary>
    /// 所选设备的属性
    /// </summary>
    private List<PropertyModel> propertyModels = new List<PropertyModel>();

    /// <summary>
    /// 目标设备属性
    /// </summary>

    private List<PropertyModel> targetPropertyModels = new List<PropertyModel>();

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
    private void deviceSelectChangedHandle(DeviceOutput input)
    {
        RESTfulResult<List<PropertyModel>> res= this.deviceService.GetDeviceControllPropertiesByDeviceTypeId(input.DeviceTypeId);

        this.propertyModels = res.Data;
    
    }

    /// <summary>
    /// 目标选择设备改变
    /// </summary>
    /// <param name="input"></param>
    private async void targetDeviceSelectChangedHanle(DeviceOutput input,ActionInput action)
    {
        //RESTfulResult<List<PropertyModel>> res = this.deviceService.GetDeviceControllPropertiesByDeviceTypeId(input.DeviceTypeId);

        //this.targetPropertyModels = res.Data;
        var res= await this.deviceService.GetSetDeviceCommandParamsByDeviceId(input.DeviceTypeId);
        if (res.Succeeded)
        {
            var data = res.Data;

            action.SetParameters = new List<ActionParams>();
            foreach (var item in data)
            {
                action.SetParameters.Add(new ActionParams() { Name = item.Key, Value = item.Value,ReadOnly=false,IsEnum=false });
            }
         
             var controller=await  this.deviceTypeService.GetDeviceControllerById(input.DeviceTypeId);

             if (controller.Succeeded)
             {
                 var control = controller.Data;
                var setparams=  control.CreateSetDeviceParameters();
                foreach (DeviceInputParameter deviceInputParameter in setparams)
                {
                    if (action.SetParameters.Any(x => x.Name == deviceInputParameter.Name))
                        continue;
                    ActionParams ap = new ActionParams();
                    ap.Name = deviceInputParameter.Name;
                    ap.ReadOnly = false;
                    if (deviceInputParameter.ValueHasEnums)
                    {
                        ap.IsEnum = true;

                        deviceInputParameter.ValueEnums.ForEach(x =>
                        {
                            ap.Items.Add(new KeyValue<string, string>() { Key = x.Key, Value = x.Value });

                        });
                      
                    }
                    else
                    {
                        ap.IsEnum = false;
                    }
                    action.SetParameters.Add(ap);
                }

             }
             else
             {
               await  this.messageService.Error(controller.Errors.ToString());
             }
            
        }
        else
        {
           await this.messageService.Error(res.Errors.ToString());
        }


    }

    /// <summary>
    /// 设备属性改变
    /// </summary>
    /// <param name="x"></param>
    private void propertyChangedHanle(TriggerElementInput x)
    {
        x.BeforeValue = "";
        x.AfterValue = "";
        //this.automationInput.From = "";
        //this.automationInput.To = "";

    }

    private void targetDevicePropertyChangedHandle(ActionInput input)
    {
      //  input.Value = "";
    }


    private void addNewTriggerCondition()
    {
        this.automationInput.Triggers.Add(new TriggerElementInput());
       
    }

    private void removeTriggerCondition(TriggerElementInput input)
    {
        var entity= this.automationInput.Triggers.Where(x => x == input).FirstOrDefault();
        if (entity != null)
        {
            this.automationInput.Triggers.Remove(entity);
        }
       
    }

    private void addAction()
    {
        this.automationInput.Actions.Add(new ActionInput());
    }

    private void removeAction(ActionInput input)
    {
        var entity = this.automationInput.Actions.Where(x => x == input).FirstOrDefault();
        if (entity != null)
        {
            this.automationInput.Actions.Remove(entity);
        }
    }

    #endregion


    private async Task submitAddAutomationHandle(EditContext editContext)
    {
        this.addNewLoading = true;


        this.backgroundTaskService.CreateNewAutomation(this.automationInput);

        this.automationInput = new AutomationInput();
        this.addNewLoading = false;
        this.addNewVisiable = false;
    }
}