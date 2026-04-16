using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.Dtos;
using UserAggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Aggregate.User;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.Interfaces;

public interface IUserService
{
    Task<UserAggregate> CreateAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default
    );

    Task<UserAggregate?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<UserAggregate>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateUserRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
