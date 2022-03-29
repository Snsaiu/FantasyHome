﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/Gardener 
//  issues:https://gitee.com/hgflydream/Gardener/issues 
// -----------------------------------------------------------------------------

using AntDesign;
using AntDesign.TableModels;
using Gardener.Base;
using Gardener.Client.Base.Model;
using Mapster;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gardener.Client.Base.Components
{
    /// <summary>
    /// 普通table基类
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class TableBase<TDto,TKey> : ReuseTabsPageBase where TDto:BaseDto<TKey>,new()
    {
        protected ITable _table;
        protected TDto[] _datas;
        protected IEnumerable<TDto> _selectedRows;
        protected int _total = 0;
        protected bool _tableIsLoading = false;
        protected bool _deletesBtnLoading = false;
        protected PageRequest pageRequest = new PageRequest();
        protected List<FilterGroup> _searchFilterGroups =new List<FilterGroup>();
        /// <summary>
        /// 默认搜索值
        /// </summary>
        protected Dictionary<string, object> _defaultSearchValue = new Dictionary<string, object>();

        [Inject]
        protected IServiceBase<TDto, TKey> _service { get; set; }
        [Inject]
        protected MessageService messageService { get; set; }
        [Inject]
        protected ConfirmService confirmService { get; set; }
        [Inject]
        protected DrawerService drawerService { get; set; }
        [Inject]
        protected IClientLocalizer localizer { get; set; }
        [Inject]
        protected NavigationManager navigation { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="pageRequest"></param>
        /// <returns></returns>
        protected virtual PageRequest ConfigurationPageRequest(PageRequest pageRequest)
        {
            return pageRequest;
        }
        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            _tableIsLoading = true;
            await base.OnInitializedAsync();
        }
        /// <summary>
        /// 组件渲染后
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (firstRender)
            {
                this.firstRenderAfter = true;
                await ReLoadTable();
                await InvokeAsync(StateHasChanged);
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override async Task OnParametersSetAsync()
        {
            var url = new Uri(navigation.Uri);
            var query = url.Query;
            Dictionary<string, StringValues> urlParams= QueryHelpers.ParseQuery(query);
            if (urlParams != null && urlParams.Count() > 0)
            {
                urlParams.ForEach(x => 
                {
                    _defaultSearchValue.Add(x.Key,x.Value.ToString());
                });
            }

            await base.OnParametersSetAsync();
        }

        /// <summary>
        /// 首次渲染后
        /// </summary>
        bool firstRenderAfter = false;

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <returns></returns>
        protected virtual async Task ReLoadTable()
        {
            await ReLoadTable(false);
        }

        /// <summary>
        /// 重置请求参数
        /// </summary>
        /// <param name="firstPage">是否重置为首页</param>
        /// <returns></returns>
        protected virtual PageRequest ReSetPageRequest(bool firstPage = true)
        {
            pageRequest = _table?.GetPageRequest() ?? new PageRequest();
            if (firstPage)
            {
                pageRequest.PageIndex = 1;
            }

            //如果有搜索条件 就拼接上
            if (_searchFilterGroups != null && _searchFilterGroups.Count > 0)
            {
                pageRequest.FilterGroups.AddRange(_searchFilterGroups);
            }
            pageRequest = ConfigurationPageRequest(pageRequest);
            return pageRequest;
        }

        /// <summary>
        /// 重新加载table
        /// </summary>
        /// <param name="firstPage">是否从首页加载</param>
        /// <returns></returns>
        protected virtual async Task ReLoadTable(bool firstPage=true)
        {
            _tableIsLoading = true;
            PageRequest pageRequest = ReSetPageRequest(firstPage);
            var pagedListResult = await _service.Search(pageRequest);
            if (pagedListResult != null)
            {
                var pagedList = pagedListResult;
                _datas = pagedList.Items.ToArray();
                _total = pagedList.TotalCount;
            }
            else
            {
                messageService.Error(localizer.Combination("加载","失败"));
            }
            _tableIsLoading = false;
        }
        
        /// <summary>
        /// 查询变化
        /// </summary>
        /// <param name="queryModel"></param>
        /// <returns></returns>
        protected virtual async Task OnChange(QueryModel<TDto> queryModel)
        {
            if (firstRenderAfter)
            {
                await ReLoadTable();
            }
        }

        /// <summary>
        /// 点击删除按钮
        /// </summary>
        /// <param name="id"></param>
        protected virtual async Task OnClickDelete(TKey id)
        {
            if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
            {
                var result = await _service.FakeDelete(id);
                if (result)
                {
                    await ReLoadTable();
                    _datas = _datas.Remove(_datas.FirstOrDefault(x => x.Id.Equals(id)));
                    //当前页被删完了
                    if(pageRequest.PageIndex>1 && _datas.Length==0)
                    {
                        pageRequest.PageIndex = pageRequest.PageIndex - 1;
                    }
                    await ReLoadTable();
                    messageService.Success(localizer.Combination("删除", "成功"));
                }
                else
                {
                    messageService.Error(localizer.Combination("删除", "失败"));
                }
            }

        }

        /// <summary>
        /// 点击删除选中按钮
        /// </summary>
        protected virtual async Task OnClickDeletes()
        {
            if (_selectedRows == null || _selectedRows.Count() == 0)
            {
                messageService.Warn(localizer["未选中任何行"]);
            }
            else
            {
                _deletesBtnLoading = true;
                if (await confirmService.YesNoDelete() == ConfirmResult.Yes)
                {
                    var result = await _service.FakeDeletes(_selectedRows.Select(x => x.Id).ToArray());
                    if (result)
                    {
                        //删除整页
                        if (_selectedRows.Count() == pageRequest.PageSize && pageRequest.PageIndex * pageRequest.PageSize >= _total)
                        {
                            pageRequest.PageIndex = pageRequest.PageIndex - 1;
                        }
                        await ReLoadTable();
                        messageService.Success(localizer.Combination("删除", "成功"));
                    }
                    else
                    {
                        messageService.Error(localizer.Combination("删除", "失败"));
                    }
                }
                _deletesBtnLoading = false;
            }
        }

        /// <summary>
        /// 点击锁定按钮
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isLocked"></param>
        protected virtual async Task OnChangeIsLocked(TDto model, bool isLocked)
        {
            var result = await _service.Lock(model.Id, isLocked);
            if (!result)
            {
                model.IsLocked = !isLocked;
                string msg = isLocked ? localizer["锁定"] : localizer["解锁"];

                messageService.Error($"{msg} {localizer["失败"]}");
            }
        }
        /// <summary>
        /// 设置其他过滤信息
        /// </summary>
        /// <param name="searchValues"></param>
        /// <returns></returns>
        protected virtual async Task SetOtherFilterGroups(List<FilterGroup> filterGroups)
        {
            _searchFilterGroups = filterGroups;
        }
    }
    /// <summary>
    /// 普通table基类
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TDrawer"></typeparam>
    public abstract class TableBase<TDto, TKey, TDrawer> : TableBase<TDto, TKey> where TDto : BaseDto<TKey>, new() where TDrawer : FeedbackComponent<DrawerInput<TKey>, DrawerOutput<TKey>>
    {
        /// <summary>
        /// 抽屉配置
        /// </summary>
        /// <returns></returns>
        protected virtual DrawerSettings GetDrawerSettings()
        {
            return new DrawerSettings { Width = 500 };
        }
        
        /// <summary>
        /// 点击添加按钮
        /// </summary>
        protected async Task OnClickAdd()
        {
            DrawerSettings drawerSettings = GetDrawerSettings();
            DrawerInput<TKey> input = DrawerInput<TKey>.IsAdd();
            var result = await drawerService.CreateDialogAsync<TDrawer, DrawerInput<TKey>, DrawerOutput<TKey>>(
                input,
                closable: drawerSettings.Closable,
                title: localizer["添加"], 
                width: drawerSettings.Width,
                placement: drawerSettings.Placement.ToString().ToLower());

            if (result.Succeeded)
            {
                //刷新列表
                pageRequest.PageIndex = 1;
                await ReLoadTable();
            }
        }
        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="model"></param>
        protected async Task OnClickEdit(TKey id)
        {
            DrawerSettings drawerSettings = GetDrawerSettings();
            DrawerInput<TKey> input = DrawerInput<TKey>.IsEdit(id);
            var result = await drawerService.CreateDialogAsync<TDrawer, DrawerInput<TKey>, DrawerOutput<TKey>>(
                input, 
                closable: drawerSettings.Closable, 
                true, 
                title: localizer["编辑"], 
                width: drawerSettings.Width,
                placement: drawerSettings.Placement.ToString().ToLower());

            if (result.Succeeded)
            {
                await ReLoadTable();
            }
        }

        /// <summary>
        /// 点击编辑按钮
        /// </summary>
        /// <param name="roleDto"></param>
        protected async Task OnClickDetail(TKey id)
        {
            DrawerSettings drawerSettings = GetDrawerSettings();
            DrawerInput<TKey> input = DrawerInput<TKey>.IsSelect(id);
            var result = await drawerService.CreateDialogAsync<TDrawer, DrawerInput<TKey>, DrawerOutput<TKey>>(
                input, 
                closable: drawerSettings.Closable,
                true, 
                title: localizer["详情"],
                width: drawerSettings.Width, 
                placement: drawerSettings.Placement.ToString().ToLower());
        }

        /// <summary>
        /// 种子数据
        /// </summary>
        /// <typeparam name="TShowSeedDataDrawer">展示种子数据抽屉</typeparam>
        /// <returns></returns>
        protected async Task OnClickShowSeedData<TShowSeedDataDrawer>() where TShowSeedDataDrawer : FeedbackComponent<string, bool>
        {
            PageRequest pageRequest = ReSetPageRequest(false);
            PageRequest pageRequestTemp = new PageRequest();
            pageRequest.Adapt(pageRequestTemp);
            pageRequestTemp.PageSize=int.MaxValue;
            pageRequestTemp.PageSize = 1;
            string seedData=await _service.GenerateSeedData(pageRequestTemp);
            var result = await drawerService.CreateDialogAsync<TShowSeedDataDrawer, string, bool>(
                      seedData,
                       true,
                       title: localizer["种子数据"],
                       width: 1300,
                       placement: "right");
        }
    }
}
