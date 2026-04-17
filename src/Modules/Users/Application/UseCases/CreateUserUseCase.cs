using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.ValueObjet;
using UserAggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Aggregate.User;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.UseCases;

public interface ICreateUserUseCase
{
    Task<UserAggregate> ExecuteAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateUserUseCase : ICreateUserUseCase
{
    private readonly IUserRepository _repository;

    public CreateUserUseCase(IUserRepository repository)
    {
        _repository = repository;
    }

    public Task<UserAggregate> ExecuteAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = UserAggregate.CreateNew(
            UserUsername.Create(request.Username),
            UserPasswordHash.Create(request.PasswordHash),
            request.PersonId,
            UserSystemRoleId.Create(request.SystemRoleId),
            request.IsActive
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}
