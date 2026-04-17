using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Entity;

public class PaymentMethodEntityConfiguration : IEntityTypeConfiguration<PaymentMethodEntity>
{
    public void Configure(EntityTypeBuilder<PaymentMethodEntity> builder)
    {
        builder.ToTable("payment_methods");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.PaymentMethodTypeId)
            .HasColumnName("payment_method_type_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.CardTypeId)
            .HasColumnName("card_type_id")
            .HasColumnType("int");

        builder
            .Property(x => x.CardIssuerId)
            .HasColumnName("card_issuer_id")
            .HasColumnType("int");

        builder
            .Property(x => x.CommercialName)
            .HasColumnName("commercial_name")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.HasIndex(x => x.CommercialName).IsUnique();

        builder
            .HasOne<PaymentMethodTypeEntity>()
            .WithMany()
            .HasForeignKey(x => x.PaymentMethodTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<CardTypeEntity>()
            .WithMany()
            .HasForeignKey(x => x.CardTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<CardIssuerEntity>()
            .WithMany()
            .HasForeignKey(x => x.CardIssuerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
