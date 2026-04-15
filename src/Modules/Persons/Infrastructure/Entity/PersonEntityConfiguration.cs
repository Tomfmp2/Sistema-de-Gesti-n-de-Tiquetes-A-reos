using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

public sealed class PersonEntityConfiguration : IEntityTypeConfiguration<PersonEntity>
{
    public void Configure(EntityTypeBuilder<PersonEntity> builder)
    {
        builder.ToTable("personas");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.DocumentTypeId)
            .HasColumnName("tipo_documento_id")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.DocumentNumber)
            .HasColumnName("numero_documento")
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder
            .Property(x => x.FirstName)
            .HasColumnName("nombres")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .HasColumnName("apellidos")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder
            .Property(x => x.BirthDate)
            .HasColumnName("fecha_nacimiento")
            .HasColumnType("date")
            .IsRequired(false);

        builder
            .Property(x => x.Gender)
            .HasColumnName("genero")
            .HasColumnType("char(1)")
            .IsRequired(false);

        builder
            .Property(x => x.DirectionId)
            .HasColumnName("direccion_id")
            .HasColumnType("int")
            .IsRequired(false);

        builder
            .HasOne<DocumentTypeEntity>()
            .WithMany()
            .HasForeignKey(x => x.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<DirectionEntity>()
            .WithMany()
            .HasForeignKey(x => x.DirectionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.DocumentTypeId, x.DocumentNumber }).IsUnique();
    }
}
