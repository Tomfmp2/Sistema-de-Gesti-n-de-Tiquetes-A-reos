using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Data;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Entity;

public class PaymentStatusEntityConfiguration : IEntityTypeConfiguration<PaymentStatusEntity>
{
    public void Configure(EntityTypeBuilder<PaymentStatusEntity> builder)
    {
        builder.ToTable("PaymentStatuses");

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

        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasData(PaymentStatusDefaultData.PaymentStatuses);
    }
}
