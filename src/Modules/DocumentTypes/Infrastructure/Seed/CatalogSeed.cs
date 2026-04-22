using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.DocumentTypes.AsNoTracking().ToListAsync();
        var desired = new[]
        {
            new { Code = "CC", Name = "Cedula de ciudadania" },
            new { Code = "CE", Name = "Cedula de extranjeria" },
            new { Code = "PAS", Name = "Pasaporte" },
            new { Code = "TI", Name = "Tarjeta de identidad" },
            new { Code = "NIT", Name = "NIT" },
        };

        foreach (var d in desired)
        {
            var codeNorm = SeedHelpers.Normalize(d.Code);
            if (existing.Any(x => SeedHelpers.Normalize(x.Code) == codeNorm))
                continue;

            context.DocumentTypes.Add(new DocumentTypeEntity { Code = d.Code, Name = d.Name });
        }

        await context.SaveChangesAsync();
    }
}

