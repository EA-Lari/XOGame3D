using Microsoft.EntityFrameworkCore;
using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;
using GameStreamer.Backend.Storage.GameStreamerDbase.Mapping;

namespace GameStreamer.Backend.Storage.GameStreamerDbase
{
    public class GameStreamerContext : DbContext
    {

        static GameStreamerContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public GameStreamerContext(DbContextOptions<GameStreamerContext> options) : base(options)
        { }

        /// <summary>
        /// Настройка свойств моделей
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("game_streamer");

            modelBuilder.ApplyConfiguration(new PlayerEntityMap());
            modelBuilder.ApplyConfiguration(new RoomEntityMap());
        }

    }
}