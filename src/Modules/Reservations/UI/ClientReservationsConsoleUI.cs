using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Application.Services;
using ReservationAggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Aggregate.Reservation;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Infrastructure.Entity;
using System;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;
using Microsoft.EntityFrameworkCore;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.UI;

/// <summary>
/// UI para rol Cliente (solo opera sobre su propio client_id).
/// </summary>
public sealed class ClientReservationsConsoleUI : IModuleUI
{
    private readonly AuthContext _auth;
    private readonly AppDbContext _ctx;
    private readonly CreateReservationUseCase _create;
    private readonly GetReservationsByClientIdUseCase _getMine;
    private readonly GetReservationByIdUseCase _getById;
    private readonly UpdateReservationUseCase _update;
    private readonly DeleteReservationUseCase _delete;
    private readonly CreateReservationFlightUseCase _createReservationFlight;
    private readonly CreateReservationPassengerUseCase _createReservationPassenger;
    private readonly CreatePassengerUseCase _createPassenger;
    private readonly IMilesTransactionService? _milesService;

    public ClientReservationsConsoleUI(
        AuthContext auth,
        AppDbContext ctx,
        CreateReservationUseCase create,
        GetReservationsByClientIdUseCase getMine,
        GetReservationByIdUseCase getById,
        UpdateReservationUseCase update,
        DeleteReservationUseCase delete,
        CreateReservationFlightUseCase createReservationFlight,
        CreateReservationPassengerUseCase createReservationPassenger,
        CreatePassengerUseCase createPassenger,
        IMilesTransactionService? milesService = null
    )
    {
        _auth = auth;
        _ctx = ctx;
        _create = create;
        _getMine = getMine;
        _getById = getById;
        _update = update;
        _delete = delete;
        _createReservationFlight = createReservationFlight;
        _createReservationPassenger = createReservationPassenger;
        _createPassenger = createPassenger;
        _milesService = milesService;
    }

