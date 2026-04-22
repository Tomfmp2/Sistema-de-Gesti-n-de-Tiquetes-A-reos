using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;
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
                Console.WriteLine("No tienes reservaciones.");
                return;
            }

            foreach (var r in list.OrderByDescending(x => x.ReservationDate.Value))
            {
                Console.WriteLine(
                    $"ID: {r.Id.Value}, Status: {r.ReservationStatusId.Value}, Total: {r.TotalValue.Value}, BookedAt: {r.ReservationDate.Value:yyyy-MM-dd HH:mm}, ExpiresAt: {(r.ExpiresAt.Value.HasValue ? r.ExpiresAt.Value.Value.ToString("yyyy-MM-dd HH:mm") : "null")}"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
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

            Console.WriteLine("Vuelos disponibles (máx 20):");
            foreach (var f in flights)
            {
                var origin = string.IsNullOrWhiteSpace(f.OriginCode) ? "?" : f.OriginCode;
                var dest = string.IsNullOrWhiteSpace(f.DestinationCode) ? "?" : f.DestinationCode;
                var airline = string.IsNullOrWhiteSpace(f.AirlineName) ? "" : $" · {f.AirlineName}";

                Console.WriteLine(
                    $"- {f.Id}: {f.FlightCode} · {origin} → {dest} · {f.DepartureDate:yyyy-MM-dd HH:mm} · Cupos: {f.AvailableSeats}{airline}"
                );
            }

            Console.Write("ID vuelo a reservar: ");
            var flightId = int.Parse(Console.ReadLine()!);
            var selected = flights.FirstOrDefault(x => x.Id == flightId);
            if (selected is null)
                throw new InvalidOperationException("Vuelo inválido.");

            Console.Write("Cantidad de pasajeros (>=1): ");
            var paxCount = int.Parse(Console.ReadLine()!);
            if (paxCount < 1)
                throw new InvalidOperationException("Cantidad inválida.");
            if (paxCount > selected.AvailableSeats)
                throw new InvalidOperationException("No hay cupos suficientes en el vuelo.");

            Console.Write("Expira en (minutos, opcional; Enter = sin expiración): ");
            var minutesRaw = (Console.ReadLine() ?? string.Empty).Trim();
            DateTime? expiresAt = null;
            if (!string.IsNullOrWhiteSpace(minutesRaw))
            {
                if (!int.TryParse(minutesRaw, out var minutes) || minutes < 1)
                    throw new InvalidOperationException("Minutos inválidos.");
                expiresAt = utcNow.AddMinutes(minutes);
            }

            // 2) Crear booking + relaciones en transacción
            await using var tx = await _ctx.Database.BeginTransactionAsync();

            var booking = await _create.ExecuteAsync(
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
                Console.WriteLine($"Pasajero {i}/{paxCount}");
                Console.Write("Nombre: ");
                var firstName = (Console.ReadLine() ?? string.Empty).Trim();
                Console.Write("Apellido: ");
                var lastName = (Console.ReadLine() ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                    throw new InvalidOperationException("Nombre y apellido son obligatorios.");

                Console.Write($"Documento ({docTypeLabel}) número: ");
                var docNumber = (Console.ReadLine() ?? string.Empty).Trim();
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

            SpectreUi.MarkupLineOrPlain(
                $"[green]Reservación creada[/] booking_id={booking.Id.Value} vuelo={selected.FlightCode} pasajeros={paxCount}.",
                $"Reservación creada booking_id={booking.Id.Value} vuelo={selected.FlightCode} pasajeros={paxCount}."
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }

    private async Task Confirm()
    {
        try
        {
            Console.Write("ID reservación: ");
            var id = int.Parse(Console.ReadLine()!);

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
            Console.Write("ID reservación: ");
            var id = int.Parse(Console.ReadLine()!);

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
            Console.Write("ID reservación: ");
            var id = int.Parse(Console.ReadLine()!);

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

            Console.WriteLine("Cancelada (cupos devueltos).");
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

        Console.WriteLine("Tipos de documento:");
        foreach (var t in types)
            Console.WriteLine($"- {t.Code}: {t.Name}");

        Console.Write("Código (p.ej. CC/PAS): ");
        var code = (Console.ReadLine() ?? string.Empty).Trim();
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

        Console.WriteLine("Tipo de pasajero:");
        foreach (var t in types)
            Console.WriteLine($"- {t.Id}: {t.Name}");

        Console.Write("Seleccione (Enter = Adulto): ");
        var raw = (Console.ReadLine() ?? string.Empty).Trim();
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

