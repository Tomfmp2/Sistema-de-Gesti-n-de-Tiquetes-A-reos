using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Entity;

public sealed class ContinentEntityConfiguration : IEntityTypeConfiguration<ContinentEntity>
{
    public void Configure(EntityTypeBuilder<ContinentEntity> builder)
    {
        // Debe coincidir con `IntialMigration` (`continents`, `id`, `name`) — en MySQL Linux las tablas son case-sensitive.
        builder.ToTable("continents");

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
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("tinyint(1)")
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasIndex(x => x.Name).IsUnique();
    }
}
