using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Interfaces;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.UI;

public sealed class FlightRoleConsoleUI
{
    private readonly IFlightRoleService _service;

    public FlightRoleConsoleUI(IFlightRoleService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        var created = await _service.CreateAsync(new CreateFlightRoleRequest("Pilot"), cancellationToken);
        Console.WriteLine($"Created flight role {created.Id.Value}: {created.Name.Value}");

        var all = await _service.GetAllAsync(cancellationToken);
        Console.WriteLine($"Total flight roles: {all.Count}");

        var found = await _service.GetByIdAsync(created.Id.Value, cancellationToken);
        Console.WriteLine(found is not null ? $"Found {found.Id.Value}: {found.Name.Value}" : "Flight role not found.");
    }
}
