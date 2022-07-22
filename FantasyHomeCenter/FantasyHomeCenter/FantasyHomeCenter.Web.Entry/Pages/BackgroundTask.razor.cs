using AntDesign;

using FantasyHomeCenter.Application.BackgroundTaskCenter;
using FantasyHomeCenter.Application.BackgroundTaskCenter.Dto;

using Microsoft.AspNetCore.Components;

namespace FantasyHomeCenter.Web.Entry.Pages
{
    public partial class BackgroundTask
    {


        /// <summary>
        /// 后台任务服务
        /// </summary>
        [Inject]
       private IBackgroundTaskService backgroundTaskService{ get; set; }

        /// <summary>
        /// 消息服务
        /// </summary>
        [Inject]
        private MessageService messageService{ get; set; }

        /// <summary>
        /// 后台任务分页列表
        /// </summary>
        private PagedList<BackgroundTaskOutput> list;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            this.list = new PagedList<BackgroundTaskOutput>();
            this.list.PageIndex = 1;
            this.list.PageSize = 10;
            this.getList();
        }

        /// <summary>
        /// 获得列表
        /// </summary>
        private void getList()
        {
            var res= this.backgroundTaskService.GetBackgroundTaskPage(new BackgroundTaskPageInput
            {
                PageIndex=this.list.PageIndex,
                PageSize=this.list.PageSize
            });
            if (res.Succeeded)
            {
                this.list = res.Data;
            }
            else
            {
                this.messageService.Error(res.Errors.ToString());
            }

        }

        /// <summary>
        /// 暂停指定任务
        /// </summary>
        /// <param name="taskName"></param>
        private void stopTask(string taskName)
        {
            var res= this.backgroundTaskService.StopTaskByName(taskName);
            if (res.Succeeded)
            {
                this.list.Items.First(x => x.TaskName == taskName).TaskState = "暂停";
            }
            else
            {
                this.messageService.Error(res.Errors.ToString());
            }
        }

        private void startTask(string taskName)
        {
            var res = this.backgroundTaskService.RestartTaskByName(taskName);
            if (res.Succeeded)
            {
                this.list.Items.First(x => x.TaskName == taskName).TaskState = "正在运行";
            }
            else
            {
                this.messageService.Error(res.Errors.ToString());
            }
        }
    }
}
