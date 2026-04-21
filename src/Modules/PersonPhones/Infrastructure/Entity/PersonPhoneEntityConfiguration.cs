using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Infrastructure.Entity;

public sealed class PersonPhoneEntityConfiguration : IEntityTypeConfiguration<PersonPhoneEntity>
{
    public void Configure(EntityTypeBuilder<PersonPhoneEntity> builder)
    {
        builder.ToTable("PersonPhones");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.PersonId)
            .HasColumnName("PersonId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.PhoneCodeId)
            .HasColumnName("PhonecodeId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.Number)
            .HasColumnName("phone_number")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder
            .Property(x => x.IsPrimary)
            .HasColumnName("is_primary")
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

        builder.HasIndex(x => x.PhoneCodeId);
        builder.HasIndex(x => new { x.PersonId, x.PhoneCodeId, x.Number }).IsUnique();
    }
}
