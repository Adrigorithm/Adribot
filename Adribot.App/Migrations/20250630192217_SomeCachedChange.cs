using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adribot.Migrations
{
    /// <inheritdoc />
    public partial class SomeCachedChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WireConfigs",
                columns: table => new
                {
                    WireConfigId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmoteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmoteData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DGuildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WireConfigs", x => x.WireConfigId);
                    table.ForeignKey(
                        name: "FK_WireConfigs_DGuilds_DGuildId",
                        column: x => x.DGuildId,
                        principalTable: "DGuilds",
                        principalColumn: "DGuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WireConfigs_DGuildId",
                table: "WireConfigs",
                column: "DGuildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable(
                name: "WireConfigs");
    }
}
