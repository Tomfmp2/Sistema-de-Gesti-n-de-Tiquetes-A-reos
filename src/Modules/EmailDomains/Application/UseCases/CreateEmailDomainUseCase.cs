using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.UseCases;

public interface ICreateEmailDomainUseCase
{
    Task<EmailDomain> ExecuteAsync(
        CreateEmailDomainRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateEmailDomainUseCase : ICreateEmailDomainUseCase
{
    private readonly IEmailDomainRepository _repository;

    public CreateEmailDomainUseCase(IEmailDomainRepository repository)
    {
        _repository = repository;
    }

    public Task<EmailDomain> ExecuteAsync(
        CreateEmailDomainRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = EmailDomain.CreateNew(EmailDomainHost.Create(request.Domain));
        return _repository.AddAsync(x, cancellationToken);
    }
}
