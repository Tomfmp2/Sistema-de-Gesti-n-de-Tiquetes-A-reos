using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;

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
            .HasColumnName("reservation_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.Number)
            .HasColumnName("invoice_number")
            .HasColumnType("varchar(30)")
            .IsRequired();

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

        builder.HasIndex(x => x.ReservationId).IsUnique();
        builder.HasIndex(x => x.Number).IsUnique();

        builder
            .HasOne<ReservationEntity>()
            .WithMany()
            .HasForeignKey(x => x.ReservationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
