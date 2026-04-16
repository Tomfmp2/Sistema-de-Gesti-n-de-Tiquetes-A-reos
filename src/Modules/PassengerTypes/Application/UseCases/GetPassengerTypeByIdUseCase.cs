using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.UseCases;

public interface IGetPassengerTypeByIdUseCase
{
    Task<PassengerType?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetPassengerTypeByIdUseCase : IGetPassengerTypeByIdUseCase
{
    private readonly IPassengerTypeRepository _repository;

    public GetPassengerTypeByIdUseCase(IPassengerTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<PassengerType?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<PassengerType?>(null);
        }

        return _repository.GetByIdAsync(PassengerTypeId.Create(id), cancellationToken);
    }
}
