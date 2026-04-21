using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.UI;

public sealed class FlightRoleConsoleUI : IModuleUI
{
    private readonly IFlightRoleService _service;

    public FlightRoleConsoleUI(IFlightRoleService service)
    {
        _service = service;
    }

    public async Task RunAsync()
    {
        bool exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Roles de vuelo", "Tripulación (comandante, copiloto, etc.)");

            var items = new (string Label, Action Action)[]
            {
                ("Crear rol", () => CreateAsync().GetAwaiter().GetResult()),
                ("Listar todos", () => ListAllAsync().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateAsync().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateAsync()
    {
        SpectreUi.ModuleHeader("Crear rol de vuelo", null);
        try
        {
            Console.Write("Nombre del rol: ");
            var name = Console.ReadLine() ?? "";
            var created = await _service.CreateAsync(new CreateFlightRoleRequest(name));
            Console.WriteLine($"Creado: ID {created.Id.Value} — {created.Name.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task ListAllAsync()
    {
        SpectreUi.ModuleHeader("Roles registrados", null);
        var all = await _service.GetAllAsync();
        foreach (var r in all)
        {
            Console.WriteLine($"ID: {r.Id.Value}, Nombre: {r.Name.Value}");
        }

        SpectreUi.Pause();
    }

    private async Task GetByIdAsync()
    {
        SpectreUi.ModuleHeader("Consultar rol", null);
        Console.Write("ID: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("ID no válido.");
            SpectreUi.Pause();
            return;
        }

        var found = await _service.GetByIdAsync(id);
        Console.WriteLine(found is not null
            ? $"ID: {found.Id.Value}, Nombre: {found.Name.Value}"
            : "No encontrado.");

        SpectreUi.Pause();
    }

    private async Task UpdateAsync()
    {
        SpectreUi.ModuleHeader("Actualizar rol", null);
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine()!);
            Console.Write("Nuevo nombre: ");
            var name = Console.ReadLine() ?? "";
            await _service.UpdateAsync(new UpdateFlightRoleRequest(id, name));
            Console.WriteLine("Actualizado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task DeleteAsync()
    {
        SpectreUi.ModuleHeader("Eliminar rol", null);
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine()!);
            await _service.DeleteAsync(id);
            Console.WriteLine("Eliminado (si existía).");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }
}
