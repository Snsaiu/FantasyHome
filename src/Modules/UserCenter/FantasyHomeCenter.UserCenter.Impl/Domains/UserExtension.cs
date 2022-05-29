// -----------------------------------------------------------------------------
// ԰��,�Ǹ��ܼ򵥵Ĺ���ϵͳ
//  gitee:https://gitee.com/hgflydream/FantasyHomeCenter 
//  issues:https://gitee.com/hgflydream/FantasyHomeCenter/issues 
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel;

#nullable disable

namespace FantasyHomeCenter.UserCenter.Impl.Domains
{
    /// <summary>
    /// �û���չ��Ϣ��
    /// </summary>
    [Description("�û���չ��Ϣ")]
    public class UserExtension : IEntity<MasterDbContextLocator>, IEntityTypeBuilder<UserExtension, MasterDbContextLocator>
    {
        /// <summary>
        /// �û�ID
        /// </summary>
        [DisplayName("�û����")]
        public int UserId { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        [DisplayName("QQ")]
        public string QQ { get; set; }
        /// <summary>
        /// ΢�ź�
        /// </summary>
        [DisplayName("΢��")]
        public string WeChat { get; set; }
        /// <summary>
        /// ����ID
        /// </summary>
        [DisplayName("���б��")]
        public int? CityId { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [DisplayName("����ʱ��")]
        public DateTimeOffset CreatedTime { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        [DisplayName("����ʱ��")]
        public DateTimeOffset? UpdatedTime { get; set; }
        /// <summary>
        /// �û���Ϣ
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="entityBuilder"></param>
        /// <param name="dbContext"></param>
        /// <param name="dbContextLocator"></param>
        public void Configure(EntityTypeBuilder<UserExtension> entityBuilder, DbContext dbContext, Type dbContextLocator)
        {
            entityBuilder.HasKey(e => e.UserId).HasName("PRIMARY");
        }

    }
}

