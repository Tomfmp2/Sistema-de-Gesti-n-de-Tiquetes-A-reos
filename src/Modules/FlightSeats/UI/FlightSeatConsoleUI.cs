using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.UI;

public sealed class FlightSeatConsoleUI : IModuleUI
{
    private readonly IFlightSeatRepository _repository;

    public FlightSeatConsoleUI(IFlightSeatRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Asientos por vuelo", "Crear / listar / consultar / actualizar / eliminar");

            var items = new List<(string Label, Action Action)>
            {
                ("Crear", () => CreateFlightSeatAsync().GetAwaiter().GetResult()),
                ("Listar", () => GetAllFlightSeatsAsync().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetFlightSeatByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateFlightSeatAsync().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteFlightSeatAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
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
            var isOccupied = SpectreUi.PromptBool("¿Asiento ocupado?", defaultValue: false);

            var useCase = new CreateFlightSeatUseCase(_repository);
            var flightSeat = await useCase.ExecuteAsync(flightId, seatCode, cabinTypeId, locationTypeId, isOccupied);
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
                ["ID", "Vuelo", "Asiento", "Cabina", "Ubicación", "Ocupado"],
                list.Select(fs => (IReadOnlyList<string>)
                [
                    fs.Id.ToString(),
                    fs.FlightId.ToString(),
                    fs.SeatCode.ToString(),
                    fs.CabinTypeId.ToString(),
                    fs.LocationTypeId.ToString(),
                    fs.IsOccupied ? "Sí" : "No"
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

            bool? newIsOccupied = null;
            var updateOcc = SpectreUi.PromptBool("¿Cambiar ocupado?", defaultValue: false);
            if (updateOcc)
                newIsOccupied = SpectreUi.PromptBool("¿Ocupado?", defaultValue: false);

            var useCase = new UpdateFlightSeatUseCase(_repository);
            var flightSeat = await useCase.ExecuteAsync(id, newFlightId, newSeatCode, newCabinTypeId, newLocationTypeId, newIsOccupied);
            
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
                ["Ocupado", flightSeat.IsOccupied ? "Sí" : "No"]
            ]
        );
    }
}
