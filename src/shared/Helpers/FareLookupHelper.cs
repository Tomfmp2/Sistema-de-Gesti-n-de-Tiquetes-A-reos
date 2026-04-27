using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

/// <summary>
/// Utilidad para consultar la tarifa aplicable a un asiento en un vuelo,
/// según: ruta del vuelo + clase de cabina + tipo de pasajero + temporada vigente.
/// Precio final = BasePrice × PriceFactor de la temporada.
/// </summary>
public static class FareLookupHelper
{
    /// <summary>
    /// Busca la tarifa vigente para el asiento seleccionado y devuelve el precio final y una etiqueta descriptiva.
    /// Devuelve (0m, "Sin tarifa registrada") si no se encuentra ninguna tarifa aplicable.
    /// </summary>
    public static async Task<(decimal Price, string Label)> LookupFareAsync(
        AppDbContext ctx,
        int flightId,
        int cabinTypeId,
        int passengerTypeId,
        CancellationToken cancellationToken = default)
    {
        // 1. Obtener la ruta y fecha de salida del vuelo
        var flight = await ctx.Set<FlightEntity>()
            .AsNoTracking()
            .Where(f => f.Id == flightId)
            .Select(f => new { f.RouteId, f.DepartureDate })
            .FirstOrDefaultAsync(cancellationToken);

        if (flight is null)
            return (0m, "Vuelo no encontrado");

        var departureDate = flight.DepartureDate.Date;

        // 2. Buscar la temporada vigente para la fecha de salida
        var season = await ctx.Set<SeasonEntity>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        // Usar primera temporada disponible como fallback (igual que el comportamiento anterior)
        var activeSeason = season.FirstOrDefault();
        if (activeSeason is null)
            return (0m, "Sin temporada registrada");

        // 3. Buscar tarifa: ruta + cabina + tipo pasajero + temporada
        var fare = await ctx.Set<FareEntity>()
            .AsNoTracking()
            .Include(f => f.CabinType)
            .Include(f => f.Season)
            .Where(f =>
                f.RouteId == flight.RouteId &&
                f.CabinTypeId == cabinTypeId &&
                f.PassengerTypeId == passengerTypeId &&
                f.SeasonId == activeSeason.Id &&
                (f.ValidFrom == null || f.ValidFrom <= departureDate) &&
                (f.ValidTo == null || f.ValidTo >= departureDate))
            .FirstOrDefaultAsync(cancellationToken);

        if (fare is null)
        {
            // Intentar sin filtro de pasajero (tarifa genérica de la clase)
            fare = await ctx.Set<FareEntity>()
                .AsNoTracking()
                .Include(f => f.CabinType)
                .Include(f => f.Season)
                .Where(f =>
                    f.RouteId == flight.RouteId &&
                    f.CabinTypeId == cabinTypeId &&
                    f.SeasonId == activeSeason.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }

        if (fare is null)
            return (0m, "Sin tarifa para esta clase");

        var finalPrice = fare.BasePrice * activeSeason.PriceFactor;
        var label = $"{fare.CabinType?.Name ?? "?"} · ${finalPrice:0.00} (base ${fare.BasePrice:0.00} × {activeSeason.PriceFactor:0.##})";

        return (finalPrice, label);
    }

    /// <summary>
    /// Devuelve un resumen de tarifas por clase para el vuelo indicado,
    /// para mostrar en el mapa de asientos antes de que el pasajero elija.
    /// </summary>
    public static async Task<IReadOnlyDictionary<int, (decimal Price, string ClassName)>> GetFaresByCabinAsync(
        AppDbContext ctx,
        int flightId,
        int passengerTypeId,
        CancellationToken cancellationToken = default)
    {
        var flight = await ctx.Set<FlightEntity>()
            .AsNoTracking()
            .Where(f => f.Id == flightId)
            .Select(f => new { f.RouteId, f.DepartureDate })
            .FirstOrDefaultAsync(cancellationToken);

        if (flight is null)
            return new Dictionary<int, (decimal, string)>();

        var departureDate = flight.DepartureDate.Date;

        var seasons = await ctx.Set<SeasonEntity>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        var activeSeason = seasons.FirstOrDefault();
        if (activeSeason is null)
            return new Dictionary<int, (decimal, string)>();

        var fares = await ctx.Set<FareEntity>()
            .AsNoTracking()
            .Include(f => f.CabinType)
            .Include(f => f.Season)
            .Where(f =>
                f.RouteId == flight.RouteId &&
                f.SeasonId == activeSeason.Id &&
                (f.ValidFrom == null || f.ValidFrom <= departureDate) &&
                (f.ValidTo == null || f.ValidTo >= departureDate))
            .ToListAsync(cancellationToken);

        var result = new Dictionary<int, (decimal Price, string ClassName)>();
        foreach (var fare in fares)
        {
            if (!result.ContainsKey(fare.CabinTypeId))
            {
                var price = fare.BasePrice * activeSeason.PriceFactor;
                result[fare.CabinTypeId] = (price, fare.CabinType?.Name ?? $"Cabina {fare.CabinTypeId}");
            }
        }

        return result;
    }
}
