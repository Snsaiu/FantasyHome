// -----------------------------------------------------------------------------
// ���´����� Furion Tools v1.0.0 ����                                          
// -----------------------------------------------------------------------------

using Furion.DatabaseAccessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

#nullable disable

namespace Gardener.Core.Entites
{
    /// <summary>
    /// �û���չ��Ϣ��
    /// </summary>
    public class UserExtension : IEntity<MasterDbContextLocator>, IEntityTypeBuilder<UserExtension, MasterDbContextLocator>
    {
        /// <summary>
        /// �û�ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// ΢�ź�
        /// </summary>
        public string WeChat { get; set; }
        /// <summary>
        /// ����ID
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public DateTime CreatedTime { get; set; }
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
            entityBuilder.HasKey(e => e.UserId)
                .HasName("PRIMARY");

            entityBuilder.HasComment("�û���չ��");

            entityBuilder.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasComment("�û�id");

            entityBuilder.Property(e => e.CityId).HasComment("����id");

            entityBuilder.Property(e => e.CreatedTime)
                .HasMaxLength(6)
                .HasComment("����ʱ��").IsRequired();

            entityBuilder.Property(e => e.QQ)
                .HasColumnType("varchar(15)")
                .HasComment("QQ");
            //.HasCharSet("utf8mb4")
            //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.Property(e => e.WeChat)
                    .HasColumnType("varchar(20)")
                    .HasComment("΢��");
            //.HasCharSet("utf8mb4")
            //.HasCollation("utf8mb4_0900_ai_ci");

            entityBuilder.HasOne(d => d.User)
                .WithOne(p => p.UserExtension)
                .HasForeignKey<UserExtension>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }

    }
}