    private sealed class PaxDraft
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string DocumentNumber { get; set; } = "";
        public int CabinTypeId { get; set; } = 1;
        public string CabinName { get; set; } = "Económica";
        public decimal LinePrice { get; set; }
    }

    private static void ShowRetryableValidation(string message)
    {
        SpectreUi.MarkupLineOrPlain($"[yellow]{message}[/]", message);
    }

    private async Task<T> RetryUntilValidAsync<T>(Func<Task<T>> attempt)
    {
        while (true)
        {
            try
            {
                return await attempt();
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (InvalidOperationException ex)
            {
                ShowRetryableValidation(ExceptionFormatting.GetDiagnosticMessage(ex));
            }
        }
    }

    private async Task RetryUntilValidAsync(Func<Task> attempt)
    {
        while (true)
        {
            try
            {
                await attempt();
                return;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (InvalidOperationException ex)
            {
                ShowRetryableValidation(ExceptionFormatting.GetDiagnosticMessage(ex));
            }
        }
    }

    private void RetryUntilValid(Action attempt)
    {
        while (true)
        {
            try
            {
                attempt();
                return;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (InvalidOperationException ex)
            {
                ShowRetryableValidation(ExceptionFormatting.GetDiagnosticMessage(ex));
            }
        }
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Reservaciones", "Crear / ver / modificar / cancelar");

            if (_auth.ClientId is null)
            {
                SpectreUi.MarkupLineOrPlain(
                    "[yellow]Tu usuario no está asociado a un cliente.[/] [dim](no hay client_id; contacta administración.)[/]",
                    "Tu usuario no está asociado a un cliente (no hay client_id; contacta administración)."
                );
                SpectreUi.Pause();
                return;
            }

            // Validar automáticamente reservas expiradas al entrar al menú
            await RevokeExpiredReservationsAsync();

            var items = new List<(string Label, Action Action)>
            {
                ("Crear reservación", () => Create().GetAwaiter().GetResult()),
                ("Listar mis reservaciones", () => ListMine().GetAwaiter().GetResult()),
                ("Check-in", () => Checkin().GetAwaiter().GetResult()),
                ("Ver ticket / pase de abordar", () => ViewTickets().GetAwaiter().GetResult()),
                ("Cancelar (por ID)", () => Cancel().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(
                items,
                "[bold]¿Qué deseas hacer?[/]",
                "[grey]Crear, listar, check-in o cancelar. «Volver» al menú principal.[/]"
            );
        }
    }

    private async Task ListMine()
    {
        try
        {
            var list = (await _getMine.ExecuteAsync(_auth.ClientId!.Value)).ToList();
            if (list.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No tienes reservaciones.[/]", "No tienes reservaciones.");
                return;
            }

            var statusById = await _ctx
                .Set<ReservationStatusEntity>()
                .AsNoTracking()
                .ToDictionaryAsync(s => s.Id, s => s.Name);

            SpectreUi.ShowTable(
                "Mis reservaciones",
                ["ID", "Estado", "Total", "Fecha", "Expira"],
                list.OrderByDescending(x => x.ReservationDate.Value)
                    .Select(r =>
                    {
                        var sid = r.ReservationStatusId.Value;
                        var statusLabel = statusById.TryGetValue(sid, out var n) && !string.IsNullOrWhiteSpace(n)
                            ? n
                            : $"#{sid}";
                        return (IReadOnlyList<string>)
                        [
                            r.Id.Value.ToString(),
                            statusLabel,
                            r.TotalValue.Value.ToString("0.00"),
                            r.ReservationDate.Value.ToString("yyyy-MM-dd HH:mm"),
                            r.ExpiresAt.Value.HasValue
                                ? r.ExpiresAt.Value.Value.ToString("yyyy-MM-dd HH:mm")
                                : "-"
                        ];
                    })
                    .ToList()
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}",
                $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}"
            );
        }

        SpectreUi.Pause();
    }

    private static decimal CabinPriceMultiplier(int cabinTypeId) =>
        cabinTypeId switch
        {
            1 => 1.0m,
            2 => 1.15m,
            3 => 1.6m,
            4 => 2.2m,
            _ => 1.0m
        };

    private static async Task<decimal> ResolveUnitFareAsync(
        AppDbContext ctx,
        int routeId,
        int passengerTypeId,
        int cabinTypeId,
        DateTime departureUtc,
        CancellationToken cancellationToken = default
    )
    {
        var exact = await ctx.Set<FareEntity>()
            .AsNoTracking()
            .Where(f => f.RouteId == routeId && f.PassengerTypeId == passengerTypeId && f.CabinTypeId == cabinTypeId)
            .Where(f => f.ValidFrom == null || f.ValidFrom <= departureUtc)
            .Where(f => f.ValidTo == null || f.ValidTo >= departureUtc)
            .OrderBy(f => f.BasePrice)
            .Select(f => f.BasePrice)
            .FirstOrDefaultAsync(cancellationToken);

        if (exact > 0m)
            return exact;

        var economy = await ctx.Set<FareEntity>()
            .AsNoTracking()
            .Where(f => f.RouteId == routeId && f.PassengerTypeId == passengerTypeId && f.CabinTypeId == 1)
            .Where(f => f.ValidFrom == null || f.ValidFrom <= departureUtc)
            .Where(f => f.ValidTo == null || f.ValidTo >= departureUtc)
            .OrderBy(f => f.BasePrice)
            .Select(f => f.BasePrice)
            .FirstOrDefaultAsync(cancellationToken);

        if (economy > 0m)
            return decimal.Round(economy * CabinPriceMultiplier(cabinTypeId), 0, MidpointRounding.AwayFromZero);

        var anyForRoute = await ctx.Set<FareEntity>()
            .AsNoTracking()
            .Where(f => f.RouteId == routeId)
            .OrderBy(f => f.BasePrice)
            .Select(f => f.BasePrice)
            .FirstOrDefaultAsync(cancellationToken);

        if (anyForRoute > 0m)
            return anyForRoute;

        return 0m;
    }

    private async Task RevokeExpiredReservationsAsync()
    {
        try
        {
            var utcNow = DateTime.UtcNow;
            
            // Buscar reservaciones pendientes (Status == 1) del cliente donde el vuelo ya pasó
            var expiredReservations = await _ctx.Set<ReservationEntity>()
                .Include(r => r.ReservationFlights)
                    .ThenInclude(rf => rf.Flight)
                .Where(r => r.ClientId == _auth.ClientId!.Value && r.ReservationStatusId == 1)
                .Where(r => r.ReservationFlights.Any(rf => rf.Flight != null && rf.Flight.DepartureDate <= utcNow))
                .ToListAsync();

            if (expiredReservations.Count == 0) return;

            var strategy = _ctx.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await _ctx.Database.BeginTransactionAsync();

                foreach (var current in expiredReservations)
                {
                    // Devolver cupos
                    var bookingFlights = current.ReservationFlights.ToList();
                    foreach (var bf in bookingFlights)
                    {
                        var pax = await _ctx.Set<ReservationPassengerEntity>()
                            .AsNoTracking()
                            .CountAsync(rp => rp.ReservationFlightId == bf.Id);

                        var flight = await _ctx.Set<FlightEntity>().FirstOrDefaultAsync(f => f.Id == bf.FlightId);
                        if (flight != null)
                        {
                            flight.AvailableSeats += pax;
                        }
                    }

                    // Marcar como cancelada/perdida (Status = 3)
                    current.ReservationStatusId = 3;
                    current.UpdatedAt = utcNow;

                    // Revertir millas
                    if (_milesService != null)
                    {
                        await _milesService.RevertMilesForReservationAsync(current.Id);
                    }
                }

                await _ctx.SaveChangesAsync();
                await tx.CommitAsync();
            });

            SpectreUi.MarkupLineOrPlain(
                $"[bold red]ATENCIÓN: Tienes {expiredReservations.Count} reservación(es) que fueron canceladas automáticamente por no hacer check-in a tiempo.[/]",
                $"ATENCIÓN: Tienes {expiredReservations.Count} reservación(es) que fueron canceladas automáticamente por no hacer check-in a tiempo."
            );
            SpectreUi.Pause();
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error al validar expiraciones:[/] {ex.Message}",
                $"Error al validar expiraciones: {ex.Message}"
            );
            SpectreUi.Pause();
        }
    }

    private async Task ShowCabinAvailabilityForFlightAsync(int flightId, CancellationToken cancellationToken = default)
    {
        var byCabin = await _ctx
            .Set<FlightSeatEntity>()
            .AsNoTracking()
            .Where(s => s.FlightId == flightId)
            .GroupBy(s => s.CabinTypeId)
            .Select(g => new
            {
                g.Key,
                Total = g.Count(),
                Free = g.Count(s => !s.IsOccupied)
            })
            .ToListAsync(cancellationToken);
        if (byCabin.Count == 0)
        {
            SpectreUi.MarkupLineOrPlain(
                "[grey]No hay asientos en catálogo para este vuelo.[/]",
                "No hay asientos en catálogo para este vuelo."
            );
            return;
        }

        var nameByCabin = await _ctx.Set<CabinTypeEntity>()
            .AsNoTracking()
            .ToDictionaryAsync(c => c.Id, c => c.Name, cancellationToken);

        var rows = byCabin
            .OrderBy(x => x.Key)
            .Select(
                b =>
                    (IReadOnlyList<string>)
                    [
                        b.Key.ToString(),
                        (nameByCabin.TryGetValue(b.Key, out var nm) ? nm : $"#{b.Key}") ?? "-",
                        b.Free.ToString(),
                        b.Total.ToString()
                    ]
            )
            .ToList();
        SpectreUi.ShowTable(
            "Clases y asientos en este vuelo (configuración del avión; disponibles = libres / total)",
            ["Id cabina", "Clase", "Disponibles", "Total asientos"],
            rows
        );
    }

    /// <summary>Pide la clase de cabina / tipo de tiquete antes de los datos personales; usa cupos del vuelo concreto.</summary>
    private async Task<int> PromptCabinTypeIdForPassengerAsync(
        int aircraftId,
        int flightId,
        CancellationToken cancellationToken = default
    )
    {
        var typeIds = await _ctx
            .Set<CabinConfigurationEntity>()
            .AsNoTracking()
            .Where(c => c.AircraftId == aircraftId)
            .Select(c => c.CabinTypeId)
            .Distinct()
            .OrderBy(id => id)
            .ToListAsync(cancellationToken);
        if (typeIds.Count == 0)
            return 1;

        var cabinStatsRows = await _ctx
            .Set<FlightSeatEntity>()
            .AsNoTracking()
            .Where(s => s.FlightId == flightId)
            .GroupBy(s => s.CabinTypeId)
            .Select(g => new
            {
                CabinTypeId = g.Key,
                Total = g.Count(),
                Free = g.Count(s => !s.IsOccupied)
            })
            .ToListAsync(cancellationToken);
        var statsByCabin = cabinStatsRows.ToDictionary(x => x.CabinTypeId, x => (x.Free, x.Total));

        var nameByCabin = await _ctx.Set<CabinTypeEntity>().AsNoTracking().ToDictionaryAsync(
            c => c.Id,
            c => c.Name,
            cancellationToken
        );

        if (typeIds.Count == 1)
        {
            var only = typeIds[0];
            statsByCabin.TryGetValue(only, out var st);
            if (st.Free < 1)
            {
                throw new InvalidOperationException(
                    "No hay asientos libres en la única clase disponible de este vuelo."
                );
            }

            var nm = nameByCabin.TryGetValue(only, out var n) ? n : $"#{only}";
            SpectreUi.MarkupLineOrPlain(
                $"[grey]Tipo de tiquete:[/] [bold]{nm}[/] — disponibles [bold]{st.Free}[/] / {st.Total} en este vuelo.",
                $"Tipo de tiquete: {nm} — disponibles {st.Free} / {st.Total} en este vuelo."
            );
            return only;
        }

        var rows = typeIds
            .Select(
                id =>
                {
                    statsByCabin.TryGetValue(id, out var st);
                    var nm = nameByCabin.TryGetValue(id, out var n) ? n : $"#{id}";
                    return (IReadOnlyList<string>)
                    [
                        id.ToString(),
                        nm ?? "-",
                        st.Free.ToString(),
                        st.Total.ToString()
                    ];
                }
            )
            .ToList();

        SpectreUi.ShowTable(
            "Elegir tipo de tiquete (clase de cabina) para este pasajero",
            ["Id cabina", "Clase", "Disponibles", "Total en el vuelo"],
            rows
        );

        var idCab = SpectreUi.PromptIntRequiredCancelable(
            "Id de cabina del tiquete",
            "0/c/cancelar — use la tabla (debe haber cupos en esa clase)",
            min: 0
        );
        if (!typeIds.Contains(idCab))
            throw new InvalidOperationException("Esa cabina no está configurada en el avión de este vuelo.");

        if (!statsByCabin.TryGetValue(idCab, out var chosen) || chosen.Free < 1)
        {
            throw new InvalidOperationException(
                "No quedan asientos libres en esa clase para este vuelo. Elija otra o otro vuelo."
            );
        }

        return idCab;
    }

    /// <summary>Revisión: muestra subtotal por cabina, permite corregir datos antes de guardar.</summary>
    private bool RunPassengerReviewLoop(
        List<PaxDraft> drafts,
        string docTypeLabel,
        int routeId,
        int passengerTypeId,
        DateTime flightDeparture,
        int aircraftId,
        int flightId,
        out decimal subtotal
    )
    {
        var done = false;
        var userCancelled = false;
        subtotal = 0m;
        while (!done)
        {
            subtotal = drafts.Sum(d => d.LinePrice);
            var rows = drafts
                .Select(
                    (d, i) =>
                        (IReadOnlyList<string>)
                        [
                            (i + 1).ToString(),
                            d.FirstName,
                            d.LastName,
                            d.CabinName,
                            d.DocumentNumber,
                            d.LinePrice.ToString("0.00")
                        ]
                )
                .ToList();

            SpectreUi.ModuleHeader("Revisar reservación", "Clase, tarifa y subtotal (documento: " + docTypeLabel + ")");
            SpectreUi.ShowTable(
                "Pasajeros y tarifas",
                ["#", "Nombre", "Apellido", "Clase", "Nº documento", "Tarifa"],
                rows
            );
            SpectreUi.MarkupLineOrPlain(
                $"[bold]Subtotal:[/] {subtotal:0.00}",
                $"Subtotal: {subtotal:0.00}"
            );

            var reviewItems = new List<(string Label, Action Action)>
            {
                ("Confirmar y guardar reservación", () => done = true),
                (
                    "Editar un pasajero (datos, clase, documento, tarifa)",
                    () => EditOnePassengerDraft(
                        drafts,
                        docTypeLabel,
                        routeId,
                        passengerTypeId,
                        flightDeparture,
                        aircraftId,
                        flightId
                    )
                ),
                (
                    "Cancelar sin guardar",
                    () =>
                    {
                        userCancelled = true;
                        done = true;
                    }
                )
            };
            MenuLogic.RunMenu(
                reviewItems,
                title: "[bold]¿Confirmas la reservación?[/]",
                subtitle: "[grey]El check-in posterior confirmará la reserva y asignará asiento en la cabina elegida.[/]"
            );
        }

        subtotal = drafts.Sum(d => d.LinePrice);
        return !userCancelled;
    }

    private void EditOnePassengerDraft(
        IReadOnlyList<PaxDraft> drafts,
        string docTypeLabel,
        int routeId,
        int passengerTypeId,
        DateTime flightDeparture,
        int aircraftId,
        int flightId
    )
    {
        if (drafts.Count == 0)
            return;

        RetryUntilValid(() =>
        {
            var idx = SpectreUi.PromptIntRequiredCancelable(
                "Número de pasajero a editar",
                $"1 a {drafts.Count} (0/c/cancelar para salir)",
                min: 0
            );
            if (idx < 1 || idx > drafts.Count)
                throw new InvalidOperationException($"Indique un número entre 1 y {drafts.Count}.");

            var d = drafts[idx - 1];

            d.CabinTypeId = PromptCabinTypeIdForPassengerAsync(aircraftId, flightId).GetAwaiter().GetResult();
            d.CabinName = ResolveCabinNameAsync(d.CabinTypeId).GetAwaiter().GetResult();

            var fn = SpectreUi.PromptRequiredCancelable("Nombre", "0/c/cancelar para salir").Trim();
            var ln = SpectreUi.PromptRequiredCancelable("Apellido", "0/c/cancelar para salir").Trim();
            if (string.IsNullOrWhiteSpace(fn) || string.IsNullOrWhiteSpace(ln))
                throw new InvalidOperationException("Nombre y apellido son obligatorios.");

            var doc = SpectreUi.PromptRequiredCancelable(
                    $"Documento ({docTypeLabel}) número",
                    "0/c/cancelar para salir"
                )
                .Trim();
            if (string.IsNullOrWhiteSpace(doc))
                throw new InvalidOperationException("El número de documento es obligatorio.");

            d.FirstName = fn;
            d.LastName = ln;
            d.DocumentNumber = doc;

            d.LinePrice = PriceOrManualAsync(routeId, passengerTypeId, d.CabinTypeId, flightDeparture)
                .GetAwaiter()
                .GetResult();
        });
    }

    private async Task<string> ResolveCabinNameAsync(int cabinTypeId)
    {
        var n = await _ctx.Set<CabinTypeEntity>().AsNoTracking()
            .Where(t => t.Id == cabinTypeId)
            .Select(t => t.Name)
            .FirstOrDefaultAsync();
        return !string.IsNullOrWhiteSpace(n) ? n! : $"#{cabinTypeId}";
    }

    private async Task<decimal> PriceOrManualAsync(
        int routeId,
        int passengerTypeId,
        int cabinTypeId,
        DateTime dep
    )
    {
        var p = await ResolveUnitFareAsync(_ctx, routeId, passengerTypeId, cabinTypeId, dep);
        if (p > 0m)
            return p;

        while (true)
        {
            var raw = SpectreUi.PromptRequiredCancelable(
                    "Tarifa manual (no hay en catálogo para esta ruta/cabina)",
                    "decimal ≥ 0 (0/c/cancelar para salir)"
                )
                .Trim();
            if (decimal.TryParse(raw, out p) && p >= 0m)
                return p;
            ShowRetryableValidation("Tarifa inválida. Ingrese un número decimal mayor o igual a 0.");
        }
    }

    /// <summary>Crea una reserva aplicando un descuento por millas. Invocado desde MilesTransactionConsoleUI.</summary>
    public async Task CreateWithDiscountAsync(decimal discountPercentage, decimal milesToDeduct)
    {
        await CreateInternal(discountPercentage, milesToDeduct);
    }

    private async Task Create()
    {
        await CreateInternal(0m, 0m);
    }

    private async Task CreateInternal(decimal discountPercentage, decimal milesToDeduct)
    {
        try
        {
            if (discountPercentage > 0m)
            {
                SpectreUi.MarkupLineOrPlain(
                    $"[bold green]Descuento por millas activo: {discountPercentage}%[/]",
                    $"Descuento por millas activo: {discountPercentage}%"
                );
            }

            // Para cliente: status inicial = Pendiente (1)
            const int pendingStatusId = 1;
            var utcNow = DateTime.UtcNow;

            SpectreUi.MarkupLineOrPlain(
                "[grey]Reserva de vuelo (sin ticket): el boarding pass se genera en Check-in.[/]",
                "Reserva de vuelo (sin ticket): el boarding pass se genera en Check-in."
            );

            // 1) Seleccionar vuelo
            var flights = await _ctx.Set<FlightEntity>()
                .AsNoTracking()
                .Include(f => f.Route!)
                .ThenInclude(r => r.OriginAirport!)
                .Include(f => f.Route!)
                .ThenInclude(r => r.DestinationAirport!)
                .Include(f => f.Airline!)
                .Where(f => f.AvailableSeats > 0 && f.DepartureDate > DateTime.UtcNow.AddHours(-1))
                .OrderBy(f => f.DepartureDate)
                .Take(30)
                .Select(f => new
                {
                    f.Id,
                    f.AircraftId,
                    f.RouteId,
                    f.FlightCode,
                    f.DepartureDate,
                    f.EstimatedArrivalDate,
                    f.AvailableSeats,
                    AirlineName = f.Airline != null ? f.Airline.Name : null,
                    OriginAirportName = f.Route != null && f.Route.OriginAirport != null ? f.Route.OriginAirport.Name : null,
                    DestinationAirportName = f.Route != null && f.Route.DestinationAirport != null ? f.Route.DestinationAirport.Name : null,
                    OriginCode = f.Route != null && f.Route.OriginAirport != null
                        ? (f.Route.OriginAirport.IcaoCode ?? f.Route.OriginAirport.IataCode)
                        : null,
                    DestinationCode = f.Route != null && f.Route.DestinationAirport != null
                        ? (f.Route.DestinationAirport.IcaoCode ?? f.Route.DestinationAirport.IataCode)
                        : null,
                    RouteMiles = f.Route != null ? f.Route.Miles : 0m
                })
                .ToListAsync();

            if (flights.Count == 0)
                throw new InvalidOperationException("No hay vuelos disponibles con cupos.");

            SpectreUi.ShowTable(
                "Vuelos disponibles (máx 30)",
                ["ID", "Vuelo", "Origen", "Destino", "Salida", "Cupos", "Aerolínea"],
                flights.Select(f =>
                {
                    var origin = !string.IsNullOrWhiteSpace(f.OriginAirportName) ? f.OriginAirportName : 
                                 (!string.IsNullOrWhiteSpace(f.OriginCode) ? f.OriginCode : "?");
                    var dest = !string.IsNullOrWhiteSpace(f.DestinationAirportName) ? f.DestinationAirportName : 
                               (!string.IsNullOrWhiteSpace(f.DestinationCode) ? f.DestinationCode : "?");
                    return (IReadOnlyList<string>)
                    [
                        f.Id.ToString(),
                        f.FlightCode ?? "-",
                        origin,
                        dest,
                        f.DepartureDate.ToString("yyyy-MM-dd HH:mm"),
                        f.AvailableSeats.ToString(),
                        f.AirlineName ?? "-"
                    ];
                }).ToList()
            );

            var selected = await RetryUntilValidAsync(async () =>
            {
                var flightId = SpectreUi.PromptIntRequiredCancelable(
                    "ID vuelo a reservar",
                    "0/c/cancelar para salir"
                );
                var s = flights.FirstOrDefault(x => x.Id == flightId);
                if (s is null)
                {
                    throw new InvalidOperationException(
                        "Ese ID no está en la lista de vuelos. Revise la tabla e intente de nuevo."
                    );
                }

                return s;
            });

            var paxCount = await RetryUntilValidAsync(async () =>
            {
                var n = SpectreUi.PromptIntRequiredCancelable(
                    "Cantidad de pasajeros",
                    $"mín=1, máx={selected.AvailableSeats} (0/c/cancelar para salir)",
                    min: 1
                );
                if (n > selected.AvailableSeats)
                {
                    throw new InvalidOperationException(
                        $"En este vuelo solo puede reservar hasta {selected.AvailableSeats} pasajero(s)."
                    );
                }

                return n;
            });

            DateTime? expiresAt = null;
            await RetryUntilValidAsync(async () =>
            {
                var minutesRaw = (SpectreUi.PromptOptionalCancelable(
                    "Expira en (minutos)",
                    "Enter = sin expiración (0/c/cancelar para salir)"
                ) ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(minutesRaw))
                {
                    expiresAt = null;
                    return;
                }

                if (!int.TryParse(minutesRaw, out var minutes) || minutes < 1)
                {
                    throw new InvalidOperationException(
                        "Minutos inválidos. Use un entero ≥ 1, o Enter para reservar sin fecha de expiración."
                    );
                }

                expiresAt = utcNow.AddMinutes(minutes);
            });

            var (docTypeId, docTypeLabel) = await PromptDocumentTypeAsync();
            var passengerTypeId = await PromptPassengerTypeAsync();

            await ShowCabinAvailabilityForFlightAsync(selected.Id);

            var drafts = new List<PaxDraft>(paxCount);
            for (var i = 1; i <= paxCount; i++)
            {
                SpectreUi.MarkupLineOrPlain(
                    $"[grey]Pasajero {i}/{paxCount}[/]",
                    $"Pasajero {i}/{paxCount}"
                );

                var draft = await RetryUntilValidAsync(async () =>
                {
                    var cabinId = await PromptCabinTypeIdForPassengerAsync(selected.AircraftId, selected.Id);
                    var cabName = await ResolveCabinNameAsync(cabinId);

                    var firstName = SpectreUi.PromptRequiredCancelable("Nombre", "0/c/cancelar para salir").Trim();
                    var lastName = SpectreUi.PromptRequiredCancelable("Apellido", "0/c/cancelar para salir").Trim();
                    if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                    {
                        throw new InvalidOperationException("Nombre y apellido son obligatorios.");
                    }

                    var docNumber = SpectreUi.PromptRequiredCancelable(
                            $"Documento ({docTypeLabel}) número",
                            "0/c/cancelar para salir"
                        )
                        .Trim();
                    if (string.IsNullOrWhiteSpace(docNumber))
                    {
                        throw new InvalidOperationException("El número de documento es obligatorio.");
                    }

                    var line = await PriceOrManualAsync(
                        selected.RouteId,
                        passengerTypeId,
                        cabinId,
                        selected.DepartureDate
                    );
                    return new PaxDraft
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        DocumentNumber = docNumber,
                        CabinTypeId = cabinId,
                        CabinName = cabName,
                        LinePrice = line
                    };
                });
                drafts.Add(draft);
            }

            if (!RunPassengerReviewLoop(
                    drafts,
                    docTypeLabel,
                    selected.RouteId,
                    passengerTypeId,
                    selected.DepartureDate,
                    selected.AircraftId,
                    selected.Id,
                    out var subtotal))
                throw new OperationCanceledException();

            await EnsureBillingAddressAsync();

            // Crear booking + relaciones en transacción
            var strategy = _ctx.Database.CreateExecutionStrategy();
            ReservationAggregate? booking = null;

            await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await _ctx.Database.BeginTransactionAsync();

                // Aplicar descuento SOLO al primer tiquete; el resto paga precio normal
                var originalTotal = subtotal;
                decimal discountedTotal;
                decimal discountedAmount = 0m;
                if (discountPercentage > 0m && drafts.Count > 0)
                {
                    // Precio de un solo tiquete = subtotal / n pasajeros
                    var singleTicketPrice = drafts.Count > 0 ? drafts[0].LinePrice : (subtotal / paxCount);
                    var discountOnOne = Math.Round(singleTicketPrice * discountPercentage / 100m, 2);
                    discountedAmount = discountOnOne;
                    discountedTotal = Math.Round(subtotal - discountOnOne, 2);
                }
                else
                {
                    discountedTotal = subtotal;
                }

                booking = await _create.ExecuteAsync(
                    new CreateReservationRequest(
                        ClientId: _auth.ClientId!.Value,
                        ReservationDate: utcNow,
                        ReservationStatusId: pendingStatusId,
                        TotalValue: discountedTotal,
                        DiscountPercentage: discountPercentage,
                        OriginalTotalValue: originalTotal,
                        ExpiresAt: expiresAt,
                        CreatedAt: utcNow,
                        UpdatedAt: utcNow
                    )
                );

                var bookingFlight = await _createReservationFlight.ExecuteAsync(
                    new CreateReservationFlightRequest(
                        ReservationId: booking.Id.Value,
                        FlightId: selected.Id,
                        PartialValue: subtotal
                    )
                );

                foreach (var d in drafts)
                {
                    var personId = await CreatePersonAsync(
                        docTypeId,
                        d.DocumentNumber,
                        d.FirstName,
                        d.LastName
                    );
                    var existingPassengerId = await _ctx.Set<PassengerEntity>()
                        .AsNoTracking()
                        .Where(p => p.PersonId == personId)
                        .Select(p => p.Id)
                        .FirstOrDefaultAsync();

                    var passengerId = existingPassengerId > 0
                        ? existingPassengerId
                        : (await _createPassenger.ExecuteAsync(
                            new CreatePassengerRequest(PersonId: personId, PassengerTypeId: passengerTypeId)
                        )).Id.Value;

                    await _createReservationPassenger.ExecuteAsync(
                        new CreateReservationPassengerRequest(
                            ReservationFlightId: bookingFlight.Id.Value,
                            PassengerId: passengerId,
                            CabinTypeId: d.CabinTypeId
                        )
                    );
                }

                var flightRow = await _ctx.Set<FlightEntity>().FirstOrDefaultAsync(f => f.Id == selected.Id);
                if (flightRow is null)
                    throw new InvalidOperationException("Vuelo no encontrado al actualizar cupos.");
                flightRow.AvailableSeats -= paxCount;
                if (flightRow.AvailableSeats < 0)
                    throw new InvalidOperationException("Cupos negativos (concurrencia).");
                await _ctx.SaveChangesAsync();

                await tx.CommitAsync();
            });

            // Deducir millas DESPUÉS del commit exitoso (si aplicó descuento)
            if (milesToDeduct > 0m && _milesService != null)
            {
                await _milesService.RedeemMilesAsync(_auth.ClientId!.Value, booking!.Id.Value, milesToDeduct);
            }

            Console.Clear();
            SpectreUi.ModuleHeader("Confirmación de Reserva", null);

            // Millas ganadas
            var milesToAccumulate = 0m;
            if (_milesService != null && selected.RouteMiles > 0m)
            {
                milesToAccumulate = selected.RouteMiles * paxCount;
                await _milesService.AccumulateMilesAsync(_auth.ClientId!.Value, booking!.Id.Value, milesToAccumulate);
            }

            // ── Recibo: estilo verde original + desglose por tiquete ──
            var title = discountPercentage > 0m ? "RESERVACIÓN CON DESCUENTO" : "RESERVACIÓN CREADA EXITOSAMENTE";
            SpectreUi.MarkupLineOrPlain(
                "[green]╔══════════════════════════════════════════════════╗[/]",
                "════════════════════════════════════════════════");
            SpectreUi.MarkupLineOrPlain(
                $"[green]║[/]  [bold]{title,-46}[/][green]║[/]",
                $"  {title}");
            SpectreUi.MarkupLineOrPlain(
                "[green]╠══════════════════════════════════════════════════╣[/]",
                "════════════════════════════════════════════════");
            SpectreUi.MarkupLineOrPlain(
                $"[green]║[/]  ID Reserva : [bold]{booking!.Id.Value,-37}[/][green]║[/]",
                $"  ID Reserva : {booking!.Id.Value}");
            SpectreUi.MarkupLineOrPlain(
                $"[green]║[/]  Vuelo      : [bold]{selected.FlightCode,-37}[/][green]║[/]",
                $"  Vuelo      : {selected.FlightCode}");
            SpectreUi.MarkupLineOrPlain(
                $"[green]║[/]  Pasajeros  : [bold]{paxCount,-37}[/][green]║[/]",
                $"  Pasajeros  : {paxCount}");
            SpectreUi.MarkupLineOrPlain(
                "[green]╠══════════════════════════════════════════════════╣[/]",
                "════════════════════════════════════════════════");
            SpectreUi.MarkupLineOrPlain(
                "[green]║[/]  [bold dim]DESGLOSE DE TIQUETES                          [/][green]║[/]",
                "  DESGLOSE DE TIQUETES");

            for (int i = 0; i < drafts.Count; i++)
            {
                var d = drafts[i];
                if (i > 0)
                    SpectreUi.MarkupLineOrPlain(
                        "[green]║[/]  [dim]------------------------------------------[/][green]║[/]",
                        "  ------------------------------------------");

                var paxHeader = $"  Tiquete #{i + 1}: {d.FirstName} {d.LastName}";
                SpectreUi.MarkupLineOrPlain(
                    $"[green]║[/] [bold]{paxHeader,-48}[/][green]║[/]",
                    paxHeader);

                if (discountPercentage > 0m && i == 0)
                {
                    var orig = d.LinePrice;
                    var dsc  = Math.Round(d.LinePrice * discountPercentage / 100m, 2);
                    var paid = orig - dsc;
                    SpectreUi.MarkupLineOrPlain(
                        $"[green]║[/]    Precio base    : [dim]{orig,20:N2}[/]       [green]║[/]",
                        $"    Precio base    : {orig:N2}");
                    SpectreUi.MarkupLineOrPlain(
                        $"[green]║[/]    Descuento -{discountPercentage}%  : [yellow]-{dsc,19:N2}[/]       [green]║[/]",
                        $"    Descuento -{discountPercentage}%  : -{dsc:N2}");
                    SpectreUi.MarkupLineOrPlain(
                        $"[green]║[/]    Subtotal       : [bold green]{paid,20:N2}[/]       [green]║[/]",
                        $"    Subtotal       : {paid:N2}");
                }
                else
                {
                    SpectreUi.MarkupLineOrPlain(
                        $"[green]║[/]    Precio normal  : [dim]{d.LinePrice,20:N2}[/]       [green]║[/]",
                        $"    Precio normal  : {d.LinePrice:N2}");
                }
            }

            SpectreUi.MarkupLineOrPlain(
                "[green]╠══════════════════════════════════════════════════╣[/]",
                "════════════════════════════════════════════════");
            SpectreUi.MarkupLineOrPlain(
                $"[green]║[/]  Total sin descuento : [dim]{booking.OriginalTotalValue,17:N2}[/]       [green]║[/]",
                $"  Total sin descuento : {booking.OriginalTotalValue:N2}");

            if (discountPercentage > 0m)
            {
                var saved = booking.OriginalTotalValue - booking.TotalValue.Value;
                SpectreUi.MarkupLineOrPlain(
                    $"[green]║[/]  Ahorro ({discountPercentage}% tiquete 1) : [yellow]-{saved,16:N2}[/]       [green]║[/]",
                    $"  Ahorro ({discountPercentage}% tiquete 1) : -{saved:N2}");
                SpectreUi.MarkupLineOrPlain(
                    $"[green]║[/]  Millas utilizadas   : [red]-{milesToDeduct,17:N0}[/]       [green]║[/]",
                    $"  Millas utilizadas   : -{milesToDeduct:N0}");
            }
            SpectreUi.MarkupLineOrPlain(
                $"[green]║[/]  [bold green]TOTAL A PAGAR        : {booking.TotalValue.Value,17:N2}[/]       [green]║[/]",
                $"  TOTAL A PAGAR        : {booking.TotalValue.Value:N2}");

            if (milesToAccumulate > 0m)
            {
                SpectreUi.MarkupLineOrPlain(
                    "[green]╠══════════════════════════════════════════════════╣[/]",
                    "════════════════════════════════════════════════");
                SpectreUi.MarkupLineOrPlain(
                    $"[green]║[/]  [bold blue]Millas ganadas : +{milesToAccumulate,21:N0}[/]       [green]║[/]",
                    $"  Millas ganadas : +{milesToAccumulate:N0}");
                var det = $"  ({paxCount} tiquetes x {selected.RouteMiles:N0} millas)";
                SpectreUi.MarkupLineOrPlain(
                    $"[green]║[/]  [dim]{det,-46}[/][green]║[/]",
                    det);
            }

            SpectreUi.MarkupLineOrPlain(
                "[green]╚══════════════════════════════════════════════════╝[/]",
                "════════════════════════════════════════════════");

            // Nota descuento parcial
            if (discountPercentage > 0m && paxCount > 1)
            {
                Console.WriteLine();
                SpectreUi.MarkupLineOrPlain(
                    $"[bold yellow]NOTA:[/] [yellow]El descuento se aplicó al Tiquete #1 únicamente.[/]",
                    $"NOTA: El descuento se aplicó al Tiquete #1 únicamente."
                );
                SpectreUi.MarkupLineOrPlain(
                    $"[yellow]Los {paxCount - 1} tiquete(s) restante(s) se cobraron a precio normal.[/]",
                    $"Los {paxCount - 1} tiquete(s) restante(s) se cobraron a precio normal."
                );
            }

            // Mensaje de advertencia rojo
            Console.WriteLine();
            SpectreUi.MarkupLineOrPlain(
                "[bold red]ATENCIÓN: Si el check-in no se completa antes del inicio del vuelo,[/]",
                "ATENCIÓN: Si el check-in no se completa antes del inicio del vuelo,"
            );
            SpectreUi.MarkupLineOrPlain(
                "[bold red]la reservación será marcada como perdida y se revocarán los puntos ganados.[/]",
                "la reservación será marcada como perdida y se revocarán los puntos ganados."
            );
            Console.WriteLine();
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}",
                $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}"
            );
        }

        SpectreUi.Pause();
    }

    private async Task Cancel()
    {
        try
        {
            var id = 0;
            ReservationAggregate? current = null;

            await RetryUntilValidAsync(async () =>
            {
                id = SpectreUi.PromptIntRequiredCancelable("ID reservación", "0/c/cancelar para salir", min: 1);

                current = await _getById.ExecuteAsync(id);
                if (current is null)
                {
                    throw new InvalidOperationException(
                        "No encontramos esa reservación. Verifique el ID (p. ej. en «Listar mis reservaciones»)."
                    );
                }

                if (current.ClientId.Value != _auth.ClientId!.Value)
                {
                    throw new InvalidOperationException("No puedes cancelar reservaciones de otro cliente.");
                }

                const int cancelledStatusId = 3;
                if (current.ReservationStatusId.Value == cancelledStatusId)
                {
                    throw new InvalidOperationException(
                        "Esa reservación ya está cancelada. Elija otra si desea anular otra reserva."
                    );
                }
            });

            if (current is null)
                return;

            const int cancelledStatusId = 3;
            var strategy = _ctx.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await _ctx.Database.BeginTransactionAsync();

                // Devolver cupos: contar pasajeros por vuelo dentro de la reserva
                var bookingFlights = await _ctx.Set<ReservationFlightEntity>()
                    .AsNoTracking()
                    .Where(rf => rf.ReservationId == id)
                    .Select(rf => new { rf.Id, rf.FlightId })
                    .ToListAsync();

                foreach (var bf in bookingFlights)
                {
                    var pax = await _ctx.Set<ReservationPassengerEntity>()
                        .AsNoTracking()
                        .CountAsync(rp => rp.ReservationFlightId == bf.Id);

                    var flight = await _ctx.Set<FlightEntity>().FirstOrDefaultAsync(f => f.Id == bf.FlightId);
                    if (flight is null)
                        throw new InvalidOperationException($"Vuelo {bf.FlightId} no encontrado al devolver cupos.");

                    flight.AvailableSeats += pax;
                }

                // Cambiar estado de la reserva (sin borrar)
                var utcNow = DateTime.UtcNow;
                await _update.ExecuteAsync(
                    new UpdateReservationRequest(
                        Id: current.Id.Value,
                        ClientId: current.ClientId.Value,
                        ReservationDate: current.ReservationDate.Value,
                        ReservationStatusId: cancelledStatusId,
                        TotalValue: current.TotalValue.Value,
                        DiscountPercentage: current.DiscountPercentage,
                        OriginalTotalValue: current.OriginalTotalValue,
                        ExpiresAt: current.ExpiresAt.Value,
                        CreatedAt: current.CreatedAt.Value,
                        UpdatedAt: utcNow
                    )
                );

                await _ctx.SaveChangesAsync();
                await tx.CommitAsync();
            });

            // Revertir millas acumuladas si la reserva se cancela
            if (_milesService != null)
            {
                await _milesService.RevertMilesForReservationAsync(id);
            }

            SpectreUi.MarkupLineOrPlain(
                $"[green]Reservación {id} cancelada exitosamente.[/] Cupos devueltos y millas ajustadas.",
                $"Reservación {id} cancelada exitosamente. Cupos devueltos y millas ajustadas."
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}",
                $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}"
            );
        }

        SpectreUi.Pause();
    }

    private async Task<(int DocumentTypeId, string Label)> PromptDocumentTypeAsync()
    {
        var types = await _ctx.Set<DocumentTypeEntity>()
            .AsNoTracking()
            .OrderBy(t => t.Id)
            .Select(t => new { t.Id, t.Name, t.Code })
            .ToListAsync();

        if (types.Count == 0)
            throw new InvalidOperationException("No hay tipos de documento (document_types).");

        SpectreUi.ShowTable(
            "Tipos de documento",
            ["Código", "Nombre"],
            types.Select(t => (IReadOnlyList<string>)[t.Code ?? "-", t.Name ?? "-"]).ToList()
        );

        while (true)
        {
            var code = SpectreUi.PromptRequiredCancelable("Código (p.ej. CC/PAS)", "0/c/cancelar para salir").Trim();
            var match = types.FirstOrDefault(t => string.Equals(t.Code, code, StringComparison.OrdinalIgnoreCase))
                        ?? types.FirstOrDefault(t => string.Equals(t.Name, code, StringComparison.OrdinalIgnoreCase));
            if (match is null)
            {
                ShowRetryableValidation("Código no reconocido. Use uno de la tabla (por ejemplo CC, PAS).");
                continue;
            }

            return (match.Id, $"{match.Code}");
        }
    }

    private async Task<int> PromptPassengerTypeAsync()
    {
        var types = await _ctx.Set<PassengerTypeEntity>()
            .AsNoTracking()
            .OrderBy(t => t.Id)
            .Select(t => new { t.Id, t.Name })
            .ToListAsync();

        if (types.Count == 0)
            throw new InvalidOperationException("No hay tipos de pasajero (passenger_types).");

        SpectreUi.ShowTable(
            "Tipos de pasajero",
            ["Id", "Nombre"],
            types.Select(t => (IReadOnlyList<string>)[t.Id.ToString(), t.Name ?? "-"]).ToList()
        );

        while (true)
        {
            var raw = (SpectreUi.PromptOptionalCancelable(
                "Seleccione Id",
                "Enter = Adulto (0/c/cancelar para salir)"
            ) ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(raw))
            {
                var adulto = types.FirstOrDefault(t =>
                    string.Equals(t.Name, "Adulto", StringComparison.OrdinalIgnoreCase)
                );
                return adulto is not null ? adulto.Id : types.First().Id;
            }

            if (!int.TryParse(raw, out var id))
            {
                ShowRetryableValidation("Use el Id numérico de la tabla o Enter para Adulto.");
                continue;
            }

            if (types.All(t => t.Id != id))
            {
                ShowRetryableValidation("Ese Id no está en la lista. Revise la tabla.");
                continue;
            }

            return id;
        }
    }

    private async Task ShowReservationCheckinTablesAsync(int clientId)
    {
        const int pendingStatusId = 1;
        const int confirmedStatusId = 2;
        var statusName = await _ctx.Set<ReservationStatusEntity>()
            .AsNoTracking()
            .ToDictionaryAsync(s => s.Id, s => s.Name ?? string.Empty);

        var mine = await _ctx.Set<ReservationEntity>()
            .AsNoTracking()
            .Where(r => r.ClientId == clientId)
            .OrderByDescending(r => r.ReservationDate)
            .Select(r => new { r.Id, r.ReservationStatusId, r.TotalValue, r.ReservationDate })
            .ToListAsync();

        var rows = new List<IReadOnlyList<string>>();
        foreach (var c in mine.Where(x => x.ReservationStatusId is pendingStatusId or confirmedStatusId))
        {
            var flightCode = await _ctx.Set<ReservationFlightEntity>()
                .AsNoTracking()
                .Where(rf => rf.ReservationId == c.Id)
                .Join(
                    _ctx.Set<FlightEntity>().AsNoTracking(),
                    rf => rf.FlightId,
                    f => f.Id,
                    (rf, f) => f.FlightCode
                )
                .FirstOrDefaultAsync();

            var rfIds = await _ctx.Set<ReservationFlightEntity>()
                .AsNoTracking()
                .Where(rf => rf.ReservationId == c.Id)
                .Select(rf => rf.Id)
                .ToListAsync();
            if (rfIds.Count == 0)
                continue;

            var rpIds = await _ctx.Set<ReservationPassengerEntity>()
                .AsNoTracking()
                .Where(rp => rfIds.Contains(rp.ReservationFlightId))
                .Select(rp => rp.Id)
                .ToListAsync();
            if (rpIds.Count == 0)
                continue;

            var tickets = await _ctx.Set<TicketEntity>()
                .AsNoTracking()
                .Where(t => rpIds.Contains(t.ReservationPassengerId))
                .Select(t => new { t.Id, t.ReservationPassengerId })
                .ToListAsync();
            var ticketByRp = tickets
                .GroupBy(t => t.ReservationPassengerId)
                .ToDictionary(g => g.Key, g => g.OrderBy(t => t.Id).First().Id);
            var ticketIds = tickets.Select(t => t.Id).Distinct().ToList();
            var withCheckinSet = (
                await _ctx.Set<CheckinEntity>()
                    .AsNoTracking()
                    .Where(ch => ticketIds.Contains(ch.TicketId))
                    .Select(ch => ch.TicketId)
                    .ToListAsync()
            ).ToHashSet();

            var pendingCheckin = 0;
            foreach (var rpid in rpIds)
            {
                if (!ticketByRp.TryGetValue(rpid, out var tktId))
                {
                    pendingCheckin++;
                    continue;
                }

                if (!withCheckinSet.Contains(tktId))
                    pendingCheckin++;
            }

            if (pendingCheckin == 0)
                continue;

            var stLabel = statusName.TryGetValue(c.ReservationStatusId, out var n) && !string.IsNullOrWhiteSpace(n)
                ? n
                : c.ReservationStatusId.ToString();
            rows.Add(
            [
                c.Id.ToString(),
                stLabel,
                flightCode ?? "-",
                c.TotalValue.ToString("0.00"),
                pendingCheckin.ToString(),
                rpIds.Count.ToString()
            ]);
        }

        if (rows.Count == 0)
        {
            SpectreUi.MarkupLineOrPlain(
                "[grey]No hay reservas tuyas con abordaje pendiente.[/]",
                "No hay reservas tuyas con abordaje pendiente."
            );
            return;
        }

        SpectreUi.ShowTable(
            "Check-in: elige el ID de reserva. Aquí se confirma la reserva (si aún estaba pendiente) y se asigna asiento en la cabina reservada.",
            ["ID", "Estado", "Vuelo", "Total", "Pend. abordar", "Total pax"],
            rows
        );
    }

    private async Task Checkin()
    {
        try
        {
            if (_auth.ClientId is null)
                throw new InvalidOperationException("No tienes client_id.");

            await ShowReservationCheckinTablesAsync(_auth.ClientId.Value);

            var available = await HasReservationsAvailableForCheckinAsync(_auth.ClientId.Value);
            if (!available)
            {
                SpectreUi.MarkupLineOrPlain(
                    "[grey]No hay reservas con pasajeros pendientes de abordar.[/]",
                    "No hay reservas con pasajeros pendientes de abordar."
                );
                SpectreUi.Pause();
                return;
            }

            SpectreUi.MarkupLineOrPlain(
                "[green]Escribe el ID de reserva (columna de la tabla superior).[/]",
                "Escribe el ID de reserva (columna de la tabla superior)."
            );

            await RetryUntilValidAsync(async () =>
            {
                var reservationId = SpectreUi.PromptIntRequiredCancelable(
                    "ID reservación a hacer check-in",
                    "0/c/cancelar para salir",
                    min: 1
                );
                await PerformCheckinAsync(_auth.ClientId.Value, reservationId);
            });

            SpectreUi.MarkupLineOrPlain(
                "[green]Check-in realizado.[/]",
                "Check-in realizado."
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}",
                $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}"
            );
        }

        SpectreUi.Pause();
    }

    private async Task ViewTickets()
    {
        try
        {
            if (_auth.ClientId is null)
                throw new InvalidOperationException("No tienes client_id.");

            await ShowReservationCheckinTablesAsync(_auth.ClientId.Value);

            SpectreUi.MarkupLineOrPlain(
                "[green]Escribe el ID de reservación para ver sus tickets/pases.[/]",
                "Escribe el ID de reservación para ver sus tickets/pases."
            );

            await RetryUntilValidAsync(async () =>
            {
                var reservationId = SpectreUi.PromptIntRequiredCancelable(
                    "ID reservación",
                    "0/c/cancelar para salir",
                    min: 1
                );

                var owned = await _ctx.Set<ReservationEntity>()
                    .AsNoTracking()
                    .Where(r => r.Id == reservationId)
                    .Select(r => r.ClientId)
                    .FirstOrDefaultAsync();

                if (owned != _auth.ClientId.Value)
                    throw new InvalidOperationException("No puedes ver tickets de otra persona.");

                await ShowTicketAfterCheckinAsync(reservationId);
            });
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}",
                $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}"
            );
        }

        SpectreUi.Pause();
    }

    private async Task<bool> HasReservationsAvailableForCheckinAsync(int clientId)
    {
        // Pendiente (1) o confirmada (2) con al menos un pasajero sin check-in aún.
        var reservationIds = await _ctx.Set<ReservationEntity>()
            .AsNoTracking()
            .Where(r => r.ClientId == clientId && (r.ReservationStatusId == 1 || r.ReservationStatusId == 2))
            .Select(r => r.Id)
            .ToListAsync();

        if (reservationIds.Count == 0)
            return false;

        var passengerIds = await _ctx.Set<ReservationPassengerEntity>()
            .AsNoTracking()
            .Where(rp => reservationIds.Contains(rp.ReservationFlight!.ReservationId))
            .Select(rp => rp.Id)
            .ToListAsync();

        if (passengerIds.Count == 0)
            return false;

        var withCheckin = await _ctx.Set<TicketEntity>()
            .AsNoTracking()
            .Where(t => passengerIds.Contains(t.ReservationPassengerId))
            .Where(t => t.Checkin != null)
            .Select(t => t.ReservationPassengerId)
            .Distinct()
            .ToListAsync();

        return passengerIds.Any(id => !withCheckin.Contains(id));
    }

    private async Task PerformCheckinAsync(int clientId, int reservationId)
    {
        const int statusPending = 1;
        const int statusConfirmed = 2;
        const int ticketStatusIssued = 1;
        const int checkinStatusDone = 2;

        var reservation = await _ctx.Set<ReservationEntity>()
            .FirstOrDefaultAsync(r => r.Id == reservationId);

        if (reservation is null)
            throw new InvalidOperationException("Reservación no encontrada.");
        if (reservation.ClientId != clientId)
            throw new InvalidOperationException("No puedes hacer check-in de otra persona.");
        if (reservation.ReservationStatusId is not (statusPending or statusConfirmed))
            throw new InvalidOperationException("Solo se puede hacer check-in con reservas pendientes o confirmadas.");

        var staffId = await _ctx.Set<StaffEntity>()
            .AsNoTracking()
            .OrderBy(s => s.Id)
            .Select(s => s.Id)
            .FirstOrDefaultAsync();
        if (staffId < 1)
            throw new InvalidOperationException("No hay personal (staff) para registrar el check-in.");

        var reservationFlights = await _ctx.Set<ReservationFlightEntity>()
            .AsNoTracking()
            .Where(rf => rf.ReservationId == reservationId)
            .Select(rf => new { rf.Id, rf.FlightId })
            .ToListAsync();

        if (reservationFlights.Count == 0)
            throw new InvalidOperationException("La reservación no tiene vuelos asociados.");

        await ShowCabinAvailabilityForFlightAsync(reservationFlights[0].FlightId);

        var baggageTypes = await _ctx.Set<BaggageTypeEntity>()
            .AsNoTracking()
            .OrderBy(bt => bt.Id)
            .Select(bt => new { bt.Id, bt.Name, bt.MaxWeightKg, bt.BasePrice })
            .ToListAsync();

        var strategy = _ctx.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var tx = await _ctx.Database.BeginTransactionAsync();

            var resRow = await _ctx.Set<ReservationEntity>().FirstAsync(r => r.Id == reservationId);
            if (resRow.ReservationStatusId == statusPending)
            {
                resRow.ReservationStatusId = statusConfirmed;
                resRow.UpdatedAt = DateTime.UtcNow;
                await _ctx.SaveChangesAsync();
            }

            foreach (var rf in reservationFlights)
            {
                var reservationPassengerRows = await _ctx.Set<ReservationPassengerEntity>()
                    .AsNoTracking()
                    .Where(rp => rp.ReservationFlightId == rf.Id)
                    .ToListAsync();

                foreach (var rpr in reservationPassengerRows)
                {
                    var existingTicket = await _ctx.Set<TicketEntity>()
                        .Include(t => t.Checkin)
                        .FirstOrDefaultAsync(t => t.ReservationPassengerId == rpr.Id);

                    if (existingTicket?.Checkin is not null)
                        continue;

                    var ticket = existingTicket
                        ?? new TicketEntity
                        {
                            ReservationPassengerId = rpr.Id,
                            Code = $"TKT-{Guid.NewGuid():N}".Substring(0, 12).ToUpperInvariant(),
                            IssueDate = DateTime.UtcNow,
                            TicketStatusId = ticketStatusIssued,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        };

                    if (existingTicket is null)
                    {
                        _ctx.Set<TicketEntity>().Add(ticket);
                        await _ctx.SaveChangesAsync();
                    }

                    var seat = await _ctx.Set<FlightSeatEntity>().Where(
                            s => s.FlightId == rf.FlightId
                                 && s.CabinTypeId == rpr.CabinTypeId
                                 && !s.IsOccupied
                        )
                        .OrderBy(s => s.Id)
                        .FirstOrDefaultAsync();

                    if (seat is null)
                    {
                        throw new InvalidOperationException(
                            $"No hay asiento libre en la cabina reservada (clase id={rpr.CabinTypeId}) para el vuelo {rf.FlightId}."
                        );
                    }

                    seat.IsOccupied = true;

                    var checkin = new CheckinEntity
                    {
                        TicketId = ticket.Id,
                        StaffId = staffId,
                        FlightSeatId = seat.Id,
                        CheckinDate = DateTime.UtcNow,
                        CheckinStatusId = checkinStatusDone,
                        BoardingPassNumber = $"BP-{ticket.Id}-{seat.SeatCode}",
                        HasCheckedBaggage = false,
                        BaggageWeightKg = null
                    };

                    _ctx.Set<CheckinEntity>().Add(checkin);
                    await _ctx.SaveChangesAsync();

                    // Extras de equipaje (0..N items)
                    if (baggageTypes.Count > 0)
                    {
                        var raw = SpectreUi.PromptOptionalCancelable(
                            $"Extras de equipaje para ticket {ticket.Code} asiento {seat.SeatCode}",
                            "Enter=sin extras | número=cuántas piezas"
                        );

                        if (!string.IsNullOrWhiteSpace(raw))
                        {
                            var pieces = int.Parse(raw);
                            if (pieces < 0)
                                pieces = 0;

                            decimal totalWeight = 0m;
                            var any = false;

                            SpectreUi.ShowTable(
                                "Tipos de equipaje",
                                ["Id", "Nombre", "Max kg", "Precio base"],
                                baggageTypes.Select(bt => (IReadOnlyList<string>)[
                                    bt.Id.ToString(),
                                    bt.Name ?? "-",
                                    bt.MaxWeightKg.ToString("0.##"),
                                    bt.BasePrice.ToString("0.00")
                                ]).ToList()
                            );

                            for (var i = 0; i < pieces; i++)
                            {
                                var typeId = SpectreUi.PromptIntRequiredCancelable(
                                    $"Tipo equipaje (pieza {i + 1}/{pieces})",
                                    "0/c/cancelar para salir",
                                    min: 1
                                );

                                var type = baggageTypes.FirstOrDefault(x => x.Id == typeId);
                                if (type is null)
                                    throw new InvalidOperationException("Tipo de equipaje inválido.");

                                var weightStr = SpectreUi.PromptRequiredCancelable(
                                    $"Peso (kg) (máx {type.MaxWeightKg:0.##})",
                                    "0/c/cancelar para salir"
                                );
                                var weight = decimal.Parse(weightStr);
                                if (weight <= 0)
                                    throw new InvalidOperationException("El peso debe ser mayor que 0.");
                                if (weight > type.MaxWeightKg)
                                    throw new InvalidOperationException(
                                        $"Peso excede el máximo del tipo seleccionado ({type.MaxWeightKg:0.##} kg)."
                                    );

                                _ctx.Set<BaggageEntity>().Add(new BaggageEntity
                                {
                                    CheckinId = checkin.Id,
                                    BaggageTypeId = type.Id,
                                    WeightKg = weight,
                                    ChargedPrice = type.BasePrice
                                });

                                totalWeight += weight;
                                any = true;
                            }

                            if (any)
                            {
                                checkin.HasCheckedBaggage = true;
                                checkin.BaggageWeightKg = totalWeight;
                                await _ctx.SaveChangesAsync();
                            }
                        }
                    }
                }
            }

            await tx.CommitAsync();
        });

        await ShowTicketAfterCheckinAsync(reservationId);
    }

    private async Task ShowTicketAfterCheckinAsync(int reservationId)
    {
        var rows = await _ctx.Set<TicketEntity>()
            .AsNoTracking()
            .Where(t => t.ReservationPassenger!.ReservationFlight!.ReservationId == reservationId)
            .Include(t => t.Checkin!)
            .ThenInclude(ch => ch.FlightSeat!)
            .Include(t => t.ReservationPassenger!)
            .ThenInclude(rp => rp.ReservationFlight!)
            .ThenInclude(rf => rf.Flight!)
            .ThenInclude(f => f.Route!)
            .ThenInclude(r => r.OriginAirport!)
            .Include(t => t.ReservationPassenger!)
            .ThenInclude(rp => rp.ReservationFlight!)
            .ThenInclude(rf => rf.Flight!)
            .ThenInclude(f => f.Route!)
            .ThenInclude(r => r.DestinationAirport!)
            .ToListAsync();

        if (rows.Count == 0)
            return;

        // Cargar extras (equipaje) por checkin
        var checkinIds = rows.Where(t => t.Checkin != null).Select(t => t.Checkin!.Id).Distinct().ToList();
        var baggages = await _ctx.Set<BaggageEntity>()
            .AsNoTracking()
            .Where(b => checkinIds.Contains(b.CheckinId))
            .Include(b => b.BaggageType)
            .ToListAsync();

        var baggageByCheckin = baggages.GroupBy(b => b.CheckinId).ToDictionary(g => g.Key, g => g.ToList());

        SpectreUi.ModuleHeader("Ticket / Pase de abordar", "Resumen del check-in");

        var tableRows = new List<IReadOnlyList<string>>();
        foreach (var t in rows.OrderBy(x => x.Id))
        {
            if (t.Checkin is null)
                continue;

            var flight = t.ReservationPassenger?.ReservationFlight?.Flight;
            var route = flight?.Route;
            var origin = route?.OriginAirport?.IataCode ?? "-";
            var dest = route?.DestinationAirport?.IataCode ?? "-";
            var departAt = flight?.DepartureDate.ToString("yyyy-MM-dd HH:mm") ?? "-";
            var seatCode = t.Checkin.FlightSeat?.SeatCode ?? "-";

            var extras = baggageByCheckin.TryGetValue(t.Checkin.Id, out var items)
                ? string.Join(
                    " | ",
                    items.Select(x => $"{x.BaggageType?.Name ?? $"Tipo {x.BaggageTypeId}"} {x.WeightKg:0.##}kg")
                )
                : "-";

            tableRows.Add(
                [
                    t.Code ?? $"TKT-{t.Id}",
                    flight?.FlightCode ?? flight?.Id.ToString() ?? "-",
                    $"{origin}-{dest}",
                    seatCode,
                    t.Checkin.BoardingPassNumber ?? "-",
                    departAt,
                    dest,
                    extras
                ]
            );
        }

        SpectreUi.ShowTable(
            "Ticket / Boarding pass",
            ["Ticket", "Vuelo", "Ruta", "Asiento", "BoardingPass", "Salida", "Destino", "Extras"],
            tableRows
        );

        // Mostrar información de descuento si aplica
        var reservation = await _ctx.Set<ReservationEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == reservationId);

        if (reservation != null && reservation.DiscountPercentage > 0m)
        {
            SpectreUi.MarkupLineOrPlain(
                $"\n[bold yellow]DESCUENTO POR FIDELIZACIÓN APLICADO[/]",
                "\nDESCUENTO POR FIDELIZACIÓN APLICADO"
            );
            SpectreUi.ShowTable(
                "Detalle de descuento",
                ["Concepto", "Valor"],
                new List<IReadOnlyList<string>>
                {
                    new[] { "Precio original", reservation.OriginalTotalValue.ToString("N2") },
                    new[] { "Descuento aplicado", $"-{reservation.DiscountPercentage}% (-{(reservation.OriginalTotalValue - reservation.TotalValue):N2})" },
                    new[] { "PRECIO FINAL", reservation.TotalValue.ToString("N2") },
                }
            );
        }
    }

    private async Task EnsureBillingAddressAsync()
    {
        // Solicita dirección de facturación y la asocia a la persona del usuario (si existe).
        var userPersonId = await _ctx.Set<UserEntity>()
            .AsNoTracking()
            .Where(u => u.Id == _auth.UserId)
            .Select(u => u.PersonId)
            .FirstOrDefaultAsync();

        if (!userPersonId.HasValue)
        {
            // Usuario sin persona asociada: no podemos guardar dirección a nivel de persona.
            return;
        }

        SpectreUi.MarkupLineOrPlain(
            "[grey]Dirección de facturación[/] (0/c/cancelar para salir)",
            "Dirección de facturación (0/c/cancelar para salir)"
        );

        // City
        var cities = await _ctx.Set<sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity.CityEntity>()
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .Select(c => new { c.Id, c.Name })
            .Take(50)
            .ToListAsync();

        if (cities.Count == 0)
            throw new InvalidOperationException("No hay ciudades (cities) registradas.");

        SpectreUi.ShowTable(
            "Ciudades (top 50)",
            ["Id", "Nombre"],
            cities.Select(c => (IReadOnlyList<string>)[c.Id.ToString(), c.Name ?? "-"]).ToList()
        );

        var cityId = 0;
        while (cityId < 1)
        {
            var cityName = SpectreUi.PromptRequiredCancelable("Ciudad (nombre)", "0/c/cancelar para salir");
            cityId = cities.FirstOrDefault(c =>
                    string.Equals(c.Name?.Trim(), cityName.Trim(), StringComparison.OrdinalIgnoreCase)
                )
                ?.Id ?? 0;
            if (cityId < 1)
            {
                ShowRetryableValidation(
                    "Ciudad no reconocida. Escriba el nombre tal como aparece en la tabla (puede copiar/pegar)."
                );
            }
        }

        var streetTypes = await _ctx.Set<StreetTypeEntity>()
            .AsNoTracking()
            .OrderBy(s => s.Id)
            .Select(s => new { s.Id, s.Name })
            .ToListAsync();

        if (streetTypes.Count == 0)
            throw new InvalidOperationException("No hay tipos de vía (street_types).");

        SpectreUi.ShowTable(
            "Tipos de vía",
            ["Id", "Nombre"],
            streetTypes.Select(s => (IReadOnlyList<string>)[s.Id.ToString(), s.Name ?? "-"]).ToList()
        );

        var streetTypeId = 0;
        while (streetTypeId < 1 || streetTypes.All(s => s.Id != streetTypeId))
        {
            streetTypeId = SpectreUi.PromptIntRequiredCancelable("StreetTypeId", "0/c/cancelar para salir", min: 1);
            if (streetTypes.All(s => s.Id != streetTypeId))
            {
                ShowRetryableValidation("Id de tipo de vía inválido. Use un Id de la tabla.");
                streetTypeId = 0;
            }
        }

        var streetName = SpectreUi.PromptRequiredCancelable("Nombre de la vía", "Ej: 10A");
        var streetNumber = SpectreUi.PromptRequiredCancelable("Número", "Ej: 20-30");
        var complement = SpectreUi.PromptOptionalCancelable("Complemento", "Apto/torre/bloque (opcional)");
        var postalCode = SpectreUi.PromptOptionalCancelable("Código postal", "opcional");

        // Ojo: LAST_INSERT_ID() es por-conexión. EF puede abrir/cerrar conexiones entre comandos, así que
        // un INSERT + SELECT LAST_INSERT_ID() podría ejecutarse en sesiones distintas y devolver 0.
        // Insertamos la dirección con EF y usamos el id generado en el entity.
        _ctx.ChangeTracker.Clear();

        var newAddress = new AddressEntity
        {
            CityId = cityId,
            StreetTypeId = streetTypeId,
            StreetName = streetName,
            StreetNumber = streetNumber,
            Complement = string.IsNullOrWhiteSpace(complement) ? null : complement,
            PostalCode = string.IsNullOrWhiteSpace(postalCode) ? null : postalCode
        };

        await _ctx.Set<AddressEntity>().AddAsync(newAddress);
        var inserted = await _ctx.SaveChangesAsync();
        if (inserted < 1 || newAddress.Id < 1)
            throw new InvalidOperationException("No se pudo obtener el id de la dirección creada.");

        var newAddressId = newAddress.Id;

#pragma warning disable EF1002
        await _ctx.Database.ExecuteSqlRawAsync(
            "UPDATE persons SET address_id = {0} WHERE id = {1}",
            newAddressId,
            userPersonId.Value
        );
#pragma warning restore EF1002
    }

    private async Task<int> CreatePersonAsync(
        int documentTypeId,
        string documentNumber,
        string firstName,
        string lastName
    )
    {
        // Id único: (document_type_id, document_number). Si ya existe, se reutiliza.
        var existing = await _ctx.Set<PersonEntity>()
            .AsNoTracking()
            .Where(p => p.DocumentTypeId == documentTypeId && p.DocumentNumber == documentNumber)
            .Select(p => new { p.Id })
            .FirstOrDefaultAsync();

        if (existing is not null && existing.Id > 0)
            return existing.Id;

        var utcNow = DateTime.UtcNow;
        var person = new PersonEntity
        {
            DocumentTypeId = documentTypeId,
            DocumentNumber = documentNumber,
            FirstName = firstName,
            LastName = lastName,
            BirthDate = null,
            Gender = null,
            AddressId = null,
            CreatedAt = utcNow,
            UpdatedAt = utcNow
        };

        _ctx.ChangeTracker.Clear();
        await _ctx.Set<PersonEntity>().AddAsync(person);
        try
        {
            var inserted = await _ctx.SaveChangesAsync();
            if (inserted < 1 || person.Id < 1)
                throw new InvalidOperationException("No se pudo obtener el id de la persona creada.");
            return person.Id;
        }
        catch (DbUpdateException)
        {
            // Carrera: otra transacción insertó el mismo documento.
            var id = await _ctx.Set<PersonEntity>()
                .AsNoTracking()
                .Where(p => p.DocumentTypeId == documentTypeId && p.DocumentNumber == documentNumber)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();

            if (id > 0)
                return id;
            throw;
        }
    }
}

