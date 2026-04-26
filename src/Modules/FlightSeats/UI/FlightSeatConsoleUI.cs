using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;
using Microsoft.EntityFrameworkCore;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.UI;

public sealed class FlightSeatConsoleUI : IModuleUI
{
    private readonly IFlightSeatRepository _repository;
    private readonly AppDbContext _ctx;

    public FlightSeatConsoleUI(IFlightSeatRepository repository, AppDbContext ctx)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Asientos por vuelo", "Mapa / disponibilidad / gestión");

            var items = new List<(string Label, Action Action)>
            {
                ("Ver asientos por vuelo (mapa por clase)", () => ViewSeatsByFlightAsync().GetAwaiter().GetResult()),
                ("Consultar disponibilidad por clase",      () => ViewAvailabilityByClassAsync().GetAwaiter().GetResult()),
                ("Ver asientos ocupados por vuelo",         () => ViewOccupiedSeatsAsync().GetAwaiter().GetResult()),
                ("─── Gestión ───────────────────────────", () => { }),
                ("Crear",           () => CreateFlightSeatAsync().GetAwaiter().GetResult()),
                ("Listar todos",    () => GetAllFlightSeatsAsync().GetAwaiter().GetResult()),
                ("Consultar por ID",() => GetFlightSeatByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar",      () => UpdateFlightSeatAsync().GetAwaiter().GetResult()),
                ("Eliminar",        () => DeleteFlightSeatAsync().GetAwaiter().GetResult()),
                ("Volver",          () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    // ─────────────────────────────────────────────
    // Nuevas vistas del requerimiento
    // ─────────────────────────────────────────────

    private async Task ViewSeatsByFlightAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Asientos por vuelo", "Mapa agrupado por clase");
            var flightId = SpectreUi.PromptIntRequiredCancelable("ID vuelo", "0/c/cancelar para salir", min: 1);

            var seats = await _ctx.Set<FlightSeatEntity>()
                .AsNoTracking()
                .Include(s => s.CabinType)
                .Where(s => s.FlightId == flightId)
                .OrderBy(s => s.CabinTypeId)
                .ThenBy(s => s.SeatCode)
                .ToListAsync();

            if (seats.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No hay asientos para ese vuelo.[/]", "No hay asientos para ese vuelo.");
                SpectreUi.Pause();
                return;
            }

            var byClass = seats
                .GroupBy(s => new { s.CabinTypeId, ClassName = s.CabinType?.Name ?? $"Cabina {s.CabinTypeId}" })
                .ToList();

            SpectreUi.MarkupLineOrPlain(
                $"[bold]Vuelo id={flightId} — {seats.Count} asientos totales[/]",
                $"Vuelo id={flightId} — {seats.Count} asientos totales"
            );

            foreach (var group in byClass)
            {
                SpectreUi.ShowTable(
                    $"{group.Key.ClassName}",
                    ["Asiento", "Estado"],
                    group.Select(s => (IReadOnlyList<string>)[
                        s.SeatCode,
                        s.Status switch
                        {
                            "Disponible" => "[green]Disponible[/]",
                            "Reservado"  => "[yellow]Reservado[/]",
                            "Ocupado"    => "[red]Ocupado[/]",
                            _            => s.Status
                        }
                    ]).ToList()
                );
            }
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain($"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}", $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task ViewAvailabilityByClassAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Disponibilidad por clase", "Cantidad disponible / total");
            var flightId = SpectreUi.PromptIntRequiredCancelable("ID vuelo", "0/c/cancelar para salir", min: 1);

            var seats = await _ctx.Set<FlightSeatEntity>()
                .AsNoTracking()
                .Include(s => s.CabinType)
                .Where(s => s.FlightId == flightId)
                .ToListAsync();

            if (seats.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No hay asientos para ese vuelo.[/]", "No hay asientos para ese vuelo.");
                SpectreUi.Pause();
                return;
            }

            var summary = seats
                .GroupBy(s => s.CabinType?.Name ?? $"Cabina {s.CabinTypeId}")
                .Select(g => new
                {
                    Clase       = g.Key,
                    Total       = g.Count(),
                    Disponibles = g.Count(s => s.Status == "Disponible"),
                    Reservados  = g.Count(s => s.Status == "Reservado"),
                    Ocupados    = g.Count(s => s.Status == "Ocupado"),
                    PctOcupacion = g.Count() == 0 ? 0 : (g.Count(s => s.Status != "Disponible") * 100 / g.Count())
                })
                .OrderBy(x => x.Clase)
                .ToList();

            SpectreUi.ShowTable(
                $"Disponibilidad por clase — Vuelo {flightId}",
                ["Clase", "Total", "Disponibles", "Reservados", "Ocupados", "% Ocupación"],
                summary.Select(x => (IReadOnlyList<string>)[
                    x.Clase,
                    x.Total.ToString(),
                    $"[green]{x.Disponibles}[/]",
                    $"[yellow]{x.Reservados}[/]",
                    $"[red]{x.Ocupados}[/]",
                    $"{x.PctOcupacion}%"
                ]).ToList()
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain($"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}", $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task ViewOccupiedSeatsAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Asientos ocupados", "Por vuelo");
            var flightId = SpectreUi.PromptIntRequiredCancelable("ID vuelo", "0/c/cancelar para salir", min: 1);

            var seats = await _ctx.Set<FlightSeatEntity>()
                .AsNoTracking()
                .Include(s => s.CabinType)
                .Where(s => s.FlightId == flightId && s.Status != "Disponible")
                .OrderBy(s => s.CabinTypeId)
                .ThenBy(s => s.SeatCode)
                .ToListAsync();

            if (seats.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[green]Todos los asientos están disponibles.[/]", "Todos los asientos están disponibles.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ShowTable(
                $"Asientos no disponibles — Vuelo {flightId}",
                ["Asiento", "Clase", "Estado"],
                seats.Select(s => (IReadOnlyList<string>)[
                    s.SeatCode,
                    s.CabinType?.Name ?? $"Cabina {s.CabinTypeId}",
                    s.Status switch
                    {
                        "Reservado" => "[yellow]Reservado[/]",
                        "Ocupado"   => "[red]Ocupado[/]",
                        _           => s.Status
                    }
                ]).ToList()
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain($"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}", $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task CreateFlightSeatAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Asientos por vuelo", "Crear");
            var flightId = SpectreUi.PromptIntRequiredCancelable("ID vuelo", "0/c/cancelar para salir", min: 1);
            var seatCode = SpectreUi.PromptRequiredCancelable("Código de asiento", "máx 5 chars (0/c/cancelar para salir)").Trim();
            if (seatCode.Length > 5)
                throw new InvalidOperationException("Código de asiento no válido (máx 5 caracteres).");

            var cabinTypeId = SpectreUi.PromptIntRequiredCancelable("ID tipo de cabina", "0/c/cancelar para salir", min: 1);
            var locationTypeId = SpectreUi.PromptIntRequiredCancelable("ID tipo de ubicación", "0/c/cancelar para salir", min: 1);
            var status = SpectreUi.PromptRequiredCancelable("Estado (Disponible/Reservado/Ocupado)", "0/c/cancelar para salir").Trim();

            var useCase = new CreateFlightSeatUseCase(_repository);
            var flightSeat = await useCase.ExecuteAsync(flightId, seatCode, cabinTypeId, locationTypeId, status);
            SpectreUi.MarkupLineOrPlain($"[green]Asiento creado[/] id={flightSeat.Id}.", $"Asiento creado id={flightSeat.Id}.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain($"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}", $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task GetFlightSeatByIdAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Asientos por vuelo", "Consultar por ID");
            var id = SpectreUi.PromptIntRequiredCancelable("ID asiento de vuelo", "0/c/cancelar para salir", min: 1);

            var useCase = new GetFlightSeatByIdUseCase(_repository);
            var flightSeat = await useCase.ExecuteAsync(id);
            
            if (flightSeat == null)
            {
                SpectreUi.MarkupLineOrPlain("[grey]Asiento no encontrado.[/]", "Asiento no encontrado.");
                return;
            }

            ShowFlightSeatCard(flightSeat);
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain($"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}", $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task GetAllFlightSeatsAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Asientos por vuelo", "Listar");
            var useCase = new GetAllFlightSeatsUseCase(_repository);
            var flightSeats = await useCase.ExecuteAsync();
            
            var list = flightSeats.ToList();
            if (!list.Any())
            {
                SpectreUi.MarkupLineOrPlain("[grey]No hay asientos de vuelo registrados.[/]", "No hay asientos de vuelo registrados.");
                return;
            }

            SpectreUi.ShowTable(
                "Asientos",
                ["ID", "Vuelo", "Asiento", "Cabina", "Ubicación", "Estado"],
                list.Select(fs => (IReadOnlyList<string>)
                [
                    fs.Id.ToString(),
                    fs.FlightId.ToString(),
                    fs.SeatCode.ToString(),
                    fs.CabinTypeId.ToString(),
                    fs.LocationTypeId.ToString(),
                    fs.Status.Value
                ]).ToList()
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain($"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}", $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task UpdateFlightSeatAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Asientos por vuelo", "Actualizar");
            var id = SpectreUi.PromptIntRequiredCancelable("ID asiento de vuelo", "0/c/cancelar para salir", min: 1);

            var newFlightIdRaw = SpectreUi.PromptOptionalCancelable("Nuevo ID vuelo", "Enter=omitir");
            int? newFlightId = string.IsNullOrWhiteSpace(newFlightIdRaw) ? null : int.Parse(newFlightIdRaw);

            var newSeatCode = SpectreUi.PromptOptionalCancelable("Nuevo código de asiento", "Enter=omitir");
            if (string.IsNullOrWhiteSpace(newSeatCode))
                newSeatCode = null;

            var newCabinTypeIdRaw = SpectreUi.PromptOptionalCancelable("Nuevo ID tipo de cabina", "Enter=omitir");
            int? newCabinTypeId = string.IsNullOrWhiteSpace(newCabinTypeIdRaw) ? null : int.Parse(newCabinTypeIdRaw);

            var newLocationTypeIdRaw = SpectreUi.PromptOptionalCancelable("Nuevo ID tipo de ubicación", "Enter=omitir");
            int? newLocationTypeId = string.IsNullOrWhiteSpace(newLocationTypeIdRaw) ? null : int.Parse(newLocationTypeIdRaw);

            string? newStatus = null;
            var updateStatus = SpectreUi.PromptBool("¿Cambiar estado?", defaultValue: false);
            if (updateStatus)
                newStatus = SpectreUi.PromptRequiredCancelable("Nuevo estado", "0/c/cancelar para salir").Trim();

            var useCase = new UpdateFlightSeatUseCase(_repository);
            var flightSeat = await useCase.ExecuteAsync(id, newFlightId, newSeatCode, newCabinTypeId, newLocationTypeId, newStatus);
            
            if (flightSeat == null)
            {
                SpectreUi.MarkupLineOrPlain("[grey]Asiento no encontrado.[/]", "Asiento no encontrado.");
                return;
            }

            SpectreUi.MarkupLineOrPlain("[green]Actualizado correctamente.[/]", "Actualizado correctamente.");
            ShowFlightSeatCard(flightSeat);
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain($"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}", $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task DeleteFlightSeatAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Asientos por vuelo", "Eliminar");
            var id = SpectreUi.PromptIntRequiredCancelable("ID asiento a eliminar", "0/c/cancelar para salir", min: 1);
            var confirm = SpectreUi.PromptBool("¿Confirma la eliminación?", defaultValue: false);
            if (!confirm)
            {
                SpectreUi.MarkupLineOrPlain("[grey]Eliminación cancelada.[/]", "Eliminación cancelada.");
                return;
            }

            var useCase = new DeleteFlightSeatUseCase(_repository);
            bool deleted = await useCase.ExecuteAsync(id);
            
            if (!deleted)
            {
                SpectreUi.MarkupLineOrPlain("[grey]Asiento no encontrado.[/]", "Asiento no encontrado.");
                return;
            }

            SpectreUi.MarkupLineOrPlain("[green]Asiento eliminado correctamente.[/]", "Asiento eliminado correctamente.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain($"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}", $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private static void ShowFlightSeatCard(Domain.Aggregate.FlightSeat flightSeat)
    {
        SpectreUi.ShowTable(
            "Asiento",
            ["Campo", "Valor"],
            [
                ["ID", flightSeat.Id.ToString()],
                ["Vuelo", flightSeat.FlightId.ToString()],
                ["Asiento", flightSeat.SeatCode.ToString()],
                ["Cabina", flightSeat.CabinTypeId.ToString()],
                ["Ubicación", flightSeat.LocationTypeId.ToString()],
                ["Estado", flightSeat.Status.Value]
            ]
        );
    }
}
