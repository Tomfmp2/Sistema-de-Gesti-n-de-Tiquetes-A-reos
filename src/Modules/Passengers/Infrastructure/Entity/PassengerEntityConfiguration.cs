using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Infrastructure.Entity;

public sealed class PassengerEntityConfiguration : IEntityTypeConfiguration<PassengerEntity>
{
    public void Configure(EntityTypeBuilder<PassengerEntity> builder)
    {
        builder.ToTable("passengers");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.PersonId)
            .HasColumnName("PersonId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.PassengerTypeId)
            .HasColumnName("PassengerTypeId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .HasOne<PersonEntity>(x => x.Person)
            .WithOne(p => p.Passenger)
            .HasForeignKey<PassengerEntity>(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<PassengerTypeEntity>(x => x.PassengerType)
            .WithMany(pt => pt.Passengers)
            .HasForeignKey(x => x.PassengerTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.PersonId).IsUnique();
        builder.HasIndex(x => x.PassengerTypeId);
    }
}
