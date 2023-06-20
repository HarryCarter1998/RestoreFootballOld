using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoreFootball.Migrations
{
    /// <inheritdoc />
    public partial class Addedsameteamrules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SameTeamRuleId",
                table: "GameweekPlayer",
                type: "int",
                nullable: true);

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
                name: "IX_GameweekPlayer_SameTeamRuleId",
                table: "GameweekPlayer",
                column: "SameTeamRuleId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameweekPlayer_SameTeamRule_SameTeamRuleId",
                table: "GameweekPlayer");

            migrationBuilder.DropTable(
                name: "SameTeamRule");

            migrationBuilder.DropIndex(
                name: "IX_GameweekPlayer_SameTeamRuleId",
                table: "GameweekPlayer");

            migrationBuilder.DropColumn(
                name: "SameTeamRuleId",
                table: "GameweekPlayer");
        }
    }
}
