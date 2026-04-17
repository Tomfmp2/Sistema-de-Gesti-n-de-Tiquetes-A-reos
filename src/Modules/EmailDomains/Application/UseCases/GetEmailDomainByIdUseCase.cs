using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.UseCases;

public interface IGetEmailDomainByIdUseCase
{
    Task<EmailDomain?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetEmailDomainByIdUseCase : IGetEmailDomainByIdUseCase
{
    private readonly IEmailDomainRepository _repository;

    public GetEmailDomainByIdUseCase(IEmailDomainRepository repository)
    {
        _repository = repository;
    }

    public Task<EmailDomain?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<EmailDomain?>(null);
        }

        return _repository.GetByIdAsync(EmailDomainId.Create(id), cancellationToken);
    }
}
