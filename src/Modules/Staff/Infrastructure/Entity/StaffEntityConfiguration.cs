using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

public class StaffEntityConfiguration : IEntityTypeConfiguration<StaffEntity>
{
    public void Configure(EntityTypeBuilder<StaffEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.PersonId).IsRequired();
        builder.Property(e => e.PositionId).IsRequired();
        builder.Property(e => e.AirlineId).IsRequired(false);
        builder.Property(e => e.AirportId).IsRequired(false);
        builder.Property(e => e.HireDate).IsRequired();
        builder.Property(e => e.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();
        builder.HasIndex(e => e.PersonId).IsUnique();

        // Relationships
        builder
            .HasOne(e => e.Person)
            .WithMany()
            .HasForeignKey(e => e.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Position)
            .WithMany()
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(e => e.Airline)
            .WithMany()
            .HasForeignKey(e => e.AirlineId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne(e => e.Airport)
            .WithMany()
            .HasForeignKey(e => e.AirportId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(e => e.FlightAssignments)
            .WithOne(fa => fa.Staff)
            .HasForeignKey("staff_id")
            .OnDelete(DeleteBehavior.Restrict);
    }
}