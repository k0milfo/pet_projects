using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_miniCRM.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HeadDepartmentId",
                table: "Managers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HeadDepartment",
                columns: table => new
                {
                    HeadDepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentNumber = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadDepartment", x => x.HeadDepartmentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_HeadDepartmentId",
                table: "Managers",
                column: "HeadDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_HeadDepartment_HeadDepartmentId",
                table: "Managers",
                column: "HeadDepartmentId",
                principalTable: "HeadDepartment",
                principalColumn: "HeadDepartmentId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_HeadDepartment_HeadDepartmentId",
                table: "Managers");

            migrationBuilder.DropTable(
                name: "HeadDepartment");

            migrationBuilder.DropIndex(
                name: "IX_Managers_HeadDepartmentId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "HeadDepartmentId",
                table: "Managers");
        }
    }
}
