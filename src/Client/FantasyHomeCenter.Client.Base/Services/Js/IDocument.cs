﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.Threading.Tasks;

namespace FantasyHomeCenter.Client.Base
{
    public interface IDocument
    {
        Task SetTitle(string title);
    }
}