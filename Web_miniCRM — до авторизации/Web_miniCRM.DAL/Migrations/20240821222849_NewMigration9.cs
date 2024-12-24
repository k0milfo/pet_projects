using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_miniCRM.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calls_Companies_CompanyId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Companies_CompanyId",
                table: "Meetings");

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ManagerId",
                table: "Invoices",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_Companies_CompanyId",
                table: "Calls",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Managers_ManagerId",
                table: "Invoices",
                column: "ManagerId",
                principalTable: "Managers",
                principalColumn: "ManagerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Companies_CompanyId",
                table: "Meetings",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Calls_Companies_CompanyId",
                table: "Calls");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Managers_ManagerId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Companies_CompanyId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ManagerId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_Calls_Companies_CompanyId",
                table: "Calls",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Companies_CompanyId",
                table: "Meetings",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId");
        }
    }
}
