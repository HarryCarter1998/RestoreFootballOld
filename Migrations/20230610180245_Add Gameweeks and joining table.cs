using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoreFootball.Migrations
{
    /// <inheritdoc />
    public partial class AddGameweeksandjoiningtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gameweek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GreenScore = table.Column<int>(type: "int", nullable: false),
                    NonBibsScore = table.Column<int>(type: "int", nullable: false),
                    YellowScore = table.Column<int>(type: "int", nullable: false),
                    OrangeScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gameweek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameweekPlayer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    Team = table.Column<int>(type: "int", nullable: false),
                    GameweekId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameweekPlayer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameweekPlayer_Gameweek_GameweekId",
                        column: x => x.GameweekId,
                        principalTable: "Gameweek",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameweekPlayer_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameweekPlayer_GameweekId",
                table: "GameweekPlayer",
                column: "GameweekId");

            migrationBuilder.CreateIndex(
                name: "IX_GameweekPlayer_PlayerId",
                table: "GameweekPlayer",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameweekPlayer");

            migrationBuilder.DropTable(
                name: "Gameweek");
        }
    }
}
