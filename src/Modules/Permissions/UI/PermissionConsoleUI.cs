using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.UI;

public sealed class PermissionConsoleUI : IModuleUI
{
    private readonly CreatePermissionUseCase _create;
    private readonly GetPermissionByIdUseCase _getById;
    private readonly GetAllPermissionsUseCase _getAll;
    private readonly UpdatePermissionUseCase _update;
    private readonly DeletePermissionUseCase _delete;

    public PermissionConsoleUI(
        CreatePermissionUseCase create,
        GetPermissionByIdUseCase getById,
        GetAllPermissionsUseCase getAll,
        UpdatePermissionUseCase update,
        DeletePermissionUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Permisos", "CRUD de permisos");
            var items = new (string Label, Action Action)[]
            {
                ("Crear permiso", () => Create().GetAwaiter().GetResult()),
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
            SpectreUi.ModuleHeader("Crear permiso", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );
            var name = SpectreUi.PromptRequiredCancelable("Name", "p.ej. flights.manage");
            var desc = SpectreUi.PromptOptionalCancelable("Descripción", "opcional");
            var created = await _create.ExecuteAsync(new CreatePermissionRequest(name, desc));
            SpectreUi.MarkupLineOrPlain(
                $"[green]Permiso creado[/] id={created.Id.Value} · [bold]{created.Name.Value}[/]",
                $"Permiso creado id={created.Id.Value} · {created.Name.Value}"
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

    private async Task ListAll()
    {
        try
        {
            var list = (await _getAll.ExecuteAsync()).ToList();
            if (list.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No hay permisos para mostrar.[/]", "No hay permisos para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Permisos", "Listado");
            SpectreUi.ShowTable(
                "Permisos",
                ["ID", "Name", "Descripción"],
                list.OrderBy(x => x.Id.Value).Select(x => (IReadOnlyList<string>)new[]
                {
                    x.Id.Value.ToString(),
                    x.Name.Value,
                    x.Description ?? "-"
                }).ToList()
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
            SpectreUi.ModuleHeader("Consultar permiso", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var x = await _getById.ExecuteAsync(id);
            if (x is null)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No encontrado.[/]", "No encontrado.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ShowTable(
                "Permiso",
                ["Campo", "Valor"],
                [
                    ["ID", x.Id.Value.ToString()],
                    ["Name", x.Name.Value],
                    ["Descripción", x.Description ?? "-"],
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
            SpectreUi.ModuleHeader("Actualizar permiso", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var name = SpectreUi.PromptRequiredCancelable("Name", "p.ej. flights.manage");
            var desc = SpectreUi.PromptOptionalCancelable("Descripción", "opcional");
            await _update.ExecuteAsync(new UpdatePermissionRequest(id, name, desc));
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
            SpectreUi.ModuleHeader("Eliminar permiso", null);
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
}

