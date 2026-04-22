using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;

public class CabinTypeEntityConfiguration : IEntityTypeConfiguration<CabinTypeEntity>
{
    public void Configure(EntityTypeBuilder<CabinTypeEntity> builder)
    {
        builder.ToTable("cabin_types");

        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(ct => ct.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(ct => ct.Name)
            .IsUnique();

        builder.HasData(CabinTypeDefaultData.CabinTypes);
    }
}
