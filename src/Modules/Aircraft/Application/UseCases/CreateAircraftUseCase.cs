using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Application.UseCases;

public class CreateAircraftUseCase
{
    private readonly IAircraftRepository _repository;

    public CreateAircraftUseCase(IAircraftRepository repository)
    {
        _repository = repository;
    }

    public async Task<Aggregate.Aircraft> ExecuteAsync(
        ModelId modelId,
        AirlineId airlineId,
        Registration registration,
        ManufacturingDate? manufacturingDate,
        IsActive isActive)
    {
        var aircraft = Aggregate.Aircraft.Create(modelId, airlineId, registration, manufacturingDate, isActive);
        await _repository.AddAsync(aircraft);
        return aircraft;
    }
}