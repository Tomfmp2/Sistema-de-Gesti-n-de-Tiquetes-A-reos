using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.UI;

public sealed class InvoiceConsoleUI : IModuleUI
{
    private readonly CreateInvoiceUseCase _create;
    private readonly GetInvoiceByIdUseCase _getById;
    private readonly GetAllInvoicesUseCase _getAll;
    private readonly UpdateInvoiceUseCase _update;
    private readonly DeleteInvoiceUseCase _delete;

    public InvoiceConsoleUI(
        CreateInvoiceUseCase create,
        GetInvoiceByIdUseCase getById,
        GetAllInvoicesUseCase getAll,
        UpdateInvoiceUseCase update,
        DeleteInvoiceUseCase delete
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
            SpectreUi.ModuleHeader("Facturas", "Emisión y gestión de facturas");
            var items = new (string Label, Action Action)[]
            {
                ("Emitir factura", () => Create().GetAwaiter().GetResult()),
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
            SpectreUi.ModuleHeader("Emitir factura", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var reservationId = SpectreUi.PromptIntRequiredCancelable("ReservationId", min: 1);
            var number = SpectreUi.PromptRequiredCancelable("Número factura", "p.ej. FAC-0001");
            var issueDate = PromptDateTimeRequired("Fecha emisión (UTC)", "yyyy-MM-dd HH:mm");
            var subtotal = PromptDecimalRequired("Subtotal", "p.ej. 120000.00");
            var taxes = PromptDecimalRequired("Impuestos", "p.ej. 22800.00");
            var total = PromptDecimalRequired("Total", "p.ej. 142800.00");
            var now = DateTime.UtcNow;

            var created = await _create.ExecuteAsync(new CreateInvoiceRequest(
                ReservationId: reservationId,
                Number: number,
                IssueDate: issueDate,
                Subtotal: subtotal,
                Taxes: taxes,
                Total: total,
                CreatedAt: now
            ));

            SpectreUi.MarkupLineOrPlain(
                $"[green]Factura creada[/] id={created.Id.Value} number=[bold]{created.Number.Value}[/]",
                $"Factura creada id={created.Id.Value} number={created.Number.Value}"
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
                SpectreUi.MarkupLineOrPlain("[grey]No hay facturas para mostrar.[/]", "No hay facturas para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Facturas", "Listado");
            SpectreUi.ShowTable(
                "Facturas",
                ["ID", "ReservaId", "Número", "Fecha", "Subtotal", "Impuestos", "Total"],
                list.OrderBy(x => x.Id.Value).Select(x => (IReadOnlyList<string>)new[]
                {
                    x.Id.Value.ToString(),
                    x.ReservationId.Value.ToString(),
                    x.Number.Value,
                    x.IssueDate.Value.ToString("yyyy-MM-dd HH:mm"),
                    x.Subtotal.Value.ToString("0.00"),
                    x.Taxes.Value.ToString("0.00"),
                    x.Total.Value.ToString("0.00"),
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
            SpectreUi.ModuleHeader("Consultar factura", null);
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
                "Factura",
                ["Campo", "Valor"],
                [
                    ["ID", x.Id.Value.ToString()],
                    ["ReservationId", x.ReservationId.Value.ToString()],
                    ["Number", x.Number.Value],
                    ["IssueDate", x.IssueDate.Value.ToString("yyyy-MM-dd HH:mm")],
                    ["Subtotal", x.Subtotal.Value.ToString("0.00")],
                    ["Taxes", x.Taxes.Value.ToString("0.00")],
                    ["Total", x.Total.Value.ToString("0.00")],
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
            SpectreUi.ModuleHeader("Actualizar factura", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var reservationId = SpectreUi.PromptIntRequiredCancelable("ReservationId", min: 1);
            var number = SpectreUi.PromptRequiredCancelable("Número factura", "p.ej. FAC-0001");
            var issueDate = PromptDateTimeRequired("Fecha emisión (UTC)", "yyyy-MM-dd HH:mm");
            var subtotal = PromptDecimalRequired("Subtotal", "p.ej. 120000.00");
            var taxes = PromptDecimalRequired("Impuestos", "p.ej. 22800.00");
            var total = PromptDecimalRequired("Total", "p.ej. 142800.00");
            var now = DateTime.UtcNow;

            await _update.ExecuteAsync(new UpdateInvoiceRequest(
                Id: id,
                ReservationId: reservationId,
                Number: number,
                IssueDate: issueDate,
                Subtotal: subtotal,
                Taxes: taxes,
                Total: total,
                CreatedAt: now
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
            SpectreUi.ModuleHeader("Eliminar factura", null);
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
}

