using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.Entity;

public sealed class AirportAirlineEntityConfiguration : IEntityTypeConfiguration<AirportAirlineEntity>
{
    public void Configure(EntityTypeBuilder<AirportAirlineEntity> builder)
    {
        builder.ToTable("airport_airline");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.AirportId)
            .HasColumnName("airport_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.AirlineId)
            .HasColumnName("airline_id")
            .HasColumnType("int")
            .IsRequired();

        builder.HasIndex(x => new { x.AirportId, x.AirlineId }).IsUnique();

        builder
            .Property(x => x.Terminal)
            .HasColumnName("terminal")
            .HasColumnType("varchar(20)")
            .IsRequired(false);

        builder
            .Property(x => x.StartDate)
            .HasColumnName("start_date")
            .HasColumnType("date")
            .IsRequired();

        builder
            .Property(x => x.EndDate)
            .HasColumnName("end_date")
            .HasColumnType("date")
            .IsRequired(false);

        builder
            .Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("tinyint(1)")
            .IsRequired();

        builder
            .HasOne<AirportEntity>(x => x.Airport)
            .WithMany(x => x.AirportAirlines)
            .HasForeignKey(x => x.AirportId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<AirlineEntity>(x => x.Airline)
            .WithMany(a => a.AirportAirlines)
            .HasForeignKey(x => x.AirlineId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}