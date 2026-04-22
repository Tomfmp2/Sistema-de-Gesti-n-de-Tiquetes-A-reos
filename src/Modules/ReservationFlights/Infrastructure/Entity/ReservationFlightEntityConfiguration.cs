using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;
namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;

public class ReservationFlightEntityConfiguration : IEntityTypeConfiguration<ReservationFlightEntity>
{
    public void Configure(EntityTypeBuilder<ReservationFlightEntity> builder)
    {
        builder.ToTable("booking_flights");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.ReservationId)
            .HasColumnName("booking_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.FlightId)
            .HasColumnName("flight_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.PartialValue)
            .HasColumnName("partial_amount")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.HasIndex(x => new { x.ReservationId, x.FlightId }).IsUnique();

        builder
            .HasOne<ReservationEntity>(x => x.Reservation)
            .WithMany(r => r.ReservationFlights)
            .HasForeignKey(x => x.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<FlightEntity>(x => x.Flight)
            .WithMany(f => f.ReservationFlights)
            .HasForeignKey(x => x.FlightId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
