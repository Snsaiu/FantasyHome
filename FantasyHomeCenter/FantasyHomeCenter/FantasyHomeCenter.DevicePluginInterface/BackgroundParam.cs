using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyHomeCenter.DevicePluginInterface
{
    /// <summary>
    /// 后台任务参数
    /// </summary>
    public class BackgroundParam
    {



        public BackgroundParam(string taskName, string description, string time)
        {
            TaskName = taskName;
            Description = description;
            Time = time;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get;  }

        /// <summary>
        /// 任务描述
        /// </summary>
        public string Description { get;  }

        /// <summary>
        /// 循环间隔时间，使用cron表达式
        /// </summary>
        public string Time { get;  }
    }
}
