using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Application.UseCases;

public class GetAircraftManufacturerByIdUseCase
{
    private readonly IAircraftManufacturerRepository _repository;

    public GetAircraftManufacturerByIdUseCase(IAircraftManufacturerRepository repository)
    {
        _repository = repository;
    }

    public async Task<AircraftManufacturer?> ExecuteAsync(AircraftManufacturerId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}