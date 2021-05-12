using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo_API.Migrations
{
    public partial class addGameIdpropertytoGameboardmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameId",
                table: "Gameboards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Gameboards");
        }
    }
}
