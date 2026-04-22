using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.RolePermissions.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in RolePermissionDefaultData.RolePermissions)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.RolePermissions.Add(new RolePermissionEntity
            {
                Id = row.Id,
                RoleId = row.RoleId,
                PermissionId = row.PermissionId
            });
        }

        await context.SaveChangesAsync();
    }
}
