using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;
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
        while (true)
        {
            Console.WriteLine("Gestión de estados de vuelo");
            Console.WriteLine("1. Crear estado de vuelo");
            Console.WriteLine("2. Consultar estado por ID");
            Console.WriteLine("3. Listar todos los estados");
            Console.WriteLine("4. Actualizar estado");
            Console.WriteLine("5. Eliminar estado");
            Console.WriteLine("0. Volver");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateFlightStatus();
                    break;
                case "2":
                    await GetFlightStatusById();
                    break;
                case "3":
                    await GetAllFlightStatuses();
                    break;
                case "4":
                    await UpdateFlightStatus();
                    break;
                case "5":
                    await DeleteFlightStatus();
                    break;
                case "0":
                    return;
            }
        }
    }

    private async Task CreateFlightStatus()
    {
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        await _createUseCase.ExecuteAsync(new CreateFlightStatusRequest(name));
        Console.WriteLine("Estado de vuelo creado");
    }

    private async Task GetFlightStatusById()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        var flightStatus = await _getByIdUseCase.ExecuteAsync(id);
        if (flightStatus != null)
        {
            Console.WriteLine($"ID: {flightStatus.Id.Value}, Nombre: {flightStatus.Name.Value}");
        }
        else
        {
            Console.WriteLine("Estado de vuelo no encontrado");
        }
    }

    private async Task GetAllFlightStatuses()
    {
        var statuses = await _getAllUseCase.ExecuteAsync();
        foreach (var s in statuses)
        {
            Console.WriteLine($"ID: {s.Id.Value}, Nombre: {s.Name.Value}");
        }
    }

    private async Task UpdateFlightStatus()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        await _updateUseCase.ExecuteAsync(new UpdateFlightStatusRequest(id, name));
        Console.WriteLine("Estado de vuelo actualizado");
    }

    private async Task DeleteFlightStatus()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        await _deleteUseCase.ExecuteAsync(id);
        Console.WriteLine("Estado de vuelo eliminado");
    }
}