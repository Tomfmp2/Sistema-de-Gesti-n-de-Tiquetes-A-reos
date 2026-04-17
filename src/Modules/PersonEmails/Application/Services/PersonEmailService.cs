using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.Services;

public sealed class PersonEmailService : IPersonEmailService
{
    private readonly ICreatePersonEmailUseCase _create;
    private readonly IGetPersonEmailByIdUseCase _getById;
    private readonly IGetAllPersonEmailsUseCase _getAll;
    private readonly IUpdatePersonEmailUseCase _update;
    private readonly IDeletePersonEmailUseCase _delete;

    public PersonEmailService(
        ICreatePersonEmailUseCase create,
        IGetPersonEmailByIdUseCase getById,
        IGetAllPersonEmailsUseCase getAll,
        IUpdatePersonEmailUseCase update,
        IDeletePersonEmailUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<PersonEmail> CreateAsync(
        CreatePersonEmailRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<PersonEmail?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<PersonEmail>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdatePersonEmailRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
