using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.ValueObjet;
using UserAggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Aggregate.User;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.UseCases;

public interface IGetUserByIdUseCase
{
    Task<UserAggregate?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetUserByIdUseCase : IGetUserByIdUseCase
{
    private readonly IUserRepository _repository;

    public GetUserByIdUseCase(IUserRepository repository)
    {
        _repository = repository;
    }

    public Task<UserAggregate?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<UserAggregate?>(null);
        }

        return _repository.GetByIdAsync(UserId.Create(id), cancellationToken);
    }
}
