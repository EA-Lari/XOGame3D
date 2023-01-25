using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStreamer.Backend.Storage.GameStreamerDbase.Migrations
{
    public partial class AddChooseColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "game_streamer",
                table: "game_rooms",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "game_streamer",
                table: "connected_players",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<bool>(
                name: "IsRandomGameChosen",
                schema: "game_streamer",
                table: "connected_players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReadyForGame",
                schema: "game_streamer",
                table: "connected_players",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRandomGameChosen",
                schema: "game_streamer",
                table: "connected_players");

            migrationBuilder.DropColumn(
                name: "IsReadyForGame",
                schema: "game_streamer",
                table: "connected_players");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "game_streamer",
                table: "game_rooms",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "game_streamer",
                table: "connected_players",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
