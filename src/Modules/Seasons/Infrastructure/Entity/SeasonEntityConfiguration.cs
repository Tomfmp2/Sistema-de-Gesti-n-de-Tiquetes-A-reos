using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Entity;

public class SeasonEntityConfiguration : IEntityTypeConfiguration<SeasonEntity>
{
    public void Configure(EntityTypeBuilder<SeasonEntity> builder)
    {
        builder.ToTable("seasons");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(s => s.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
        builder.Property(s => s.Description).HasColumnName("description").HasMaxLength(150);
        builder.Property(s => s.PriceFactor).HasColumnName("price_factor").IsRequired().HasDefaultValue(1.0000m).HasPrecision(5, 4);
        builder.HasIndex(s => s.Name).IsUnique();
    }
}