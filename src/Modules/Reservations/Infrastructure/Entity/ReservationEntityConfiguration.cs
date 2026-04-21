using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;

public class ReservationEntityConfiguration : IEntityTypeConfiguration<ReservationEntity>
{
    public void Configure(EntityTypeBuilder<ReservationEntity> builder)
    {
        builder.ToTable("reservations");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.ClientId)
            .HasColumnName("ClientId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.ReservationDate)
            .HasColumnName("reservation_date")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.ReservationStatusId)
            .HasColumnName("ReservationstatusId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.TotalValue)
            .HasColumnName("total_value")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .Property(x => x.ExpiresAt)
            .HasColumnName("expires_at")
            .HasColumnType("datetime");

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .HasOne(x => x.ReservationStatus)
            .WithMany()
            .HasForeignKey(x => x.ReservationStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Client)
            .WithMany()
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
