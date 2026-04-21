using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;

public sealed class AirportEntityConfiguration : IEntityTypeConfiguration<AirportEntity>
{
    public void Configure(EntityTypeBuilder<AirportEntity> builder)
    {
        builder.ToTable("airports");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder
            .Property(x => x.IataCode)
            .HasColumnName("iata_code")
            .HasColumnType("varchar(3)")
            .IsRequired();

        builder.HasIndex(x => x.IataCode).IsUnique();

        builder
            .Property(x => x.IcaoCode)
            .HasColumnName("icao_code")
            .HasColumnType("varchar(4)")
            .IsRequired(false);

        builder.HasIndex(x => x.IcaoCode).IsUnique();

        builder
            .Property(x => x.CityId)
            .HasColumnName("CityId")
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne<CityEntity>()
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}