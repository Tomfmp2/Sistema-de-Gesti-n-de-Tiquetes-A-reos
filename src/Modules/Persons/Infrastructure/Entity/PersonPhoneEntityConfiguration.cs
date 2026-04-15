using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

public sealed class PersonPhoneEntityConfiguration : IEntityTypeConfiguration<PersonPhoneEntity>
{
    public void Configure(EntityTypeBuilder<PersonPhoneEntity> builder)
    {
        builder.ToTable("personas_telefonos");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.PersonId)
            .HasColumnName("persona_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.PhoneCodeId)
            .HasColumnName("codigo_telefono_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.Number)
            .HasColumnName("numero")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder
            .Property(x => x.IsPrimary)
            .HasColumnName("es_principal")
            .HasColumnType("tinyint(1)")
            .IsRequired();

        builder
            .HasOne<PersonEntity>()
            .WithMany()
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<PhoneCodeEntity>()
            .WithMany()
            .HasForeignKey(x => x.PhoneCodeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.PersonId, x.PhoneCodeId, x.Number }).IsUnique();
    }
}
