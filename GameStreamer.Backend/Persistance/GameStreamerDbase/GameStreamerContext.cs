using Microsoft.EntityFrameworkCore;

namespace GameStreamer.Backend.Persistance.GameStreamerDbase
{
    public class GameStreamerContext : DbContext
    {
        public GameStreamerContext(DbContextOptions<GameStreamerContext> options) : base(options)
        {   }

        // TODO: Add DbSets Here

        /// <summary>
        /// Настройка свойств моделей
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("dashboard");

            //modelBuilder.ApplyConfiguration(new QuarterReportEntityMap());
            //modelBuilder.ApplyConfiguration(new ClientsReportEntityMap());
            //modelBuilder.ApplyConfiguration(new ContractsReportEntityMap());
        }

    }
}