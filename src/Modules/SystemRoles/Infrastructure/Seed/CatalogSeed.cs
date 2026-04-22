using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.SystemRoles.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in SystemRoleDefaultData.SystemRoles)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.SystemRoles.Add(new SystemRoleEntity { Id = row.Id, Name = row.Name, Description = row.Description });
        }

        await context.SaveChangesAsync();
    }
}

