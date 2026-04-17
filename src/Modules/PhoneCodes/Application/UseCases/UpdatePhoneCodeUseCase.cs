using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.UseCases;

public interface IUpdatePhoneCodeUseCase
{
    Task ExecuteAsync(
        UpdatePhoneCodeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdatePhoneCodeUseCase : IUpdatePhoneCodeUseCase
{
    private readonly IPhoneCodeRepository _repository;

    public UpdatePhoneCodeUseCase(IPhoneCodeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdatePhoneCodeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = PhoneCode.Create(
            PhoneCodeId.Create(request.Id),
            PhoneDialCode.Create(request.CountryDialCode),
            PhoneCodeCountryLabel.Create(request.CountryName)
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
