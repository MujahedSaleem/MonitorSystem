using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorSystem.Data.Migrations
{
    public partial class ADDrefrenceToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Reference",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Projects");
        }
    }
}
