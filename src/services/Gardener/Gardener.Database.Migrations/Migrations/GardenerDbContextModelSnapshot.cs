﻿// <auto-generated />
using System;
using Gardener.EntityFramwork.Core.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gardener.Database.Migrations.Migrations
{
    [DbContext(typeof(GardenerDbContext))]
    partial class GardenerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Gardener.Core.Entites.Box", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("No")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Section")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Week")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Box");
                });

            modelBuilder.Entity("Gardener.Core.Entites.ClassHourSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("ClassHourSetting");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Classes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("GradeId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Curriculum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4");

                    b.Property<string>("SubjectId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Curriculum");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Fill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("BoxId")
                        .HasColumnType("int");

                    b.Property<string>("ClassName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClassesId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CurriculumId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CurriculumName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TeacherId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("TeacherName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("BoxId");

                    b.ToTable("Fill");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("Gardener.Core.Entites.SchoolYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SchoolYear");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Semester");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CardID")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateEntrance")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsFinish")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CardID")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateEntrance")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32) CHARACTER SET utf8mb4");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("WorkYears")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("Gardener.Core.Entites.TutorialScheduleRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ForObject")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("No")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RuleSort")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("TutorialScheduleRule");
                });

            modelBuilder.Entity("Gardener.Core.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Icon")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500) CHARACTER SET utf8mb4");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UniqueName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64) CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Url")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Resource");
                });

            modelBuilder.Entity("Gardener.Core.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("角色id");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasComment("创建时间");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasComment("是否删除");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComment("名称");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasComment("备注");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasComment("更新时间");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b
                        .HasComment("角色表");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "超级管理员",
                            Remark = "拥有所有权限"
                        },
                        new
                        {
                            Id = 2,
                            CreatedTime = new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "普通人",
                            Remark = "没有关联权限"
                        });
                });

            modelBuilder.Entity("Gardener.Core.RoleResource", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("ResourceId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("RoleId", "ResourceId");

                    b.HasIndex("ResourceId");

                    b.ToTable("RoleResource");
                });

            modelBuilder.Entity("Gardener.Core.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("用户id");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)")
                        .HasComment("账号");

                    b.Property<string>("Avatar")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComment("头像");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasComment("创建时间");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("邮箱");

                    b.Property<int>("Gender")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasComment("性别");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)")
                        .HasComment("是否删除");

                    b.Property<string>("Mobile")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("手机");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)")
                        .HasComment("密码");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasComment("更新时间");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b
                        .HasComment("用户表");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Account = "admin",
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 12, 16, 21, 22, 200, DateTimeKind.Unspecified).AddTicks(6016), new TimeSpan(0, 8, 0, 0, 0)),
                            Gender = 0,
                            IsDeleted = false,
                            Password = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Account = "testuser",
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 12, 16, 21, 22, 200, DateTimeKind.Unspecified).AddTicks(6972), new TimeSpan(0, 8, 0, 0, 0)),
                            Gender = 0,
                            IsDeleted = false,
                            Password = "testuser"
                        });
                });

            modelBuilder.Entity("Gardener.Core.UserExtension", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasComment("用户id");

                    b.Property<int?>("CityId")
                        .HasColumnType("int")
                        .HasComment("城市id");

                    b.Property<DateTime>("CreatedTime")
                        .HasMaxLength(6)
                        .HasColumnType("datetime(6)")
                        .HasComment("创建时间");

                    b.Property<string>("QQ")
                        .HasColumnType("varchar(15)")
                        .HasComment("QQ");

                    b.Property<string>("WeChat")
                        .HasColumnType("varchar(20)")
                        .HasComment("微信");

                    b.HasKey("UserId")
                        .HasName("PRIMARY");

                    b.ToTable("UserExtension");

                    b
                        .HasComment("用户扩展表");
                });

            modelBuilder.Entity("Gardener.Core.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1,
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 12, 16, 21, 22, 197, DateTimeKind.Unspecified).AddTicks(5204), new TimeSpan(0, 8, 0, 0, 0))
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 2,
                            CreatedTime = new DateTimeOffset(new DateTime(2020, 11, 12, 16, 21, 22, 197, DateTimeKind.Unspecified).AddTicks(8334), new TimeSpan(0, 8, 0, 0, 0))
                        });
                });

            modelBuilder.Entity("Gardener.Core.Entites.Fill", b =>
                {
                    b.HasOne("Gardener.Core.Entites.Box", null)
                        .WithMany("Fills")
                        .HasForeignKey("BoxId");
                });

            modelBuilder.Entity("Gardener.Core.Resource", b =>
                {
                    b.HasOne("Gardener.Core.Resource", "Parent")
                        .WithMany("Childrens")
                        .HasForeignKey("ParentId");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Gardener.Core.RoleResource", b =>
                {
                    b.HasOne("Gardener.Core.Resource", "Resource")
                        .WithMany("RoleResources")
                        .HasForeignKey("ResourceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gardener.Core.Role", "Role")
                        .WithMany("RoleResources")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resource");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Gardener.Core.UserExtension", b =>
                {
                    b.HasOne("Gardener.Core.User", "User")
                        .WithOne("UserExtension")
                        .HasForeignKey("Gardener.Core.UserExtension", "UserId")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gardener.Core.UserRole", b =>
                {
                    b.HasOne("Gardener.Core.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gardener.Core.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Gardener.Core.Entites.Box", b =>
                {
                    b.Navigation("Fills");
                });

            modelBuilder.Entity("Gardener.Core.Resource", b =>
                {
                    b.Navigation("Childrens");

                    b.Navigation("RoleResources");
                });

            modelBuilder.Entity("Gardener.Core.Role", b =>
                {
                    b.Navigation("RoleResources");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Gardener.Core.User", b =>
                {
                    b.Navigation("UserExtension");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}