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
            .HasColumnName("origin_status_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.DestinationStatusId)
            .HasColumnName("destination_status_id")
            .HasColumnType("int")
            .IsRequired();

        builder.HasIndex(x => new { x.OriginStatusId, x.DestinationStatusId }).IsUnique();

        builder
            .HasOne<FlightStatusEntity>()
            .WithMany()
            .HasForeignKey(x => x.OriginStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<FlightStatusEntity>()
            .WithMany()
            .HasForeignKey(x => x.DestinationStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
