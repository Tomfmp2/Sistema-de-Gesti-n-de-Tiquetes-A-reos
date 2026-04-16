using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.Services;

public sealed class PersonPhoneService : IPersonPhoneService
{
    private readonly ICreatePersonPhoneUseCase _create;
    private readonly IGetPersonPhoneByIdUseCase _getById;
    private readonly IGetAllPersonPhonesUseCase _getAll;
    private readonly IUpdatePersonPhoneUseCase _update;
    private readonly IDeletePersonPhoneUseCase _delete;

    public PersonPhoneService(
        ICreatePersonPhoneUseCase create,
        IGetPersonPhoneByIdUseCase getById,
        IGetAllPersonPhonesUseCase getAll,
        IUpdatePersonPhoneUseCase update,
        IDeletePersonPhoneUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<PersonPhone> CreateAsync(
        CreatePersonPhoneRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<PersonPhone?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<PersonPhone>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdatePersonPhoneRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
