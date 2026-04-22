using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Data;

public static class ReservationStatusSeeder
{
    /// <summary>
    /// Seed idempotente para `booking_statuses` (requerido por el módulo de Reservas).
    /// No depende de migraciones ni nombres de foreign keys.
    /// </summary>
    public static async Task EnsureAsync(AppDbContext context, CancellationToken cancellationToken = default)
    {
        var existingIds = await context.Database.SqlQueryRaw<int>(
            "SELECT id AS Value FROM booking_statuses"
        ).ToListAsync(cancellationToken);

        var existing = new HashSet<int>(existingIds);
        foreach (var s in ReservationStatusDefaultData.ReservationStatuses)
        {
            if (existing.Contains(s.Id))
                continue;

            await context.Database.ExecuteSqlRawAsync(
                "INSERT INTO booking_statuses (id, name) VALUES ({0}, {1})",
                [s.Id, s.Name ?? $"status_{s.Id}"],
                cancellationToken
            );
        }
    }
}

