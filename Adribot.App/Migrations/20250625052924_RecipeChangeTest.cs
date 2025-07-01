using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adribot.Migrations
{
    /// <inheritdoc />
    public partial class RecipeChangeTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StarEmojis",
                table: "DGuilds");

            migrationBuilder.DropColumn(
                name: "StarEmotes",
                table: "DGuilds");

            migrationBuilder.DropColumn(
                name: "StarThreshold",
                table: "DGuilds");

            migrationBuilder.DropColumn(
                name: "StarboardChannel",
                table: "DGuilds");

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Ingredients", x => x.IngredientId));

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Servings = table.Column<short>(type: "smallint", nullable: false),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Difficulty = table.Column<byte>(type: "tinyint", nullable: false),
                    OvenMode = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Duration = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table => table.PrimaryKey("PK_Recipes", x => x.RecipeId));

            migrationBuilder.CreateTable(
                name: "Starboards",
                columns: table => new
                {
                    StarboardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DGuildId = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Threshold = table.Column<int>(type: "int", nullable: false),
                    EmojiStrings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmoteStrings = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Starboards", x => x.StarboardId);
                    table.ForeignKey(
                        name: "FK_Starboards_DGuilds_DGuildId",
                        column: x => x.DGuildId,
                        principalTable: "DGuilds",
                        principalColumn: "DGuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeIngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false),
                    Optional = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => x.RecipeIngredientId);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageLinks",
                columns: table => new
                {
                    MessageLinkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalMessageId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    ReferenceMessageId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    StarboardId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageLinks", x => x.MessageLinkId);
                    table.ForeignKey(
                        name: "FK_MessageLinks_Starboards_StarboardId",
                        column: x => x.StarboardId,
                        principalTable: "Starboards",
                        principalColumn: "StarboardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageLinks_StarboardId",
                table: "MessageLinks",
                column: "StarboardId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Starboards_DGuildId",
                table: "Starboards",
                column: "DGuildId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageLinks");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Starboards");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.AddColumn<string>(
                name: "StarEmojis",
                table: "DGuilds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StarEmotes",
                table: "DGuilds",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StarThreshold",
                table: "DGuilds",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StarboardChannel",
                table: "DGuilds",
                type: "decimal(20,0)",
                nullable: true);
        }
    }
}
