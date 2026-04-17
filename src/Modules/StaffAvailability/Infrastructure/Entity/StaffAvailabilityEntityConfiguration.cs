using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.Entity;

public class StaffAvailabilityEntityConfiguration : IEntityTypeConfiguration<StaffAvailabilityEntity>
{
    public void Configure(EntityTypeBuilder<StaffAvailabilityEntity> builder)
    {
        builder.HasKey(sa => sa.Id);

        builder.Property(sa => sa.Id)
            .HasColumnName("id")
            .HasColumnType("varchar(36)")
            .IsRequired();

        builder.Property(sa => sa.StaffId)
            .HasColumnName("staff_id")
            .HasColumnType("varchar(36)")
            .IsRequired();

        builder.Property(sa => sa.AvailabilityStatusId)
            .HasColumnName("availability_status_id")
            .HasColumnType("varchar(36)")
            .IsRequired();

        builder.Property(sa => sa.StartDate)
            .HasColumnName("start_date")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(sa => sa.EndDate)
            .HasColumnName("end_date")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(sa => sa.Observation)
            .HasColumnName("observation")
            .HasColumnType("varchar(255)")
            .IsRequired(false);

        builder.HasOne<StaffEntity>()
            .WithMany()
            .HasForeignKey(sa => sa.StaffId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<AvailabilityStatusEntity>()
            .WithMany()
            .HasForeignKey(sa => sa.AvailabilityStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}