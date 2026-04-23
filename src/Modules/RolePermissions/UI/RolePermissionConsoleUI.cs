using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.UI;

public sealed class RolePermissionConsoleUI : IModuleUI
{
    private readonly CreateRolePermissionUseCase _create;
    private readonly GetRolePermissionByIdUseCase _getById;
    private readonly GetAllRolePermissionsUseCase _getAll;
    private readonly UpdateRolePermissionUseCase _update;
    private readonly DeleteRolePermissionUseCase _delete;

    public RolePermissionConsoleUI(
        CreateRolePermissionUseCase create,
        GetRolePermissionByIdUseCase getById,
        GetAllRolePermissionsUseCase getAll,
        UpdateRolePermissionUseCase update,
        DeleteRolePermissionUseCase delete
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
            SpectreUi.ModuleHeader("Rol ↔ Permiso", "Asignación de permisos a roles");
            var items = new (string Label, Action Action)[]
            {
                ("Asignar permiso a rol", () => Create().GetAwaiter().GetResult()),
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
            SpectreUi.ModuleHeader("Asignar permiso a rol", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );
            var roleId = SpectreUi.PromptIntRequiredCancelable("RoleId", min: 1);
            var permissionId = SpectreUi.PromptIntRequiredCancelable("PermissionId", min: 1);
            var created = await _create.ExecuteAsync(new CreateRolePermissionRequest(roleId, permissionId));
            SpectreUi.MarkupLineOrPlain(
                $"[green]Asignación creada[/] id={created.Id.Value} role={created.RoleId.Value} perm={created.PermissionId.Value}",
                $"Asignación creada id={created.Id.Value} role={created.RoleId.Value} perm={created.PermissionId.Value}"
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
                SpectreUi.MarkupLineOrPlain("[grey]No hay asignaciones para mostrar.[/]", "No hay asignaciones para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Rol ↔ Permiso", "Listado");
            SpectreUi.ShowTable(
                "Asignaciones",
                ["ID", "RoleId", "PermissionId"],
                list.OrderBy(x => x.Id.Value).Select(x => (IReadOnlyList<string>)new[]
                {
                    x.Id.Value.ToString(),
                    x.RoleId.Value.ToString(),
                    x.PermissionId.Value.ToString()
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
            SpectreUi.ModuleHeader("Consultar asignación", null);
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
                "Asignación",
                ["Campo", "Valor"],
                [
                    ["ID", x.Id.Value.ToString()],
                    ["RoleId", x.RoleId.Value.ToString()],
                    ["PermissionId", x.PermissionId.Value.ToString()],
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
            SpectreUi.ModuleHeader("Actualizar asignación", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var roleId = SpectreUi.PromptIntRequiredCancelable("RoleId", min: 1);
            var permissionId = SpectreUi.PromptIntRequiredCancelable("PermissionId", min: 1);
            await _update.ExecuteAsync(new UpdateRolePermissionRequest(id, roleId, permissionId));
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
            SpectreUi.ModuleHeader("Eliminar asignación", null);
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

