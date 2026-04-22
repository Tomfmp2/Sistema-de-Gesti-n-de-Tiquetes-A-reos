using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Data;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;

public class CountryEntityConfiguration : IEntityTypeConfiguration<CountryEntity>
{
    public void Configure(EntityTypeBuilder<CountryEntity> builder)
    {
        builder.ToTable("countries");

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
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder
            .Property(x => x.CodeIso)
            .HasColumnName("CodeIso")
            .HasColumnType("varchar(3)")
            .IsRequired();

        builder
            .Property(x => x.ContinentId)
            .HasColumnName("ContinentId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .HasOne<ContinentEntity>(x => x.Continent)
            .WithMany(x => x.Countries)
            .HasForeignKey(x => x.ContinentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.CodeIso).IsUnique();
        builder.HasIndex(x => x.ContinentId);

        builder.HasData(CountryDefaultData.Countries);
    }
}
