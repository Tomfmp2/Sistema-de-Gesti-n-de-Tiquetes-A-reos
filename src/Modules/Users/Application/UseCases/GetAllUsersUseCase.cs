using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Repositories;
using UserAggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Aggregate.User;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.UseCases;

public interface IGetAllUsersUseCase
{
    Task<IReadOnlyList<UserAggregate>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllUsersUseCase : IGetAllUsersUseCase
{
    private readonly IUserRepository _repository;

    public GetAllUsersUseCase(IUserRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<UserAggregate>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
