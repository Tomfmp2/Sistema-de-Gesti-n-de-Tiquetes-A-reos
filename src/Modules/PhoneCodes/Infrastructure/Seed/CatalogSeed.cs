using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.PhoneCodes.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in PhoneCodeDefaultData.PhoneCodes)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.PhoneCodes.Add(new PhoneCodeEntity
            {
                Id = row.Id,
                CountryDialCode = row.CountryDialCode,
                CountryName = row.CountryName
            });
        }

        await context.SaveChangesAsync();
    }
}

