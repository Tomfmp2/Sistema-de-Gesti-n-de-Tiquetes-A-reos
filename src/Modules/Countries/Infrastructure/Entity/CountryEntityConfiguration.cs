using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;

public class CountryEntityConfiguration : IEntityTypeConfiguration<CountryEntity>
{
    public void Configure(EntityTypeBuilder<CountryEntity> builder)
    {
        builder.ToTable("countries");

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
            .Property(x => x.CodeIso)
            .HasColumnName("code_iso")
            .HasColumnType("varchar(3)")
            .IsRequired();

        builder
            .Property(x => x.ContinentId)
            .HasColumnName("continent_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("tinyint(1)")
            .HasDefaultValue(true)
            .IsRequired();

        builder
            .HasOne<ContinentEntity>(x => x.Continent)
            .WithMany(x => x.Countries)
            .HasForeignKey(x => x.ContinentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.CodeIso).IsUnique();
        builder.HasIndex(x => x.ContinentId);
    }
}
