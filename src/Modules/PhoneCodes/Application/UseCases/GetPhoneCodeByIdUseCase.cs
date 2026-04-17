using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.UseCases;

public interface IGetPhoneCodeByIdUseCase
{
    Task<PhoneCode?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetPhoneCodeByIdUseCase : IGetPhoneCodeByIdUseCase
{
    private readonly IPhoneCodeRepository _repository;

    public GetPhoneCodeByIdUseCase(IPhoneCodeRepository repository)
    {
        _repository = repository;
    }

    public Task<PhoneCode?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<PhoneCode?>(null);
        }

        return _repository.GetByIdAsync(PhoneCodeId.Create(id), cancellationToken);
    }
}
