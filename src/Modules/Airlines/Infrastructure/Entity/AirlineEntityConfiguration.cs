using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;

public sealed class AirlineEntityConfiguration : IEntityTypeConfiguration<AirlineEntity>
{
    public void Configure(EntityTypeBuilder<AirlineEntity> builder)
    {
        builder.ToTable("airlines");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder
            .Property(x => x.IataCode)
            .HasColumnName("iata_code")
            .HasColumnType("varchar(3)")
            .IsRequired();

        builder.HasIndex(x => x.IataCode).IsUnique();

        builder
            .Property(x => x.OriginCountryId)
            .HasColumnName("origin_country_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("tinyint(1)")
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder.HasOne<CountryEntity>(x => x.OriginCountry)
            .WithMany(c => c.Airlines)
            .HasForeignKey(x => x.OriginCountryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
