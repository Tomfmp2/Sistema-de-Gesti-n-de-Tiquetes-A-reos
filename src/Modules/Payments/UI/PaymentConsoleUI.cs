using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.UI;

public sealed class PaymentConsoleUI : IModuleUI
{
    private readonly CreatePaymentUseCase _create;
    private readonly GetPaymentByIdUseCase _getById;
    private readonly GetAllPaymentsUseCase _getAll;
    private readonly UpdatePaymentUseCase _update;
    private readonly DeletePaymentUseCase _delete;

    public PaymentConsoleUI(
        CreatePaymentUseCase create,
        GetPaymentByIdUseCase getById,
        GetAllPaymentsUseCase getAll,
        UpdatePaymentUseCase update,
        DeletePaymentUseCase delete
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
            SpectreUi.ModuleHeader("Pagos", "Gestión de pagos por reservación");
            var items = new (string Label, Action Action)[]
            {
                ("Registrar pago", () => Create().GetAwaiter().GetResult()),
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
            SpectreUi.ModuleHeader("Registrar pago", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var reservationId = SpectreUi.PromptIntRequiredCancelable("ReservationId", min: 1);
            var amount = PromptDecimalRequired("Monto", "p.ej. 150000.00");
            var paymentDate = PromptDateTimeRequired("Fecha pago (UTC)", "yyyy-MM-dd HH:mm");
            var paymentStatusId = SpectreUi.PromptIntRequiredCancelable("PaymentStatusId", min: 1);
            var paymentMethodId = SpectreUi.PromptIntRequiredCancelable("PaymentMethodId", min: 1);

            var now = DateTime.UtcNow;
            var created = await _create.ExecuteAsync(new CreatePaymentRequest(
                ReservationId: reservationId,
                Amount: amount,
                PaymentDate: paymentDate,
                PaymentStatusId: paymentStatusId,
                PaymentMethodId: paymentMethodId,
                CreatedAt: now,
                UpdatedAt: now
            ));

            SpectreUi.MarkupLineOrPlain(
                $"[green]Pago creado[/] id={created.Id.Value} reserva={created.ReservationId.Value} monto={created.Amount.Value:0.00}",
                $"Pago creado id={created.Id.Value} reserva={created.ReservationId.Value} monto={created.Amount.Value:0.00}"
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
                SpectreUi.MarkupLineOrPlain("[grey]No hay pagos para mostrar.[/]", "No hay pagos para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Pagos", "Listado");
            SpectreUi.ShowTable(
                "Pagos",
                ["ID", "ReservaId", "Monto", "Fecha", "EstadoId", "MétodoId"],
                list.OrderBy(x => x.Id.Value).Select(x => (IReadOnlyList<string>)new[]
                {
                    x.Id.Value.ToString(),
                    x.ReservationId.Value.ToString(),
                    x.Amount.Value.ToString("0.00"),
                    x.PaymentDate.Value.ToString("yyyy-MM-dd HH:mm"),
                    x.PaymentStatusId.Value.ToString(),
                    x.PaymentMethodId.Value.ToString()
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
            SpectreUi.ModuleHeader("Consultar pago", null);
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
                "Pago",
                ["Campo", "Valor"],
                [
                    ["ID", x.Id.Value.ToString()],
                    ["ReservationId", x.ReservationId.Value.ToString()],
                    ["Amount", x.Amount.Value.ToString("0.00")],
                    ["PaymentDate", x.PaymentDate.Value.ToString("yyyy-MM-dd HH:mm")],
                    ["PaymentStatusId", x.PaymentStatusId.Value.ToString()],
                    ["PaymentMethodId", x.PaymentMethodId.Value.ToString()],
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
            SpectreUi.ModuleHeader("Actualizar pago", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var reservationId = SpectreUi.PromptIntRequiredCancelable("ReservationId", min: 1);
            var amount = PromptDecimalRequired("Monto", "p.ej. 150000.00");
            var paymentDate = PromptDateTimeRequired("Fecha pago (UTC)", "yyyy-MM-dd HH:mm");
            var paymentStatusId = SpectreUi.PromptIntRequiredCancelable("PaymentStatusId", min: 1);
            var paymentMethodId = SpectreUi.PromptIntRequiredCancelable("PaymentMethodId", min: 1);
            var now = DateTime.UtcNow;

            await _update.ExecuteAsync(new UpdatePaymentRequest(
                Id: id,
                ReservationId: reservationId,
                Amount: amount,
                PaymentDate: paymentDate,
                PaymentStatusId: paymentStatusId,
                PaymentMethodId: paymentMethodId,
                CreatedAt: now,
                UpdatedAt: now
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
            SpectreUi.ModuleHeader("Eliminar pago", null);
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

