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
                ("Consultar por ID", () => GetRouteLayoverById().GetAwaiter().GetResult()),
                ("Listar todas", () => GetAllRouteLayovers().GetAwaiter().GetResult()),
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
            Console.Write("ID ruta: ");
            var routeId = int.Parse(Console.ReadLine()!);
            Console.Write("ID aeropuerto de escala: ");
            var layoverAirportId = int.Parse(Console.ReadLine()!);
            Console.Write("Orden en la secuencia: ");
            var sequenceOrder = int.Parse(Console.ReadLine()!);
            Console.Write("Duración de escala (min): ");
            var layoverDurationMin = int.Parse(Console.ReadLine()!);
            await _createUseCase.ExecuteAsync(
                RouteId.Create(routeId),
                LayoverAirportId.Create(layoverAirportId),
                SequenceOrder.Create(sequenceOrder),
                LayoverDurationMin.Create(layoverDurationMin)
            );
            Console.WriteLine("Escala creada");
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
            Console.Write("ID escala: ");
            var id = int.Parse(Console.ReadLine()!);
            var routeLayover = await _getByIdUseCase.ExecuteAsync(RouteLayoverId.Create(id));
            if (routeLayover != null)
            {
                Console.WriteLine($"ID: {routeLayover.Id.Value}, Ruta: {routeLayover.RouteId.Value}, Aeropuerto escala: {routeLayover.LayoverAirportId.Value}, Orden: {routeLayover.SequenceOrder.Value}, Duración min: {routeLayover.LayoverDurationMin.Value}");
            }
            else
            {
                Console.WriteLine("Escala no encontrada");
            }
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
            foreach (var rl in routeLayovers)
            {
                Console.WriteLine($"ID: {rl.Id.Value}, Ruta: {rl.RouteId.Value}, Aeropuerto escala: {rl.LayoverAirportId.Value}, Orden: {rl.SequenceOrder.Value}, Duración min: {rl.LayoverDurationMin.Value}");
            }
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
            Console.Write("ID escala: ");
            var id = int.Parse(Console.ReadLine()!);
            var existing = await _getByIdUseCase.ExecuteAsync(RouteLayoverId.Create(id));
            if (existing == null)
            {
                Console.WriteLine("Escala no encontrada");
                SpectreUi.Pause();
                return;
            }
            Console.Write("ID ruta: ");
            var routeId = int.Parse(Console.ReadLine()!);
            Console.Write("ID aeropuerto de escala: ");
            var layoverAirportId = int.Parse(Console.ReadLine()!);
            Console.Write("Orden en la secuencia: ");
            var sequenceOrder = int.Parse(Console.ReadLine()!);
            Console.Write("Duración de escala (min): ");
            var layoverDurationMin = int.Parse(Console.ReadLine()!);
            await _updateUseCase.ExecuteAsync(
                RouteLayoverId.Create(id),
                RouteId.Create(routeId),
                LayoverAirportId.Create(layoverAirportId),
                SequenceOrder.Create(sequenceOrder),
                LayoverDurationMin.Create(layoverDurationMin)
            );
            Console.WriteLine("Escala actualizada");
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
            Console.Write("ID escala: ");
            var id = int.Parse(Console.ReadLine()!);
            await _deleteUseCase.ExecuteAsync(RouteLayoverId.Create(id));
            Console.WriteLine("Escala eliminada");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }
}