using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameStreamer.Backend.Storage.GameStreamerDbase.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "game_streamer");

            migrationBuilder.CreateTable(
                name: "game_rooms",
                schema: "game_streamer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    hub_group_id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    room_guid = table.Column<Guid>(type: "UUID", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_rooms", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "connected_players",
                schema: "game_streamer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    room_id = table.Column<int>(type: "integer", nullable: false),
                    nickname = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    chat_hub_id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    game_hub_id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    room_hub_id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    player_guid = table.Column<Guid>(type: "UUID", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_connected_players", x => x.id);
                    table.ForeignKey(
                        name: "FK_connected_players_game_rooms_room_id",
                        column: x => x.room_id,
                        principalSchema: "game_streamer",
                        principalTable: "game_rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_connected_players_room_id",
                schema: "game_streamer",
                table: "connected_players",
                column: "room_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "connected_players",
                schema: "game_streamer");

            migrationBuilder.DropTable(
                name: "game_rooms",
                schema: "game_streamer");
        }
    }
}
