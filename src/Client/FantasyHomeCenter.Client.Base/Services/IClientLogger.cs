// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace FantasyHomeCenter.Client.Base
{
    public interface IClientLogger
    {
        void Debug(string msg, int? code = null, Exception ex = null);
        void Fatal(string msg, int? code = null, Exception ex = null, bool sendNotify = true);
        void Error(string msg, int? code = null, Exception ex = null, bool sendNotify = true);
        void Info(string msg, int? code = null, Exception ex = null, bool sendNotify = false);
        void Warn(string msg, int? code = null, Exception ex = null, bool sendNotify = true);
    }
}
