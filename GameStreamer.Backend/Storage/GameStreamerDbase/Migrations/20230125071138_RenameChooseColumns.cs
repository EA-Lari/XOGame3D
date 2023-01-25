using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStreamer.Backend.Storage.GameStreamerDbase.Migrations
{
    public partial class RenameChooseColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsReadyForGame",
                schema: "game_streamer",
                table: "connected_players",
                newName: "is_ready_for_game");

            migrationBuilder.RenameColumn(
                name: "IsRandomGameChosen",
                schema: "game_streamer",
                table: "connected_players",
                newName: "is_random_game_mode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "is_ready_for_game",
                schema: "game_streamer",
                table: "connected_players",
                newName: "IsReadyForGame");

            migrationBuilder.RenameColumn(
                name: "is_random_game_mode",
                schema: "game_streamer",
                table: "connected_players",
                newName: "IsRandomGameMode");
        }
    }
}
