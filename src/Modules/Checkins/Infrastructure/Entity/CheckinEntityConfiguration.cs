using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Infrastructure.Entity;

public class CheckinEntityConfiguration : IEntityTypeConfiguration<CheckinEntity>
{
    public void Configure(EntityTypeBuilder<CheckinEntity> builder)
    {
        builder.ToTable("checkins");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.TicketId)
            .HasColumnName("ticket_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.StaffId)
            .HasColumnName("staff_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.FlightSeatId)
            .HasColumnName("flight_seat_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.CheckinDate)
            .HasColumnName("checkin_date")
            .HasColumnType("datetime")
            .IsRequired();

        builder
            .Property(x => x.CheckinStatusId)
            .HasColumnName("checkin_status_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.BoardingPassNumber)
            .HasColumnName("boarding_pass_number")
            .HasColumnType("varchar(20)")
            .IsRequired();

        builder
            .Property(x => x.HasCheckedBaggage)
            .HasColumnName("checked_baggage")
            .HasColumnType("tinyint(1)")
            .IsRequired();

        builder
            .Property(x => x.BaggageWeightKg)
            .HasColumnName("baggage_weight_kg")
            .HasColumnType("decimal(5,2)");

        builder.HasIndex(x => x.TicketId).IsUnique();
        builder.HasIndex(x => x.FlightSeatId).IsUnique();
        builder.HasIndex(x => x.BoardingPassNumber).IsUnique();

        builder
            .HasOne<TicketEntity>()
            .WithMany()
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<CheckinStatusEntity>()
            .WithMany()
            .HasForeignKey(x => x.CheckinStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<StaffEntity>()
            .WithMany()
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<FlightSeatEntity>()
            .WithMany()
            .HasForeignKey(x => x.FlightSeatId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
