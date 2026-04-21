using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;

public class RouteEntityConfiguration : IEntityTypeConfiguration<RouteEntity>
{
    public void Configure(EntityTypeBuilder<RouteEntity> builder)
    {
        builder.ToTable("routes");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("Id").ValueGeneratedOnAdd();
        builder.Property(r => r.OriginAirportId).HasColumnName("Origin_airportId").IsRequired();
        builder.Property(r => r.DestinationAirportId).HasColumnName("Destination_airportId").IsRequired();
        builder.Property(r => r.DistanceKm).HasColumnName("distance_km");
        builder.Property(r => r.EstimatedDurationMin).HasColumnName("estimated_duration_min");
        builder.HasIndex(r => new { r.OriginAirportId, r.DestinationAirportId }).IsUnique();

        // Relationships
        builder
            .HasOne(r => r.OriginAirport)
            .WithMany()
            .HasForeignKey(r => r.OriginAirportId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(r => r.DestinationAirport)
            .WithMany()
            .HasForeignKey(r => r.DestinationAirportId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(r => r.Flights)
            .WithOne(f => f.Route)
            .HasForeignKey("route_id")
            .OnDelete(DeleteBehavior.Restrict);
    }
}