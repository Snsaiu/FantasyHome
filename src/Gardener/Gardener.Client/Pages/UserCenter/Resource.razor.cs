﻿// -----------------------------------------------------------------------------
// 文件头
// -----------------------------------------------------------------------------

using AntDesign;
using Gardener.Application.Dtos;
using Gardener.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mapster;
using System.Linq;
using System;
using Gardener.Enums;

namespace Gardener.Client.Pages.UserCenter
{
    public partial class Resource
    {
        List<ResourceDto> resourceDtos = new List<ResourceDto>();
        private bool treeIsLoading;


        [Inject]
        IResourceService ResourceService { get; set; }
        [Inject]
        MessageService MessageService { get; set; }
        [Inject]
        ConfirmService ConfirmSvr { get; set; }
        Tree tree;
        private bool allExpanded;

        /// <summary>
        /// 页面初始化完成
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            treeIsLoading = true;
            var resourceResult = await ResourceService.GetTree();
            if (resourceResult.Successed)
            {
                resourceDtos.AddRange(resourceResult.Data);
            }
            else
            {
                MessageService.Error("节点加载失败");
            }
            treeIsLoading = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        private async Task Expand(List<TreeNode> nodes,bool flag)
        {
            foreach (var node in nodes)
            {
                node.Expand(flag);
                if (node.ChildNodes != null && node.ChildNodes.Count > 0) 
                {
                    await Expand(node.ChildNodes, flag);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task OnExpandClick()
        {
            allExpanded = !allExpanded;
           await Expand(tree.ChildNodes, allExpanded);
        }
        /// <summary>
        /// 加载子节点
        /// </summary>
        /// <param name="args"></param>
        private async Task OnNodeLoadDelayAsync(TreeEventArgs args)
        {
            treeIsLoading = true;
            var parentNode = ((ResourceDto)args.Node.DataItem);
            parentNode.Children = new List<ResourceDto>();
            var resourceResult = await ResourceService.GetChildren(parentNode.Id);
            if (resourceResult.Successed)
            {
                resourceResult.Data?.ForEach(x =>
                {
                    if (x.Children == null)
                    {
                        x.Children = new List<ResourceDto>();
                    }
                    parentNode.Children.Add(x);
                });
            }
            else
            {
                MessageService.Error("节点加载失败");
            }
            treeIsLoading = false;
        }
        /// <summary>
        /// 点击节点
        /// </summary>
        /// <param name="args"></param>
        private async Task OnNodeClick(TreeEventArgs args)
        {
            formIsLoading = true;
            ((ResourceDto)args.Node.DataItem).Adapt(editModel);
            formIsLoading = false;
        }
        private bool drawerVisible;
        private bool formIsLoading;
        private string drawerTitle = string.Empty;
        private ResourceDto editModel = new ResourceDto();
        /// <summary>
        /// 删除选中节点
        /// </summary>
        /// <returns></returns>
        private async Task OnDeleteSelectedNodeClick()
        {
            var selectedNode = tree.SelectedNodes?.FirstOrDefault();
            if (selectedNode != null)
            {
                var resource = ((ResourceDto)selectedNode.DataItem);
                if (resource.Type.Equals(ResourceType.ROOT))
                {
                    MessageService.Error("根节点无法删除");
                    treeIsLoading = false;
                    return;
                }
                treeIsLoading = true;
                if (await ConfirmSvr.YesNoDelete() == ConfirmResult.Yes)
                {
                   
                    List<int> ids = GetAllDeleteResourceId(resource);
                    var result = await ResourceService.FakeDeletes(ids.ToArray());
                    if (result.Successed)
                    {
                        var parentNode = ((ResourceDto)selectedNode.ParentNode.DataItem);
                        parentNode.Children.Remove(resource);
                        MessageService.Success("删除成功");
                    }
                    else
                    {
                        MessageService.Error("删除失败");
                    }
                }
                treeIsLoading = false;
            }
            else
            {
                MessageService.Warn("请先选择节点");
            }
        }
        /// <summary>
        /// 递归获取所有要删除的id
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        private List<int> GetAllDeleteResourceId(ResourceDto resource)
        {

            List<int> result = new List<int>() { resource.Id };

            if (resource.Children == null || !resource.Children.Any())
            {
                return result;
            }
            resource.Children.ForEach(x =>
            {

                result.AddRange(GetAllDeleteResourceId(x));
            });

            return result;
        }
        /// <summary>
        /// 编辑选中节点
        /// </summary>
        /// <returns></returns>
        private async Task OnEditSelectedNodeClick()
        {
            var selectedNode = tree.SelectedNodes?.FirstOrDefault();
            if (selectedNode != null)
            {
                var resource = ((ResourceDto)selectedNode.DataItem);
                resource.Adapt(editModel);
                drawerTitle = "编辑";
                drawerVisible = true;
            }
            else
            {
                MessageService.Warn("请先选择节点");
            }
        }
        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <returns></returns>
        private async Task OnAddChildNodeClick()
        {
            var selectedNode = tree.SelectedNodes?.FirstOrDefault();
            if (selectedNode != null)
            {
                drawerTitle = "添加";
                var pResource = ((ResourceDto)selectedNode.DataItem);
                var newNode = new ResourceDto();
                newNode.ResourceId = Guid.NewGuid().ToString();
                newNode.ParentId = pResource.Id;
                newNode.Order = (pResource.Children == null || !pResource.Children.Any() ? 0 : pResource.Children.Last().Order + 1);
                newNode.Adapt(editModel);
                drawerVisible = true;
            }
            else
            {
                MessageService.Warn("请先选择父节点");
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="editContext"></param>
        /// <returns></returns>
        private async Task OnFormFinish(EditContext editContext)
        {
            formIsLoading = true;
            var selectedNode = tree.SelectedNodes?.FirstOrDefault();
            var resource = ((ResourceDto)selectedNode.DataItem);
            if (editModel.Id > 0)
            {
                //更新
                var result = await ResourceService.Update(editModel);
                if (result.Successed)
                {
                    editModel.Adapt(resource);
                    var parentResource= (ResourceDto)selectedNode.ParentNode.DataItem;
                    parentResource.Children = parentResource.Children.OrderBy(x => x.Order).ToList();
                    MessageService.Success("更新成功");
                    drawerVisible = false;
                }
                else
                {
                    MessageService.Error("更新失败");
                }
            }
            else
            {
                //新增
                var result = await ResourceService.Insert(editModel);
                if (result.Successed)
                {
                    if (resource.Children == null)
                    {
                        resource.Children = new List<ResourceDto>();
                    }
                    resource.Children.Add(result.Data);

                    resource.Children = resource.Children.OrderBy(x => x.Order).ToList();

                    MessageService.Success("添加成功");
                    drawerVisible = false;
                }
                else
                {
                    MessageService.Error("添加失败");
                }
            }
            formIsLoading = false;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="editContext"></param>
        private async Task OnFormFinishFailed(EditContext editContext)
        {

        }
        /// <summary>
        /// 表单取消
        /// </summary>
        private async Task OnFormCancel()
        {
            //new ResourceDto().Adapt(editModel);
            drawerVisible = false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task OnDrawerClose()
        {
            drawerVisible = false;
        }
    }
}
