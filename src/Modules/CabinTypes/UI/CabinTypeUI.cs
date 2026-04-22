using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.UI;

public class CabinTypeUI : IModuleUI
{
    private readonly CreateCabinTypeUseCase _createUseCase;
    private readonly GetAllCabinTypesUseCase _getAllUseCase;
    private readonly GetCabinTypeByIdUseCase _getByIdUseCase;
    private readonly UpdateCabinTypeUseCase _updateUseCase;
    private readonly DeleteCabinTypeUseCase _deleteUseCase;

    public CabinTypeUI(AppDbContext context)
    {
        var repository = new CabinTypeRepository(context);
        _createUseCase = new CreateCabinTypeUseCase(repository);
        _getAllUseCase = new GetAllCabinTypesUseCase(repository);
        _getByIdUseCase = new GetCabinTypeByIdUseCase(repository);
        _updateUseCase = new UpdateCabinTypeUseCase(repository);
        _deleteUseCase = new DeleteCabinTypeUseCase(repository);
    }

    public async Task RunAsync()
    {
        bool exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Tipos de cabina", "Clases Económica, Business, etc.");

            var items = new (string Label, Action Action)[]
            {
                ("Crear", () => CreateCabinTypeAsync().GetAwaiter().GetResult()),
                ("Listar", () => ViewAllCabinTypesAsync().GetAwaiter().GetResult()),
                ("Consultar por ID", () => ViewCabinTypeByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateCabinTypeAsync().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteCabinTypeAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateCabinTypeAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Tipos de cabina", "Crear");
            var name = SpectreUi.PromptRequiredCancelable("Nombre", "0/c/cancelar para salir");

            var cabinType = await _createUseCase.ExecuteAsync(name);
            SpectreUi.MarkupLineOrPlain(
                $"[green]Creado[/] id={cabinType.Id.Value}.",
                $"Creado id={cabinType.Id.Value}."
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

    private async Task ViewAllCabinTypesAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Tipos de cabina", "Listar");
            var cabinTypes = (await _getAllUseCase.ExecuteAsync()).ToList();
            if (cabinTypes.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No hay tipos de cabina.[/]", "No hay tipos de cabina.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ShowTable(
                "Tipos de cabina",
                ["ID", "Nombre"],
                cabinTypes
                    .OrderBy(x => x.Id.Value)
                    .Select(ct => (IReadOnlyList<string>)
                    [
                        ct.Id.Value.ToString(),
                        ct.Name.Value
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

    private async Task ViewCabinTypeByIdAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Tipos de cabina", "Consultar por ID");
            var id = SpectreUi.PromptIntRequiredCancelable("ID", "0/c/cancelar para salir", min: 1);

            var cabinType = await _getByIdUseCase.ExecuteAsync(CabinTypeId.Create(id));
            if (cabinType != null)
            {
                SpectreUi.ShowTable(
                    "Tipo de cabina",
                    ["Campo", "Valor"],
                    [
                        ["ID", cabinType.Id.Value.ToString()],
                        ["Nombre", cabinType.Name.Value]
                    ]
                );
            }
            else
            {
                SpectreUi.MarkupLineOrPlain("[grey]No encontrado.[/]", "No encontrado.");
            }
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

    private async Task UpdateCabinTypeAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Tipos de cabina", "Actualizar");
            var id = SpectreUi.PromptIntRequiredCancelable("ID", "0/c/cancelar para salir", min: 1);
            var name = SpectreUi.PromptRequiredCancelable("Nuevo nombre", "0/c/cancelar para salir");

            await _updateUseCase.ExecuteAsync(CabinTypeId.Create(id), name);
            SpectreUi.MarkupLineOrPlain("[green]Actualizado correctamente.[/]", "Actualizado correctamente.");
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

    private async Task DeleteCabinTypeAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Tipos de cabina", "Eliminar");
            var id = SpectreUi.PromptIntRequiredCancelable("ID", "0/c/cancelar para salir", min: 1);
            var confirm = SpectreUi.PromptBool("¿Confirma la eliminación?", defaultValue: false);
            if (!confirm)
            {
                SpectreUi.MarkupLineOrPlain("[grey]Eliminación cancelada.[/]", "Eliminación cancelada.");
                SpectreUi.Pause();
                return;
            }

            await _deleteUseCase.ExecuteAsync(CabinTypeId.Create(id));
            SpectreUi.MarkupLineOrPlain("[green]Eliminado.[/]", "Eliminado.");
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
}
