using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo_API.Migrations
{
    public partial class AddMessagepropertytoMoveAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "MoveActions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "GameCreatorID",
                table: "Gameboards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gameboards_GameCreatorID",
                table: "Gameboards",
                column: "GameCreatorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Gameboards_Players_GameCreatorID",
                table: "Gameboards",
                column: "GameCreatorID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gameboards_Players_GameCreatorID",
                table: "Gameboards");

            migrationBuilder.DropIndex(
                name: "IX_Gameboards_GameCreatorID",
                table: "Gameboards");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "MoveActions");

            migrationBuilder.DropColumn(
                name: "GameCreatorID",
                table: "Gameboards");
        }
    }
}
