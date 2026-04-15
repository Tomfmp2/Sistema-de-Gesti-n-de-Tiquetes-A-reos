using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;

public sealed class DirectionEntityConfiguration : IEntityTypeConfiguration<DirectionEntity>
{
    public void Configure(EntityTypeBuilder<DirectionEntity> builder)
    {
        builder.ToTable("direcciones");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.CityId)
            .HasColumnName("ciudad_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.StreetTypeId)
            .HasColumnName("tipo_via_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.StreetName)
            .HasColumnName("nombre_via")
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder
            .Property(x => x.StreetNumber)
            .HasColumnName("numero")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder
            .Property(x => x.Complement)
            .HasColumnName("complemento")
            .HasColumnType("varchar(100)")
            .IsRequired(false);

        builder
            .Property(x => x.PostalCode)
            .HasColumnName("codigo_postal")
            .HasColumnType("varchar(20)")
            .IsRequired(false);

        builder
            .HasOne<CityEntity>()
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<StreetTypeEntity>()
            .WithMany()
            .HasForeignKey(x => x.StreetTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
