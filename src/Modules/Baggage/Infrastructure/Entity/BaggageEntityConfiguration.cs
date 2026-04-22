using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Config;

public class BaggageEntityConfiguration : IEntityTypeConfiguration<BaggageEntity>
{
    public void Configure(EntityTypeBuilder<BaggageEntity> builder)
    {
        builder.ToTable("baggage");

        builder.HasKey(e => e.Id).HasName("PK_baggage");

        builder.Property(e => e.Id)
            .IsRequired()
            .HasColumnName("id");

        builder.Property(e => e.CheckinId)
            .IsRequired()
            .HasColumnName("checkin_id");

        builder.Property(e => e.BaggageTypeId)
            .IsRequired()
            .HasColumnName("baggage_type_id");

        builder.Property(e => e.WeightKg)
            .IsRequired()
            .HasColumnType("decimal(5,2)")
            .HasColumnName("weight_kg")
            .HasPrecision(5, 2);

        builder.Property(e => e.ChargedPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)")
            .HasColumnName("charged_price")
            .HasPrecision(18, 2)
            .HasDefaultValue(0m);

        builder.HasIndex(e => e.CheckinId).HasDatabaseName("IX_baggage_checkin_id");
        builder.HasIndex(e => e.BaggageTypeId).HasDatabaseName("IX_baggage_baggage_type_id");

        builder
            .HasOne<CheckinEntity>(x => x.Checkin)
            .WithMany(c => c.Baggages)
            .HasForeignKey(e => e.CheckinId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<BaggageTypeEntity>(x => x.BaggageType)
            .WithMany(bt => bt.Baggages)
            .HasForeignKey(e => e.BaggageTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
