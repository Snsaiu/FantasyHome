﻿// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace FantasyHomeCenter.Client.Base
{
    public class TableSearchField
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Type Type { get; set; }

        public string Value { get; set; }

        public IEnumerable<string> Values { get; set; }

        public bool Multiple { get; set; }
    }
}
