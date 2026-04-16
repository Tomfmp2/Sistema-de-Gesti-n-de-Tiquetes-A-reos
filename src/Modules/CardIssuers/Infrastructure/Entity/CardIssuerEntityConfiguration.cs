using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Entity;

public class CardIssuerEntityConfiguration : IEntityTypeConfiguration<CardIssuerEntity>
{
    public void Configure(EntityTypeBuilder<CardIssuerEntity> builder)
    {
        builder.ToTable("card_issuers");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.HasIndex(x => x.Name).IsUnique();
    }
}
