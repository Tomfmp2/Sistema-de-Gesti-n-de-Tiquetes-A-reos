using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.Entity;

public class FlightAssignmentEntityConfiguration : IEntityTypeConfiguration<FlightAssignmentEntity>
{
    public void Configure(EntityTypeBuilder<FlightAssignmentEntity> builder)
    {
        builder.ToTable("FlightAssignments");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.FlightId)
            .HasColumnName("FlightId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.StaffId)
            .HasColumnName("StaffId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.FlightRoleId)
            .HasColumnName("FlightroleId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .HasOne(x => x.Flight)
            .WithMany()
            .HasForeignKey(x => x.FlightId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.Staff)
            .WithMany()
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.Restrict);

        // TODO: Agregar FK para FlightRoleId cuando el módulo FlightRoles esté completado
        // builder
        //     .HasOne<FlightRoleEntity>()
        //     .WithMany()
        //     .HasForeignKey(x => x.FlightRoleId)
        //     .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.FlightId, x.StaffId, x.FlightRoleId }).IsUnique();
    }
}
