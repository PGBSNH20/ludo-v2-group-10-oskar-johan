using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo_API.Migrations
{
    public partial class AddPlayerPropertyToMoveAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MoveActions_PlayerId",
                table: "MoveActions",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoveActions_Players_PlayerId",
                table: "MoveActions",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveActions_Players_PlayerId",
                table: "MoveActions");

            migrationBuilder.DropIndex(
                name: "IX_MoveActions_PlayerId",
                table: "MoveActions");
        }
    }
}
