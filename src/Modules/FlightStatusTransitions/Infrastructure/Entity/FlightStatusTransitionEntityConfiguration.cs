using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Infrastructure.Entity;

public class FlightStatusTransitionEntityConfiguration : IEntityTypeConfiguration<FlightStatusTransitionEntity>
{
    public void Configure(EntityTypeBuilder<FlightStatusTransitionEntity> builder)
    {
        builder.ToTable("flight_status_transitions");

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
            .HasOne<FlightStatusEntity>(x => x.OriginStatus)
            .WithMany(s => s.OriginTransitions)
            .HasForeignKey(x => x.OriginStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<FlightStatusEntity>(x => x.DestinationStatus)
            .WithMany(s => s.DestinationTransitions)
            .HasForeignKey(x => x.DestinationStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
