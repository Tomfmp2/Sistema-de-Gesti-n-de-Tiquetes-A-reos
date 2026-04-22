using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Entity;

public class StaffPositionEntityConfiguration : IEntityTypeConfiguration<StaffPositionEntity>
{
    public void Configure(EntityTypeBuilder<StaffPositionEntity> builder)
    {
        builder.ToTable("staff_positions");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
        builder.HasIndex(e => e.Name).IsUnique();
    }
}
