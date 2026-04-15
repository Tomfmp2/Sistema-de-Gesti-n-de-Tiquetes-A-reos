using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

public sealed class PassengerEntityConfiguration : IEntityTypeConfiguration<PassengerEntity>
{
    public void Configure(EntityTypeBuilder<PassengerEntity> builder)
    {
        builder.ToTable("pasajeros");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.PersonId)
            .HasColumnName("persona_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.PassengerTypeId)
            .HasColumnName("tipo_pasajero_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .HasOne<PersonEntity>()
            .WithMany()
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<PassengerTypeEntity>()
            .WithMany()
            .HasForeignKey(x => x.PassengerTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.PersonId).IsUnique();
    }
}
