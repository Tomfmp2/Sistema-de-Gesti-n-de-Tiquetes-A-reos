using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.Regions.AsNoTracking().ToListAsync();
        var countries = await context.Countries.AsNoTracking().ToListAsync();

        int CountryId(string codeIso)
        {
            var norm = SeedHelpers.Normalize(codeIso);
            var c = countries.FirstOrDefault(x => SeedHelpers.Normalize(x.CodeIso) == norm);
            if (c is null)
                throw new InvalidOperationException($"No existe country con code_iso='{codeIso}'. Ejecuta seed de Countries primero.");
            return c.Id;
        }

        var desired = new[]
        {
            new { Name = "Cundinamarca", Type = "Departamento", CountryId = CountryId("COL") },
            new { Name = "Antioquia", Type = "Departamento", CountryId = CountryId("COL") },
            new { Name = "Valle del Cauca", Type = "Departamento", CountryId = CountryId("COL") },
            new { Name = "Atlantico", Type = "Departamento", CountryId = CountryId("COL") },

            new { Name = "Florida", Type = "Estado", CountryId = CountryId("USA") },
            new { Name = "Nueva York", Type = "Estado", CountryId = CountryId("USA") },
            new { Name = "California", Type = "Estado", CountryId = CountryId("USA") },

            new { Name = "Ciudad de Mexico", Type = "Entidad federativa", CountryId = CountryId("MEX") },

            new { Name = "Comunidad de Madrid", Type = "Comunidad autonoma", CountryId = CountryId("ESP") },
            new { Name = "Ile-de-France", Type = "Region", CountryId = CountryId("FRA") },
            new { Name = "Inglaterra", Type = "Nacion constituyente", CountryId = CountryId("GBR") },

            new { Name = "Sao Paulo", Type = "Estado", CountryId = CountryId("BRA") },
            new { Name = "Buenos Aires", Type = "Provincia", CountryId = CountryId("ARG") },
            new { Name = "Region Metropolitana", Type = "Region", CountryId = CountryId("CHL") },
            new { Name = "Lima", Type = "Departamento", CountryId = CountryId("PER") },
            new { Name = "Ontario", Type = "Provincia", CountryId = CountryId("CAN") },
            new { Name = "Tokio", Type = "Prefectura", CountryId = CountryId("JPN") },
            new { Name = "Nueva Gales del Sur", Type = "Estado", CountryId = CountryId("AUS") },
        };

        foreach (var r in desired)
        {
            var nameNorm = SeedHelpers.Normalize(r.Name);
            if (existing.Any(x => x.CountryId == r.CountryId && SeedHelpers.Normalize(x.Name) == nameNorm))
                continue;

            context.Regions.Add(new RegionEntity
            {
                Name = r.Name,
                Type = r.Type,
                CountryId = r.CountryId
            });
        }

        await context.SaveChangesAsync();
    }
}

