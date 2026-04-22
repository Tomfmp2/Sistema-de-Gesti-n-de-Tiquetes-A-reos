using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;

public class ReservationStatusEntityConfiguration
    : IEntityTypeConfiguration<ReservationStatusEntity>
{
    public void Configure(EntityTypeBuilder<ReservationStatusEntity> builder)
    {
        builder.ToTable("booking_statuses");

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
    }
}
