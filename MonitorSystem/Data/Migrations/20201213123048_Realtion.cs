using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MonitorSystem.Data.Migrations
{
    public partial class Realtion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Projects_ProjectId",
                table: "Movements");
         

            migrationBuilder.AddColumn<Guid>(
                name: "ContractorId",
                table: "Movements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Movements_ContractorId",
                table: "Movements",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Contractors_ContractorId",
                table: "Movements",
                column: "ContractorId",
                principalTable: "Contractors",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Projects_ProjectId",
                table: "Movements",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Contractors_ContractorId",
                table: "Movements");

            migrationBuilder.DropForeignKey(
                name: "FK_Movements_Projects_ProjectId",
                table: "Movements");

            migrationBuilder.DropIndex(
                name: "IX_Movements_ContractorId",
                table: "Movements");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "Movements");

            migrationBuilder.AddForeignKey(
                name: "FK_Movements_Projects_ProjectId",
                table: "Movements",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
