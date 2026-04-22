using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Data;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;

public sealed class StreetTypeEntityConfiguration : IEntityTypeConfiguration<StreetTypeEntity>
{
    public void Configure(EntityTypeBuilder<StreetTypeEntity> builder)
    {
        builder.ToTable("StreetTypes");

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
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.HasData(StreetTypeDefaultData.StreetTypes);
    }
}
