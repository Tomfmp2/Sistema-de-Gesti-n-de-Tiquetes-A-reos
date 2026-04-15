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
            .Property(x => x.name)
            .HasColumnName("name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder
            .Property(x => x.codeIso)
            .HasColumnName("code_iso")
            .HasColumnType("varchar(3)")
            .IsRequired();

        builder
            .Property(x => x.continentId)
            .HasColumnName("continent_id")
            .HasColumnType("int")
            .IsRequired();
        builder
            .HasOne<ContinentEntity>()
            .WithMany()
            .HasForeignKey(x => x.continentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
