using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;

namespace GameStreamer.Backend.Storage.GameStreamerDbase.Mapping
{
    public class PlayerEntityMap : IEntityTypeConfiguration<PlayerEntity>
    {
        public void Configure(EntityTypeBuilder<PlayerEntity> builder)
        {
            builder.ToTable("connected_players", "game_streamer");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.RoomId).HasColumnName("room_id");
            builder.Property(p => p.Nickname).HasColumnName("nickname").HasMaxLength(64);
            builder.Property(p => p.ChatHubId).HasColumnName("chat_hub_id").HasMaxLength(32);
            builder.Property(p => p.GameHubId).HasColumnName("game_hub_id").HasMaxLength(32);
            builder.Property(p => p.RoomHubId).HasColumnName("room_hub_id").HasMaxLength(32);
            builder.Property(p => p.PlayerGuid).HasColumnName("player_guid").HasColumnType("UUID");
            builder.Property(p => p.CreatedAt).HasColumnName("created_at");

            builder.HasOne<RoomEntity>(player => player.Room)
                .WithMany(room => room.JoinedPlayers)
                .HasForeignKey(player => player.RoomId)
                .IsRequired();

            // Convert enum <==> int on read/write to table
            //builder.Property(c => c.ClientType).HasConversion<int>();

            #region Default Data

            #endregion

        }
    }
}
