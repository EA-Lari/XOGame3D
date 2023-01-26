using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;

namespace GameStreamer.Backend.Storage.GameStreamerDbase.Mapping
{
    public class RoomEntityMap : IEntityTypeConfiguration<RoomEntity>
    {
        public void Configure(EntityTypeBuilder<RoomEntity> builder)
        {
            builder.ToTable("game_rooms", "game_streamer");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.HubGroupId).HasColumnName("hub_group_id").HasMaxLength(32);
            builder.Property(p => p.RoomGuid).HasColumnName("room_guid").HasColumnType("UUID");
            builder.Property(p => p.CreatedAt).HasColumnName("created_at");

            builder
                .HasMany<JoinedPlayerEntity>(room => room.JoinedPlayers)
                .WithOne(player => player.Room);

            #region Default Data

            #endregion

        }
    }
}
