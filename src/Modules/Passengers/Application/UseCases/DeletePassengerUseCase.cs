using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.UseCases;

public interface IDeletePassengerUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeletePassengerUseCase : IDeletePassengerUseCase
{
    private readonly IPassengerRepository _repository;

    public DeletePassengerUseCase(IPassengerRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(PassengerId.Create(id), cancellationToken);
    }
}
