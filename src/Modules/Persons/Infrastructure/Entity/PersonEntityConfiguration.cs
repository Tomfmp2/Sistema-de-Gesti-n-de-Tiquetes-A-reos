using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

public sealed class PersonEntityConfiguration : IEntityTypeConfiguration<PersonEntity>
{
    public void Configure(EntityTypeBuilder<PersonEntity> builder)
    {
        builder.ToTable("persons");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.DocumentTypeId)
            .HasColumnName("DocumenttypeId")
            .HasColumnType("int")
            .IsRequired();

        builder
            .Property(x => x.DocumentNumber)
            .HasColumnName("document_number")
            .HasColumnType("varchar(30)")
            .IsRequired();

        builder
            .Property(x => x.FirstName)
            .HasColumnName("first_name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .HasColumnName("last_name")
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder
            .Property(x => x.BirthDate)
            .HasColumnName("birth_date")
            .HasColumnType("date")
            .IsRequired(false);

        builder
            .Property(x => x.Gender)
            .HasColumnName("gender")
            .HasColumnType("char(1)")
            .IsRequired(false);

        builder
            .Property(x => x.DirectionId)
            .HasColumnName("DirectionId")
            .HasColumnType("int")
            .IsRequired(false);

        builder
            .Property(x => x.CreatedAt)
            .HasColumnName("CreatedAt")
            .HasColumnType("datetime(6)")
            .IsRequired();

        builder
            .Property(x => x.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .HasColumnType("datetime(6)")
            .IsRequired();

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

        builder.HasIndex(x => x.DirectionId);
        builder.HasIndex(x => new { x.DocumentTypeId, x.DocumentNumber }).IsUnique();
    }
}
