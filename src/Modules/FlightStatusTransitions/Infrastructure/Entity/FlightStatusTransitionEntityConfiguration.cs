using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Infrastructure.Data;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Infrastructure.Entity;

public class FlightStatusTransitionEntityConfiguration : IEntityTypeConfiguration<FlightStatusTransitionEntity>
{
    public void Configure(EntityTypeBuilder<FlightStatusTransitionEntity> builder)
    {
        builder.ToTable("FlightStatusTransitions");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.OriginStatusId)
            .HasColumnName("Origin_statusId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.DestinationStatusId)
            .HasColumnName("Destination_statusId")
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

        builder.HasData(FlightStatusTransitionDefaultData.FlightStatusTransitions);
    }
}
