using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo_API.Migrations
{
    public partial class addCurrentPlayerandGameStartDatetoGameboard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "Gameboards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPlayerID",
                table: "Gameboards",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GameStartDate",
                table: "Gameboards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Gameboards_CurrentPlayerID",
                table: "Gameboards",
                column: "CurrentPlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Gameboards_Players_CurrentPlayerID",
                table: "Gameboards",
                column: "CurrentPlayerID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gameboards_Players_CurrentPlayerID",
                table: "Gameboards");

            migrationBuilder.DropIndex(
                name: "IX_Gameboards_CurrentPlayerID",
                table: "Gameboards");

            migrationBuilder.DropColumn(
                name: "CurrentPlayerID",
                table: "Gameboards");

            migrationBuilder.DropColumn(
                name: "GameStartDate",
                table: "Gameboards");

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "Gameboards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
