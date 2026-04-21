using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;

public class AircraftModelEntityConfiguration : IEntityTypeConfiguration<AircraftModelEntity>
{
    public void Configure(EntityTypeBuilder<AircraftModelEntity> builder)
    {
        builder.ToTable("aircraft_models");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.ManufacturerId)
            .HasColumnName("ManufacturerId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.ModelName)
            .HasColumnName("model_name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder
            .Property(x => x.MaxCapacity)
            .HasColumnName("max_capacity")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.MaxTakeoffWeightKg)
            .HasColumnName("max_takeoff_weight_kg")
            .HasColumnType("decimal(10,2)");

        builder
            .Property(x => x.FuelConsumptionKgH)
            .HasColumnName("fuel_consumption_kg_h")
            .HasColumnType("decimal(10,2)");

        builder
            .Property(x => x.CruisingSpeedKmh)
            .HasColumnName("cruising_speed_kmh")
            .HasColumnType("int");

        builder
            .Property(x => x.CruisingAltitudeFt)
            .HasColumnName("cruising_altitude_ft")
            .HasColumnType("int");

        // Relationships
        builder
            .HasOne(x => x.Manufacturer)
            .WithMany()
            .HasForeignKey(x => x.ManufacturerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.Aircrafts)
            .WithOne(x => x.Model)
            .HasForeignKey("model_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ManufacturerId);
        builder.HasIndex(x => x.ModelName);
    }
}