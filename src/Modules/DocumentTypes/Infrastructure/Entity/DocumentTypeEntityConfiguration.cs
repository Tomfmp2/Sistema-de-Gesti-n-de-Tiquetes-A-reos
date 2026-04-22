using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Data;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;

public sealed class DocumentTypeEntityConfiguration : IEntityTypeConfiguration<DocumentTypeEntity>
{
    public void Configure(EntityTypeBuilder<DocumentTypeEntity> builder)
    {
        builder.ToTable("document_types");

        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("int")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder
            .Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder
            .Property(x => x.Code)
            .HasColumnName("code")
            .HasColumnType("varchar(10)")
            .IsRequired();

        builder.HasIndex(x => x.Code).IsUnique();

        builder.HasData(DocumentTypeDefaultData.DocumentTypes);
    }
}
