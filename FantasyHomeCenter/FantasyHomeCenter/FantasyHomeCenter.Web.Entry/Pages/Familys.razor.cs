using AntDesign;
using FantasyHomeCenter.Application.FamilyCenter;
using FantasyHomeCenter.Application.FamilyCenter.Dto;
using Furion.UnifyResult;
using Microsoft.AspNetCore.Components;

namespace FantasyHomeCenter.Web.Entry.Pages;

public partial class Familys
{
    
    [Inject]
    private IFamilyService familyService { get; set; }

    private FamilyPageInput pageinput=new FamilyPageInput()
    {
        PageIndex = 1,
        PageSize = 20,
    };
    
    [Inject]
    private MessageService messageService { get; set; }
    
    private PagedList<FamilyWithDeivesOuput> list = new ();

    /// <summary>
    /// 创建家人模型
    /// </summary>
    private CreateFamilyInput createFamilyInput = new CreateFamilyInput();

    private bool creatingFamilyLoading = false;

    private Form<CreateFamilyInput> createFamilyInputForm;

    /// <summary>
    /// 编辑家人弹框显示
    /// </summary>
    private bool editFamilyDialogVisbible = false;

    /// <summary>
    /// 创建新家庭用户是否显示弹框
    /// </summary>
    private bool createFamilyDialogVisible = false;

    protected override void OnInitialized()
    {
        base.OnInitialized();
       var listResult= this.familyService.GetFamilysWithDevices(pageinput);
       if (listResult.Succeeded)
       {
           this.list = listResult.Data;
           
       }
       else
       {
           this.messageService.Warning(listResult.Errors.ToString());
       }
    }


    private void editFamilyHanle(FamilyWithDeivesOuput input)
    {
        this.editFamilyDialogVisbible = true;
    }
    
    /// <summary>
    /// 添加新家人提交
    /// </summary>
    private void submitCreateFamilyHandle()
    {
        this.creatingFamilyLoading = true;
        RESTfulResult<CreateFamilyOutput> result = this.familyService.CreateNewFamily(this.createFamilyInput);
        if (result.Succeeded)
        {
            var listResult= this.familyService.GetFamilysWithDevices(pageinput);
            if (listResult.Succeeded)
            {
                this.list = listResult.Data;
           
            }
            else
            {
                this.messageService.Warning(listResult.Errors.ToString());
            }
            
           
            
        }
        else
        {
            this.messageService.Error(result.Errors.ToString());
        }

        this.createFamilyInput = new();
        this.creatingFamilyLoading = false;
        this.createFamilyDialogVisible = false;

    }

    private void openCreateNewFamilyDialog()
    {
        this.createFamilyDialogVisible=true;
    }
}