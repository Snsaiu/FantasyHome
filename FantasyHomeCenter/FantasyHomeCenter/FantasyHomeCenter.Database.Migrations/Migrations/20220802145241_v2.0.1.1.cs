using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyHomeCenter.Database.Migrations.Migrations
{
    public partial class v2011 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "Automation",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                table: "Automation");
        }
    }
}
