using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameStreamer.Backend.Migrations.GameStreamerContextMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "game_streamer");

            migrationBuilder.CreateTable(
                name: "connected_players",
                schema: "game_streamer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    connection_id = table.Column<string>(type: "text", nullable: false),
                    client_type = table.Column<int>(type: "integer", nullable: false),
                    room_guid = table.Column<Guid>(type: "uuid", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    nick_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_connected_players", x => x.id);
                });

            migrationBuilder.InsertData(
                schema: "game_streamer",
                table: "connected_players",
                columns: new[] { "id", "client_type", "connection_id", "is_active", "nick_name", "room_guid" },
                values: new object[,]
                {
                    { 1, 0, "qwerty123", false, "noob1", new Guid("00000000-0000-0000-0000-000000000000") },
                    { 2, 1, "qwerty456", false, "noob2", new Guid("00000000-0000-0000-0000-000000000000") },
                    { 3, 2, "qwerty789", false, "noob3", new Guid("00000000-0000-0000-0000-000000000000") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "connected_players",
                schema: "game_streamer");
        }
    }
}
