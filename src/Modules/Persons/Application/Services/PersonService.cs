using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.Services;

public sealed class PersonService : IPersonService
{
    private readonly ICreatePersonUseCase _create;
    private readonly IGetPersonByIdUseCase _getById;
    private readonly IGetAllPersonsUseCase _getAll;
    private readonly IUpdatePersonUseCase _update;
    private readonly IDeletePersonUseCase _delete;

    public PersonService(
        ICreatePersonUseCase create,
        IGetPersonByIdUseCase getById,
        IGetAllPersonsUseCase getAll,
        IUpdatePersonUseCase update,
        IDeletePersonUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Person> CreateAsync(
        CreatePersonRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Person>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdatePersonRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
