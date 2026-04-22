using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

public static class CatalogSeed
{
    /// <summary>
    /// Orquestador de seed de catálogos. Cada módulo mantiene su propio seeder (estilo Continents).
    /// Este método solo coordina el orden para respetar FK/dependencias.
    /// </summary>
    public static async Task SeedAsync(AppDbContext context)
    {
        // Geography
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Seed.CatalogSeed.SeedAsync(context);

        // Persons & Related
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Seed.CatalogSeed.SeedAsync(context);

        // Addresses
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Seed.CatalogSeed.SeedAsync(context);

        // Flight pricing / passenger / cabin catalogs
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Seed.CatalogSeed.SeedAsync(context);

        // Core statuses
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Infrastructure.Seed.CatalogSeed.SeedAsync(context);

        // Billing / payments catalogs
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Seed.CatalogSeed.SeedAsync(context);

        // Staff catalogs
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Seed.CatalogSeed.SeedAsync(context);

        // Aircraft catalogs
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Seed.CatalogSeed.SeedAsync(context);

        // Auth catalogs
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
        await sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Seed.CatalogSeed.SeedAsync(context);
    }
}

