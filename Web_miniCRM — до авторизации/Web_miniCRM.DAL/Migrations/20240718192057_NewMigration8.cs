using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_miniCRM.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Managers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Managers");
        }
    }
}
