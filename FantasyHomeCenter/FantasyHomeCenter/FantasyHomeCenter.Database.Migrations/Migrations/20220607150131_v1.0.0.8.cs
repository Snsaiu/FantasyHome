using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyHomeCenter.Database.Migrations.Migrations
{
    public partial class v1008 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommandConstParamsDevice");

            migrationBuilder.AddColumn<int>(
                name: "DeviceId",
                table: "CommandConstParams",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommandConstParams_DeviceId",
                table: "CommandConstParams",
                column: "DeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommandConstParams_Device_DeviceId",
                table: "CommandConstParams",
                column: "DeviceId",
                principalTable: "Device",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommandConstParams_Device_DeviceId",
                table: "CommandConstParams");

            migrationBuilder.DropIndex(
                name: "IX_CommandConstParams_DeviceId",
                table: "CommandConstParams");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "CommandConstParams");

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
    }
}
