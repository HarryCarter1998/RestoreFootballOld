using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoreFootball.Migrations
{
    /// <inheritdoc />
    public partial class Renamedtogrouping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameweekPlayer_SameTeamRule_SameTeamRuleId",
                table: "GameweekPlayer");

            migrationBuilder.DropTable(
                name: "SameTeamRule");

            migrationBuilder.RenameColumn(
                name: "SameTeamRuleId",
                table: "GameweekPlayer",
                newName: "GroupingId");

            migrationBuilder.RenameIndex(
                name: "IX_GameweekPlayer_SameTeamRuleId",
                table: "GameweekPlayer",
                newName: "IX_GameweekPlayer_GroupingId");

            migrationBuilder.CreateTable(
                name: "Grouping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameweekId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grouping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grouping_Gameweek_GameweekId",
                        column: x => x.GameweekId,
                        principalTable: "Gameweek",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grouping_GameweekId",
                table: "Grouping",
                column: "GameweekId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameweekPlayer_Grouping_GroupingId",
                table: "GameweekPlayer",
                column: "GroupingId",
                principalTable: "Grouping",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameweekPlayer_Grouping_GroupingId",
                table: "GameweekPlayer");

            migrationBuilder.DropTable(
                name: "Grouping");

            migrationBuilder.RenameColumn(
                name: "GroupingId",
                table: "GameweekPlayer",
                newName: "SameTeamRuleId");

            migrationBuilder.RenameIndex(
                name: "IX_GameweekPlayer_GroupingId",
                table: "GameweekPlayer",
                newName: "IX_GameweekPlayer_SameTeamRuleId");

            migrationBuilder.CreateTable(
                name: "SameTeamRule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameweekId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SameTeamRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SameTeamRule_Gameweek_GameweekId",
                        column: x => x.GameweekId,
                        principalTable: "Gameweek",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SameTeamRule_GameweekId",
                table: "SameTeamRule",
                column: "GameweekId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameweekPlayer_SameTeamRule_SameTeamRuleId",
                table: "GameweekPlayer",
                column: "SameTeamRuleId",
                principalTable: "SameTeamRule",
                principalColumn: "Id");
        }
    }
}
