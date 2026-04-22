using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.UseCases;

public interface IUpdateInvoiceUseCase
{
    Task ExecuteAsync(
        UpdateInvoiceRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateInvoiceUseCase : IUpdateInvoiceUseCase
{
    private readonly IInvoiceRepository _repository;

    public UpdateInvoiceUseCase(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateInvoiceRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var number = request.Number;
        ArgumentNullException.ThrowIfNull(number);
        var x = Invoice.Create(InvoiceId.Create(request.Id), InvoiceReservationId.Create(request.ReservationId), InvoiceNumber.Create(number), InvoiceIssueDate.Create(request.IssueDate), InvoiceSubtotal.Create(request.Subtotal), InvoiceTaxes.Create(request.Taxes), InvoiceTotal.Create(request.Total), InvoiceCreatedAt.Create(request.CreatedAt));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
