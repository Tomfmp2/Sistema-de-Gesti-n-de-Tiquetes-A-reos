using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;

public class ReservationEntityConfiguration : IEntityTypeConfiguration<ReservationEntity>
{
    public void Configure(EntityTypeBuilder<ReservationEntity> builder)
    {
        builder.ToTable("bookings");

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
            .HasColumnName("booked_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.ReservationStatusId)
            .HasColumnName("booking_status_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.TotalValue)
            .HasColumnName("total_amount")
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
            .HasOne<ReservationStatusEntity>(x => x.ReservationStatus)
            .WithMany(s => s.Reservations)
            .HasForeignKey(x => x.ReservationStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<ClientEntity>(x => x.Client)
            .WithMany(c => c.Reservations)
            .HasForeignKey(x => x.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
