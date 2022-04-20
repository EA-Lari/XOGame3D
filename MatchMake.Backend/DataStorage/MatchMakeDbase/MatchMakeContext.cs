using Microsoft.EntityFrameworkCore;
using MatchMake.Backend.Storage.DbaseMapping;
using MatchMake.Backend.DataStorage.MatchMakeDbase.Mapping;

namespace MatchMake.Backend.DataStorage.MatchMake.Context
{
    public class MatchMakeContext : DbContext
    {
        
        public MatchMakeContext(DbContextOptions<MatchMakeContext> options) : base(options)
        {   }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoomMap());
            modelBuilder.ApplyConfiguration(new PlayerMap());
        }

    }
}