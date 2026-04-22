using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.Permissions.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in PermissionDefaultData.Permissions)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.Permissions.Add(new PermissionEntity { Id = row.Id, Name = row.Name, Description = row.Description });
        }

        await context.SaveChangesAsync();
    }
}

