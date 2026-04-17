using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.UseCases;

public interface IGetAllPhoneCodesUseCase
{
    Task<IReadOnlyList<PhoneCode>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllPhoneCodesUseCase : IGetAllPhoneCodesUseCase
{
    private readonly IPhoneCodeRepository _repository;

    public GetAllPhoneCodesUseCase(IPhoneCodeRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<PhoneCode>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
