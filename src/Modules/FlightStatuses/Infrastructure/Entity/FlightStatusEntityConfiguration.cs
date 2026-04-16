using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;

public class FlightStatusEntityConfiguration : IEntityTypeConfiguration<FlightStatusEntity>
{
    public void Configure(EntityTypeBuilder<FlightStatusEntity> builder)
    {
        builder.ToTable("flight_statuses");
        builder.HasKey(fs => fs.Id);
        builder.Property(fs => fs.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(fs => fs.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
    }
}