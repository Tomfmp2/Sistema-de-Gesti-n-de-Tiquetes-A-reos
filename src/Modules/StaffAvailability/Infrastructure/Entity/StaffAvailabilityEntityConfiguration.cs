using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.Entity;

public class StaffAvailabilityEntityConfiguration : IEntityTypeConfiguration<StaffAvailabilityEntity>
{
    public void Configure(EntityTypeBuilder<StaffAvailabilityEntity> builder)
    {
        builder.ToTable("staff_availability");

        builder.HasKey(sa => sa.Id);

        builder.Property(sa => sa.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(sa => sa.StaffId)
            .HasColumnName("staff_id")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(sa => sa.AvailabilityStatusId)
            .HasColumnName("availability_status_id")
            .HasColumnType("int")
            .IsRequired();

        builder.Property(sa => sa.StartsAt)
            .HasColumnName("starts_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(sa => sa.EndsAt)
            .HasColumnName("ends_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(sa => sa.Notes)
            .HasColumnName("notes")
            .HasColumnType("varchar(255)");

        builder
            .HasOne<StaffEntity>(x => x.Staff)
            .WithMany(s => s.StaffAvailabilities)
            .HasForeignKey(sa => sa.StaffId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<AvailabilityStatusEntity>(x => x.AvailabilityStatus)
            .WithMany(st => st.StaffAvailabilities)
            .HasForeignKey(sa => sa.AvailabilityStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
