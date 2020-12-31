using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorSystem.Data.Migrations
{
    public partial class updateContraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_ContractorId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectName",
                table: "Projects");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ContractorId",
                table: "Projects",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectName_ContractorId",
                table: "Projects",
                columns: new[] { "ProjectName", "ContractorId" },
                unique: true,
                filter: "[ProjectName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_ContractorId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectName_ContractorId",
                table: "Projects");

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
    }
}
