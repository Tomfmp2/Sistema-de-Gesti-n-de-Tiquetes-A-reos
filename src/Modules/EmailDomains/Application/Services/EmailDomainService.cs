using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.Services;

public sealed class EmailDomainService : IEmailDomainService
{
    private readonly ICreateEmailDomainUseCase _create;
    private readonly IGetEmailDomainByIdUseCase _getById;
    private readonly IGetAllEmailDomainsUseCase _getAll;
    private readonly IUpdateEmailDomainUseCase _update;
    private readonly IDeleteEmailDomainUseCase _delete;

    public EmailDomainService(
        ICreateEmailDomainUseCase create,
        IGetEmailDomainByIdUseCase getById,
        IGetAllEmailDomainsUseCase getAll,
        IUpdateEmailDomainUseCase update,
        IDeleteEmailDomainUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<EmailDomain> CreateAsync(
        CreateEmailDomainRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<EmailDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<EmailDomain>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateEmailDomainRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
