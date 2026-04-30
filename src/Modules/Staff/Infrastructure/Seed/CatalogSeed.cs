using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.Staff.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in StaffDefaultData.Rows)
        {
            if (idSet.Contains(row.Id))
                continue;

            var personId = await context.Persons.AsNoTracking()
                .Where(p =>
                    p.DocumentTypeId == row.PersonDocumentTypeId && p.DocumentNumber == row.PersonDocumentNumber
                )
                .Select(p => (int?)p.Id)
                .FirstOrDefaultAsync();

            if (personId is null)
            {
                throw new InvalidOperationException(
                    $"Seed staff id={row.Id}: no hay persona con documento tipo {row.PersonDocumentTypeId} número {row.PersonDocumentNumber}."
                );
            }

            context.Staff.Add(
                new StaffEntity
                {
                    Id = row.Id,
                    PersonId = personId.Value,
                    PositionId = row.PositionId,
                    AirlineId = row.AirlineId,
                    AirportId = row.AirportId,
                    HireDate = row.HireDate,
                    IsActive = row.IsActive,
                    CreatedAt = row.CreatedAt,
                    UpdatedAt = row.UpdatedAt
                }
            );
        }

        await context.SaveChangesAsync();
    }
}
