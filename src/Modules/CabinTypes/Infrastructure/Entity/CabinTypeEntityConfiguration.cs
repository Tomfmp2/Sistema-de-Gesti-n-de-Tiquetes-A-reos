using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;

public class CabinTypeEntityConfiguration : IEntityTypeConfiguration<CabinTypeEntity>
{
    public void Configure(EntityTypeBuilder<CabinTypeEntity> builder)
    {
        builder.ToTable("CabinTypes");

        builder.HasKey(ct => ct.Id);

        builder.Property(ct => ct.Id)
            .HasColumnName("Id")
            .ValueGeneratedOnAdd();

        builder.Property(ct => ct.Name)
            .HasColumnName("Name")
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(ct => ct.Name)
            .IsUnique();
    }
}