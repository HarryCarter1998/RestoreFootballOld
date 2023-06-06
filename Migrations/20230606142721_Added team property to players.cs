using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoreFootball.Migrations
{
    /// <inheritdoc />
    public partial class Addedteampropertytoplayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Team",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team",
                table: "Player");
        }
    }
}
