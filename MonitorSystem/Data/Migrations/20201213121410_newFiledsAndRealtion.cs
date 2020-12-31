using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorSystem.Data.Migrations
{
    public partial class newFiledsAndRealtion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverName",
                table: "Movements",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverName",
                table: "Movements");
        }
    }
}
