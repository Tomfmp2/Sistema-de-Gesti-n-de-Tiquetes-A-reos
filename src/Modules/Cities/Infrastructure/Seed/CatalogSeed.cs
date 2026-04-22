using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.Cities.AsNoTracking().ToListAsync();
        var regions = await context.Regions.AsNoTracking().ToListAsync();
        var countries = await context.Countries.AsNoTracking().ToListAsync();

        int RegionId(string regionName, string countryCodeIso)
        {
            // Regions se filtran por nombre + país (vía country_id)
            var countryId = countries
                .FirstOrDefault(c => SeedHelpers.Normalize(c.CodeIso) == SeedHelpers.Normalize(countryCodeIso))
                ?.Id;

            if (countryId is null)
                throw new InvalidOperationException($"No existe country con code_iso='{countryCodeIso}'.");

            var norm = SeedHelpers.Normalize(regionName);
            var region = regions.FirstOrDefault(r => r.CountryId == countryId && SeedHelpers.Normalize(r.Name) == norm);
            if (region is null)
                throw new InvalidOperationException($"No existe region '{regionName}' para country '{countryCodeIso}'. Ejecuta seed de Regions primero.");

            return region.Id;
        }

        var desired = new[]
        {
            new { Name = "Bogota", RegionId = RegionId("Cundinamarca", "COL") },
            new { Name = "Medellin", RegionId = RegionId("Antioquia", "COL") },
            new { Name = "Cali", RegionId = RegionId("Valle del Cauca", "COL") },
            new { Name = "Barranquilla", RegionId = RegionId("Atlantico", "COL") },

            new { Name = "Miami", RegionId = RegionId("Florida", "USA") },
            new { Name = "Nueva York", RegionId = RegionId("Nueva York", "USA") },
            new { Name = "Los Angeles", RegionId = RegionId("California", "USA") },

            new { Name = "Ciudad de Mexico", RegionId = RegionId("Ciudad de Mexico", "MEX") },

            new { Name = "Madrid", RegionId = RegionId("Comunidad de Madrid", "ESP") },
            new { Name = "Paris", RegionId = RegionId("Ile-de-France", "FRA") },
            new { Name = "Londres", RegionId = RegionId("Inglaterra", "GBR") },

            new { Name = "Sao Paulo", RegionId = RegionId("Sao Paulo", "BRA") },
            new { Name = "Buenos Aires", RegionId = RegionId("Buenos Aires", "ARG") },
            new { Name = "Santiago", RegionId = RegionId("Region Metropolitana", "CHL") },
            new { Name = "Lima", RegionId = RegionId("Lima", "PER") },
            new { Name = "Toronto", RegionId = RegionId("Ontario", "CAN") },
            new { Name = "Tokio", RegionId = RegionId("Tokio", "JPN") },
            new { Name = "Sidney", RegionId = RegionId("Nueva Gales del Sur", "AUS") },
        };

        foreach (var c in desired)
        {
            var norm = SeedHelpers.Normalize(c.Name);
            if (existing.Any(x => x.RegionId == c.RegionId && SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.Cities.Add(new CityEntity { Name = c.Name, RegionId = c.RegionId });
        }

        await context.SaveChangesAsync();
    }
}

