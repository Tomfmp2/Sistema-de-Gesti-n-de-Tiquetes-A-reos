using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.Services;

public sealed class PhoneCodeService : IPhoneCodeService
{
    private readonly ICreatePhoneCodeUseCase _create;
    private readonly IGetPhoneCodeByIdUseCase _getById;
    private readonly IGetAllPhoneCodesUseCase _getAll;
    private readonly IUpdatePhoneCodeUseCase _update;
    private readonly IDeletePhoneCodeUseCase _delete;

    public PhoneCodeService(
        ICreatePhoneCodeUseCase create,
        IGetPhoneCodeByIdUseCase getById,
        IGetAllPhoneCodesUseCase getAll,
        IUpdatePhoneCodeUseCase update,
        IDeletePhoneCodeUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<PhoneCode> CreateAsync(
        CreatePhoneCodeRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<PhoneCode?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<PhoneCode>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdatePhoneCodeRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
