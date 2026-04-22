using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Data;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Entity;

public class CardIssuerEntityConfiguration : IEntityTypeConfiguration<CardIssuerEntity>
{
    public void Configure(EntityTypeBuilder<CardIssuerEntity> builder)
    {
        builder.ToTable("CardIssuers");

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

        builder.HasData(CardIssuerDefaultData.CardIssuers);
    }
}
