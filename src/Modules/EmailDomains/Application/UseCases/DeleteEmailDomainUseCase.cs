using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.UseCases;

public interface IDeleteEmailDomainUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteEmailDomainUseCase : IDeleteEmailDomainUseCase
{
    private readonly IEmailDomainRepository _repository;

    public DeleteEmailDomainUseCase(IEmailDomainRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(EmailDomainId.Create(id), cancellationToken);
    }
}
