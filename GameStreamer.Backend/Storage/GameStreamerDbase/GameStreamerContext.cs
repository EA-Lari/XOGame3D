using Microsoft.EntityFrameworkCore;
using GameStreamer.Backend.Persistance.GameStreamerDbase.Mapping;
using GameStreamer.Backend.Persistance.GameStreamerDbase.Entities;

namespace GameStreamer.Backend.Persistance.GameStreamerDbase
{
    public class GameStreamerContext : DbContext
    {
        public GameStreamerContext(DbContextOptions<GameStreamerContext> options) : base(options)
        {   }

        public DbSet<ConnectedPlayerEntity> ConnectedPlayers { get; set; }

        /// <summary>
        /// Настройка свойств моделей
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("game_streamer");

            modelBuilder.ApplyConfiguration(new ConnectedPlayerEntityMap());
        }

    }
}