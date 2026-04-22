using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.Entity;



public class FlightAssignmentEntityConfiguration : IEntityTypeConfiguration<FlightAssignmentEntity>

{

    public void Configure(EntityTypeBuilder<FlightAssignmentEntity> builder)

    {

        builder.ToTable("flight_crew_assignments");



        builder.HasKey(x => x.Id);



        builder

            .Property(x => x.Id)

            .HasColumnName("id")

            .HasColumnType("int")

            .ValueGeneratedOnAdd()

            .IsRequired();



        builder

            .Property(x => x.FlightId)

            .HasColumnName("flight_id")

            .HasColumnType("int")

            .IsRequired();



        builder

            .Property(x => x.StaffId)

            .HasColumnName("staff_id")

            .HasColumnType("int")

            .IsRequired();



        builder

            .Property(x => x.FlightRoleId)

            .HasColumnName("crew_role_id")

            .HasColumnType("int")

            .IsRequired();



        builder.HasIndex(x => new { x.FlightId, x.StaffId }).IsUnique();



        builder
            .HasOne<FlightEntity>(x => x.Flight)
            .WithMany(f => f.FlightAssignments)
            .HasForeignKey(x => x.FlightId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<StaffEntity>(x => x.Staff)
            .WithMany(s => s.FlightAssignments)
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.Restrict);



        builder
            .HasOne<FlightRoleEntity>(x => x.FlightRole)
            .WithMany(r => r.FlightAssignments)
            .HasForeignKey(x => x.FlightRoleId)
            .OnDelete(DeleteBehavior.Restrict);

    }

}

