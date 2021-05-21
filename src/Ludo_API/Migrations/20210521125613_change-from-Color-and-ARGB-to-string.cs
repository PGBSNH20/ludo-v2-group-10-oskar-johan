using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo_API.Migrations
{
    public partial class changefromColorandARGBtostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorArgb",
                table: "Players");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "ColorArgb",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
