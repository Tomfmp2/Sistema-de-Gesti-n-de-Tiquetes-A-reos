using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Entity;

public sealed class SystemRoleEntityConfiguration : IEntityTypeConfiguration<SystemRoleEntity>
{
    public void Configure(EntityTypeBuilder<SystemRoleEntity> builder)
    {
        // En el esquema MySQL del proyecto la tabla es `system_roles`.
        // Mantener esto en sync evita errores en runtime (login, FKs, etc.).
        builder.ToTable("system_roles");

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

        builder
            .Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("varchar(150)")
            .IsRequired(false);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}
