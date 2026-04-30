using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Infrastructure.Entity;

public class MilesTransactionEntityConfiguration : IEntityTypeConfiguration<MilesTransactionEntity>
{
    public void Configure(EntityTypeBuilder<MilesTransactionEntity> builder)
    {
        builder.ToTable("miles_transactions");

        builder.HasKey(mt => mt.Id);

        builder.Property(mt => mt.Amount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(mt => mt.TransactionType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(mt => mt.TransactionDate)
            .IsRequired();

        // Relaciones
        builder.HasOne(mt => mt.Client)
            .WithMany(c => c.MilesTransactions)
            .HasForeignKey(mt => mt.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(mt => mt.Reservation)
            .WithMany()
            .HasForeignKey(mt => mt.ReservationId)
            .OnDelete(DeleteBehavior.SetNull); // Si se elimina reserva, mantener la transacción (auditoría)
    }
}
