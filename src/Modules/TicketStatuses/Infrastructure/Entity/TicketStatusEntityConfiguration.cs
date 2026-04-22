using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Infrastructure.Data;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Infrastructure.Entity;

public class TicketStatusEntityConfiguration : IEntityTypeConfiguration<TicketStatusEntity>
{
    public void Configure(EntityTypeBuilder<TicketStatusEntity> builder)
    {
        builder.ToTable("TicketStatuses");

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

        builder.HasData(TicketStatusDefaultData.TicketStatuses);
    }
}
