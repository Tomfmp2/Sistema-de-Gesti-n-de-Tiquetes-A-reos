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
            Console.WriteLine("Season Management");
            Console.WriteLine("1. Create Season");
            Console.WriteLine("2. Get Season by ID");
            Console.WriteLine("3. Get All Seasons");
            Console.WriteLine("4. Update Season");
            Console.WriteLine("5. Delete Season");
            Console.WriteLine("0. Back");
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
        Console.Write("Name: ");
        var name = Console.ReadLine();
        Console.Write("Description (optional): ");
        var description = Console.ReadLine();
        Console.Write("Price Factor: ");
        var priceFactor = decimal.Parse(Console.ReadLine());
        await _createUseCase.ExecuteAsync(
            SeasonName.Create(name),
            SeasonDescription.Create(description),
            PriceFactor.Create(priceFactor)
        );
        Console.WriteLine("Season created");
    }

    private async Task GetSeasonById()
    {
        Console.Write("Season ID: ");
        var id = int.Parse(Console.ReadLine());
        var season = await _getByIdUseCase.ExecuteAsync(SeasonId.Create(id));
        if (season != null)
        {
            Console.WriteLine($"ID: {season.Id.Value}, Name: {season.Name.Value}, Description: {season.Description.Value}, Price Factor: {season.PriceFactor.Value}");
        }
        else
        {
            Console.WriteLine("Season not found");
        }
    }

    private async Task GetAllSeasons()
    {
        var seasons = await _getAllUseCase.ExecuteAsync();
        foreach (var s in seasons)
        {
            Console.WriteLine($"ID: {s.Id.Value}, Name: {s.Name.Value}, Description: {s.Description.Value}, Price Factor: {s.PriceFactor.Value}");
        }
    }

    private async Task UpdateSeason()
    {
        Console.Write("Season ID: ");
        var id = int.Parse(Console.ReadLine());
        var existing = await _getByIdUseCase.ExecuteAsync(SeasonId.Create(id));
        if (existing == null)
        {
            Console.WriteLine("Season not found");
            return;
        }
        Console.Write("Name: ");
        var name = Console.ReadLine();
        Console.Write("Description (optional): ");
        var description = Console.ReadLine();
        Console.Write("Price Factor: ");
        var priceFactor = decimal.Parse(Console.ReadLine());
        await _updateUseCase.ExecuteAsync(
            SeasonId.Create(id),
            SeasonName.Create(name),
            SeasonDescription.Create(description),
            PriceFactor.Create(priceFactor)
        );
        Console.WriteLine("Season updated");
    }

    private async Task DeleteSeason()
    {
        Console.Write("Season ID: ");
        var id = int.Parse(Console.ReadLine());
        await _deleteUseCase.ExecuteAsync(SeasonId.Create(id));
        Console.WriteLine("Season deleted");
    }
}