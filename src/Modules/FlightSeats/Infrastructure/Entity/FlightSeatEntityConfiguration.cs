using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;

public class FlightSeatEntityConfiguration : IEntityTypeConfiguration<FlightSeatEntity>
{
    public void Configure(EntityTypeBuilder<FlightSeatEntity> builder)
    {
        builder.ToTable("FlightSeats");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.FlightId)
            .HasColumnName("FlightId")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.SeatCode)
            .HasColumnName("seat_code")
            .HasColumnType("varchar(5)")
            .IsRequired();

        builder.Property(x => x.CabinTypeId)
            .HasColumnName("CabinTypeId")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.LocationTypeId)
            .HasColumnName("LocationtypeId")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.IsOccupied)
            .HasColumnName("is_occupied")
            .HasColumnType("tinyint(1)")
            .IsRequired();

        builder.HasIndex(x => new { x.FlightId, x.SeatCode })
            .IsUnique();

        builder.HasOne<FlightEntity>(x => x.Flight)
            .WithMany(f => f.FlightSeats)
            .HasForeignKey(x => x.FlightId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<CabinTypeEntity>(x => x.CabinType)
            .WithMany(ct => ct.FlightSeats)
            .HasForeignKey(x => x.CabinTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<SeatLocationTypeEntity>(x => x.LocationType)
            .WithMany(lt => lt.FlightSeats)
            .HasForeignKey(x => x.LocationTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(FlightSeatDefaultData.FlightSeats);
    }
}
