using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;

public class ReservationPassengerEntityConfiguration
    : IEntityTypeConfiguration<ReservationPassengerEntity>
{
    public void Configure(EntityTypeBuilder<ReservationPassengerEntity> builder)
    {
        builder.ToTable("ReservationPassengers");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.ReservationFlightId)
            .HasColumnName("ReservationflightId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.PassengerId)
            .HasColumnName("PassengerId")
            .HasColumnType("int")
            .IsRequired();

        builder.HasIndex(x => new { x.ReservationFlightId, x.PassengerId }).IsUnique();

        builder
            .HasOne<ReservationFlightEntity>()
            .WithMany()
            .HasForeignKey(x => x.ReservationFlightId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
