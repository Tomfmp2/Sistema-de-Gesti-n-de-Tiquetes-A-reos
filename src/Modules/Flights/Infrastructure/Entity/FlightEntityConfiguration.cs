using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            .HasColumnName("id")
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
            .HasColumnName("airline_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.RouteId)
            .HasColumnName("route_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.AircraftId)
            .HasColumnName("aircraft_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.DepartureDate)
            .HasColumnName("departure_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.EstimatedArrivalDate)
            .HasColumnName("estimated_arrival_at")
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
            .HasColumnName("flight_status_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.RescheduledAt)
            .HasColumnName("rescheduled_at")
            .HasColumnType("datetime");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder.HasIndex(x => x.FlightCode).IsUnique();

        builder
            .HasOne<AirlineEntity>(x => x.Airline)
            .WithMany(a => a.Flights)
            .HasForeignKey(x => x.AirlineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<RouteEntity>(x => x.Route)
            .WithMany(r => r.Flights)
            .HasForeignKey(x => x.RouteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<AircraftEntity>(x => x.Aircraft)
            .WithMany(ac => ac.Flights)
            .HasForeignKey(x => x.AircraftId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<FlightStatusEntity>(x => x.FlightStatus)
            .WithMany(fs => fs.Flights)
            .HasForeignKey(x => x.FlightStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
