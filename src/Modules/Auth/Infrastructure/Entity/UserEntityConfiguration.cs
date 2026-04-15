using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Auth.Infrastructure.Entity;

public sealed class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("usuarios");

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
            .IsRequired(false);

        builder
            .Property(x => x.SystemRoleId)
            .HasColumnName("rol_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.Username)
            .HasColumnName("nombre_usuario")
            .HasColumnType("varchar(64)")
            .IsRequired();

        builder
            .Property(x => x.PasswordHash)
            .HasColumnName("password_hash")
            .HasColumnType("varchar(255)")
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .HasColumnName("activo")
            .HasColumnType("tinyint(1)")
            .IsRequired();

        builder
            .HasOne<PersonEntity>()
            .WithMany()
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<SystemRoleEntity>()
            .WithMany()
            .HasForeignKey(x => x.SystemRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Username).IsUnique();
        builder.HasIndex(x => x.PersonId).IsUnique();
    }
}
