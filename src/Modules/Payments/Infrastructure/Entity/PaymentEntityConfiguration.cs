using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Infrastructure.Entity;

public class PaymentEntityConfiguration : IEntityTypeConfiguration<PaymentEntity>
{
    public void Configure(EntityTypeBuilder<PaymentEntity> builder)
    {
        builder.ToTable("payments");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.ReservationId)
            .HasColumnName("booking_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.Amount)
            .HasColumnName("amount")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .Property(x => x.PaymentDate)
            .HasColumnName("paid_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.PaymentStatusId)
            .HasColumnName("payment_status_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.PaymentMethodId)
            .HasColumnName("payment_method_id")
            .HasColumnType("int")
            .IsRequired();

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
            .HasOne<ReservationEntity>(x => x.Reservation)
            .WithMany(r => r.Payments)
            .HasForeignKey(x => x.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<PaymentStatusEntity>(x => x.PaymentStatus)
            .WithMany(ps => ps.Payments)
            .HasForeignKey(x => x.PaymentStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<PaymentMethodEntity>(x => x.PaymentMethod)
            .WithMany(pm => pm.Payments)
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
