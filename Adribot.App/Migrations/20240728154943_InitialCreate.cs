using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Adribot.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DGuilds",
                columns: table => new
                {
                    DGuildId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    StarboardChannel = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    StarEmoji = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StarThreshold = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DGuilds", x => x.DGuildId);
                });

            migrationBuilder.CreateTable(
                name: "DMembers",
                columns: table => new
                {
                    DMemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Mention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DGuildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMembers", x => x.DMemberId);
                    table.ForeignKey(
                        name: "FK_DMembers_DGuilds_DGuildId",
                        column: x => x.DGuildId,
                        principalTable: "DGuilds",
                        principalColumn: "DGuildId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IcsCalendars",
                columns: table => new
                {
                    IcsCalendarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DMemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcsCalendars", x => x.IcsCalendarId);
                    table.ForeignKey(
                        name: "FK_IcsCalendars_DMembers_DMemberId",
                        column: x => x.DMemberId,
                        principalTable: "DMembers",
                        principalColumn: "DMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Infractions",
                columns: table => new
                {
                    InfractionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DMemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Infractions", x => x.InfractionId);
                    table.ForeignKey(
                        name: "FK_Infractions_DMembers_DMemberId",
                        column: x => x.DMemberId,
                        principalTable: "DMembers",
                        principalColumn: "DMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    ReminderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Channel = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DMemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.ReminderId);
                    table.ForeignKey(
                        name: "FK_Reminders_DMembers_DMemberId",
                        column: x => x.DMemberId,
                        principalTable: "DMembers",
                        principalColumn: "DMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    DMemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                    table.ForeignKey(
                        name: "FK_Tags_DMembers_DMemberId",
                        column: x => x.DMemberId,
                        principalTable: "DMembers",
                        principalColumn: "DMemberId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    End = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IsAllDay = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Organiser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IcsCalendarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_IcsCalendars_IcsCalendarId",
                        column: x => x.IcsCalendarId,
                        principalTable: "IcsCalendars",
                        principalColumn: "IcsCalendarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DMembers_DGuildId",
                table: "DMembers",
                column: "DGuildId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_IcsCalendarId",
                table: "Events",
                column: "IcsCalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_IcsCalendars_DMemberId",
                table: "IcsCalendars",
                column: "DMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Infractions_DMemberId",
                table: "Infractions",
                column: "DMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_DMemberId",
                table: "Reminders",
                column: "DMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_DMemberId",
                table: "Tags",
                column: "DMemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Infractions");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "IcsCalendars");

            migrationBuilder.DropTable(
                name: "DMembers");

            migrationBuilder.DropTable(
                name: "DGuilds");
        }
    }
}
