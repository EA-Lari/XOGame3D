using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static MatchMake.Backend.Storage.DbaseMapping.RoomMap;

namespace MatchMake.Backend.Storage.DbaseMapping
{

    public class RoomMap : IEntityTypeConfiguration<SomeExampleContext>
    {
        public void Configure(EntityTypeBuilder<SomeExampleContext> builder)
        {
            builder.ToTable("tRooms", "matchmake_backend");
            builder.HasKey(p => p.Id);
        }

        public class SomeExampleContext : DbContext {
            public int Id { get; set; }
        }

    }
}
