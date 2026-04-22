using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Data;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Entity;

public sealed class EmailDomainEntityConfiguration : IEntityTypeConfiguration<EmailDomainEntity>
{
    public void Configure(EntityTypeBuilder<EmailDomainEntity> builder)
    {
        builder.ToTable("email_domains");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Domain)
            .HasColumnName("domain")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.HasIndex(x => x.Domain).IsUnique();

        builder.HasData(EmailDomainDefaultData.EmailDomains);
    }
}
