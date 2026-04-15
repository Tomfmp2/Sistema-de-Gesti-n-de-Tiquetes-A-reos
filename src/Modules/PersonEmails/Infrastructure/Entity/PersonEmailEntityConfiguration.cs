using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Infrastructure.Entity;

public sealed class PersonEmailEntityConfiguration : IEntityTypeConfiguration<PersonEmailEntity>
{
    public void Configure(EntityTypeBuilder<PersonEmailEntity> builder)
    {
        builder.ToTable("person_emails");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.PersonId)
            .HasColumnName("person_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.EmailLocalPart)
            .HasColumnName("email_local_part")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder
            .Property(x => x.EmailDomainId)
            .HasColumnName("email_domain_id")
            .HasColumnType("int")
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
            .HasOne<EmailDomainEntity>()
            .WithMany()
            .HasForeignKey(x => x.EmailDomainId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.PersonId, x.EmailLocalPart, x.EmailDomainId }).IsUnique();
        builder.HasIndex(x => x.EmailDomainId);
        builder.HasIndex(x => x.PersonId);
    }
}
