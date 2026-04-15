using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;

public sealed class StreetTypeEntityConfiguration : IEntityTypeConfiguration<StreetTypeEntity>
{
    public void Configure(EntityTypeBuilder<StreetTypeEntity> builder)
    {
        builder.ToTable("tipos_via");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnName("nombre")
            .HasColumnType("varchar(80)")
            .IsRequired();
    }
}
