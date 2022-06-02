using AntDesign;
using FantasyHomeCenter.Application.DeviceCenter;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class Devices
{
    
    [Inject]
    private IDeviceService deviceService { get; set; }

    private IEnumerable<DeviceOutput> data=new List<DeviceOutput>();

    private ITable table;
    
    

    private int pageindex = 1;
    private int pagesize = 10;

    /// <summary>
    /// 添加设备弹框
    /// </summary>
    private bool addDeviceDialog = false;

    /// <summary>
    /// 添加新设备弹框form实例
    /// </summary>
    private Form<AddDeviceInput> addDeviceForm;

    /// <summary>
    /// 添加新设备模型
    /// </summary>
    private AddDeviceInput addDeviceInputModel = new AddDeviceInput();

    /// <summary>
    /// 添加新设备弹框请求等待进度
    /// </summary>
    private bool addDeviceLoading = false;
    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
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
        DeviceInput input = null;
        if (typeid != null)
            input = new DeviceInput(pageindex, pagesize, typeid.Value);
        else input = new DeviceInput(pageindex, pagesize);
        var res= await  this.deviceService.GetDevices(input);
        this.data = res.Items;
    }

    /// <summary>
    /// 添加新设备请求
    /// </summary>
    /// <param name="context"></param>
    private async Task submitAddDevicehandle(EditContext context)
    {
        
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