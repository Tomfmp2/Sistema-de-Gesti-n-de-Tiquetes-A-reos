using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;
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
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;
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
        CreatePassengerUseCase createPassenger
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
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Reservaciones", "Crear / ver / modificar / cancelar");

            if (_auth.ClientId is null)
            {
                Console.WriteLine("Tu usuario no está asociado a un cliente (client_id).");
                SpectreUi.Pause();
                return;
            }

            var items = new List<(string Label, Action Action)>
            {
                ("Crear reservación", () => Create().GetAwaiter().GetResult()),
                ("Listar mis reservaciones", () => ListMine().GetAwaiter().GetResult()),
                ("Check-in", () => Checkin().GetAwaiter().GetResult()),
                ("Confirmar (por ID)", () => Confirm().GetAwaiter().GetResult()),
                ("Cambiar expiración (por ID)", () => Update().GetAwaiter().GetResult()),
                ("Cancelar (por ID)", () => Cancel().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
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

            SpectreUi.ShowTable(
                "Mis reservaciones",
                ["ID", "EstadoId", "Total", "Fecha", "Expira"],
                list.OrderByDescending(x => x.ReservationDate.Value)
                    .Select(r => (IReadOnlyList<string>)
                    [
                        r.Id.Value.ToString(),
                        r.ReservationStatusId.Value.ToString(),
                        r.TotalValue.Value.ToString("0.00"),
                        r.ReservationDate.Value.ToString("yyyy-MM-dd HH:mm"),
                        r.ExpiresAt.Value.HasValue ? r.ExpiresAt.Value.Value.ToString("yyyy-MM-dd HH:mm") : "-"
                    ])
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

    private async Task Create()
    {
        try
        {
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
                .Take(20)
                .Select(f => new
                {
                    f.Id,
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
                        : null
                })
                .ToListAsync();

            if (flights.Count == 0)
                throw new InvalidOperationException("No hay vuelos disponibles con cupos.");

            SpectreUi.ShowTable(
                "Vuelos disponibles (máx 20)",
                ["ID", "Código", "Ruta", "Salida", "Cupos", "Aerolínea"],
                flights.Select(f =>
                {
                    var origin = string.IsNullOrWhiteSpace(f.OriginCode) ? "?" : f.OriginCode;
                    var dest = string.IsNullOrWhiteSpace(f.DestinationCode) ? "?" : f.DestinationCode;
                    return (IReadOnlyList<string>)
                    [
                        f.Id.ToString(),
                        f.FlightCode ?? "-",
                        $"{origin} → {dest}",
                        f.DepartureDate.ToString("yyyy-MM-dd HH:mm"),
                        f.AvailableSeats.ToString(),
                        f.AirlineName ?? "-"
                    ];
                }).ToList()
            );

            var flightId = SpectreUi.PromptIntRequiredCancelable(
                "ID vuelo a reservar",
                "0/c/cancelar para salir"
            );
            var selected = flights.FirstOrDefault(x => x.Id == flightId);
            if (selected is null)
                throw new InvalidOperationException("Vuelo inválido.");

            var paxCount = SpectreUi.PromptIntRequiredCancelable(
                "Cantidad de pasajeros",
                $"mín=1, máx={selected.AvailableSeats} (0/c/cancelar para salir)",
                min: 1
            );
            if (paxCount < 1)
                throw new InvalidOperationException("Cantidad inválida.");
            if (paxCount > selected.AvailableSeats)
                throw new InvalidOperationException("No hay cupos suficientes en el vuelo.");

            var minutesRaw = (SpectreUi.PromptOptionalCancelable(
                "Expira en (minutos)",
                "Enter = sin expiración (0/c/cancelar para salir)"
            ) ?? string.Empty).Trim();
            DateTime? expiresAt = null;
            if (!string.IsNullOrWhiteSpace(minutesRaw))
            {
                if (!int.TryParse(minutesRaw, out var minutes) || minutes < 1)
                    throw new InvalidOperationException("Minutos inválidos.");
                expiresAt = utcNow.AddMinutes(minutes);
            }

            // 1.5) Dirección de facturación (se guarda en la persona del usuario/cliente)
            await EnsureBillingAddressAsync();

            // 2) Crear booking + relaciones en transacción
            var strategy = _ctx.Database.CreateExecutionStrategy();
            ReservationAggregate? booking = null;

            await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await _ctx.Database.BeginTransactionAsync();

                booking = await _create.ExecuteAsync(
                    new CreateReservationRequest(
                        ClientId: _auth.ClientId!.Value,
                        ReservationDate: utcNow,
                        ReservationStatusId: pendingStatusId,
                        TotalValue: 0m,
                        ExpiresAt: expiresAt,
                        CreatedAt: utcNow,
                        UpdatedAt: utcNow
                    )
                );

                var bookingFlight = await _createReservationFlight.ExecuteAsync(
                    new CreateReservationFlightRequest(
                        ReservationId: booking.Id.Value,
                        FlightId: selected.Id,
                        PartialValue: 0m
                    )
                );

                var (docTypeId, docTypeLabel) = await PromptDocumentTypeAsync();
                var passengerTypeId = await PromptPassengerTypeAsync();

                for (var i = 1; i <= paxCount; i++)
                {
                    SpectreUi.MarkupLineOrPlain(
                        $"[grey]Pasajero {i}/{paxCount}[/]",
                        $"Pasajero {i}/{paxCount}"
                    );
                    var firstName = SpectreUi.PromptRequiredCancelable("Nombre", "0/c/cancelar para salir").Trim();
                    var lastName = SpectreUi.PromptRequiredCancelable("Apellido", "0/c/cancelar para salir").Trim();
                    if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                        throw new InvalidOperationException("Nombre y apellido son obligatorios.");

                    var docNumber = SpectreUi.PromptRequiredCancelable(
                        $"Documento ({docTypeLabel}) número",
                        "0/c/cancelar para salir"
                    ).Trim();
                    if (string.IsNullOrWhiteSpace(docNumber))
                        throw new InvalidOperationException("Número de documento es obligatorio.");

                    var personId = await CreatePersonAsync(docTypeId, docNumber, firstName, lastName);
                    var passenger = await _createPassenger.ExecuteAsync(
                        new CreatePassengerRequest(PersonId: personId, PassengerTypeId: passengerTypeId)
                    );

                    await _createReservationPassenger.ExecuteAsync(
                        new CreateReservationPassengerRequest(
                            ReservationFlightId: bookingFlight.Id.Value,
                            PassengerId: passenger.Id.Value
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

            SpectreUi.MarkupLineOrPlain(
                $"[green]Reservación creada[/] booking_id={booking!.Id.Value} vuelo={selected.FlightCode} pasajeros={paxCount}.",
                $"Reservación creada booking_id={booking.Id.Value} vuelo={selected.FlightCode} pasajeros={paxCount}."
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

    private async Task Confirm()
    {
        try
        {
            var id = SpectreUi.PromptIntRequiredCancelable("ID reservación", "0/c/cancelar para salir", min: 1);

            var current = await _getById.ExecuteAsync(id);
            if (current is null)
                throw new InvalidOperationException("No encontrada.");
            if (current.ClientId.Value != _auth.ClientId!.Value)
                throw new InvalidOperationException("No puedes confirmar reservaciones de otro cliente.");

            // 2 = Confirmada
            const int confirmedStatusId = 2;
            if (current.ReservationStatusId.Value == confirmedStatusId)
            {
                Console.WriteLine("Ya está confirmada.");
                SpectreUi.Pause();
                return;
            }

            var utcNow = DateTime.UtcNow;
            await _update.ExecuteAsync(
                new UpdateReservationRequest(
                    Id: current.Id.Value,
                    ClientId: current.ClientId.Value,
                    ReservationDate: current.ReservationDate.Value,
                    ReservationStatusId: confirmedStatusId,
                    TotalValue: current.TotalValue.Value,
                    ExpiresAt: current.ExpiresAt.Value,
                    CreatedAt: current.CreatedAt.Value,
                    UpdatedAt: utcNow
                )
            );

            Console.WriteLine("Confirmada.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }

    private async Task Update()
    {
        try
        {
            var id = SpectreUi.PromptIntRequiredCancelable("ID reservación", "0/c/cancelar para salir", min: 1);

            var current = await _getById.ExecuteAsync(id);
            if (current is null)
                throw new InvalidOperationException("No encontrada.");

            if (current.ClientId.Value != _auth.ClientId!.Value)
                throw new InvalidOperationException("No puedes modificar reservaciones de otro cliente.");

            // Solo se permite cambiar expiración mientras está Pendiente.
            const int pendingStatusId = 1;
            if (current.ReservationStatusId.Value != pendingStatusId)
            {
                throw new InvalidOperationException(
                    "Solo puedes cambiar la expiración cuando la reserva está en estado Pendiente."
                );
            }

            Console.Write(
                $"Expira en (minutos; Enter=mantener, 0=quitar) (actual={(current.ExpiresAt.Value.HasValue ? current.ExpiresAt.Value.Value.ToString("yyyy-MM-dd HH:mm") : "null")}): "
            );
            var minutesRaw = (Console.ReadLine() ?? string.Empty).Trim();
            var utcNow = DateTime.UtcNow;
            DateTime? expiresAt = current.ExpiresAt.Value;
            if (!string.IsNullOrWhiteSpace(minutesRaw))
            {
                if (!int.TryParse(minutesRaw, out var minutes) || minutes < 0)
                    throw new InvalidOperationException("Minutos inválidos.");
                expiresAt = minutes == 0 ? null : utcNow.AddMinutes(minutes);
            }

            await _update.ExecuteAsync(
                new UpdateReservationRequest(
                    Id: current.Id.Value,
                    ClientId: current.ClientId.Value,
                    ReservationDate: current.ReservationDate.Value,
                    ReservationStatusId: current.ReservationStatusId.Value,
                    TotalValue: current.TotalValue.Value,
                    ExpiresAt: expiresAt,
                    CreatedAt: current.CreatedAt.Value,
                    UpdatedAt: utcNow
                )
            );

            Console.WriteLine("Expiración actualizada.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }

    private async Task Cancel()
    {
        try
        {
            var id = SpectreUi.PromptIntRequiredCancelable("ID reservación", "0/c/cancelar para salir", min: 1);

            var current = await _getById.ExecuteAsync(id);
            if (current is null)
                throw new InvalidOperationException("No encontrada.");

            if (current.ClientId.Value != _auth.ClientId!.Value)
                throw new InvalidOperationException("No puedes cancelar reservaciones de otro cliente.");

            // 3 = Cancelada
            const int cancelledStatusId = 3;
            if (current.ReservationStatusId.Value == cancelledStatusId)
            {
                Console.WriteLine("Ya está cancelada.");
                SpectreUi.Pause();
                return;
            }

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
                        ExpiresAt: current.ExpiresAt.Value,
                        CreatedAt: current.CreatedAt.Value,
                        UpdatedAt: utcNow
                    )
                );

                await _ctx.SaveChangesAsync();
                await tx.CommitAsync();
            });

            Console.WriteLine("Cancelada (cupos devueltos).");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
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

        var code = SpectreUi.PromptRequiredCancelable("Código (p.ej. CC/PAS)", "0/c/cancelar para salir").Trim();
        var match = types.FirstOrDefault(t => string.Equals(t.Code, code, StringComparison.OrdinalIgnoreCase))
                    ?? types.FirstOrDefault(t => string.Equals(t.Name, code, StringComparison.OrdinalIgnoreCase));
        if (match is null)
            throw new InvalidOperationException("Tipo de documento inválido.");

        return (match.Id, $"{match.Code}");
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

        var raw = (SpectreUi.PromptOptionalCancelable(
            "Seleccione Id",
            "Enter = Adulto (0/c/cancelar para salir)"
        ) ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(raw))
        {
            var adulto = types.FirstOrDefault(t => string.Equals(t.Name, "Adulto", StringComparison.OrdinalIgnoreCase));
            if (adulto is not null)
                return adulto.Id;
            return types.First().Id;
        }

        if (!int.TryParse(raw, out var id))
            throw new InvalidOperationException("Tipo inválido.");
        if (types.All(t => t.Id != id))
            throw new InvalidOperationException("Tipo inválido.");
        return id;
    }

    private async Task Checkin()
    {
        try
        {
            if (_auth.ClientId is null)
                throw new InvalidOperationException("No tienes client_id.");

            var available = await HasReservationsAvailableForCheckinAsync(_auth.ClientId.Value);
            if (!available)
            {
                SpectreUi.MarkupLineOrPlain(
                    "[grey]No tienes reservaciones disponibles para hacer check-in.[/]",
                    "No tienes reservaciones disponibles para hacer check-in."
                );
                SpectreUi.Pause();
                return;
            }

            SpectreUi.MarkupLineOrPlain(
                "[green]Tienes reservaciones disponibles para check-in.[/]",
                "Tienes reservaciones disponibles para check-in."
            );

            var reservationId = SpectreUi.PromptIntRequiredCancelable(
                "ID reservación a hacer check-in",
                "0/c/cancelar para salir",
                min: 1
            );

            await PerformCheckinAsync(_auth.ClientId.Value, reservationId);

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

    private async Task<bool> HasReservationsAvailableForCheckinAsync(int clientId)
    {
        // Disponible = reserva Confirmada (2) con al menos un pasajero sin check-in aún.
        const int confirmedStatusId = 2;

        var reservationIds = await _ctx.Set<ReservationEntity>()
            .AsNoTracking()
            .Where(r => r.ClientId == clientId && r.ReservationStatusId == confirmedStatusId)
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
        const int confirmedStatusId = 2;
        const int ticketStatusIssued = 1; // Emitido
        const int checkinStatusDone = 2; // Realizado

        var reservation = await _ctx.Set<ReservationEntity>()
            .FirstOrDefaultAsync(r => r.Id == reservationId);

        if (reservation is null)
            throw new InvalidOperationException("Reservación no encontrada.");
        if (reservation.ClientId != clientId)
            throw new InvalidOperationException("No puedes hacer check-in de otra persona.");
        if (reservation.ReservationStatusId != confirmedStatusId)
            throw new InvalidOperationException("Solo puedes hacer check-in cuando la reservación está Confirmada.");

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

        var strategy = _ctx.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            await using var tx = await _ctx.Database.BeginTransactionAsync();

            foreach (var rf in reservationFlights)
            {
                var reservationPassengers = await _ctx.Set<ReservationPassengerEntity>()
                    .AsNoTracking()
                    .Where(rp => rp.ReservationFlightId == rf.Id)
                    .Select(rp => rp.Id)
                    .ToListAsync();

                foreach (var reservationPassengerId in reservationPassengers)
                {
                    var existingTicket = await _ctx.Set<TicketEntity>()
                        .Include(t => t.Checkin)
                        .FirstOrDefaultAsync(t => t.ReservationPassengerId == reservationPassengerId);

                    if (existingTicket?.Checkin is not null)
                    {
                        continue; // ya hizo check-in
                    }

                    var ticket = existingTicket ?? new TicketEntity
                    {
                        ReservationPassengerId = reservationPassengerId,
                        Code = $"TKT-{Guid.NewGuid():N}".Substring(0, 12).ToUpperInvariant(),
                        IssueDate = DateTime.UtcNow,
                        TicketStatusId = ticketStatusIssued,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    if (existingTicket is null)
                    {
                        _ctx.Set<TicketEntity>().Add(ticket);
                        await _ctx.SaveChangesAsync(); // necesita Id para el boarding pass/checkin
                    }

                    var seat = await _ctx.Set<FlightSeatEntity>()
                        .FirstOrDefaultAsync(s => s.FlightId == rf.FlightId && !s.IsOccupied);

                    if (seat is null)
                        throw new InvalidOperationException($"No hay asientos disponibles para el vuelo {rf.FlightId}.");

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
                }
            }

            await tx.CommitAsync();
        });
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

        var cityId = SpectreUi.PromptIntRequiredCancelable("CityId", "0/c/cancelar para salir", min: 1);
        if (cities.All(c => c.Id != cityId))
            throw new InvalidOperationException("CityId inválido.");

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

        var streetTypeId = SpectreUi.PromptIntRequiredCancelable("StreetTypeId", "0/c/cancelar para salir", min: 1);
        if (streetTypes.All(s => s.Id != streetTypeId))
            throw new InvalidOperationException("StreetTypeId inválido.");

        var streetName = SpectreUi.PromptRequiredCancelable("Nombre de la vía", "Ej: 10A");
        var streetNumber = SpectreUi.PromptRequiredCancelable("Número", "Ej: 20-30");
        var complement = SpectreUi.PromptOptionalCancelable("Complemento", "Apto/torre/bloque (opcional)");
        var postalCode = SpectreUi.PromptOptionalCancelable("Código postal", "opcional");

        var address = new AddressEntity
        {
            CityId = cityId,
            StreetTypeId = streetTypeId,
            StreetName = streetName,
            StreetNumber = streetNumber,
            Complement = complement,
            PostalCode = postalCode
        };
        _ctx.Set<AddressEntity>().Add(address);
        await _ctx.SaveChangesAsync();

        // Actualiza persona.address_id (si existe la columna y mapeo)
        // Preferimos SQL directo para evitar que falle si el entity no expone AddressId.
#pragma warning disable EF1002
        await _ctx.Database.ExecuteSqlRawAsync(
            "UPDATE persons SET address_id = {0} WHERE id = {1}",
            address.Id,
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
        // Insertamos con SQL directo para no depender de mapeos EF en runtime.
        var utcNow = DateTime.UtcNow;
        var inserted = await _ctx.Database.ExecuteSqlRawAsync(
            """
            INSERT INTO persons
              (document_type_id, document_number, first_name, last_name, birth_date, gender, address_id, created_at, updated_at)
            VALUES
              ({0}, {1}, {2}, {3}, NULL, NULL, NULL, {4}, {5})
            """,
            documentTypeId,
            documentNumber,
            firstName,
            lastName,
            utcNow,
            utcNow
        );

        if (inserted != 1)
            throw new InvalidOperationException("No se pudo crear la persona.");

        var ids = await _ctx.Database.SqlQueryRaw<int>("SELECT LAST_INSERT_ID() AS Value").ToListAsync();
        var id = ids.FirstOrDefault();
        if (id < 1)
            throw new InvalidOperationException("No se pudo obtener el id de la persona creada.");
        return id;
    }
}

