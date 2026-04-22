using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Data;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Entity;

public class CardTypeEntityConfiguration : IEntityTypeConfiguration<CardTypeEntity>
{
    public void Configure(EntityTypeBuilder<CardTypeEntity> builder)
    {
        builder.ToTable("CardTypes");

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

        builder.HasData(CardTypeDefaultData.CardTypes);
    }
}
