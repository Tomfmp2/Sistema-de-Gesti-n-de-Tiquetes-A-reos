using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.UseCases;

public interface IGetCheckinByIdUseCase
{
    Task<Checkin?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetCheckinByIdUseCase : IGetCheckinByIdUseCase
{
    private readonly ICheckinRepository _repository;

    public GetCheckinByIdUseCase(ICheckinRepository repository)
    {
        _repository = repository;
    }

    public Task<Checkin?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Checkin?>(null);
        }

        return _repository.GetByIdAsync(CheckinId.Create(id), cancellationToken);
    }
}
