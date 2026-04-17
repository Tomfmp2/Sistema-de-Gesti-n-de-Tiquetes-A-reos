using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

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

        // Foreign keys commented out until related modules are created
        // builder.HasOne(/* related entity */).WithMany().HasForeignKey(e => e.PersonId);
        // builder.HasOne(/* related entity */).WithMany().HasForeignKey(e => e.PositionId);
        // builder.HasOne(/* related entity */).WithMany().HasForeignKey(e => e.AirlineId);
        // builder.HasOne(/* related entity */).WithMany().HasForeignKey(e => e.AirportId);
    }
}