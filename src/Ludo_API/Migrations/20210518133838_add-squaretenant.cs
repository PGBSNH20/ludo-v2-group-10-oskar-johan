using Microsoft.EntityFrameworkCore.Migrations;

namespace Ludo_API.Migrations
{
    public partial class addsquaretenant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Squares_Players_OccupiedByID",
                table: "Squares");

            migrationBuilder.DropIndex(
                name: "IX_Squares_OccupiedByID",
                table: "Squares");

            migrationBuilder.DropColumn(
                name: "OccupiedByID",
                table: "Squares");

            migrationBuilder.RenameColumn(
                name: "PieceCount",
                table: "Squares",
                newName: "TenantID");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "SquareTenant",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SquareIndex = table.Column<int>(type: "int", nullable: false),
                    PlayerID = table.Column<int>(type: "int", nullable: true),
                    PieceCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquareTenant", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SquareTenant_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MoveActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    OptionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiceRoll = table.Column<int>(type: "int", nullable: false),
                    ValidMove = table.Column<bool>(type: "bit", nullable: false),
                    StartSquareID = table.Column<int>(type: "int", nullable: true),
                    DestinationSquareID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveActions_SquareTenant_DestinationSquareID",
                        column: x => x.DestinationSquareID,
                        principalTable: "SquareTenant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MoveActions_SquareTenant_StartSquareID",
                        column: x => x.StartSquareID,
                        principalTable: "SquareTenant",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Squares_TenantID",
                table: "Squares",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_MoveActions_DestinationSquareID",
                table: "MoveActions",
                column: "DestinationSquareID");

            migrationBuilder.CreateIndex(
                name: "IX_MoveActions_StartSquareID",
                table: "MoveActions",
                column: "StartSquareID");

            migrationBuilder.CreateIndex(
                name: "IX_SquareTenant_PlayerID",
                table: "SquareTenant",
                column: "PlayerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Squares_SquareTenant_TenantID",
                table: "Squares",
                column: "TenantID",
                principalTable: "SquareTenant",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Squares_SquareTenant_TenantID",
                table: "Squares");

            migrationBuilder.DropTable(
                name: "MoveActions");

            migrationBuilder.DropTable(
                name: "SquareTenant");

            migrationBuilder.DropIndex(
                name: "IX_Squares_TenantID",
                table: "Squares");

            migrationBuilder.RenameColumn(
                name: "TenantID",
                table: "Squares",
                newName: "PieceCount");

            migrationBuilder.AddColumn<int>(
                name: "OccupiedByID",
                table: "Squares",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.CreateIndex(
                name: "IX_Squares_OccupiedByID",
                table: "Squares",
                column: "OccupiedByID");

            migrationBuilder.AddForeignKey(
                name: "FK_Squares_Players_OccupiedByID",
                table: "Squares",
                column: "OccupiedByID",
                principalTable: "Players",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
