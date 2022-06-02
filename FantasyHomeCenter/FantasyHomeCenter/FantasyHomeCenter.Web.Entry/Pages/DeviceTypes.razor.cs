using AntDesign;
using FantasyHomeCenter.Application.DeviceCenter.Dto;
using Microsoft.AspNetCore.Components.Forms;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class DeviceTypes
{

    #region 属性字段

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
    private IEnumerable<DeviceTypeOutput> data = new List<DeviceTypeOutput>();

    /// <summary>
    /// 被选中的设备类型集合
    /// </summary>
    private IEnumerable<DeviceTypeOutput> selectedRows = new List<DeviceTypeOutput>();


    /// <summary>
    /// 添加设备类型表单
    /// </summary>
    private Form<AddDeviceTypeInput> addDeviceTypeForm;

    /// <summary>
    /// 添加新的设备类型模型
    /// </summary>
    private AddDeviceTypeInput addDeviceTypeInputModel = new AddDeviceTypeInput();


    /// <summary>
    /// 添加新设备类型等待动画
    /// </summary>
    private bool addDeviceTypeLoading = false;

    #endregion

    #region 方法


    /// <summary>
    /// 提交添加新设备类型表单完成
    /// </summary>
    /// <param name="context"></param>
    private async Task submitAddDeviceTypeHandle(EditContext context)
    {
        
    }

    /// <summary>
    /// 提交添加新设备类型表达错误
    /// </summary>
    /// <param name="editContext"></param>
    private async Task submitAddDeviceTypeFail(EditContext editContext)
    {
        
    }
    
    
    #endregion



}