﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System.Collections.Generic;
using System.Reflection;

namespace FantasyHomeCenter.Client.Base
{
    public class ClientModuleContext
    {
        public List<string> ModeuleDlls { get; set; }

        public Assembly[] ModeuleAssemblies { get; set; }
    }
}
