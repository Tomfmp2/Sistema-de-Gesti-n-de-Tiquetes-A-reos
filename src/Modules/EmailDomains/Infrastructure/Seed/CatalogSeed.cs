using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.EmailDomains.AsNoTracking().ToListAsync();
        var desired = new[] { "gmail.com", "outlook.com", "hotmail.com", "yahoo.com", "icloud.com" };

        foreach (var domain in desired)
        {
            var norm = SeedHelpers.Normalize(domain);
            if (existing.Any(x => SeedHelpers.Normalize(x.Domain) == norm))
                continue;

            context.EmailDomains.Add(new EmailDomainEntity { Domain = domain });
        }

        await context.SaveChangesAsync();
    }
}

