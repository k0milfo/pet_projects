using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_miniCRM.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_HeadDepartment_HeadDepartmentId",
                table: "Managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeadDepartment",
                table: "HeadDepartment");

            migrationBuilder.RenameTable(
                name: "HeadDepartment",
                newName: "HeadDepartments");

            migrationBuilder.AlterColumn<string>(
                name: "NumberPhone",
                table: "HeadDepartments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "HeadDepartments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "HeadDepartments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "HeadDepartments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentNumber",
                table: "HeadDepartments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeadDepartments",
                table: "HeadDepartments",
                column: "HeadDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_HeadDepartments_HeadDepartmentId",
                table: "Managers",
                column: "HeadDepartmentId",
                principalTable: "HeadDepartments",
                principalColumn: "HeadDepartmentId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Managers_HeadDepartments_HeadDepartmentId",
                table: "Managers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HeadDepartments",
                table: "HeadDepartments");

            migrationBuilder.RenameTable(
                name: "HeadDepartments",
                newName: "HeadDepartment");

            migrationBuilder.AlterColumn<string>(
                name: "NumberPhone",
                table: "HeadDepartment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "HeadDepartment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "HeadDepartment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "HeadDepartment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentNumber",
                table: "HeadDepartment",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HeadDepartment",
                table: "HeadDepartment",
                column: "HeadDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_HeadDepartment_HeadDepartmentId",
                table: "Managers",
                column: "HeadDepartmentId",
                principalTable: "HeadDepartment",
                principalColumn: "HeadDepartmentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
