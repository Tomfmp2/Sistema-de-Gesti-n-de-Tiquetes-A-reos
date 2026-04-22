using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Infrastructure.Data;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Infrastructure.Entity;

public class CheckinStatusEntityConfiguration : IEntityTypeConfiguration<CheckinStatusEntity>
{
    public void Configure(EntityTypeBuilder<CheckinStatusEntity> builder)
    {
        builder.ToTable("CheckinStatuses");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.HasData(CheckinStatusDefaultData.CheckinStatuses);
    }
}
