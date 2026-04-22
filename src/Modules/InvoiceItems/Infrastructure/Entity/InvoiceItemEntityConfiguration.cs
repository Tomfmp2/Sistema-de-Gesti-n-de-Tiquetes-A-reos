using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Infrastructure.Entity;

public class InvoiceItemEntityConfiguration : IEntityTypeConfiguration<InvoiceItemEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceItemEntity> builder)
    {
        builder.ToTable("invoice_items");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.InvoiceId)
            .HasColumnName("invoice_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.InvoiceItemTypeId)
            .HasColumnName("invoice_item_type_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("varchar(200)")
            .IsRequired();

        builder
            .Property(x => x.Quantity)
            .HasColumnName("quantity")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.UnitPrice)
            .HasColumnName("unit_price")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .Property(x => x.Subtotal)
            .HasColumnName("subtotal")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .Property(x => x.ReservationPassengerId)
            .HasColumnName("reservation_passenger_id")
            .HasColumnType("int");

        builder
            .HasOne<InvoiceEntity>(x => x.Invoice)
            .WithMany(i => i.InvoiceItems)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<InvoiceItemTypeEntity>(x => x.InvoiceItemType)
            .WithMany(it => it.InvoiceItems)
            .HasForeignKey(x => x.InvoiceItemTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<ReservationPassengerEntity>(x => x.ReservationPassenger)
            .WithMany(rp => rp.InvoiceItems)
            .HasForeignKey(x => x.ReservationPassengerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
