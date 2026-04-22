using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Infrastructure.Entity;

public class CheckinStatusEntityConfiguration : IEntityTypeConfiguration<CheckinStatusEntity>
{
    public void Configure(EntityTypeBuilder<CheckinStatusEntity> builder)
    {
        builder.ToTable("checkin_statuses");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(50)")
            .IsRequired();
    }
}
