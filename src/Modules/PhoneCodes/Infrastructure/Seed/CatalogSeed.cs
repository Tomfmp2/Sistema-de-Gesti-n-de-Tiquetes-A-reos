using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.PhoneCodes.AsNoTracking().ToListAsync();
        var desired = new[]
        {
            new { Dial = "+57", Country = "Colombia" },
            new { Dial = "+1", Country = "Estados Unidos / Canada" },
            new { Dial = "+52", Country = "Mexico" },
            new { Dial = "+34", Country = "Espana" },
            new { Dial = "+33", Country = "Francia" },
            new { Dial = "+44", Country = "Reino Unido" },
            new { Dial = "+55", Country = "Brasil" },
            new { Dial = "+54", Country = "Argentina" },
            new { Dial = "+56", Country = "Chile" },
            new { Dial = "+51", Country = "Peru" },
        };

        foreach (var p in desired)
        {
            var norm = SeedHelpers.Normalize(p.Dial);
            if (existing.Any(x => SeedHelpers.Normalize(x.CountryDialCode) == norm))
                continue;

            context.PhoneCodes.Add(new PhoneCodeEntity { CountryDialCode = p.Dial, CountryName = p.Country });
        }

        await context.SaveChangesAsync();
    }
}

