using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;

public class FlightSeatEntityConfiguration : IEntityTypeConfiguration<FlightSeatEntity>
{
    public void Configure(EntityTypeBuilder<FlightSeatEntity> builder)
    {
        builder.ToTable("flight_seats");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.FlightId)
            .HasColumnName("flight_id")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.SeatCode)
            .HasColumnName("seat_code")
            .HasColumnType("varchar(5)")
            .IsRequired();

        builder.Property(x => x.CabinTypeId)
            .HasColumnName("cabin_type_id")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.LocationTypeId)
            .HasColumnName("location_type_id")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.IsOccupied)
            .HasColumnName("is_occupied")
            .HasColumnType("tinyint(1)")
            .IsRequired();

        builder.HasIndex(x => new { x.FlightId, x.SeatCode })
            .IsUnique();

        builder.HasOne<FlightEntity>()
            .WithMany()
            .HasForeignKey(x => x.FlightId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<CabinTypeEntity>()
            .WithMany()
            .HasForeignKey(x => x.CabinTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<SeatLocationTypeEntity>()
            .WithMany()
            .HasForeignKey(x => x.LocationTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
