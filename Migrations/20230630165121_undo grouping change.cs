using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoreFootball.Migrations
{
    /// <inheritdoc />
    public partial class undogroupingchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grouping_Gameweek_GameweekId",
                table: "Grouping");

            migrationBuilder.AlterColumn<int>(
                name: "GameweekId",
                table: "Grouping",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Grouping_Gameweek_GameweekId",
                table: "Grouping",
                column: "GameweekId",
                principalTable: "Gameweek",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grouping_Gameweek_GameweekId",
                table: "Grouping");

            migrationBuilder.AlterColumn<int>(
                name: "GameweekId",
                table: "Grouping",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Grouping_Gameweek_GameweekId",
                table: "Grouping",
                column: "GameweekId",
                principalTable: "Gameweek",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
