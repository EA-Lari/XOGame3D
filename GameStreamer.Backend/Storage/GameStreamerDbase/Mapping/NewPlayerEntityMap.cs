using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;

namespace GameStreamer.Backend.Storage.GameStreamerDbase.Mapping
{
    public class NewPlayerEntityMap : IEntityTypeConfiguration<NewPlayerEntity>
    {
        public void Configure(EntityTypeBuilder<NewPlayerEntity> builder)
        {
            builder.ToTable("new_players_without_room", "game_streamer");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Nickname).HasColumnName("nickname").HasMaxLength(64);

            builder.Property(p => p.CreatedAt).HasColumnName("created_at");

            #region Default Data

            #endregion
        }
    }
}