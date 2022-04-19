using Microsoft.EntityFrameworkCore;

namespace MatchMake.Backend.DataStorage.MatchMake.Context
{
    public class MatchMakeContext : DbContext
    {
        
        public MatchMakeContext(DbContextOptions<MatchMakeContext> options) : base(options)
        {   }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration();
            modelBuilder.ApplyConfiguration();
        }

    }
}
