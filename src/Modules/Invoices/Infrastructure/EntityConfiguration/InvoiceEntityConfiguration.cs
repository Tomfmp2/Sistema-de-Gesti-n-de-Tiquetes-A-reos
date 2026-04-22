using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Infrastructure.Entity;

public class InvoiceEntityConfiguration : IEntityTypeConfiguration<InvoiceEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.ToTable("invoices");

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
            .Property(x => x.Number)
            .HasColumnName("invoice_number")
            .HasColumnType("varchar(30)");

        builder
            .Property(x => x.IssueDate)
            .HasColumnName("issue_date")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.Subtotal)
            .HasColumnName("subtotal")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .Property(x => x.Taxes)
            .HasColumnName("taxes")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .Property(x => x.Total)
            .HasColumnName("total")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime")
            .IsRequired();

        // Relationships
        builder
            .HasOne<ReservationEntity>(x => x.Reservation)
            .WithMany(r => r.Invoices)
            .HasForeignKey(x => x.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.InvoiceItems)
            .WithOne(ii => ii.Invoice)
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.ReservationId);
        builder.HasIndex(x => x.Number).IsUnique();
    }
}