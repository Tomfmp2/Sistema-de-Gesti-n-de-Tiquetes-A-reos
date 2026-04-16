using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.Entity;

public class TicketEntityConfiguration : IEntityTypeConfiguration<TicketEntity>
{
    public void Configure(EntityTypeBuilder<TicketEntity> builder)
    {
        builder.ToTable("tickets");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.ReservationPassengerId)
            .HasColumnName("reservation_passenger_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.Code)
            .HasColumnName("ticket_code")
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder
            .Property(x => x.IssueDate)
            .HasColumnName("issue_date")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.TicketStatusId)
            .HasColumnName("ticket_status_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("datetime")
            .IsRequired();

        builder.HasIndex(x => x.ReservationPassengerId).IsUnique();
        builder.HasIndex(x => x.Code).IsUnique();

        builder
            .HasOne<ReservationPassengerEntity>()
            .WithMany()
            .HasForeignKey(x => x.ReservationPassengerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<TicketStatusEntity>()
            .WithMany()
            .HasForeignKey(x => x.TicketStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
