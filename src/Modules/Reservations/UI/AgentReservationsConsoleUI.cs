using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;
using Microsoft.EntityFrameworkCore;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.UI;

/// <summary>
/// UI para Agente: puede crear/listar reservaciones para cualquier cliente (por documento).
/// </summary>
public sealed class AgentReservationsConsoleUI : IModuleUI
{
    private readonly AppDbContext _ctx;
    private readonly CreateReservationUseCase _create;
    private readonly GetAllReservationsUseCase _getAll;
    private readonly GetReservationByIdUseCase _getById;
    private readonly UpdateReservationUseCase _update;
    private readonly CreateReservationFlightUseCase _createReservationFlight;
    private readonly CreateReservationPassengerUseCase _createReservationPassenger;
    private readonly CreatePassengerUseCase _createPassenger;

    public AgentReservationsConsoleUI(
        AppDbContext ctx,
        CreateReservationUseCase create,
        GetAllReservationsUseCase getAll,
        GetReservationByIdUseCase getById,
        UpdateReservationUseCase update,
        CreateReservationFlightUseCase createReservationFlight,
        CreateReservationPassengerUseCase createReservationPassenger,
        CreatePassengerUseCase createPassenger
    )
    {
        _ctx = ctx;
        _create = create;
        _getAll = getAll;
        _getById = getById;
        _update = update;
        _createReservationFlight = createReservationFlight;
        _createReservationPassenger = createReservationPassenger;
        _createPassenger = createPassenger;
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Reservaciones (Agente)", "Crear y consultar reservaciones por cliente");
            var items = new List<(string Label, Action Action)>
            {
                ("Crear reservación para cliente", () => CreateForClient().GetAwaiter().GetResult()),
                ("Listar reservaciones por cliente", () => ListByClient().GetAwaiter().GetResult()),
                ("Consultar reservación por ID", () => GetById().GetAwaiter().GetResult()),
                ("Confirmar reservación (por ID)", () => Confirm().GetAwaiter().GetResult()),
                ("Cancelar reservación (por ID)", () => Cancel().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task ListByClient()
    {
        try
        {
            SpectreUi.ModuleHeader("Reservaciones por cliente", null);
            var clientId = await ResolveClientIdByDocumentAsync(createIfMissing: false);

            var list = (await _getAll.ExecuteAsync())
                .Where(r => r.ClientId.Value == clientId)
                .ToList();

            if (list.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No hay reservaciones para ese cliente.[/]", "No hay reservaciones para ese cliente.");
                SpectreUi.Pause();
                return;
            }

            var statusById = await _ctx
                .Set<ReservationStatusEntity>()
                .AsNoTracking()
                .ToDictionaryAsync(s => s.Id, s => s.Name);

            SpectreUi.ShowTable(
                $"Reservaciones (client_id={clientId})",
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
                            r.ExpiresAt.Value.HasValue ? r.ExpiresAt.Value.Value.ToString("yyyy-MM-dd HH:mm") : "-"
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
            SpectreUi.ShowException(ex);
        }
        SpectreUi.Pause();
    }

    private async Task CreateForClient()
    {
        try
        {
            SpectreUi.ModuleHeader("Crear reservación (Agente)", "Se crea a nombre del cliente indicado");

            var clientId = await ResolveClientIdByDocumentAsync(createIfMissing: true);

            const int pendingStatusId = 1;
            var utcNow = DateTime.UtcNow;

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
                    f.FlightCode,
                    f.DepartureDate,
                    f.AvailableSeats,
                    AirlineName = f.Airline != null ? f.Airline.Name : null,
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
                "Vuelos disponibles (máx 30)",
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

            var flightId = SpectreUi.PromptIntRequiredCancelable("ID vuelo a reservar", "0/c/cancelar", min: 1);
            var selected = flights.FirstOrDefault(x => x.Id == flightId);
            if (selected is null)
                throw new InvalidOperationException("Vuelo inválido.");

            var paxCount = SpectreUi.PromptIntRequiredCancelable(
                "Cantidad de pasajeros",
                $"mín=1, máx={selected.AvailableSeats} (0/c/cancelar)",
                min: 1
            );
            if (paxCount > selected.AvailableSeats)
                throw new InvalidOperationException("No hay cupos suficientes en el vuelo.");

            // Expiración opcional
            var minutesRaw = (SpectreUi.PromptOptionalCancelable(
                "Expira en (minutos)",
                "Enter = sin expiración (0/c/cancelar)"
            ) ?? string.Empty).Trim();
            DateTime? expiresAt = null;
            if (!string.IsNullOrWhiteSpace(minutesRaw))
            {
                if (!int.TryParse(minutesRaw, out var minutes) || minutes < 1)
                    throw new InvalidOperationException("Minutos inválidos.");
                expiresAt = utcNow.AddMinutes(minutes);
            }

            // 2) Crear booking + relaciones en transacción
            var strategy = _ctx.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var tx = await _ctx.Database.BeginTransactionAsync();

                var booking = await _create.ExecuteAsync(new CreateReservationRequest(
                    ClientId: clientId,
                    ReservationDate: utcNow,
                    ReservationStatusId: pendingStatusId,
                    TotalValue: 0m,
                    ExpiresAt: expiresAt,
                    CreatedAt: utcNow,
                    UpdatedAt: utcNow
                ));

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
                    SpectreUi.MarkupLineOrPlain($"[grey]Pasajero {i}/{paxCount}[/]", $"Pasajero {i}/{paxCount}");
                    var firstName = SpectreUi.PromptRequiredCancelable("Nombre", "0/c/cancelar").Trim();
                    var lastName = SpectreUi.PromptRequiredCancelable("Apellido", "0/c/cancelar").Trim();
                    var docNumber = SpectreUi.PromptRequiredCancelable($"Documento ({docTypeLabel}) número", "0/c/cancelar").Trim();

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

                // Reservar cupos (concurrencia)
                var flightRow = await _ctx.Set<FlightEntity>().FirstOrDefaultAsync(f => f.Id == selected.Id);
                if (flightRow is null)
                    throw new InvalidOperationException("Vuelo no encontrado al actualizar cupos.");

                flightRow.AvailableSeats -= paxCount;
                if (flightRow.AvailableSeats < 0)
                    throw new InvalidOperationException("Cupos negativos (concurrencia).");

                await _ctx.SaveChangesAsync();
                await tx.CommitAsync();

                SpectreUi.MarkupLineOrPlain(
                    $"[green]Reservación creada[/] booking_id={booking.Id.Value} client_id={clientId}.",
                    $"Reservación creada booking_id={booking.Id.Value} client_id={clientId}."
                );
            });
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.ShowException(ex);
        }

        SpectreUi.Pause();
    }

    private async Task<int> ResolveClientIdByDocumentAsync(bool createIfMissing)
    {
        SpectreUi.MarkupLineOrPlain(
            "[grey]Identificación del cliente[/] (por documento)",
            "Identificación del cliente (por documento)"
        );

        var (documentTypeId, documentTypeLabel) = await PromptDocumentTypeAsync();
        var documentNumber = SpectreUi.PromptRequiredCancelable(
            "Número de documento",
            "0/c/cancelar"
        ).Trim();

        var personId = await _ctx.Set<PersonEntity>()
            .AsNoTracking()
            .Where(p => p.DocumentTypeId == documentTypeId && p.DocumentNumber == documentNumber)
            .Select(p => (int?)p.Id)
            .FirstOrDefaultAsync();

        if (!personId.HasValue || personId.Value < 1)
        {
            if (!createIfMissing)
                throw new InvalidOperationException($"No existe cliente con documento {documentTypeLabel} {documentNumber}.");

            SpectreUi.MarkupLineOrPlain(
                "[yellow]No existe una persona con ese documento.[/] Se creará el cliente.",
                "No existe una persona con ese documento. Se creará el cliente."
            );

            var firstName = SpectreUi.PromptRequiredCancelable("Nombre", "0/c/cancelar").Trim();
            var lastName = SpectreUi.PromptRequiredCancelable("Apellido", "0/c/cancelar").Trim();
            personId = await CreatePersonAsync(documentTypeId, documentNumber, firstName, lastName);
        }

        var clientId = await _ctx.Set<ClientEntity>()
            .AsNoTracking()
            .Where(c => c.PersonId == personId.Value)
            .Select(c => (int?)c.Id)
            .FirstOrDefaultAsync();

        if (clientId.HasValue && clientId.Value > 0)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[grey]Cliente encontrado[/] client_id={clientId.Value} (doc {documentTypeLabel} {documentNumber})",
                $"Cliente encontrado client_id={clientId.Value} (doc {documentTypeLabel} {documentNumber})"
            );
            return clientId.Value;
        }

        if (!createIfMissing)
            throw new InvalidOperationException($"La persona {documentTypeLabel} {documentNumber} no está registrada como cliente.");

        var utcNow = DateTime.UtcNow;
        var inserted = await _ctx.Database.ExecuteSqlRawAsync(
            """
            INSERT INTO clients (person_id, created_at)
            VALUES ({0}, {1})
            """,
            personId.Value,
            utcNow
        );

        if (inserted != 1)
            throw new InvalidOperationException("No se pudo crear el cliente.");

        var ids = await _ctx.Database.SqlQueryRaw<int>("SELECT LAST_INSERT_ID() AS Value").ToListAsync();
        var newId = ids.FirstOrDefault();
        if (newId < 1)
            throw new InvalidOperationException("No se pudo obtener el id del cliente creado.");

        SpectreUi.MarkupLineOrPlain(
            $"[green]Cliente creado[/] client_id={newId} (doc {documentTypeLabel} {documentNumber})",
            $"Cliente creado client_id={newId} (doc {documentTypeLabel} {documentNumber})"
        );
        return newId;
    }

