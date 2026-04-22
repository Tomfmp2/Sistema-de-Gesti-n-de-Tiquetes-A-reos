using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.UI;

public class SeatLocationTypeConsoleUI : IModuleUI
{
    private readonly CreateSeatLocationTypeUseCase _createUseCase;
    private readonly GetSeatLocationTypeByIdUseCase _getByIdUseCase;
    private readonly GetAllSeatLocationTypesUseCase _getAllUseCase;
    private readonly UpdateSeatLocationTypeUseCase _updateUseCase;
    private readonly DeleteSeatLocationTypeUseCase _deleteUseCase;

    public SeatLocationTypeConsoleUI(CreateSeatLocationTypeUseCase createUseCase, GetSeatLocationTypeByIdUseCase getByIdUseCase, GetAllSeatLocationTypesUseCase getAllUseCase, UpdateSeatLocationTypeUseCase updateUseCase, DeleteSeatLocationTypeUseCase deleteUseCase)
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
            Console.WriteLine("Gestión de tipos de ubicación de asiento");
            Console.WriteLine("1. Crear tipo de ubicación");
            Console.WriteLine("2. Consultar tipo por ID");
            Console.WriteLine("3. Listar todos los tipos");
            Console.WriteLine("4. Actualizar tipo");
            Console.WriteLine("5. Eliminar tipo");
            Console.WriteLine("0. Volver");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateSeatLocationType();
                    break;
                case "2":
                    await GetSeatLocationTypeById();
                    break;
                case "3":
                    await GetAllSeatLocationTypes();
                    break;
                case "4":
                    await UpdateSeatLocationType();
                    break;
                case "5":
                    await DeleteSeatLocationType();
                    break;
                case "0":
                    return;
            }
        }
    }

    private async Task CreateSeatLocationType()
    {
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        await _createUseCase.ExecuteAsync(SeatLocationTypeName.Create(name));
        Console.WriteLine("Tipo de ubicación creado");
    }

    private async Task GetSeatLocationTypeById()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        var seatLocationType = await _getByIdUseCase.ExecuteAsync(SeatLocationTypeId.Create(id));
        if (seatLocationType != null)
        {
            Console.WriteLine($"ID: {seatLocationType.Id.Value}, Nombre: {seatLocationType.Name.Value}");
        }
        else
        {
            Console.WriteLine("Tipo de ubicación no encontrado");
        }
    }

    private async Task GetAllSeatLocationTypes()
    {
        var seatLocationTypes = await _getAllUseCase.ExecuteAsync();
        foreach (var s in seatLocationTypes)
        {
            Console.WriteLine($"ID: {s.Id.Value}, Name: {s.Name.Value}");
        }
    }

    private async Task UpdateSeatLocationType()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        await _updateUseCase.ExecuteAsync(SeatLocationTypeId.Create(id), SeatLocationTypeName.Create(name));
        Console.WriteLine("Tipo de ubicación actualizado");
    }

    private async Task DeleteSeatLocationType()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        await _deleteUseCase.ExecuteAsync(SeatLocationTypeId.Create(id));
        Console.WriteLine("Tipo de ubicación eliminado");
    }
}