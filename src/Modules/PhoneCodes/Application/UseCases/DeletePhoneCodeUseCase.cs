using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.UseCases;

public interface IDeletePhoneCodeUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeletePhoneCodeUseCase : IDeletePhoneCodeUseCase
{
    private readonly IPhoneCodeRepository _repository;

    public DeletePhoneCodeUseCase(IPhoneCodeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(PhoneCodeId.Create(id), cancellationToken);
    }
}
