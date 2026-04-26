using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.UI;

public sealed class FlightConsoleUI : IModuleUI
{
    private readonly CreateFlightUseCase _create;
    private readonly GetFlightByIdUseCase _getById;
    private readonly GetAllFlightsUseCase _getAll;
    private readonly UpdateFlightUseCase _update;
    private readonly DeleteFlightUseCase _delete;
    private readonly sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Application.UseCases.GenerateFlightSeatsUseCase _generateSeats;

    public FlightConsoleUI(
        CreateFlightUseCase create,
        GetFlightByIdUseCase getById,
        GetAllFlightsUseCase getAll,
        UpdateFlightUseCase update,
        DeleteFlightUseCase delete,
        sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Application.UseCases.GenerateFlightSeatsUseCase generateSeats
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
        _generateSeats = generateSeats;
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Vuelos", "Gestión de vuelos (código, ruta, avión, fechas, cupos, estado)");

            var items = new (string Label, Action Action)[]
            {
                ("Crear vuelo", () => Create().GetAwaiter().GetResult()),
                ("Listar todos", () => ListAll().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetById().GetAwaiter().GetResult()),
                ("Actualizar", () => Update().GetAwaiter().GetResult()),
                ("Eliminar", () => Delete().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task Create()
    {
        try
        {
            SpectreUi.ModuleHeader("Crear vuelo", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var code = SpectreUi.PromptRequiredCancelable("Código de vuelo", "p.ej. AV123");
            var airlineId = SpectreUi.PromptIntRequiredCancelable("AirlineId", min: 1);
            var routeId = SpectreUi.PromptIntRequiredCancelable("RouteId", min: 1);
            var aircraftId = SpectreUi.PromptIntRequiredCancelable("AircraftId", min: 1);
            var departure = PromptDateTimeRequired("Salida (UTC)", "yyyy-MM-dd HH:mm");
            var eta = PromptDateTimeRequired("Llegada estimada (UTC)", "yyyy-MM-dd HH:mm");
            var totalCapacity = SpectreUi.PromptIntRequiredCancelable("Capacidad total", min: 1);
            var availableSeats = SpectreUi.PromptIntRequiredCancelable("Asientos disponibles", min: 0);
            var statusId = SpectreUi.PromptIntRequiredCancelable("FlightStatusId", min: 1);

            var now = DateTime.UtcNow;
            var created = await _create.ExecuteAsync(new CreateFlightRequest(
                FlightCode: code,
                AirlineId: airlineId,
                RouteId: routeId,
                AircraftId: aircraftId,
                DepartureDate: departure,
                EstimatedArrivalDate: eta,
                TotalCapacity: totalCapacity,
                AvailableSeats: availableSeats,
                FlightStatusId: statusId,
                RescheduledAt: null,
                CreatedAt: now,
                UpdatedAt: now
            ));

            SpectreUi.MarkupLineOrPlain(
                $"[green]Vuelo creado[/] id={created.Id.Value} · [bold]{created.FlightCode.Value}[/]",
                $"Vuelo creado id={created.Id.Value} · {created.FlightCode.Value}"
            );

            // Generar asientos
            var seatsGenerated = await _generateSeats.ExecuteAsync(created.Id.Value, created.AircraftId.Value);
            if (seatsGenerated > 0)
            {
                SpectreUi.MarkupLineOrPlain(
                    $"[green]Se generaron {seatsGenerated} asientos para este vuelo.[/]",
                    $"Se generaron {seatsGenerated} asientos para este vuelo."
                );
            }
            else
            {
                SpectreUi.MarkupLineOrPlain(
                    "[yellow]Advertencia: No se encontraron configuraciones de cabina para generar los asientos.[/]",
                    "Advertencia: No se encontraron configuraciones de cabina para generar los asientos."
                );
            }
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

    private async Task ListAll()
    {
        try
        {
            var flights = (await _getAll.ExecuteAsync()).ToList();
            if (flights.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No hay vuelos para mostrar.[/]", "No hay vuelos para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Vuelos", "Listado");
            SpectreUi.ShowTable(
                "Vuelos",
                ["ID", "Código", "RutaId", "AerolíneaId", "AviónId", "Salida", "Cupos", "EstadoId"],
                flights
                    .OrderBy(f => f.Id.Value)
                    .Select(f => (IReadOnlyList<string>)new[]
                    {
                        f.Id.Value.ToString(),
                        f.FlightCode.Value,
                        f.RouteId.Value.ToString(),
                        f.AirlineId.Value.ToString(),
                        f.AircraftId.Value.ToString(),
                        f.DepartureDate.Value.ToString("yyyy-MM-dd HH:mm"),
                        $"{f.AvailableSeats.Value}/{f.TotalCapacity.Value}",
                        f.FlightStatusId.Value.ToString()
                    })
                    .ToList()
            );
        }
        catch (Exception ex)
        {
            SpectreUi.ShowException(ex);
        }

        SpectreUi.Pause();
    }

    private async Task GetById()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar vuelo", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var flight = await _getById.ExecuteAsync(id);
            if (flight is null)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No encontrado.[/]", "No encontrado.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ShowTable(
                "Vuelo",
                ["Campo", "Valor"],
                [
                    ["ID", flight.Id.Value.ToString()],
                    ["Código", flight.FlightCode.Value],
                    ["AirlineId", flight.AirlineId.Value.ToString()],
                    ["RouteId", flight.RouteId.Value.ToString()],
                    ["AircraftId", flight.AircraftId.Value.ToString()],
                    ["Salida", flight.DepartureDate.Value.ToString("yyyy-MM-dd HH:mm")],
                    ["Llegada", flight.EstimatedArrivalDate.Value.ToString("yyyy-MM-dd HH:mm")],
                    ["Capacidad", flight.TotalCapacity.Value.ToString()],
                    ["Disponibles", flight.AvailableSeats.Value.ToString()],
                    ["EstadoId", flight.FlightStatusId.Value.ToString()],
                    ["Reprogramado", flight.RescheduledAt.Value?.ToString("yyyy-MM-dd HH:mm") ?? "-"],
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

    private async Task Update()
    {
        try
        {
            SpectreUi.ModuleHeader("Actualizar vuelo", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var code = SpectreUi.PromptRequiredCancelable("Código de vuelo", "p.ej. AV123");
            var airlineId = SpectreUi.PromptIntRequiredCancelable("AirlineId", min: 1);
            var routeId = SpectreUi.PromptIntRequiredCancelable("RouteId", min: 1);
            var aircraftId = SpectreUi.PromptIntRequiredCancelable("AircraftId", min: 1);
            var departure = PromptDateTimeRequired("Salida (UTC)", "yyyy-MM-dd HH:mm");
            var eta = PromptDateTimeRequired("Llegada estimada (UTC)", "yyyy-MM-dd HH:mm");
            var totalCapacity = SpectreUi.PromptIntRequiredCancelable("Capacidad total", min: 1);
            var availableSeats = SpectreUi.PromptIntRequiredCancelable("Asientos disponibles", min: 0);
            var statusId = SpectreUi.PromptIntRequiredCancelable("FlightStatusId", min: 1);

            var rescheduledRaw = SpectreUi.PromptOptionalCancelable("Reprogramado en (UTC)", "yyyy-MM-dd HH:mm (opcional)");
            DateTime? rescheduledAt = null;
            if (!string.IsNullOrWhiteSpace(rescheduledRaw))
            {
                if (!DateTime.TryParse(rescheduledRaw, out var parsed))
                    throw new InvalidOperationException("Fecha/hora inválida.");
                rescheduledAt = parsed;
            }

            var now = DateTime.UtcNow;
            await _update.ExecuteAsync(new UpdateFlightRequest(
                Id: id,
                FlightCode: code,
                AirlineId: airlineId,
                RouteId: routeId,
                AircraftId: aircraftId,
                DepartureDate: departure,
                EstimatedArrivalDate: eta,
                TotalCapacity: totalCapacity,
                AvailableSeats: availableSeats,
                FlightStatusId: statusId,
                RescheduledAt: rescheduledAt,
                CreatedAt: now,
                UpdatedAt: now
            ));

            SpectreUi.MarkupLineOrPlain("[green]Actualizado.[/]", "Actualizado.");
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

    private async Task Delete()
    {
        try
        {
            SpectreUi.ModuleHeader("Eliminar vuelo", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            await _delete.ExecuteAsync(id);
            SpectreUi.MarkupLineOrPlain("[green]Eliminado.[/]", "Eliminado.");
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

    private static DateTime PromptDateTimeRequired(string label, string formatHint)
    {
        while (true)
        {
            var raw = SpectreUi.PromptRequiredCancelable(label, formatHint);
            if (DateTime.TryParse(raw, out var dt))
                return dt;
            SpectreUi.MarkupLineOrPlain("[red]Fecha/hora inválida.[/]", "Fecha/hora inválida.");
        }
    }
}

