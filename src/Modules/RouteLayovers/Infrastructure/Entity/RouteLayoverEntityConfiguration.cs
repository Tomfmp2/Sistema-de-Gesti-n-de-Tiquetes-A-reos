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
        builder.ToTable("RouteLayovers");
        builder.HasKey(rl => rl.Id);
        builder.Property(rl => rl.Id).HasColumnName("Id").ValueGeneratedOnAdd();
        builder.Property(rl => rl.RouteId).HasColumnName("RouteId").IsRequired();
        builder.Property(rl => rl.LayoverAirportId).HasColumnName("LayoverairportId").IsRequired();
        builder.Property(rl => rl.SequenceOrder).HasColumnName("SequenceOrder").IsRequired();
        builder.Property(rl => rl.LayoverDurationMin).HasColumnName("LayoverDurationMin").IsRequired().HasDefaultValue(0);
        builder.HasIndex(rl => new { rl.RouteId, rl.SequenceOrder }).IsUnique();

        builder
            .HasOne<RouteEntity>()
            .WithMany()
            .HasForeignKey(rl => rl.RouteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<AirportEntity>()
            .WithMany()
            .HasForeignKey(rl => rl.LayoverAirportId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}