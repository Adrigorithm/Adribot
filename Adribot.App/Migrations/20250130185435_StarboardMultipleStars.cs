using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adribot.Migrations
{
    /// <inheritdoc />
    public partial class StarboardMultipleStars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StarEmoji",
                table: "DGuilds",
                newName: "StarEmotes");

            migrationBuilder.AddColumn<string>(
                name: "StarEmojis",
                table: "DGuilds",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarEmojis",
                table: "DGuilds");

            migrationBuilder.RenameColumn(
                name: "StarEmotes",
                table: "DGuilds",
                newName: "StarEmoji");
        }
    }
}
