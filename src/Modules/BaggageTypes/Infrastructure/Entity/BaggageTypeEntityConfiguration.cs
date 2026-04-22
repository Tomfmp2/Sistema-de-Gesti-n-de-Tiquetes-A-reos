namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Entity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Data;

public class BaggageTypeEntityConfiguration : IEntityTypeConfiguration<BaggageTypeEntity>
{
    public void Configure(EntityTypeBuilder<BaggageTypeEntity> builder)
    {
        builder.ToTable("baggage_types");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.MaxWeightKg)
            .HasColumnName("max_weight_kg")
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(x => x.BasePrice)
            .HasColumnName("base_price")
            .IsRequired()
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.HasData(BaggageTypeDefaultData.BaggageTypes);
    }
}
