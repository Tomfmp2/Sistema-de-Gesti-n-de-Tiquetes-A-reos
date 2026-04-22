using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
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
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Tipos de ubicación de asiento", "Ventana / Pasillo / Centro…");

            var items = new (string Label, Action Action)[]
            {
                ("Crear", () => CreateSeatLocationType().GetAwaiter().GetResult()),
                ("Listar", () => GetAllSeatLocationTypes().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetSeatLocationTypeById().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateSeatLocationType().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteSeatLocationType().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateSeatLocationType()
    {
        try
        {
            SpectreUi.ModuleHeader("Crear tipo", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var name = SpectreUi.PromptRequiredCancelable("Nombre");
            await _createUseCase.ExecuteAsync(SeatLocationTypeName.Create(name));
            SpectreUi.MarkupLineOrPlain("[green]Creado.[/]", "Creado.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }

    private async Task GetSeatLocationTypeById()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar tipo", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var seatLocationType = await _getByIdUseCase.ExecuteAsync(SeatLocationTypeId.Create(id));
            if (seatLocationType is null)
            {
                Console.WriteLine("No encontrado.");
            }
            else
            {
                SpectreUi.ShowTable(
                    "Tipo de ubicación",
                    ["Campo", "Valor"],
                    [
                        ["ID", seatLocationType.Id.Value.ToString()],
                        ["Nombre", seatLocationType.Name.Value],
                    ]
                );
            }
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }

    private async Task GetAllSeatLocationTypes()
    {
        try
        {
            var seatLocationTypes = (await _getAllUseCase.ExecuteAsync()).ToList();
            if (seatLocationTypes.Count == 0)
            {
                Console.WriteLine("No hay tipos para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Tipos de ubicación de asiento", "Listado");
            SpectreUi.ShowTable(
                "Tipos",
                ["ID", "Nombre"],
                seatLocationTypes
                    .OrderBy(x => x.Id.Value)
                    .Select(x => (IReadOnlyList<string>)new[]
                    {
                        x.Id.Value.ToString(),
                        x.Name.Value
                    })
                    .ToList()
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }

    private async Task UpdateSeatLocationType()
    {
        try
        {
            SpectreUi.ModuleHeader("Actualizar tipo", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var name = SpectreUi.PromptRequiredCancelable("Nombre");
            await _updateUseCase.ExecuteAsync(
                SeatLocationTypeId.Create(id),
                SeatLocationTypeName.Create(name)
            );
            SpectreUi.MarkupLineOrPlain("[green]Actualizado.[/]", "Actualizado.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }

    private async Task DeleteSeatLocationType()
    {
        try
        {
            SpectreUi.ModuleHeader("Eliminar tipo", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            await _deleteUseCase.ExecuteAsync(SeatLocationTypeId.Create(id));
            SpectreUi.MarkupLineOrPlain("[green]Eliminado.[/]", "Eliminado.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }

        SpectreUi.Pause();
    }
}