using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;

public sealed class PassengerTypeEntityConfiguration : IEntityTypeConfiguration<PassengerTypeEntity>
{
    public void Configure(EntityTypeBuilder<PassengerTypeEntity> builder)
    {
        builder.ToTable("PassengerTypes");

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

        builder
            .Property(x => x.MinAge)
            .HasColumnName("min_age")
            .HasColumnType("int")
            .IsRequired(false);

        builder
            .Property(x => x.MaxAge)
            .HasColumnName("max_age")
            .HasColumnType("int")
            .IsRequired(false);

        builder.HasIndex(x => x.Name).IsUnique();
    }
}
