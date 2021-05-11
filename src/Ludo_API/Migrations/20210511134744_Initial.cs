using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo_API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ColorArgb = table.Column<int>(type: "int", nullable: false),
                    GameboardID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Gameboards",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastPlayerID = table.Column<int>(type: "int", nullable: true),
                    GameDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gameboards", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Gameboards_Players_LastPlayerID",
                        column: x => x.LastPlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Squares",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    GameboardId = table.Column<int>(type: "int", nullable: false),
                    OccupiedByID = table.Column<int>(type: "int", nullable: true),
                    PieceCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Squares", x => new { x.ID, x.GameboardId });
                    table.ForeignKey(
                        name: "FK_Squares_Gameboards_GameboardId",
                        column: x => x.GameboardId,
                        principalTable: "Gameboards",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Squares_Players_OccupiedByID",
                        column: x => x.OccupiedByID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gameboards_LastPlayerID",
                table: "Gameboards",
                column: "LastPlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Players_GameboardID",
                table: "Players",
                column: "GameboardID");

            migrationBuilder.CreateIndex(
                name: "IX_Squares_GameboardId",
                table: "Squares",
                column: "GameboardId");

            migrationBuilder.CreateIndex(
                name: "IX_Squares_OccupiedByID",
                table: "Squares",
                column: "OccupiedByID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Gameboards_GameboardID",
                table: "Players",
                column: "GameboardID",
                principalTable: "Gameboards",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gameboards_Players_LastPlayerID",
                table: "Gameboards");

            migrationBuilder.DropTable(
                name: "Squares");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Gameboards");
        }
    }
}
