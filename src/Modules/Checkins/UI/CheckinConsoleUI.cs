using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.UI;

public sealed class CheckinConsoleUI : IModuleUI
{
    private readonly CreateCheckinUseCase _create;
    private readonly GetCheckinByIdUseCase _getById;
    private readonly GetAllCheckinsUseCase _getAll;
    private readonly UpdateCheckinUseCase _update;
    private readonly DeleteCheckinUseCase _delete;

    public CheckinConsoleUI(
        CreateCheckinUseCase create,
        GetCheckinByIdUseCase getById,
        GetAllCheckinsUseCase getAll,
        UpdateCheckinUseCase update,
        DeleteCheckinUseCase delete
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
            SpectreUi.ModuleHeader("Check-ins", "Registro de check-in / pase de abordar / asiento");
            var items = new (string Label, Action Action)[]
            {
                ("Crear check-in", () => Create().GetAwaiter().GetResult()),
                ("Listar todos", () => ListAll().GetAwaiter().GetResult()),
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
            SpectreUi.ModuleHeader("Crear check-in", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var ticketId = SpectreUi.PromptIntRequiredCancelable("TicketId", min: 1);
            var staffId = SpectreUi.PromptIntRequiredCancelable("StaffId", min: 1);
            var flightSeatId = SpectreUi.PromptIntRequiredCancelable("FlightSeatId", min: 1);
            var checkinDate = PromptDateTimeRequired("Fecha check-in (UTC)", "yyyy-MM-dd HH:mm");
            var checkinStatusId = SpectreUi.PromptIntRequiredCancelable("CheckinStatusId", min: 1);
            var bp = SpectreUi.PromptRequiredCancelable("BoardingPassNumber", "p.ej. BP-123-12A");
            var hasBaggage = SpectreUi.PromptBool("¿Tiene equipaje chequeado?", defaultValue: false);
            decimal? baggageKg = null;
            if (hasBaggage)
                baggageKg = PromptDecimalOptional("Peso equipaje (kg)", "opcional");

            var created = await _create.ExecuteAsync(new CreateCheckinRequest(
                TicketId: ticketId,
                StaffId: staffId,
                FlightSeatId: flightSeatId,
                CheckinDate: checkinDate,
                CheckinStatusId: checkinStatusId,
                BoardingPassNumber: bp,
                HasCheckedBaggage: hasBaggage,
                BaggageWeightKg: baggageKg
            ));

            SpectreUi.MarkupLineOrPlain(
                $"[green]Check-in creado[/] id={created.Id.Value} · bp={created.BoardingPassNumber.Value}",
                $"Check-in creado id={created.Id.Value} · bp={created.BoardingPassNumber.Value}"
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
                SpectreUi.MarkupLineOrPlain("[grey]No hay check-ins para mostrar.[/]", "No hay check-ins para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Check-ins", "Listado");
            SpectreUi.ShowTable(
                "Check-ins",
                ["ID", "TicketId", "StaffId", "FlightSeatId", "Fecha", "StatusId", "BP"],
                list.OrderBy(x => x.Id.Value).Select(x => (IReadOnlyList<string>)new[]
                {
                    x.Id.Value.ToString(),
                    x.TicketId.Value.ToString(),
                    x.StaffId.Value.ToString(),
                    x.FlightSeatId.Value.ToString(),
                    x.CheckinDate.Value.ToString("yyyy-MM-dd HH:mm"),
                    x.CheckinStatusId.Value.ToString(),
                    x.BoardingPassNumber.Value
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
            SpectreUi.ModuleHeader("Consultar check-in", null);
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
                "Check-in",
                ["Campo", "Valor"],
                [
                    ["ID", x.Id.Value.ToString()],
                    ["TicketId", x.TicketId.Value.ToString()],
                    ["StaffId", x.StaffId.Value.ToString()],
                    ["FlightSeatId", x.FlightSeatId.Value.ToString()],
                    ["CheckinDate", x.CheckinDate.Value.ToString("yyyy-MM-dd HH:mm")],
                    ["CheckinStatusId", x.CheckinStatusId.Value.ToString()],
                    ["BoardingPassNumber", x.BoardingPassNumber.Value],
                    ["HasCheckedBaggage", x.HasCheckedBaggage.Value ? "Sí" : "No"],
                    ["BaggageWeightKg", x.BaggageWeightKg.Value?.ToString("0.00") ?? "-"],
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
            SpectreUi.ModuleHeader("Actualizar check-in", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var ticketId = SpectreUi.PromptIntRequiredCancelable("TicketId", min: 1);
            var staffId = SpectreUi.PromptIntRequiredCancelable("StaffId", min: 1);
            var flightSeatId = SpectreUi.PromptIntRequiredCancelable("FlightSeatId", min: 1);
            var checkinDate = PromptDateTimeRequired("Fecha check-in (UTC)", "yyyy-MM-dd HH:mm");
            var checkinStatusId = SpectreUi.PromptIntRequiredCancelable("CheckinStatusId", min: 1);
            var bp = SpectreUi.PromptRequiredCancelable("BoardingPassNumber", "p.ej. BP-123-12A");
            var hasBaggage = SpectreUi.PromptBool("¿Tiene equipaje chequeado?", defaultValue: false);
            decimal? baggageKg = null;
            if (hasBaggage)
                baggageKg = PromptDecimalOptional("Peso equipaje (kg)", "opcional");

            await _update.ExecuteAsync(new UpdateCheckinRequest(
                Id: id,
                TicketId: ticketId,
                StaffId: staffId,
                FlightSeatId: flightSeatId,
                CheckinDate: checkinDate,
                CheckinStatusId: checkinStatusId,
                BoardingPassNumber: bp,
                HasCheckedBaggage: hasBaggage,
                BaggageWeightKg: baggageKg
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
            SpectreUi.ModuleHeader("Eliminar check-in", null);
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

    private static DateTime PromptDateTimeRequired(string label, string help)
    {
        while (true)
        {
            var raw = SpectreUi.PromptRequiredCancelable(label, help);
            if (DateTime.TryParse(raw, out var dt))
                return dt;
            SpectreUi.MarkupLineOrPlain("[red]Fecha/hora inválida.[/]", "Fecha/hora inválida.");
        }
    }

    private static decimal? PromptDecimalOptional(string label, string help)
    {
        while (true)
        {
            var raw = SpectreUi.PromptOptionalCancelable(label, help);
            if (string.IsNullOrWhiteSpace(raw))
                return null;
            if (decimal.TryParse(raw, out var v))
                return v;
            SpectreUi.MarkupLineOrPlain("[red]Número inválido.[/]", "Número inválido.");
        }
    }
}

