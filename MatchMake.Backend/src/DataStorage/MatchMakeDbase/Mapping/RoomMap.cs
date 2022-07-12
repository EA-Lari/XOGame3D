using Microsoft.EntityFrameworkCore;
using MatchMake.Backend.Storage.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatchMake.Backend.Storage.DbaseMapping
{

    public class RoomMap : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {

            builder.ToTable("tRooms", "matchmake_backend");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.RoomGuid).HasColumnType("UUID");
            builder.Property(p => p.RoomState);
            builder.Property(p => p.CreatedDt).HasColumnType("timestamp with time zone");

        }
    }

}