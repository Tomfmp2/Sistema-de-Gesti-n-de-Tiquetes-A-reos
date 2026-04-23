using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.UI;

public sealed class SystemRoleConsoleUI : IModuleUI
{
    private readonly CreateSystemRoleUseCase _create;
    private readonly GetSystemRoleByIdUseCase _getById;
    private readonly GetAllSystemRolesUseCase _getAll;
    private readonly UpdateSystemRoleUseCase _update;
    private readonly DeleteSystemRoleUseCase _delete;

    public SystemRoleConsoleUI(
        CreateSystemRoleUseCase create,
        GetSystemRoleByIdUseCase getById,
        GetAllSystemRolesUseCase getAll,
        UpdateSystemRoleUseCase update,
        DeleteSystemRoleUseCase delete
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
            SpectreUi.ModuleHeader("Roles del sistema", "CRUD de roles");
            var items = new (string Label, Action Action)[]
            {
                ("Crear rol", () => Create().GetAwaiter().GetResult()),
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
            SpectreUi.ModuleHeader("Crear rol", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );
            var name = SpectreUi.PromptRequiredCancelable("Nombre", "p.ej. Administrador / Cliente / Checkin");
            var desc = SpectreUi.PromptOptionalCancelable("Descripción", "opcional");
            var created = await _create.ExecuteAsync(new CreateSystemRoleRequest(name, desc));
            SpectreUi.MarkupLineOrPlain(
                $"[green]Rol creado[/] id={created.Id.Value} · [bold]{created.Name.Value}[/]",
                $"Rol creado id={created.Id.Value} · {created.Name.Value}"
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
                SpectreUi.MarkupLineOrPlain("[grey]No hay roles para mostrar.[/]", "No hay roles para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Roles del sistema", "Listado");
            SpectreUi.ShowTable(
                "Roles",
                ["ID", "Nombre", "Descripción"],
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
            SpectreUi.ModuleHeader("Consultar rol", null);
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
                "Rol",
                ["Campo", "Valor"],
                [
                    ["ID", x.Id.Value.ToString()],
                    ["Nombre", x.Name.Value],
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
            SpectreUi.ModuleHeader("Actualizar rol", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var name = SpectreUi.PromptRequiredCancelable("Nombre");
            var desc = SpectreUi.PromptOptionalCancelable("Descripción", "opcional");
            await _update.ExecuteAsync(new UpdateSystemRoleRequest(id, name, desc));
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
            SpectreUi.ModuleHeader("Eliminar rol", null);
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

