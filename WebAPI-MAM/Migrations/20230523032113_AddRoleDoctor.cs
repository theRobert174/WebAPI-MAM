using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI_MAM.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleDoctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Doctors");
        }
    }
}
