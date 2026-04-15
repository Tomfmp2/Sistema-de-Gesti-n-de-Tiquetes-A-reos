using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

public sealed class PhoneCodeEntityConfiguration : IEntityTypeConfiguration<PhoneCodeEntity>
{
    public void Configure(EntityTypeBuilder<PhoneCodeEntity> builder)
    {
        builder.ToTable("codigos_telefono");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.DialCode)
            .HasColumnName("codigo")
            .HasColumnType("varchar(10)")
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnName("nombre")
            .HasColumnType("varchar(100)")
            .IsRequired(false);

        builder.HasIndex(x => x.DialCode).IsUnique();
    }
}
