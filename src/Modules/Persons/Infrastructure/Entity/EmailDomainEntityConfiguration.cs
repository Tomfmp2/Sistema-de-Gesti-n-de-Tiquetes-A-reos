using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

public sealed class EmailDomainEntityConfiguration : IEntityTypeConfiguration<EmailDomainEntity>
{
    public void Configure(EntityTypeBuilder<EmailDomainEntity> builder)
    {
        builder.ToTable("dominios_email");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Domain)
            .HasColumnName("dominio")
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder.HasIndex(x => x.Domain).IsUnique();
    }
}
