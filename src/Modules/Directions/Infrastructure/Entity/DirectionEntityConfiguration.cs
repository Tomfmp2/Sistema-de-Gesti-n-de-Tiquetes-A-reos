using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;

public sealed class DirectionEntityConfiguration : IEntityTypeConfiguration<DirectionEntity>
{
    public void Configure(EntityTypeBuilder<DirectionEntity> builder)
    {
        builder.ToTable("directions");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.StreetTypeId)
            .HasColumnName("Street_typeId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.StreetName)
            .HasColumnName("street_name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder
            .Property(x => x.StreetNumber)
            .HasColumnName("street_number")
            .HasColumnType("varchar(20)")
            .IsRequired(false);

        builder
            .Property(x => x.Complement)
            .HasColumnName("complement")
            .HasColumnType("varchar(100)")
            .IsRequired(false);

        builder
            .Property(x => x.CityId)
            .HasColumnName("CityId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.PostalCode)
            .HasColumnName("postal_code")
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

        builder.HasIndex(x => x.CityId);
        builder.HasIndex(x => x.StreetTypeId);
    }
}
