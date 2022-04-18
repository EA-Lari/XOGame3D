using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static MatchMake.Backend.Storage.DbaseMapping.SomeExampleContextMap;

namespace MatchMake.Backend.Storage.DbaseMapping
{

    public class SomeExampleContextMap : IEntityTypeConfiguration<SomeExampleContext>
    {
        public void Configure(EntityTypeBuilder<SomeExampleContext> builder)
        {
            builder.ToTable("SomeExampleContext", "example");
            builder.HasKey(p => p.Id);

            //builder.Property(p => p.Id).UseNpgsqlIdentityColumn();
            //builder.Property(p => p.SourceId);
            //builder.Property(p => p.RequestUrl);
            //builder.Property(p => p.Request);
            //builder.Property(p => p.Response);
            //builder.Property(p => p.ResponseStatusCode);
            //builder.Property(p => p.CreationDate);
        }

        public class SomeExampleContext : DbContext {
            public int Id { get; set; }
        }

    }
}
