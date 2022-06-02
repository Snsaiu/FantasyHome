using AntDesign;
using AntDesign.Core.Helpers.MemberPath;
using FantasyHomeCenter.Application.RoomCenter;
using FantasyHomeCenter.Application.RoomCenter.Dto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class Rooms
{
    
    #region 属性字段

    /// <summary>
    /// 显示添加设备类型
    /// </summary>
    private bool addRoomDialog = false;

    /// <summary>
    /// 设备类型列表
    /// </summary>
    private Table<RoomOutput> table;

    /// <summary>
    /// 列表数据源头
    /// </summary>
    private PagedList<RoomOutput> data = new PagedList<RoomOutput>();

    /// <summary>
    /// 被选中的设备类型集合
    /// </summary>
    private IEnumerable<RoomOutput> selectedRows = new List<RoomOutput>();


    /// <summary>
    /// 加载表格数据时候等待条
    /// </summary>
    private bool loadTableLoading = false;
    
    /// <summary>
    /// 添加设备类型表单
    /// </summary>
    private Form<AddRoomInput> addRoomForm;
    
    /// <summary>
    /// 路由管理器
    /// </summary>
    [Inject]
    private NavigationManager navigationManager { get; set; }

    /// <summary>
    /// 添加新的设备类型模型
    /// </summary>
    private AddRoomInput addRoomInputModel = new AddRoomInput();

    private RoomInput roomInputCondition = new RoomInput()
    {
        PageIndex = 1,
        PageSize = 10
    };


    /// <summary>
    /// 设备类型服务
    /// </summary>
    [Inject]
    private IRoomService roomService { get; set; }
    /// <summary>
    /// 添加新设备类型等待动画
    /// </summary>
    private bool addRoomLoading = false;
    
    /// <summary>
    /// 消息弹框
    /// </summary>
    [Inject]
    private MessageService messageService { get; set; }

    #endregion

    #region 方法

    protected async override Task OnInitializedAsync()
    {
        await this.pageChanged();
    }
    

    /// <summary>
    /// 提交添加新设备类型表单完成
    /// </summary>
    /// <param name="context"></param>
    private async Task submitAddRoomHandle(EditContext context)
    {
        this.addRoomLoading = true;
        var  add_res = await this.roomService.AddNewRoomAsync(this.addRoomInputModel);
        if (add_res.Succeeded)
        {
            this.addRoomLoading = false;
            await this.pageChanged();
            await this.messageService.Success("添加成功!");
            this.addRoomDialog = false;
          
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
    private async Task submitAddRoomFail(EditContext editContext)
    {
    
    }

    private async Task pageChanged()
    {
        this.loadTableLoading = true;
        var data_res= await this.roomService.GetRoomListPageAsync(this.roomInputCondition);
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
    private async Task deleteRoomById(int id)
    {
     var res_data=  await  this.roomService.DeleteRoomListAsync(new List<int>() { id });
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

    #endregion


}