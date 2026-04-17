using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;

public class RouteEntityConfiguration : IEntityTypeConfiguration<RouteEntity>
{
    public void Configure(EntityTypeBuilder<RouteEntity> builder)
    {
        builder.ToTable("routes");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(r => r.OriginAirportId).HasColumnName("origin_airport_id").IsRequired();
        builder.Property(r => r.DestinationAirportId).HasColumnName("destination_airport_id").IsRequired();
        builder.Property(r => r.DistanceKm).HasColumnName("distance_km");
        builder.Property(r => r.EstimatedDurationMin).HasColumnName("estimated_duration_min");
        builder.HasIndex(r => new { r.OriginAirportId, r.DestinationAirportId }).IsUnique();
    }
}