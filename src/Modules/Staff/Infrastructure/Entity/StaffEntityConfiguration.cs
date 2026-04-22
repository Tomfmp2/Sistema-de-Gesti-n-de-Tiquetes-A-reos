using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

public class StaffEntityConfiguration : IEntityTypeConfiguration<StaffEntity>
{
    public void Configure(EntityTypeBuilder<StaffEntity> builder)
    {
        builder.ToTable("staff");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasColumnType("int").ValueGeneratedOnAdd();
        builder.Property(e => e.PersonId).HasColumnName("person_id").HasColumnType("int").IsRequired();
        builder.Property(e => e.PositionId).HasColumnName("position_id").HasColumnType("int").IsRequired();
        builder.Property(e => e.AirlineId).HasColumnName("airline_id").HasColumnType("int").IsRequired(false);
        builder.Property(e => e.AirportId).HasColumnName("airport_id").HasColumnType("int").IsRequired(false);
        builder.Property(e => e.HireDate).HasColumnName("hire_date").HasColumnType("date").IsRequired();
        builder.Property(e => e.IsActive).HasColumnName("is_active").HasColumnType("tinyint(1)").IsRequired().HasDefaultValue(true);
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").HasColumnType("datetime").IsRequired();
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime").IsRequired();
        builder.HasIndex(e => e.PersonId).IsUnique();
        builder.HasIndex(e => e.PositionId);
        builder.HasIndex(e => e.AirlineId);
        builder.HasIndex(e => e.AirportId);

        // Relationships
        builder
            .HasOne<PersonEntity>(x => x.Person)
            .WithOne(p => p.Staff)
            .HasForeignKey<StaffEntity>(e => e.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<StaffPositionEntity>(x => x.Position)
            .WithMany(p => p.Staff)
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<AirlineEntity>(x => x.Airline)
            .WithMany(a => a.Staff)
            .HasForeignKey(e => e.AirlineId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasOne<AirportEntity>(x => x.Airport)
            .WithMany(a => a.Staff)
            .HasForeignKey(e => e.AirportId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}