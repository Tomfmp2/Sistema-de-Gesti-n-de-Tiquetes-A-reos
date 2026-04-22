using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Entity;

public class SeatLocationTypeEntityConfiguration : IEntityTypeConfiguration<SeatLocationTypeEntity>
{
    public void Configure(EntityTypeBuilder<SeatLocationTypeEntity> builder)
    {
        builder.ToTable("SeatLocationTypes");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("Id").ValueGeneratedOnAdd();
        builder.Property(s => s.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
        builder.HasIndex(s => s.Name).IsUnique();
        builder.HasData(SeatLocationTypeDefaultData.SeatLocationTypes);
    }
}
