using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.UseCases;
using UserAggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Aggregate.User;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.Services;

public sealed class UserService : IUserService
{
    private readonly ICreateUserUseCase _create;
    private readonly IGetUserByIdUseCase _getById;
    private readonly IGetAllUsersUseCase _getAll;
    private readonly IUpdateUserUseCase _update;
    private readonly IDeleteUserUseCase _delete;

    public UserService(
        ICreateUserUseCase create,
        IGetUserByIdUseCase getById,
        IGetAllUsersUseCase getAll,
        IUpdateUserUseCase update,
        IDeleteUserUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<UserAggregate> CreateAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<UserAggregate?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<UserAggregate>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateUserRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
