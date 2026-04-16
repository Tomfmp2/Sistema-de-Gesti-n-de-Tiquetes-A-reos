using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.ValueObjet;
using UserAggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Aggregate.User;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.UseCases;

public interface IUpdateUserUseCase
{
    Task ExecuteAsync(
        UpdateUserRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserRepository _repository;

    public UpdateUserUseCase(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        UpdateUserRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var existing = await _repository.GetByIdAsync(UserId.Create(request.Id), cancellationToken);
        if (existing is null)
        {
            throw new InvalidOperationException($"No existe usuario {request.Id}.");
        }

        var x = UserAggregate.Create(
            UserId.Create(request.Id),
            UserUsername.Create(request.Username),
            UserPasswordHash.Create(request.PasswordHash),
            request.PersonId,
            UserSystemRoleId.Create(request.SystemRoleId),
            request.IsActive,
            request.LastAccessAt,
            existing.CreatedAt,
            existing.UpdatedAt
        );
        await _repository.UpdateAsync(x, cancellationToken);
    }
}
