using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.UseCases;

public interface IGetAllEmailDomainsUseCase
{
    Task<IReadOnlyList<EmailDomain>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllEmailDomainsUseCase : IGetAllEmailDomainsUseCase
{
    private readonly IEmailDomainRepository _repository;

    public GetAllEmailDomainsUseCase(IEmailDomainRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<EmailDomain>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
