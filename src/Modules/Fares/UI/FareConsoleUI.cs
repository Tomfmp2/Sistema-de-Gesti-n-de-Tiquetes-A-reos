using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.UI;

public sealed class FareConsoleUI : IModuleUI
{
    private readonly CreateFareUseCase _create;
    private readonly GetFareByIdUseCase _getById;
    private readonly GetAllFaresUseCase _getAll;
    private readonly UpdateFareUseCase _update;
    private readonly DeleteFareUseCase _delete;

    public FareConsoleUI(
        CreateFareUseCase create,
        GetFareByIdUseCase getById,
        GetAllFaresUseCase getAll,
        UpdateFareUseCase update,
        DeleteFareUseCase delete
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
            SpectreUi.ModuleHeader("Tarifas", "Gestión de tarifas por ruta/cabina/pasajero/temporada");
            var items = new (string Label, Action Action)[]
            {
                ("Crear tarifa", () => Create().GetAwaiter().GetResult()),
                ("Listar todas", () => ListAll().GetAwaiter().GetResult()),
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
            SpectreUi.ModuleHeader("Crear tarifa", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var routeId = SpectreUi.PromptIntRequiredCancelable("RouteId", min: 1);
            var cabinTypeId = SpectreUi.PromptIntRequiredCancelable("CabinTypeId", min: 1);
            var passengerTypeId = SpectreUi.PromptIntRequiredCancelable("PassengerTypeId", min: 1);
            var seasonId = SpectreUi.PromptIntRequiredCancelable("SeasonId", min: 1);
            var basePrice = PromptDecimalRequired("Precio base", "p.ej. 250000.00");
            var validFrom = PromptDateTimeOptional("Válida desde", "yyyy-MM-dd HH:mm (opcional)");
            var validTo = PromptDateTimeOptional("Válida hasta", "yyyy-MM-dd HH:mm (opcional)");

            var created = await _create.ExecuteAsync(new CreateFareRequest(
                RouteId: routeId,
                CabinTypeId: cabinTypeId,
                PassengerTypeId: passengerTypeId,
                SeasonId: seasonId,
                BasePrice: basePrice,
                ValidFrom: validFrom,
                ValidTo: validTo
            ));

            SpectreUi.MarkupLineOrPlain(
                $"[green]Tarifa creada[/] id={created.Id.Value} · route={created.RouteId.Value} · price={created.BasePrice.Value:0.00}",
                $"Tarifa creada id={created.Id.Value} · route={created.RouteId.Value} · price={created.BasePrice.Value:0.00}"
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
                SpectreUi.MarkupLineOrPlain("[grey]No hay tarifas para mostrar.[/]", "No hay tarifas para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Tarifas", "Listado");
            SpectreUi.ShowTable(
                "Tarifas",
                ["ID", "RouteId", "CabinTypeId", "PassengerTypeId", "SeasonId", "BasePrice", "Vigencia"],
                list.OrderBy(x => x.Id.Value).Select(x => (IReadOnlyList<string>)new[]
                {
                    x.Id.Value.ToString(),
                    x.RouteId.Value.ToString(),
                    x.CabinTypeId.Value.ToString(),
                    x.PassengerTypeId.Value.ToString(),
                    x.SeasonId.Value.ToString(),
                    x.BasePrice.Value.ToString("0.00"),
                    $"{x.ValidFrom.Value?.ToString("yyyy-MM-dd") ?? "-"} → {x.ValidTo.Value?.ToString("yyyy-MM-dd") ?? "-"}"
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
            SpectreUi.ModuleHeader("Consultar tarifa", null);
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
                "Tarifa",
                ["Campo", "Valor"],
                [
                    ["ID", x.Id.Value.ToString()],
                    ["RouteId", x.RouteId.Value.ToString()],
                    ["CabinTypeId", x.CabinTypeId.Value.ToString()],
                    ["PassengerTypeId", x.PassengerTypeId.Value.ToString()],
                    ["SeasonId", x.SeasonId.Value.ToString()],
                    ["BasePrice", x.BasePrice.Value.ToString("0.00")],
                    ["ValidFrom", x.ValidFrom.Value?.ToString("yyyy-MM-dd HH:mm") ?? "-"],
                    ["ValidTo", x.ValidTo.Value?.ToString("yyyy-MM-dd HH:mm") ?? "-"],
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
            SpectreUi.ModuleHeader("Actualizar tarifa", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var routeId = SpectreUi.PromptIntRequiredCancelable("RouteId", min: 1);
            var cabinTypeId = SpectreUi.PromptIntRequiredCancelable("CabinTypeId", min: 1);
            var passengerTypeId = SpectreUi.PromptIntRequiredCancelable("PassengerTypeId", min: 1);
            var seasonId = SpectreUi.PromptIntRequiredCancelable("SeasonId", min: 1);
            var basePrice = PromptDecimalRequired("Precio base", "p.ej. 250000.00");
            var validFrom = PromptDateTimeOptional("Válida desde", "yyyy-MM-dd HH:mm (opcional)");
            var validTo = PromptDateTimeOptional("Válida hasta", "yyyy-MM-dd HH:mm (opcional)");

            await _update.ExecuteAsync(new UpdateFareRequest(
                Id: id,
                RouteId: routeId,
                CabinTypeId: cabinTypeId,
                PassengerTypeId: passengerTypeId,
                SeasonId: seasonId,
                BasePrice: basePrice,
                ValidFrom: validFrom,
                ValidTo: validTo
            ));

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
            SpectreUi.ModuleHeader("Eliminar tarifa", null);
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

    private static decimal PromptDecimalRequired(string label, string help)
    {
        while (true)
        {
            var raw = SpectreUi.PromptRequiredCancelable(label, help);
            if (decimal.TryParse(raw, out var value))
                return value;
            SpectreUi.MarkupLineOrPlain("[red]Número inválido.[/]", "Número inválido.");
        }
    }

    private static DateTime? PromptDateTimeOptional(string label, string help)
    {
        while (true)
        {
            var raw = SpectreUi.PromptOptionalCancelable(label, help);
            if (string.IsNullOrWhiteSpace(raw))
                return null;
            if (DateTime.TryParse(raw, out var dt))
                return dt;
            SpectreUi.MarkupLineOrPlain("[red]Fecha/hora inválida.[/]", "Fecha/hora inválida.");
        }
    }
}

