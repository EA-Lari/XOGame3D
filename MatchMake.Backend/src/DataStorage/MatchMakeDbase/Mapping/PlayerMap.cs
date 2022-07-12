using Microsoft.EntityFrameworkCore;
using MatchMake.Backend.Storage.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchMake.Backend.DataStorage.MatchMakeDbase.Mapping
{
    public class PlayerMap : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("tPlayers", "matchmake_backend");
            builder.HasKey(p => p.Id);           

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.NickName).HasMaxLength(16);
            builder.Property(p => p.PlayerGuid).HasColumnType("UUID");
            builder.Property(p => p.EnterDt).HasColumnType("timestamp with time zone");
            builder.Property(p => p.IsReadyForGame);
            builder.Property(p => p.ParentRoomGuid).HasColumnType("UUID");
        }
    }
}