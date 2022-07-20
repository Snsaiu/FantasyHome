using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyHomeCenter.Database.Migrations.Migrations
{
    public partial class v2000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceTypeName = table.Column<string>(type: "text", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    PluginPath = table.Column<string>(type: "text", nullable: true),
                    PluginName = table.Column<string>(type: "text", nullable: true),
                    Author = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<string>(type: "text", nullable: true),
                    PluginDescription = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoomName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UiDeviceType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeName = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UiDeviceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    TypeId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    Config = table.Column<string>(type: "TEXT", nullable: true),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Device_DeviceType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "DeviceType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Device_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UiDevice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FamilyId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceCode = table.Column<string>(type: "TEXT", nullable: false),
                    Ip = table.Column<string>(type: "TEXT", nullable: true),
                    UiDeviceTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UiDevice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UiDevice_Family_FamilyId",
                        column: x => x.FamilyId,
                        principalTable: "Family",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UiDevice_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UiDevice_UiDeviceType_UiDeviceTypeId",
                        column: x => x.UiDeviceTypeId,
                        principalTable: "UiDeviceType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Role_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandConstParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    DeviceId = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandConstParams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommandConstParams_Device_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Device",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandConstParams_DeviceId",
                table: "CommandConstParams",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_RoomId",
                table: "Device",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Device_TypeId",
                table: "Device",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_UiDevice_FamilyId",
                table: "UiDevice",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_UiDevice_RoomId",
                table: "UiDevice",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_UiDevice_UiDeviceTypeId",
                table: "UiDevice",
                column: "UiDeviceTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandConstParams");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "UiDevice");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Family");

            migrationBuilder.DropTable(
                name: "UiDeviceType");

            migrationBuilder.DropTable(
                name: "DeviceType");

            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}
