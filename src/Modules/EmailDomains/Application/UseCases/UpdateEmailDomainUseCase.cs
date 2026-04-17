using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.UseCases;

public interface IUpdateEmailDomainUseCase
{
    Task ExecuteAsync(
        UpdateEmailDomainRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateEmailDomainUseCase : IUpdateEmailDomainUseCase
{
    private readonly IEmailDomainRepository _repository;

    public UpdateEmailDomainUseCase(IEmailDomainRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateEmailDomainRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = EmailDomain.Create(
            EmailDomainId.Create(request.Id),
            EmailDomainHost.Create(request.Domain)
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
