using AntDesign;
using FantasyHomeCenter.Application.DeviceCenter;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.Application.SensorDeviceCenter;
using FantasyHomeCenter.Application.SensorDeviceCenter.Dto;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class DeviceTypes
{

    #region 属性字段


    /// <summary>
    /// 扫描设备弹框是否显示
    /// </summary>
    private bool scanDeviceDialogVisible = false;
    
    /// <summary>
    /// 显示添加设备类型
    /// </summary>
    private bool addDeviceTypeDialog = false;

    /// <summary>
    /// 设备类型列表
    /// </summary>
    private Table<DeviceTypeOutput> table;

    /// <summary>
    /// 列表数据源头
    /// </summary>
    private PagedList<DeviceTypeOutput> data = new PagedList<DeviceTypeOutput>();

    /// <summary>
    /// 被选中的设备类型集合
    /// </summary>
    private IEnumerable<DeviceTypeOutput> selectedRows = new List<DeviceTypeOutput>();
    
    /// <summary>
    /// 配置管理器
    /// </summary>
    [Inject]
    private IConfiguration configuration { get; set; }
    
    /// <summary>
    /// 传感器设备服务
    /// </summary>
    [Inject]
    private ISensorDeviceService sensorDeviceService { get; set; }


    /// <summary>
    /// 加载表格数据时候等待条
    /// </summary>
    private bool loadTableLoading = false;
    
    /// <summary>
    /// 添加设备类型表单
    /// </summary>
    private Form<AddDeviceTypeInput> addDeviceTypeForm;

    private Upload upload;

    /// <summary>
    /// 是否可以提交添加新设备
    /// </summary>
    private bool canSubmitAddDeviceType = false;
        
    
    /// <summary>
    /// 路由管理器
    /// </summary>
    [Inject]
    private NavigationManager navigationManager { get; set; }

    /// <summary>
    /// 添加新的设备类型模型
    /// </summary>
    private AddDeviceTypeInput addDeviceTypeInputModel = new AddDeviceTypeInput();

    private DeviceTypeInput deviceTypeInputCondition = new DeviceTypeInput()
    {
        PageIndex = 1,
        PageSize = 10
    };


    /// <summary>
    /// 设备类型服务
    /// </summary>
    [Inject]
    private IDeviceTypeService deviceTypeService { get; set; }
    /// <summary>
    /// 添加新设备类型等待动画
    /// </summary>
    private bool addDeviceTypeLoading = false;

    /// <summary>
    /// 上传地址
    /// </summary>
    private string uploadPath = "";
    
    
    /// <summary>
    /// 消息弹框
    /// </summary>
    [Inject]
    private MessageService messageService { get; set; }


    /// <summary>
    /// 未被注册的扫描到的设备
    /// </summary>
    private List<ScanDeviceOutputDto> unregitScanDevices = new();

    #endregion

    #region 方法


    /// <summary>
    /// 获得没有扫描设备
    /// </summary>
    private void openScanDeviceDialogHandle()
    {
        this.scanDeviceDialogVisible = true;
        var notRegistScanDevices = this.sensorDeviceService.GetNotRegistScanDevices();
        if (notRegistScanDevices.Succeeded)
        {
            this.unregitScanDevices = notRegistScanDevices.Data;
        }
        else
        {
            this.messageService.Error(notRegistScanDevices.Errors.ToString());
        }
    }

    protected async override Task OnInitializedAsync()
    {
        this.uploadPath = this.configuration["PluginRootUrl"] + "api/device-type/upload-plugin";
        await this.pageChanged();
    }
    

    /// <summary>
    /// 提交添加新设备类型表单完成
    /// </summary>
    /// <param name="context"></param>
    private async Task submitAddDeviceTypeHandle(EditContext context)
    {

        if (this.canSubmitAddDeviceType == false)
        {
            await this.messageService.Error("请上传插件包，只有解析成功才能提交");
            return;
        }
      
        var  add_res = await this.deviceTypeService.AddDeviceTypeAsync(this.addDeviceTypeInputModel);
        if (add_res.Succeeded)
        {
            await this.pageChanged();
            await this.messageService.Success("添加成功!");
            this.addDeviceTypeDialog = false;
          
        }
        else
        {
          await this.messageService.Error(add_res.Errors.ToString());
        }
    }

    /// <summary>
    /// 提交添加新设备类型表达错误
    /// </summary>
    /// <param name="editContext"></param>
    private async Task submitAddDeviceTypeFail(EditContext editContext)
    {
    
    }

    private async Task pageChanged()
    {
        this.loadTableLoading = true;
        var data_res= await this.deviceTypeService.GetListPageAsync(this.deviceTypeInputCondition);
        if (data_res.Succeeded)
        {
            this.data = data_res.Data;
            this.loadTableLoading = false;
        }
        else
        {
            this.loadTableLoading = false;
            this.navigationManager.NavigateTo("/error");
        }
    }


    /// <summary>
    /// 删除一条数据
    /// </summary>
    /// <param name="id"></param>
    private async Task remoteDeviceTypeById(int id)
    {
        
     var res_data=  await  this.deviceTypeService.DeleteDeviceTypeList(new List<int>() { id });
     if (res_data.Succeeded)
     {
        await this.messageService.Success("删除成功");
        await this.pageChanged();
     }
     else
     {
        await this.messageService.Error("删除失败");
     }
    }

    private void uploadChanged(UploadInfo item)
    {
       
    }

    private bool beforeUploadDevicePlugin(UploadFileItem item)
    {
        if (string.IsNullOrWhiteSpace(this.addDeviceTypeInputModel.DeviceTypeName))
        {
            this.messageService.Error("请先填写设备设备类型名称");
            return false;
        }

        if (item.Ext != ".zip")
        {
            this.messageService.Error("插件是以zip包存储，请导入zip后缀的文件");
            return false;
        }
        // this.upload.FileList = new List<UploadFileItem>() { item };
        // this.upload.ShowUploadList = true;
        // this.upload.FileList.Clear();
        // this.upload.FileList.Add(item);
        return true;
    }
    private void upLoadSuccess(UploadInfo model)
    {
        var res= JsonConvert.DeserializeObject<RESTfulResult<AddDevicePluginOutput>>(model.File.Response);
        if (res.Succeeded)
        {
             var data = res.Data;
             this.addDeviceTypeInputModel.Author = data.Author;
             this.addDeviceTypeInputModel.PluginPath = data.Path;
             this.addDeviceTypeInputModel.PluginDescription = data.Description;
             this.addDeviceTypeInputModel.PluginName = data.PluginName;
             this.addDeviceTypeInputModel.Version = data.Version;
             this.addDeviceTypeInputModel.Key = data.Key;
            canSubmitAddDeviceType = true;
        }
        else
        {
            canSubmitAddDeviceType = false;
            this.messageService.Error(res.Errors.ToString());
        }
    }

    #endregion



}