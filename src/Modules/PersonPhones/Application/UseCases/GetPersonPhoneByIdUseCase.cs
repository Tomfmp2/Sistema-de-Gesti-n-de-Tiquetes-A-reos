using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.UseCases;

public interface IGetPersonPhoneByIdUseCase
{
    Task<PersonPhone?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetPersonPhoneByIdUseCase : IGetPersonPhoneByIdUseCase
{
    private readonly IPersonPhoneRepository _repository;

    public GetPersonPhoneByIdUseCase(IPersonPhoneRepository repository)
    {
        _repository = repository;
    }

    public Task<PersonPhone?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<PersonPhone?>(null);
        }

        return _repository.GetByIdAsync(PersonPhoneId.Create(id), cancellationToken);
    }
}
