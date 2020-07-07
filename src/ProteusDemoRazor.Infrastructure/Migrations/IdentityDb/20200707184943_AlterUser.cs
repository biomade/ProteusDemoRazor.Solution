using Microsoft.EntityFrameworkCore.Migrations;

namespace Proteus.Infrastructure.Migrations.IdentityDb
{
    public partial class AlterUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "User");

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "User",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "User");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
