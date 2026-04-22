using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
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
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Temporadas", "Gestión de temporadas");

            var items = new (string Label, Action Action)[]
            {
                ("Crear temporada", () => CreateSeason().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetSeasonById().GetAwaiter().GetResult()),
                ("Listar todas", () => GetAllSeasons().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateSeason().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteSeason().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateSeason()
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task GetSeasonById()
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task GetAllSeasons()
    {
        try
        {
            var seasons = await _getAllUseCase.ExecuteAsync();
            foreach (var s in seasons)
            {
                Console.WriteLine($"ID: {s.Id.Value}, Nombre: {s.Name.Value}, Descripción: {s.Description.Value}, Factor precio: {s.PriceFactor.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task UpdateSeason()
    {
        try
        {
            Console.Write("ID temporada: ");
            var id = int.Parse(Console.ReadLine()!);
            var existing = await _getByIdUseCase.ExecuteAsync(SeasonId.Create(id));
            if (existing == null)
            {
                Console.WriteLine("Temporada no encontrada");
                SpectreUi.Pause();
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
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task DeleteSeason()
    {
        try
        {
            Console.Write("ID temporada: ");
            var id = int.Parse(Console.ReadLine()!);
            await _deleteUseCase.ExecuteAsync(SeasonId.Create(id));
            Console.WriteLine("Temporada eliminada");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }
}