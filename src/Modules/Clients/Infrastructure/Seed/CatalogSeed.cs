using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var personId = await context.Persons.AsNoTracking()
            .Where(p =>
                p.DocumentTypeId == ClientDefaultData.CatalogPersonDocumentTypeId
                && p.DocumentNumber == ClientDefaultData.CatalogPersonDocumentNumber
            )
            .Select(p => (int?)p.Id)
            .FirstOrDefaultAsync();

        if (personId is null)
            return;

        var exists = await context.Clients.AnyAsync(c => c.PersonId == personId.Value);
        if (exists)
            return;

        context.Clients.Add(
            new ClientEntity
            {
                PersonId = personId.Value,
                CreatedAt = ClientDefaultData.CatalogCreatedAt
            }
        );

        await context.SaveChangesAsync();
    }
}
