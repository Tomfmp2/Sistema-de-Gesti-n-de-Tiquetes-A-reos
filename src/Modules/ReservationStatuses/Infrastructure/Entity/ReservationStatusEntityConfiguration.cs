using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Data;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;

public class ReservationStatusEntityConfiguration
    : IEntityTypeConfiguration<ReservationStatusEntity>
{
    public void Configure(EntityTypeBuilder<ReservationStatusEntity> builder)
    {
        builder.ToTable("ReservationStatuses");

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

        builder.HasData(ReservationStatusDefaultData.ReservationStatuses);
    }
}
