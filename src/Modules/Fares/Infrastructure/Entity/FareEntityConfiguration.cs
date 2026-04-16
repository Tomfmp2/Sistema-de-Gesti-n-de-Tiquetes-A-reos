using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Entity;

public class FareEntityConfiguration : IEntityTypeConfiguration<FareEntity>
{
    public void Configure(EntityTypeBuilder<FareEntity> builder)
    {
        builder.ToTable("fares");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.RouteId)
            .HasColumnName("route_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.CabinTypeId)
            .HasColumnName("cabin_type_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.PassengerTypeId)
            .HasColumnName("passenger_type_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.SeasonId)
            .HasColumnName("season_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.BasePrice)
            .HasColumnName("base_price")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .Property(x => x.ValidFrom)
            .HasColumnName("valid_from")
            .HasColumnType("date");

        builder
            .Property(x => x.ValidTo)
            .HasColumnName("valid_to")
            .HasColumnType("date");
    }
}
