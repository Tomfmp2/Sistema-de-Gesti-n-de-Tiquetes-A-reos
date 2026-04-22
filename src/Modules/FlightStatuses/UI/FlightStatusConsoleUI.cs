using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.UI;

public class FlightStatusConsoleUI : IModuleUI
{
    private readonly CreateFlightStatusUseCase _createUseCase;
    private readonly GetFlightStatusByIdUseCase _getByIdUseCase;
    private readonly GetAllFlightStatusesUseCase _getAllUseCase;
    private readonly UpdateFlightStatusUseCase _updateUseCase;
    private readonly DeleteFlightStatusUseCase _deleteUseCase;

    public FlightStatusConsoleUI(CreateFlightStatusUseCase createUseCase, GetFlightStatusByIdUseCase getByIdUseCase, GetAllFlightStatusesUseCase getAllUseCase, UpdateFlightStatusUseCase updateUseCase, DeleteFlightStatusUseCase deleteUseCase)
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
            SpectreUi.ModuleHeader("Estados de vuelo", "Programado / En vuelo / Retrasado…");

            var items = new (string Label, Action Action)[]
            {
                ("Crear", () => CreateFlightStatus().GetAwaiter().GetResult()),
                ("Listar", () => GetAllFlightStatuses().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetFlightStatusById().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateFlightStatus().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteFlightStatus().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateFlightStatus()
    {
        try
        {
            SpectreUi.ModuleHeader("Crear estado", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );
            var name = SpectreUi.PromptRequiredCancelable("Nombre");
            await _createUseCase.ExecuteAsync(new CreateFlightStatusRequest(name));
            SpectreUi.MarkupLineOrPlain("[green]Creado.[/]", "Creado.");
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

    private async Task GetFlightStatusById()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar estado", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var flightStatus = await _getByIdUseCase.ExecuteAsync(id);
            if (flightStatus is null)
            {
                Console.WriteLine("No encontrado.");
            }
            else
            {
                SpectreUi.ShowTable(
                    "Estado de vuelo",
                    ["Campo", "Valor"],
                    [
                        ["ID", flightStatus.Id.Value.ToString()],
                        ["Nombre", flightStatus.Name.Value],
                    ]
                );
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

    private async Task GetAllFlightStatuses()
    {
        try
        {
            var statuses = (await _getAllUseCase.ExecuteAsync()).ToList();
            if (statuses.Count == 0)
            {
                Console.WriteLine("No hay estados para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Estados de vuelo", "Listado");
            SpectreUi.ShowTable(
                "Estados",
                ["ID", "Nombre"],
                statuses
                    .OrderBy(x => x.Id.Value)
                    .Select(x => (IReadOnlyList<string>)new[]
                    {
                        x.Id.Value.ToString(),
                        x.Name.Value
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

    private async Task UpdateFlightStatus()
    {
        try
        {
            SpectreUi.ModuleHeader("Actualizar estado", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var name = SpectreUi.PromptRequiredCancelable("Nombre");
            await _updateUseCase.ExecuteAsync(new UpdateFlightStatusRequest(id, name));
            SpectreUi.MarkupLineOrPlain("[green]Actualizado.[/]", "Actualizado.");
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

    private async Task DeleteFlightStatus()
    {
        try
        {
            SpectreUi.ModuleHeader("Eliminar estado", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            await _deleteUseCase.ExecuteAsync(id);
            SpectreUi.MarkupLineOrPlain("[green]Eliminado.[/]", "Eliminado.");
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