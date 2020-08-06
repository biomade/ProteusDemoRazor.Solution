using Microsoft.EntityFrameworkCore.Migrations;

namespace Proteus.Infrastructure.Migrations.IdentityDb
{
    public partial class DupeSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UserOnLine",
                table: "User",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserOnLine",
                table: "User");
        }
    }
}
