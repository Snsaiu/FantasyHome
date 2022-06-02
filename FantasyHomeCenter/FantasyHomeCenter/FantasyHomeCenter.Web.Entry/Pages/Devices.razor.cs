using AntDesign;
using FantasyHomeCenter.Application.DeviceCenter;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using FantasyHomeCenter.Application.RoomCenter;
using FantasyHomeCenter.Application.RoomCenter.Dto;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class Devices
{
    
    [Inject]
    private IDeviceService deviceService { get; set; }
    
    [Inject]
    private IDeviceTypeService deviceTypeService { get; set; }
    
    [Inject]
    private IRoomService roomService { get; set; }

    private PagedList<DeviceOutput> data=new PagedList<DeviceOutput>();

    private DeviceInput condition = new DeviceInput(1, 20);

    private ITable table;
    
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
     
       
        var res= await  this.deviceService.GetDevices(this.condition);

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
            this.addDeviceDialog = true;
        }
        else
        {
           await this.messageService.Error(res.Errors.ToString());
        }
        
      
        
    }

    /// <summary>
    /// 添加新设备请求
    /// </summary>
    /// <param name="context"></param>
    private async Task submitAddDeviceHandle(EditContext context)
    {
        
        this.addDeviceLoading = true;
       RESTfulResult<int> res= await  this.deviceService.AddDeviceAsync(this.addDeviceInputModel);
       this.addDeviceLoading = false;
       if (res.Succeeded)
       {
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