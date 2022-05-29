// -----------------------------------------------------------------------------
// 园丁,是个很简单的管理系统
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using FantasyHomeCenter.Attachment.Dtos;
using FantasyHomeCenter.Attachment.Enums;
using FantasyHomeCenter.Client.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Gardener.Attachment.Client.Pages
{
    public partial class AttachmentEdit : EditDrawerBase<AttachmentDto, Guid>
    {

        [Required(ErrorMessage = "业务类型不能为空")]
        private string _currentEditModelBusinessType
        {
            get
            {
                return _editModel.BusinessType.ToString();
            }
            set
            {
                _editModel.BusinessType = (AttachmentBusinessType)Enum.Parse(typeof(AttachmentBusinessType), value);
            }
        }

        [Required(ErrorMessage = "文件类型不能为空")]
        private string _currentEditModelFileType
        {
            get
            {
                return _editModel.FileType.ToString();
            }
            set
            {
                _editModel.FileType = (AttachmentFileType)Enum.Parse(typeof(AttachmentFileType), value);
            }
        }
    }
}
