using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<User> AddAsync(User entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(User entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(UserId id, CancellationToken cancellationToken = default);
}
