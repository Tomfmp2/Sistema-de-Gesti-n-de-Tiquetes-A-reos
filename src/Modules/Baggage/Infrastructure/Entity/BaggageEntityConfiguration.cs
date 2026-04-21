using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Config;

public class BaggageEntityConfiguration : IEntityTypeConfiguration<BaggageEntity>
{
    public void Configure(EntityTypeBuilder<BaggageEntity> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK_Baggage");

        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnName("Id");

        builder.Property(e => e.CheckinId)
            .IsRequired()
            .HasColumnName("CheckinId");

        builder.Property(e => e.BaggageTypeId)
            .IsRequired()
            .HasColumnName("BaggageTypeId");

        builder.Property(e => e.WeightKg)
            .IsRequired()
            .HasColumnType("decimal(5,2)")
            .HasColumnName("WeightKg")
            .HasPrecision(5, 2);

        builder.Property(e => e.ChargedPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasColumnName("ChargedPrice")
            .HasPrecision(18, 2)
            .HasDefaultValue(0m);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasColumnName("CreatedAt");

        builder.Property(e => e.UpdatedAt)
            .IsRequired()
            .HasColumnName("UpdatedAt");

        builder.HasIndex(e => e.CheckinId).HasName("IX_Baggage_CheckinId");
        builder.HasIndex(e => e.BaggageTypeId).HasName("IX_Baggage_BaggageTypeId");
    }
}
