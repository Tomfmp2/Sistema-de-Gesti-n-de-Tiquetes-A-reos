using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.UseCases;

public interface IDeletePersonEmailUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeletePersonEmailUseCase : IDeletePersonEmailUseCase
{
    private readonly IPersonEmailRepository _repository;

    public DeletePersonEmailUseCase(IPersonEmailRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(PersonEmailId.Create(id), cancellationToken);
    }
}
