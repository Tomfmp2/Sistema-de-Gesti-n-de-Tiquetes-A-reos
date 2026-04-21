using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Entity;

public class AvailabilityStatusEntityConfiguration : IEntityTypeConfiguration<AvailabilityStatusEntity>
{
    public void Configure(EntityTypeBuilder<AvailabilityStatusEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
        builder.Property(e => e.Name).HasColumnName("Name").HasMaxLength(50).IsRequired();
        builder.HasIndex(e => e.Name).IsUnique();
    }
}