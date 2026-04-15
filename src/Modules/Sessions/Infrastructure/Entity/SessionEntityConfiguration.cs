using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Infrastructure.Entity;

public sealed class SessionEntityConfiguration : IEntityTypeConfiguration<SessionEntity>
{
    public void Configure(EntityTypeBuilder<SessionEntity> builder)
    {
        builder.ToTable("sessions");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.UserId)
            .HasColumnName("user_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.StartedAt)
            .HasColumnName("started_at")
            .HasColumnType("datetime(6)")
            .IsRequired();

        builder
            .Property(x => x.ClosedAt)
            .HasColumnName("closed_at")
            .HasColumnType("datetime(6)")
            .IsRequired(false);

        builder
            .Property(x => x.OriginIp)
            .HasColumnName("origin_ip")
            .HasColumnType("varchar(45)")
            .IsRequired(false);

        builder
            .Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("tinyint(1)")
            .IsRequired();

        builder
            .HasOne<UserEntity>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.UserId);
    }
}
