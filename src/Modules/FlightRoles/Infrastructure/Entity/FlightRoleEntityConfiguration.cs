using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Entity;

public class FlightRoleEntityConfiguration : IEntityTypeConfiguration<FlightRoleEntity>
{
    public void Configure(EntityTypeBuilder<FlightRoleEntity> builder)
    {
        builder.ToTable("flight_crew_roles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.FlightAssignments)
            .WithOne(fa => fa.FlightRole)
            .HasForeignKey(fa => fa.FlightRoleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
