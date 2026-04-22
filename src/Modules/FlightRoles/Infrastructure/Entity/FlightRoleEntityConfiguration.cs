using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Entity;

public class FlightRoleEntityConfiguration : IEntityTypeConfiguration<FlightRoleEntity>
{
    public void Configure(EntityTypeBuilder<FlightRoleEntity> builder)
    {
        builder.ToTable("FlightRoles");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(100)")
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(x => x.Name).IsUnique();

        builder.HasMany(x => x.FlightAssignments)
            .WithOne()
            .HasForeignKey("flight_role_id")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(FlightRoleDefaultData.FlightRoles);
    }
}
