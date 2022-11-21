using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GameStreamer.Backend.Storage.GameStreamerDbase.Entities;

namespace GameStreamer.Backend.Storage.GameStreamerDbase.Mapping
{
    public class ConnectedPlayerEntityMap : IEntityTypeConfiguration<ConnectedPlayerEntity>
    {
        public void Configure(EntityTypeBuilder<ConnectedPlayerEntity> builder)
        {
            builder.ToTable("connected_players", "game_streamer");

            builder.HasKey(k => k.Id);
            builder.Property(k => k.Id).ValueGeneratedOnAdd();

            builder.Property(k => k.Id).HasColumnName("id");
            builder.Property(k => k.ConnectionId).HasColumnName("connection_id");
            builder.Property(k => k.ClientType).HasColumnName("client_type");
            builder.Property(k => k.RoomGuid).HasColumnName("room_guid");
            builder.Property(k => k.IsActive).HasColumnName("is_active");
            builder.Property(k => k.NickName).HasColumnName("nick_name");

            // Convert enum <==> int on read/write to table
            builder.Property(c => c.ClientType).HasConversion<int>();

            #region Default Data

            var connectedPlayersList = new List<ConnectedPlayerEntity>
            {
                new ConnectedPlayerEntity { Id = 1, ConnectionId = "qwerty123", ClientType = TypeOfConnectedClient.Unknown, RoomGuid = Guid.Empty, IsActive = false, NickName = "noob1" },
                new ConnectedPlayerEntity { Id = 2, ConnectionId = "qwerty456", ClientType = TypeOfConnectedClient.AngularWeb, RoomGuid = Guid.Empty, IsActive = false, NickName = "noob2" },
                new ConnectedPlayerEntity { Id = 3, ConnectionId = "qwerty789", ClientType = TypeOfConnectedClient.WpfDesktop, RoomGuid = Guid.Empty, IsActive = false, NickName = "noob3" }
            };

            builder.HasData(connectedPlayersList);

            #endregion

        }
    }
}
