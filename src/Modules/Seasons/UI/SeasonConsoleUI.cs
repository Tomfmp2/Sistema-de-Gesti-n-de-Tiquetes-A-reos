using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.UI;

public class SeasonConsoleUI : IModuleUI
{
    private readonly CreateSeasonUseCase _createUseCase;
    private readonly GetSeasonByIdUseCase _getByIdUseCase;
    private readonly GetAllSeasonsUseCase _getAllUseCase;
    private readonly UpdateSeasonUseCase _updateUseCase;
    private readonly DeleteSeasonUseCase _deleteUseCase;

    public SeasonConsoleUI(CreateSeasonUseCase createUseCase, GetSeasonByIdUseCase getByIdUseCase, GetAllSeasonsUseCase getAllUseCase, UpdateSeasonUseCase updateUseCase, DeleteSeasonUseCase deleteUseCase)
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
            Console.WriteLine("Gestión de temporadas");
            Console.WriteLine("1. Crear temporada");
            Console.WriteLine("2. Consultar temporada por ID");
            Console.WriteLine("3. Listar todas las temporadas");
            Console.WriteLine("4. Actualizar temporada");
            Console.WriteLine("5. Eliminar temporada");
            Console.WriteLine("0. Volver");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateSeason();
                    break;
                case "2":
                    await GetSeasonById();
                    break;
                case "3":
                    await GetAllSeasons();
                    break;
                case "4":
                    await UpdateSeason();
                    break;
                case "5":
                    await DeleteSeason();
                    break;
                case "0":
                    return;
            }
        }
    }

    private async Task CreateSeason()
    {
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        Console.Write("Descripción (opcional): ");
        var description = Console.ReadLine();
        Console.Write("Factor de precio: ");
        var priceFactor = decimal.Parse(Console.ReadLine()!);
        await _createUseCase.ExecuteAsync(
            SeasonName.Create(name),
            SeasonDescription.Create(description),
            PriceFactor.Create(priceFactor)
        );
        Console.WriteLine("Temporada creada");
    }

    private async Task GetSeasonById()
    {
        Console.Write("ID temporada: ");
        var id = int.Parse(Console.ReadLine()!);
        var season = await _getByIdUseCase.ExecuteAsync(SeasonId.Create(id));
        if (season != null)
        {
            Console.WriteLine($"ID: {season.Id.Value}, Nombre: {season.Name.Value}, Descripción: {season.Description.Value}, Factor precio: {season.PriceFactor.Value}");
        }
        else
        {
            Console.WriteLine("Temporada no encontrada");
        }
    }

    private async Task GetAllSeasons()
    {
        var seasons = await _getAllUseCase.ExecuteAsync();
        foreach (var s in seasons)
        {
            Console.WriteLine($"ID: {s.Id.Value}, Nombre: {s.Name.Value}, Descripción: {s.Description.Value}, Factor precio: {s.PriceFactor.Value}");
        }
    }

    private async Task UpdateSeason()
    {
        Console.Write("ID temporada: ");
        var id = int.Parse(Console.ReadLine()!);
        var existing = await _getByIdUseCase.ExecuteAsync(SeasonId.Create(id));
        if (existing == null)
        {
            Console.WriteLine("Temporada no encontrada");
            return;
        }
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        Console.Write("Descripción (opcional): ");
        var description = Console.ReadLine();
        Console.Write("Factor de precio: ");
        var priceFactor = decimal.Parse(Console.ReadLine()!);
        await _updateUseCase.ExecuteAsync(
            SeasonId.Create(id),
            SeasonName.Create(name),
            SeasonDescription.Create(description),
            PriceFactor.Create(priceFactor)
        );
        Console.WriteLine("Temporada actualizada");
    }

    private async Task DeleteSeason()
    {
        Console.Write("ID temporada: ");
        var id = int.Parse(Console.ReadLine()!);
        await _deleteUseCase.ExecuteAsync(SeasonId.Create(id));
        Console.WriteLine("Temporada eliminada");
    }
}