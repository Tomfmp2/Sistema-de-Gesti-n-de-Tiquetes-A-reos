using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Auth.Infrastructure.Entity;

public sealed class RolePermissionEntityConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
{
    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.ToTable("roles_permisos");

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder
            .Property(x => x.RoleId)
            .HasColumnName("rol_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.PermissionId)
            .HasColumnName("permiso_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .HasOne<SystemRoleEntity>()
            .WithMany()
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne<PermissionEntity>()
            .WithMany()
            .HasForeignKey(x => x.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
