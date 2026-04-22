using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;

public class ReservationPassengerEntityConfiguration
    : IEntityTypeConfiguration<ReservationPassengerEntity>
{
    public void Configure(EntityTypeBuilder<ReservationPassengerEntity> builder)
    {
        builder.ToTable("booking_passengers");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.ReservationFlightId)
            .HasColumnName("booking_flight_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.PassengerId)
            .HasColumnName("passenger_id")
            .HasColumnType("int")
            .IsRequired();

        builder.HasIndex(x => new { x.ReservationFlightId, x.PassengerId }).IsUnique();

        builder
            .HasOne<ReservationFlightEntity>(x => x.ReservationFlight)
            .WithMany(rf => rf.ReservationPassengers)
            .HasForeignKey(x => x.ReservationFlightId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<PassengerEntity>(x => x.Passenger)
            .WithMany(p => p.ReservationPassengers)
            .HasForeignKey(x => x.PassengerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
