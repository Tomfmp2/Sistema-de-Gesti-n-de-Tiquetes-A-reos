using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.UseCases;

public interface ICreatePhoneCodeUseCase
{
    Task<PhoneCode> ExecuteAsync(
        CreatePhoneCodeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreatePhoneCodeUseCase : ICreatePhoneCodeUseCase
{
    private readonly IPhoneCodeRepository _repository;

    public CreatePhoneCodeUseCase(IPhoneCodeRepository repository)
    {
        _repository = repository;
    }

    public Task<PhoneCode> ExecuteAsync(
        CreatePhoneCodeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = PhoneCode.CreateNew(
            PhoneDialCode.Create(request.CountryDialCode),
            PhoneCodeCountryLabel.Create(request.CountryName)
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}
