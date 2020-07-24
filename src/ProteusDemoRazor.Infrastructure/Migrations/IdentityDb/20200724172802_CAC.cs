using Microsoft.EntityFrameworkCore.Migrations;

namespace Proteus.Infrastructure.Migrations.IdentityDb
{
    public partial class CAC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbprint",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbprint",
                table: "User");
        }
    }
}
