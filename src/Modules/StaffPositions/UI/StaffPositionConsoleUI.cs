using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.UI;

public class StaffPositionConsoleUI : IModuleUI
{
    private readonly CreateStaffPositionUseCase _createUseCase;
    private readonly GetStaffPositionByIdUseCase _getByIdUseCase;
    private readonly GetAllStaffPositionsUseCase _getAllUseCase;
    private readonly UpdateStaffPositionUseCase _updateUseCase;
    private readonly DeleteStaffPositionUseCase _deleteUseCase;

    public StaffPositionConsoleUI(
        CreateStaffPositionUseCase createUseCase,
        GetStaffPositionByIdUseCase getByIdUseCase,
        GetAllStaffPositionsUseCase getAllUseCase,
        UpdateStaffPositionUseCase updateUseCase,
        DeleteStaffPositionUseCase deleteUseCase)
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
            SpectreUi.ModuleHeader("Cargos", "Posiciones del personal");

            var items = new (string Label, Action Action)[]
            {
                ("Crear", () => CreateStaffPosition().GetAwaiter().GetResult()),
                ("Listar", () => GetAllStaffPositions().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetStaffPositionById().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateStaffPosition().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteStaffPosition().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateStaffPosition()
    {
        try
        {
            SpectreUi.ModuleHeader("Crear cargo", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var name = SpectreUi.PromptRequiredCancelable("Nombre");
            var staffPositionName = StaffPositionName.Create(name);
            await _createUseCase.ExecuteAsync(staffPositionName);
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

    private async Task GetStaffPositionById()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar cargo", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var staffPositionId = StaffPositionId.Create(id);
            var staffPosition = await _getByIdUseCase.ExecuteAsync(staffPositionId);
            if (staffPosition is null)
            {
                Console.WriteLine("No encontrado.");
            }
            else
            {
                SpectreUi.ShowTable(
                    "Cargo",
                    ["Campo", "Valor"],
                    [
                        ["ID", staffPosition.Id.Value.ToString()],
                        ["Nombre", staffPosition.Name.Value],
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

    private async Task GetAllStaffPositions()
    {
        try
        {
            var staffPositions = (await _getAllUseCase.ExecuteAsync()).ToList();
            if (staffPositions.Count == 0)
            {
                Console.WriteLine("No hay cargos para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Cargos", "Listado");
            SpectreUi.ShowTable(
                "Cargos",
                ["ID", "Nombre"],
                staffPositions
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

    private async Task UpdateStaffPosition()
    {
        try
        {
            SpectreUi.ModuleHeader("Actualizar cargo", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var name = SpectreUi.PromptRequiredCancelable("Nuevo nombre");
            var staffPositionId = StaffPositionId.Create(id);
            var staffPositionName = StaffPositionName.Create(name);
            await _updateUseCase.ExecuteAsync(staffPositionId, staffPositionName);
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

    private async Task DeleteStaffPosition()
    {
        try
        {
            SpectreUi.ModuleHeader("Eliminar cargo", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var staffPositionId = StaffPositionId.Create(id);
            await _deleteUseCase.ExecuteAsync(staffPositionId);
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