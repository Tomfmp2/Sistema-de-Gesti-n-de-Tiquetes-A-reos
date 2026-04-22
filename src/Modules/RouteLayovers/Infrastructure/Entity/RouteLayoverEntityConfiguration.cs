using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.Entity;

public class RouteLayoverEntityConfiguration : IEntityTypeConfiguration<RouteLayoverEntity>
{
    public void Configure(EntityTypeBuilder<RouteLayoverEntity> builder)
    {
        builder.ToTable("route_stopovers");
        builder.HasKey(rl => rl.Id);
        builder.Property(rl => rl.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(rl => rl.RouteId).HasColumnName("route_id").IsRequired();
        builder.Property(rl => rl.LayoverAirportId).HasColumnName("stopover_airport_id").IsRequired();
        builder.Property(rl => rl.SequenceOrder).HasColumnName("stop_order").IsRequired();
        builder.Property(rl => rl.LayoverDurationMin).HasColumnName("layover_min").IsRequired().HasDefaultValue(0);
        builder.HasIndex(rl => new { rl.RouteId, rl.SequenceOrder }).IsUnique();

        builder
            .HasOne<RouteEntity>(x => x.Route)
            .WithMany(r => r.RouteLayovers)
            .HasForeignKey(rl => rl.RouteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<AirportEntity>(x => x.LayoverAirport)
            .WithMany(a => a.RouteLayovers)
            .HasForeignKey(rl => rl.LayoverAirportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}