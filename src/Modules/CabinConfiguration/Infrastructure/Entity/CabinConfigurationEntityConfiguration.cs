using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Entity;

public class CabinConfigurationEntityConfiguration : IEntityTypeConfiguration<CabinConfigurationEntity>
{
    public void Configure(EntityTypeBuilder<CabinConfigurationEntity> builder)
    {
        builder.ToTable("cabin_configurations");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.AircraftId)
            .HasColumnName("aircraft_id")
            .IsRequired();

        builder.Property(x => x.CabinTypeId)
            .HasColumnName("cabin_type_id")
            .IsRequired();

        builder.Property(x => x.StartRow)
            .HasColumnName("row_start")
            .IsRequired();

        builder.Property(x => x.EndRow)
            .HasColumnName("row_end")
            .IsRequired();

        builder.Property(x => x.SeatsPerRow)
            .HasColumnName("seats_per_row")
            .IsRequired();

        builder.Property(x => x.SeatLetters)
            .HasColumnName("seat_letters")
            .HasMaxLength(10)
            .IsRequired();

        builder.HasIndex(x => new { x.AircraftId, x.CabinTypeId })
            .IsUnique();

        builder.HasOne<AircraftEntity>(x => x.Aircraft)
            .WithMany(a => a.CabinConfigurations)
            .HasForeignKey(x => x.AircraftId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<CabinTypeEntity>(x => x.CabinType)
            .WithMany(ct => ct.CabinConfigurations)
            .HasForeignKey(x => x.CabinTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}