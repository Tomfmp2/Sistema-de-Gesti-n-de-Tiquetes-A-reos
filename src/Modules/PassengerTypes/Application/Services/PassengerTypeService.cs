using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.Services;

public sealed class PassengerTypeService : IPassengerTypeService
{
    private readonly ICreatePassengerTypeUseCase _create;
    private readonly IGetPassengerTypeByIdUseCase _getById;
    private readonly IGetAllPassengerTypesUseCase _getAll;
    private readonly IUpdatePassengerTypeUseCase _update;
    private readonly IDeletePassengerTypeUseCase _delete;

    public PassengerTypeService(
        ICreatePassengerTypeUseCase create,
        IGetPassengerTypeByIdUseCase getById,
        IGetAllPassengerTypesUseCase getAll,
        IUpdatePassengerTypeUseCase update,
        IDeletePassengerTypeUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<PassengerType> CreateAsync(
        CreatePassengerTypeRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<PassengerType?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<PassengerType>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdatePassengerTypeRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
