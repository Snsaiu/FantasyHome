﻿// <auto-generated />
using System;
using Gardener.EntityFramwork.Core.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gardener.Database.Migrations.Migrations
{
    [DbContext(typeof(GardenerDbContext))]
    [Migration("20201020065114_v0.0.1")]
    partial class v001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0-rc.2.20475.6");

            modelBuilder.Entity("Gardener.Core.Entities.ClassHourSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ClassHourSetting");
                });

            modelBuilder.Entity("Gardener.Core.Entities.Classes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Gardener.Core.Entities.Curriculum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Curriculum");
                });

            modelBuilder.Entity("Gardener.Core.Entities.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Number")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("Gardener.Core.Entities.SchoolYear", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("SchoolYear");
                });

            modelBuilder.Entity("Gardener.Core.Entities.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Semester");
                });

            modelBuilder.Entity("Gardener.Core.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Avatar")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DateOfDeath")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<int>("Sex")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Gardener.Core.Entities.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Avatar")
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<int>("Sex")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("Gardener.Core.Entities.TutorialScheduleRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TutorialScheduleRule");
                });

            modelBuilder.Entity("Gardener.Core.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Remark")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "超级管理员",
                            Remark = "拥有所有权限"
                        },
                        new
                        {
                            Id = 2,
                            Name = "普通人",
                            Remark = "没有关联权限"
                        });
                });

            modelBuilder.Entity("Gardener.Core.RoleSecurity", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SecurityId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoleId", "SecurityId");

                    b.HasIndex("SecurityId");

                    b.ToTable("RoleSecurity");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            SecurityId = 1
                        },
                        new
                        {
                            RoleId = 1,
                            SecurityId = 2
                        },
                        new
                        {
                            RoleId = 1,
                            SecurityId = 3
                        },
                        new
                        {
                            RoleId = 1,
                            SecurityId = 4
                        },
                        new
                        {
                            RoleId = 1,
                            SecurityId = 5
                        },
                        new
                        {
                            RoleId = 1,
                            SecurityId = 6
                        });
                });

            modelBuilder.Entity("Gardener.Core.Security", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("UniqueName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Security");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UniqueName = "ViewRoles"
                        },
                        new
                        {
                            Id = 2,
                            UniqueName = "ViewSecuries"
                        },
                        new
                        {
                            Id = 3,
                            UniqueName = "GetRoles"
                        },
                        new
                        {
                            Id = 4,
                            UniqueName = "InsertRole"
                        },
                        new
                        {
                            Id = 5,
                            UniqueName = "GiveUserRole"
                        },
                        new
                        {
                            Id = 6,
                            UniqueName = "GiveRoleSecurity"
                        });
                });

            modelBuilder.Entity("Gardener.Core.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Account")
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Account = "admin",
                            Password = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Account = "Fur",
                            Password = "dotnetchina"
                        });
                });

            modelBuilder.Entity("Gardener.Core.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("Gardener.Core.RoleSecurity", b =>
                {
                    b.HasOne("Gardener.Core.Role", "Role")
                        .WithMany("RoleSecurities")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gardener.Core.Security", "Security")
                        .WithMany("RoleSecurities")
                        .HasForeignKey("SecurityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Security");
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

            modelBuilder.Entity("Gardener.Core.Role", b =>
                {
                    b.Navigation("RoleSecurities");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Gardener.Core.Security", b =>
                {
                    b.Navigation("RoleSecurities");
                });

            modelBuilder.Entity("Gardener.Core.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