    private async Task GetById()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar reservación", null);
            var id = SpectreUi.PromptIntRequiredCancelable("ID reservación", "0/c/cancelar", min: 1);
            var x = await _getById.ExecuteAsync(id);
            if (x is null)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No encontrada.[/]", "No encontrada.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ShowTable(
                "Reservación",
                ["Campo", "Valor"],
                [
                    ["ID", x.Id.Value.ToString()],
                    ["ClientId", x.ClientId.Value.ToString()],
                    ["StatusId", x.ReservationStatusId.Value.ToString()],
                    ["Total", x.TotalValue.Value.ToString("0.00")],
                    ["Fecha", x.ReservationDate.Value.ToString("yyyy-MM-dd HH:mm")],
                    ["Expira", x.ExpiresAt.Value?.ToString("yyyy-MM-dd HH:mm") ?? "-"],
                ]
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.ShowException(ex);
        }
        SpectreUi.Pause();
    }

    private async Task Confirm()
    {
        try
        {
            var id = SpectreUi.PromptIntRequiredCancelable("ID reservación", "0/c/cancelar", min: 1);
            var current = await _getById.ExecuteAsync(id);
            if (current is null)
                throw new InvalidOperationException("No encontrada.");

            const int confirmedStatusId = 2;
            if (current.ReservationStatusId.Value == confirmedStatusId)
            {
                SpectreUi.MarkupLineOrPlain("[grey]Ya está confirmada.[/]", "Ya está confirmada.");
                SpectreUi.Pause();
                return;
            }

            var utcNow = DateTime.UtcNow;
            await _update.ExecuteAsync(new UpdateReservationRequest(
                Id: current.Id.Value,
                ClientId: current.ClientId.Value,
                ReservationDate: current.ReservationDate.Value,
                ReservationStatusId: confirmedStatusId,
                TotalValue: current.TotalValue.Value,
                ExpiresAt: current.ExpiresAt.Value,
                CreatedAt: current.CreatedAt.Value,
                UpdatedAt: utcNow
            ));

            SpectreUi.MarkupLineOrPlain("[green]Reservación confirmada.[/]", "Reservación confirmada.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.ShowException(ex);
        }
        SpectreUi.Pause();
    }

    private async Task Cancel()
    {
        try
        {
            var id = SpectreUi.PromptIntRequiredCancelable("ID reservación", "0/c/cancelar", min: 1);
            var current = await _getById.ExecuteAsync(id);
            if (current is null)
                throw new InvalidOperationException("No encontrada.");

            const int cancelledStatusId = 3;
            if (current.ReservationStatusId.Value == cancelledStatusId)
            {
                SpectreUi.MarkupLineOrPlain("[grey]Ya está cancelada.[/]", "Ya está cancelada.");
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

                var utcNow = DateTime.UtcNow;
                await _update.ExecuteAsync(new UpdateReservationRequest(
                    Id: current.Id.Value,
                    ClientId: current.ClientId.Value,
                    ReservationDate: current.ReservationDate.Value,
                    ReservationStatusId: cancelledStatusId,
                    TotalValue: current.TotalValue.Value,
                    ExpiresAt: current.ExpiresAt.Value,
                    CreatedAt: current.CreatedAt.Value,
                    UpdatedAt: utcNow
                ));

                await _ctx.SaveChangesAsync();
                await tx.CommitAsync();
            });

            SpectreUi.MarkupLineOrPlain("[green]Reservación cancelada[/] (cupos devueltos).", "Reservación cancelada (cupos devueltos).");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.ShowException(ex);
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

        var code = SpectreUi.PromptRequiredCancelable("Código (p.ej. CC/PAS)", "0/c/cancelar").Trim();
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
            "Enter = Adulto (0/c/cancelar)"
        ) ?? string.Empty).Trim();

        if (string.IsNullOrWhiteSpace(raw))
        {
            var adulto = types.FirstOrDefault(t => string.Equals(t.Name, "Adulto", StringComparison.OrdinalIgnoreCase));
            return adulto?.Id ?? types.First().Id;
        }

        if (!int.TryParse(raw, out var id))
            throw new InvalidOperationException("Tipo inválido.");
        if (types.All(t => t.Id != id))
            throw new InvalidOperationException("Tipo inválido.");
        return id;
    }

    private async Task<int> CreatePersonAsync(int documentTypeId, string documentNumber, string firstName, string lastName)
    {
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

