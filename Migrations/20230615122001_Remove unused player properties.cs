using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestoreFootball.Migrations
{
    /// <inheritdoc />
    public partial class Removeunusedplayerproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignedUp",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Team",
                table: "Player");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SignedUp",
                table: "Player",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Team",
                table: "Player",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
