using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.EmailDomains.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in EmailDomainDefaultData.EmailDomains)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.EmailDomains.Add(new EmailDomainEntity { Id = row.Id, Domain = row.Domain });
        }

        await context.SaveChangesAsync();
    }
}

