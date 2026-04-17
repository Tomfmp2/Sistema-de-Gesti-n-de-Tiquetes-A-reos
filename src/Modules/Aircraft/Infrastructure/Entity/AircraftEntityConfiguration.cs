using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;

public class AircraftEntityConfiguration : IEntityTypeConfiguration<AircraftEntity>
{
    public void Configure(EntityTypeBuilder<AircraftEntity> builder)
    {
        builder.ToTable("aircraft");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.ModelId)
            .HasColumnName("model_id")
            .IsRequired();

        builder.Property(a => a.AirlineId)
            .HasColumnName("airline_id")
            .IsRequired();

        builder.Property(a => a.Registration)
            .HasColumnName("registration")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(a => a.ManufacturingDate)
            .HasColumnName("manufacturing_date")
            .HasColumnType("date");

        builder.Property(a => a.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

        builder.HasOne(a => a.Model)
            .WithMany()
            .HasForeignKey(a => a.ModelId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Airline)
            .WithMany()
            .HasForeignKey(a => a.AirlineId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => a.Registration)
            .IsUnique();
    }
}