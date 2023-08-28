using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoreFootball.Migrations
{
    /// <inheritdoc />
    public partial class Addedwinstreakfieldtoplayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WinStreak",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WinStreak",
                table: "Player");
        }
    }
}
