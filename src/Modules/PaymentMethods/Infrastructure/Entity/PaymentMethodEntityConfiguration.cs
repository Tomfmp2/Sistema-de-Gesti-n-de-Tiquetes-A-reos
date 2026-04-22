using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Entity;

public class PaymentMethodEntityConfiguration : IEntityTypeConfiguration<PaymentMethodEntity>
{
    public void Configure(EntityTypeBuilder<PaymentMethodEntity> builder)
    {
        builder.ToTable("PaymentMethods");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.PaymentMethodTypeId)
            .HasColumnName("Payment_method_typeId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.CardTypeId)
            .HasColumnName("Card_typeId")
            .HasColumnType("int");

        builder
            .Property(x => x.CardIssuerId)
            .HasColumnName("Card_issuerId")
            .HasColumnType("int");

        builder
            .Property(x => x.CommercialName)
            .HasColumnName("commercial_name")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.HasIndex(x => x.CommercialName).IsUnique();

        builder
            .HasOne<PaymentMethodTypeEntity>(x => x.PaymentMethodType)
            .WithMany(t => t.PaymentMethods)
            .HasForeignKey(x => x.PaymentMethodTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<CardTypeEntity>(x => x.CardType)
            .WithMany(ct => ct.PaymentMethods)
            .HasForeignKey(x => x.CardTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<CardIssuerEntity>(x => x.CardIssuer)
            .WithMany(ci => ci.PaymentMethods)
            .HasForeignKey(x => x.CardIssuerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(PaymentMethodDefaultData.PaymentMethods);
    }
}
