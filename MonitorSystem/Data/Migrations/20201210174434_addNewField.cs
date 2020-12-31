using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorSystem.Data.Migrations
{
    public partial class addNewField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_ContractorId",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Projects",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ContractorId",
                table: "Projects",
                column: "ContractorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectName",
                table: "Projects",
                column: "ProjectName",
                unique: true,
                filter: "[ProjectName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_ContractorId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectName",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Projects");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ContractorId",
                table: "Projects",
                column: "ContractorId");
        }
    }
}
