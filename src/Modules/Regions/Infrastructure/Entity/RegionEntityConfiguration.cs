using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Entity;

public class RegionEntityConfiguration : IEntityTypeConfiguration<RegionEntity>
{
    public void Configure(EntityTypeBuilder<RegionEntity> builder)
    {
        builder.ToTable("regions");

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
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder
            .Property(x => x.Type)
            .HasColumnName("type")
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder
            .Property(x => x.CountryId)
            .HasColumnName("country_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("tinyint(1)")
            .HasDefaultValue(true)
            .IsRequired();

        builder
            .HasOne<CountryEntity>(x => x.Country)
            .WithMany(x => x.Regions)
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.CountryId);
    }
}
