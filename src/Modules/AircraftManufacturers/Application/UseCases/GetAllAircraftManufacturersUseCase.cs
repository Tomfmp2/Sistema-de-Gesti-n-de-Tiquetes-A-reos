using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Application.UseCases;

public class GetAllAircraftManufacturersUseCase
{
    private readonly IAircraftManufacturerRepository _repository;

    public GetAllAircraftManufacturersUseCase(IAircraftManufacturerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AircraftManufacturer>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}