using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.UseCases;

public interface IDeletePassengerTypeUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeletePassengerTypeUseCase : IDeletePassengerTypeUseCase
{
    private readonly IPassengerTypeRepository _repository;

    public DeletePassengerTypeUseCase(IPassengerTypeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(PassengerTypeId.Create(id), cancellationToken);
    }
}
