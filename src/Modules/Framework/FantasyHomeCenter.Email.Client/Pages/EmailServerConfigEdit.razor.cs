// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Client.Base;
using FantasyHomeCenter.Email.Dtos;
using FantasyHomeCenter.Email.Enums;
using System;
using System.Collections.Generic;

namespace FantasyHomeCenter.Email.Client.Pages
{
    public partial class EmailServerConfigEdit : EditDrawerBase<EmailServerConfigDto, Guid>
    {
        private IEnumerable<string> _tags
        {
            get
            {
                return _editModel.Tags?.Split(",");
            }
            set
            {
                _editModel.Tags = string.Join(",", value);

            }
        }

    }
}
