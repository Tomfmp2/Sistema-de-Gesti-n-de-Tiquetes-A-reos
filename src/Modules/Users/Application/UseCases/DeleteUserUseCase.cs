using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.UseCases;

public interface IDeleteUserUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserRepository _repository;

    public DeleteUserUseCase(IUserRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(UserId.Create(id), cancellationToken);
    }
}
