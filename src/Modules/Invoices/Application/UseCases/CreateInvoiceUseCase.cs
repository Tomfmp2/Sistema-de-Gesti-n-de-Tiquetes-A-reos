using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.UseCases;

public interface ICreateInvoiceUseCase
{
    Task<Invoice> ExecuteAsync(
        CreateInvoiceRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateInvoiceUseCase : ICreateInvoiceUseCase
{
    private readonly IInvoiceRepository _repository;

    public CreateInvoiceUseCase(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public Task<Invoice> ExecuteAsync(
        CreateInvoiceRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Invoice.Create(new InvoiceId(0), InvoiceReservationId.Create(request.ReservationId), InvoiceNumber.Create(request.Number), InvoiceIssueDate.Create(request.IssueDate), InvoiceSubtotal.Create(request.Subtotal), InvoiceTaxes.Create(request.Taxes), InvoiceTotal.Create(request.Total), InvoiceCreatedAt.Create(request.CreatedAt));
        return _repository.AddAsync(x, cancellationToken);
    }
}
