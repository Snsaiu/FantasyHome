using AntDesign;

using FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class Automation
{

    #region 字段


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

    }

    #endregion



}