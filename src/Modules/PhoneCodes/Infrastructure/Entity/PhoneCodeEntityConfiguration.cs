using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Data;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Entity;

public sealed class PhoneCodeEntityConfiguration : IEntityTypeConfiguration<PhoneCodeEntity>
{
    public void Configure(EntityTypeBuilder<PhoneCodeEntity> builder)
    {
        builder.ToTable("PhoneCodes");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.CountryDialCode)
            .HasColumnName("country_dial_code")
            .HasColumnType("varchar(5)")
            .IsRequired();

        builder
            .Property(x => x.CountryName)
            .HasColumnName("country_name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.HasIndex(x => x.CountryDialCode).IsUnique();

        builder.HasData(PhoneCodeDefaultData.PhoneCodes);
    }
}
