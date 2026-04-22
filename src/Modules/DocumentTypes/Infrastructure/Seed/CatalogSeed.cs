using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.DocumentTypes.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in DocumentTypeDefaultData.DocumentTypes)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.DocumentTypes.Add(new DocumentTypeEntity { Id = row.Id, Code = row.Code, Name = row.Name });
        }

        await context.SaveChangesAsync();
    }
}

