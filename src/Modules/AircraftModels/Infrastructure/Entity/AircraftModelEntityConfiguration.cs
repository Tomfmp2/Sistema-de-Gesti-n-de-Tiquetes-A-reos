using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;

public class AircraftModelEntityConfiguration : IEntityTypeConfiguration<AircraftModelEntity>
{
    public void Configure(EntityTypeBuilder<AircraftModelEntity> builder)
    {
        builder.HasKey(am => am.Id);

        builder.Property(am => am.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(am => am.ManufacturerId)
            .HasColumnName("manufacturer_id")
            .IsRequired();

        builder.Property(am => am.ModelName)
            .HasColumnName("model_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(am => am.MaxCapacity)
            .HasColumnName("max_capacity")
            .IsRequired();

        builder.Property(am => am.MaxTakeoffWeightKg)
            .HasColumnName("max_takeoff_weight_kg")
            .HasColumnType("decimal(10,2)")
            .IsRequired(false);

        builder.Property(am => am.FuelConsumptionKgH)
            .HasColumnName("fuel_consumption_kg_h")
            .HasColumnType("decimal(8,2)")
            .IsRequired(false);

        builder.Property(am => am.CruisingSpeedKmh)
            .HasColumnName("cruising_speed_kmh")
            .IsRequired(false);

        builder.Property(am => am.CruisingAltitudeFt)
            .HasColumnName("cruising_altitude_ft")
            .IsRequired(false);

        // Foreign key constraints (commented out until related entities are created)
        // builder.HasOne<AircraftManufacturerEntity>()
        //     .WithMany()
        //     .HasForeignKey(am => am.ManufacturerId)
        //     .OnDelete(DeleteBehavior.Restrict);
    }
}