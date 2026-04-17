using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.Entity;

public class RouteLayoverEntityConfiguration : IEntityTypeConfiguration<RouteLayoverEntity>
{
    public void Configure(EntityTypeBuilder<RouteLayoverEntity> builder)
    {
        builder.ToTable("route_layovers");
        builder.HasKey(rl => rl.Id);
        builder.Property(rl => rl.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(rl => rl.RouteId).HasColumnName("route_id").IsRequired();
        builder.Property(rl => rl.LayoverAirportId).HasColumnName("layover_airport_id").IsRequired();
        builder.Property(rl => rl.SequenceOrder).HasColumnName("sequence_order").IsRequired();
        builder.Property(rl => rl.LayoverDurationMin).HasColumnName("layover_duration_min").IsRequired().HasDefaultValue(0);
        builder.HasIndex(rl => new { rl.RouteId, rl.SequenceOrder }).IsUnique();
    }
}