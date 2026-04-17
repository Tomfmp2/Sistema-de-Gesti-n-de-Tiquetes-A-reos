using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;

public class ReservationEntityConfiguration : IEntityTypeConfiguration<ReservationEntity>
{
    public void Configure(EntityTypeBuilder<ReservationEntity> builder)
    {
        builder.ToTable("reservations");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.ClientId)
            .HasColumnName("client_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.ReservationDate)
            .HasColumnName("reservation_date")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.ReservationStatusId)
            .HasColumnName("reservation_status_id")
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
            .HasColumnName("created_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .HasOne<ReservationStatusEntity>()
            .WithMany()
            .HasForeignKey(x => x.ReservationStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
