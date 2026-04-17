using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.UseCases;

public interface IGetAllPersonPhonesUseCase
{
    Task<IReadOnlyList<PersonPhone>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllPersonPhonesUseCase : IGetAllPersonPhonesUseCase
{
    private readonly IPersonPhoneRepository _repository;

    public GetAllPersonPhonesUseCase(IPersonPhoneRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<PersonPhone>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
