using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.UI;

public sealed class FlightAssignmentConsoleUI
{
    private readonly CreateFlightAssignmentUseCase _createUseCase;
    private readonly GetFlightAssignmentByIdUseCase _getByIdUseCase;
    private readonly GetAllFlightAssignmentsUseCase _getAllUseCase;
    private readonly UpdateFlightAssignmentUseCase _updateUseCase;
    private readonly DeleteFlightAssignmentUseCase _deleteUseCase;

    public FlightAssignmentConsoleUI(
        CreateFlightAssignmentUseCase createUseCase,
        GetFlightAssignmentByIdUseCase getByIdUseCase,
        GetAllFlightAssignmentsUseCase getAllUseCase,
        UpdateFlightAssignmentUseCase updateUseCase,
        DeleteFlightAssignmentUseCase deleteUseCase)
    {
        _createUseCase = createUseCase ?? throw new ArgumentNullException(nameof(createUseCase));
        _getByIdUseCase = getByIdUseCase ?? throw new ArgumentNullException(nameof(getByIdUseCase));
        _getAllUseCase = getAllUseCase ?? throw new ArgumentNullException(nameof(getAllUseCase));
        _updateUseCase = updateUseCase ?? throw new ArgumentNullException(nameof(updateUseCase));
        _deleteUseCase = deleteUseCase ?? throw new ArgumentNullException(nameof(deleteUseCase));
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Asignaciones de tripulación", "Vuelo ↔ Personal ↔ Rol");

            var items = new (string Label, Action Action)[]
            {
                ("Crear", () => CreateFlightAssignmentAsync().GetAwaiter().GetResult()),
                ("Listar", () => GetAllFlightAssignmentsAsync().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetFlightAssignmentByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateFlightAssignmentAsync().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteFlightAssignmentAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateFlightAssignmentAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Crear asignación", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var flightId = SpectreUi.PromptIntRequiredCancelable("ID vuelo", min: 1);
            var staffId = SpectreUi.PromptIntRequiredCancelable("ID personal", min: 1);
            var flightRoleId = SpectreUi.PromptIntRequiredCancelable("ID rol de vuelo", min: 1);

            var created = await _createUseCase.ExecuteAsync(flightId, staffId, flightRoleId);
            SpectreUi.MarkupLineOrPlain(
                $"[green]Asignación creada[/] id={created.Id.Value}.",
                $"Asignación creada id={created.Id.Value}."
            );
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

    private async Task GetFlightAssignmentByIdAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar asignación", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID asignación", min: 1);
            var found = await _getByIdUseCase.ExecuteAsync(id);
            if (found is null)
            {
                Console.WriteLine("No encontrado.");
            }
            else
            {
                SpectreUi.ShowTable(
                    "Asignación",
                    ["Campo", "Valor"],
                    [
                        ["ID", found.Id.Value.ToString()],
                        ["VueloId", found.FlightId.Value.ToString()],
                        ["PersonalId", found.StaffId.Value.ToString()],
                        ["RolVueloId", found.FlightRoleId.Value.ToString()],
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

    private async Task GetAllFlightAssignmentsAsync()
    {
        try
        {
            var flightAssignments = await _getAllUseCase.ExecuteAsync();
            
            var list = flightAssignments.ToList();
            if (!list.Any())
            {
                Console.WriteLine("No hay asignaciones registradas.");
                SpectreUi.Pause();
                return;
            }
            SpectreUi.ModuleHeader("Asignaciones de tripulación", "Listado");
            SpectreUi.ShowTable(
                "Asignaciones",
                ["ID", "Vuelo", "Personal", "Rol"],
                list
                    .OrderBy(x => x.Id.Value)
                    .Select(x => (IReadOnlyList<string>)new[]
                    {
                        x.Id.Value.ToString(),
                        x.FlightId.Value.ToString(),
                        x.StaffId.Value.ToString(),
                        x.FlightRoleId.Value.ToString()
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

    private async Task UpdateFlightAssignmentAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Actualizar asignación", "Deja vacío para mantener");
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID asignación", min: 1);

            int? newFlightId = null;
            var flightInput = SpectreUi.PromptOptionalCancelable("Nuevo ID vuelo", "Enter = mantener");
            if (!string.IsNullOrWhiteSpace(flightInput))
                newFlightId = int.Parse(flightInput);

            int? newStaffId = null;
            var staffInput = SpectreUi.PromptOptionalCancelable("Nuevo ID personal", "Enter = mantener");
            if (!string.IsNullOrWhiteSpace(staffInput))
                newStaffId = int.Parse(staffInput);

            int? newFlightRoleId = null;
            var roleInput = SpectreUi.PromptOptionalCancelable("Nuevo ID rol de vuelo", "Enter = mantener");
            if (!string.IsNullOrWhiteSpace(roleInput))
                newFlightRoleId = int.Parse(roleInput);

            var updated = await _updateUseCase.ExecuteAsync(id, newFlightId, newStaffId, newFlightRoleId);
            if (updated is null)
            {
                Console.WriteLine("No encontrado.");
            }
            else
            {
                SpectreUi.MarkupLineOrPlain("[green]Actualizado.[/]", "Actualizado.");
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

    private async Task DeleteFlightAssignmentAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Eliminar asignación", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID asignación", min: 1);
            var deleted = await _deleteUseCase.ExecuteAsync(id);
            if (!deleted)
            {
                Console.WriteLine("No encontrado.");
            }
            else
            {
                SpectreUi.MarkupLineOrPlain("[green]Eliminado.[/]", "Eliminado.");
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
}
