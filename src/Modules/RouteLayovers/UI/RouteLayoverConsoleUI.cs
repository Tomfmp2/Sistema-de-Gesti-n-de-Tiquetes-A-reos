using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.UI;

public class RouteLayoverConsoleUI : IModuleUI
{
    private readonly CreateRouteLayoverUseCase _createUseCase;
    private readonly GetRouteLayoverByIdUseCase _getByIdUseCase;
    private readonly GetAllRouteLayoversUseCase _getAllUseCase;
    private readonly UpdateRouteLayoverUseCase _updateUseCase;
    private readonly DeleteRouteLayoverUseCase _deleteUseCase;

    public RouteLayoverConsoleUI(CreateRouteLayoverUseCase createUseCase, GetRouteLayoverByIdUseCase getByIdUseCase, GetAllRouteLayoversUseCase getAllUseCase, UpdateRouteLayoverUseCase updateUseCase, DeleteRouteLayoverUseCase deleteUseCase)
    {
        _createUseCase = createUseCase;
        _getByIdUseCase = getByIdUseCase;
        _getAllUseCase = getAllUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Escalas de ruta", "Gestión de escalas en una ruta");

            var items = new (string Label, Action Action)[]
            {
                ("Crear escala", () => CreateRouteLayover().GetAwaiter().GetResult()),
                ("Listar todas", () => GetAllRouteLayovers().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetRouteLayoverById().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateRouteLayover().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteRouteLayover().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateRouteLayover()
    {
        try
        {
            SpectreUi.ModuleHeader("Crear escala", "Ruta y tiempos");
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var routeId = SpectreUi.PromptIntRequiredCancelable("ID ruta", min: 1);
            var layoverAirportId = SpectreUi.PromptIntRequiredCancelable("ID aeropuerto de escala", min: 1);
            var sequenceOrder = SpectreUi.PromptIntRequiredCancelable("Orden en la secuencia", min: 1);
            var layoverDurationMin = SpectreUi.PromptIntRequiredCancelable("Duración de escala (min)", min: 1);
            await _createUseCase.ExecuteAsync(
                RouteId.Create(routeId),
                LayoverAirportId.Create(layoverAirportId),
                SequenceOrder.Create(sequenceOrder),
                LayoverDurationMin.Create(layoverDurationMin)
            );
            SpectreUi.MarkupLineOrPlain("[green]Escala creada.[/]", "Escala creada.");
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

    private async Task GetRouteLayoverById()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar escala", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID escala", min: 1);
            var routeLayover = await _getByIdUseCase.ExecuteAsync(RouteLayoverId.Create(id));
            if (routeLayover != null)
            {
                SpectreUi.ShowTable(
                    "Escala de ruta",
                    ["Campo", "Valor"],
                    [
                        ["ID", routeLayover.Id.Value.ToString()],
                        ["RutaId", routeLayover.RouteId.Value.ToString()],
                        ["AeropuertoEscalaId", routeLayover.LayoverAirportId.Value.ToString()],
                        ["Orden", routeLayover.SequenceOrder.Value.ToString()],
                        ["Duración (min)", routeLayover.LayoverDurationMin.Value.ToString()],
                    ]
                );
            }
            else
            {
                Console.WriteLine("Escala no encontrada");
            }
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

    private async Task GetAllRouteLayovers()
    {
        try
        {
            var routeLayovers = await _getAllUseCase.ExecuteAsync();
            var list = routeLayovers.ToList();
            if (list.Count == 0)
            {
                Console.WriteLine("No hay escalas para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Escalas de ruta", "Listado");
            SpectreUi.ShowTable(
                "Escalas",
                ["ID", "Ruta", "Aeropuerto", "Orden", "Min"],
                list
                    .OrderBy(x => x.Id.Value)
                    .Select(x => (IReadOnlyList<string>)new[]
                    {
                        x.Id.Value.ToString(),
                        x.RouteId.Value.ToString(),
                        x.LayoverAirportId.Value.ToString(),
                        x.SequenceOrder.Value.ToString(),
                        x.LayoverDurationMin.Value.ToString()
                    })
                    .ToList()
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task UpdateRouteLayover()
    {
        try
        {
            SpectreUi.ModuleHeader("Actualizar escala", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID escala", min: 1);
            var existing = await _getByIdUseCase.ExecuteAsync(RouteLayoverId.Create(id));
            if (existing == null)
            {
                Console.WriteLine("Escala no encontrada");
                SpectreUi.Pause();
                return;
            }

            var routeId = SpectreUi.PromptIntRequiredCancelable("ID ruta", min: 1);
            var layoverAirportId = SpectreUi.PromptIntRequiredCancelable("ID aeropuerto de escala", min: 1);
            var sequenceOrder = SpectreUi.PromptIntRequiredCancelable("Orden en la secuencia", min: 1);
            var layoverDurationMin = SpectreUi.PromptIntRequiredCancelable("Duración de escala (min)", min: 1);
            await _updateUseCase.ExecuteAsync(
                RouteLayoverId.Create(id),
                RouteId.Create(routeId),
                LayoverAirportId.Create(layoverAirportId),
                SequenceOrder.Create(sequenceOrder),
                LayoverDurationMin.Create(layoverDurationMin)
            );
            SpectreUi.MarkupLineOrPlain("[green]Escala actualizada.[/]", "Escala actualizada.");
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

    private async Task DeleteRouteLayover()
    {
        try
        {
            SpectreUi.ModuleHeader("Eliminar escala", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID escala", min: 1);
            await _deleteUseCase.ExecuteAsync(RouteLayoverId.Create(id));
            SpectreUi.MarkupLineOrPlain("[green]Escala eliminada.[/]", "Escala eliminada.");
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
}