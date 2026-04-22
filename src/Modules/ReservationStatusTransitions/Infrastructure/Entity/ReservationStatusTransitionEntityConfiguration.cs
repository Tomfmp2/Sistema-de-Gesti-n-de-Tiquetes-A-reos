using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Infrastructure.Entity;

public class ReservationStatusTransitionEntityConfiguration
    : IEntityTypeConfiguration<ReservationStatusTransitionEntity>
{
    public void Configure(EntityTypeBuilder<ReservationStatusTransitionEntity> builder)
    {
        builder.ToTable("booking_status_transitions");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.OriginStatusId)
            .HasColumnName("from_status_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.DestinationStatusId)
            .HasColumnName("to_status_id")
            .HasColumnType("int")
            .IsRequired();

        builder.HasIndex(x => new { x.OriginStatusId, x.DestinationStatusId }).IsUnique();

        builder
            .HasOne<ReservationStatusEntity>(x => x.OriginStatus)
            .WithMany(s => s.OriginTransitions)
            .HasForeignKey(x => x.OriginStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<ReservationStatusEntity>(x => x.DestinationStatus)
            .WithMany(s => s.DestinationTransitions)
            .HasForeignKey(x => x.DestinationStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
