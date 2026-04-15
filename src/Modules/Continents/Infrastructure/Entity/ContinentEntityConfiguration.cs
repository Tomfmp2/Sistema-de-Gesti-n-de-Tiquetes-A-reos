using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Entity;

public sealed class ContinentEntityConfiguration : IEntityTypeConfiguration<ContinentEntity>
{
    public void Configure(EntityTypeBuilder<ContinentEntity> builder)
    {
        builder.ToTable("continents");

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

        builder.HasData(
            new ContinentEntity { Id = 1, Name = "América" },
            new ContinentEntity { Id = 2, Name = "Europa" },
            new ContinentEntity { Id = 3, Name = "Asia" },
            new ContinentEntity { Id = 4, Name = "África" },
            new ContinentEntity { Id = 5, Name = "Oceanía" }
        );
    }
}
