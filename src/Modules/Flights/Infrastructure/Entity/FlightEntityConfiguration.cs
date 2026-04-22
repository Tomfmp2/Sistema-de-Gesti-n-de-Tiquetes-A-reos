using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;

public class FlightEntityConfiguration : IEntityTypeConfiguration<FlightEntity>
{
    public void Configure(EntityTypeBuilder<FlightEntity> builder)
    {
        builder.ToTable("flights");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.FlightCode)
            .HasColumnName("flight_code")
            .HasColumnType("varchar(10)")
            .IsRequired();

        builder
            .Property(x => x.AirlineId)
            .HasColumnName("AirlineId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.RouteId)
            .HasColumnName("RouteId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.AircraftId)
            .HasColumnName("AircraftId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.DepartureDate)
            .HasColumnName("departure_date")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.EstimatedArrivalDate)
            .HasColumnName("estimated_arrival_date")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.TotalCapacity)
            .HasColumnName("total_capacity")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.AvailableSeats)
            .HasColumnName("available_seats")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.FlightStatusId)
            .HasColumnName("FlightstatusId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.RescheduledAt)
            .HasColumnName("rescheduled_at")
            .HasColumnType("datetime");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("datetime")
            .IsRequired();

        builder.HasIndex(x => x.FlightCode).IsUnique();

        builder
            .HasOne<AirlineEntity>()
            .WithMany()
            .HasForeignKey(x => x.AirlineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<RouteEntity>()
            .WithMany(x => x.Flights)
            .HasForeignKey(x => x.RouteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<AircraftEntity>()
            .WithMany()
            .HasForeignKey(x => x.AircraftId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<FlightStatusEntity>()
            .WithMany()
            .HasForeignKey(x => x.FlightStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(FlightDefaultData.Flights);
    }
}
