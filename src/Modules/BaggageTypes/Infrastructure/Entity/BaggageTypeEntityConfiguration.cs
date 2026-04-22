namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Entity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Data;

public class BaggageTypeEntityConfiguration : IEntityTypeConfiguration<BaggageTypeEntity>
{
    public void Configure(EntityTypeBuilder<BaggageTypeEntity> builder)
    {
        builder.Property(x => x.Id).HasColumnName("Id");

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(x => x.MaxWeightKg)
            .HasColumnName("MaxWeightKg")
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(x => x.BasePrice)
            .HasColumnName("BasePrice")
            .IsRequired()
            .HasPrecision(18, 2)
            .HasDefaultValue(0);

        builder.HasData(BaggageTypeDefaultData.BaggageTypes);
    }
}
