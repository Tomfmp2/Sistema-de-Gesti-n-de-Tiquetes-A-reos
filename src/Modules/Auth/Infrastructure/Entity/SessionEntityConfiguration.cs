using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Auth.Infrastructure.Entity;

public sealed class SessionEntityConfiguration : IEntityTypeConfiguration<SessionEntity>
{
    public void Configure(EntityTypeBuilder<SessionEntity> builder)
    {
        builder.ToTable("sesiones");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.UserId)
            .HasColumnName("usuario_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.IssuedAtUtc)
            .HasColumnName("emitida_en_utc")
            .HasColumnType("datetime(6)")
            .IsRequired();

        builder
            .Property(x => x.ExpiresAtUtc)
            .HasColumnName("expira_en_utc")
            .HasColumnType("datetime(6)")
            .IsRequired();

        builder
            .Property(x => x.IsRevoked)
            .HasColumnName("revocada")
            .HasColumnType("tinyint(1)")
            .IsRequired();

        builder
            .Property(x => x.RefreshToken)
            .HasColumnName("refresh_token")
            .HasColumnType("varchar(500)")
            .IsRequired(false);

        builder
            .HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
