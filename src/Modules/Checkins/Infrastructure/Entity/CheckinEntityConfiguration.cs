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
        builder.ToTable("check_ins");

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
            .HasColumnName("checked_in_at")
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
            .HasOne<TicketEntity>(x => x.Ticket)
            .WithOne(t => t.Checkin)
            .HasForeignKey<CheckinEntity>(x => x.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<CheckinStatusEntity>(x => x.CheckinStatus)
            .WithMany(cs => cs.Checkins)
            .HasForeignKey(x => x.CheckinStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<StaffEntity>(x => x.Staff)
            .WithMany(s => s.Checkins)
            .HasForeignKey(x => x.StaffId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<FlightSeatEntity>(x => x.FlightSeat)
            .WithOne(fs => fs.Checkin)
            .HasForeignKey<CheckinEntity>(x => x.FlightSeatId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
