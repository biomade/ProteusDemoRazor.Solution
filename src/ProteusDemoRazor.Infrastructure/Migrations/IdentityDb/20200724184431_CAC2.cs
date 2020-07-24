using Microsoft.EntityFrameworkCore.Migrations;

namespace Proteus.Infrastructure.Migrations.IdentityDb
{
    public partial class CAC2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbprint",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "EDI",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GovPOCEmail",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GovPOCName",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GovPOCPhoneNumber",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EDI",
                table: "User");

            migrationBuilder.DropColumn(
                name: "GovPOCEmail",
                table: "User");

            migrationBuilder.DropColumn(
                name: "GovPOCName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "GovPOCPhoneNumber",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Thumbprint",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
