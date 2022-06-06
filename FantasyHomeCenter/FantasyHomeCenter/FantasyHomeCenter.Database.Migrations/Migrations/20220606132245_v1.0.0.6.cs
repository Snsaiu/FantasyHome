using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyHomeCenter.Database.Migrations.Migrations
{
    public partial class v1006 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PluginDescription",
                table: "DeviceType",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PluginDescription",
                table: "DeviceType");
        }
    }
}
