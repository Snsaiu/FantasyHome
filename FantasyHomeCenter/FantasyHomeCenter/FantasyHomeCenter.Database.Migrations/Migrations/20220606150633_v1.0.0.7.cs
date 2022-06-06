using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyHomeCenter.Database.Migrations.Migrations
{
    public partial class v1007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GetCommandParameter",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "InitCommandParameter",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "SetCommandParameter",
                table: "Device");

            migrationBuilder.CreateTable(
                name: "CommandConstParams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandConstParams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommandConstParamsDevice",
                columns: table => new
                {
                    ConstCommandParamsId = table.Column<int>(type: "int", nullable: false),
                    DevicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandConstParamsDevice", x => new { x.ConstCommandParamsId, x.DevicesId });
                    table.ForeignKey(
                        name: "FK_CommandConstParamsDevice_CommandConstParams_ConstCommandParamsId",
                        column: x => x.ConstCommandParamsId,
                        principalTable: "CommandConstParams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandConstParamsDevice_Device_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Device",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommandConstParamsDevice_DevicesId",
                table: "CommandConstParamsDevice",
                column: "DevicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandConstParamsDevice");

            migrationBuilder.DropTable(
                name: "CommandConstParams");

            migrationBuilder.AddColumn<string>(
                name: "GetCommandParameter",
                table: "Device",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InitCommandParameter",
                table: "Device",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SetCommandParameter",
                table: "Device",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
