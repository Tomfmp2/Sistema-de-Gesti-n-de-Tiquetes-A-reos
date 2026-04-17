using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Application.UseCases;

public class DeleteAircraftManufacturerUseCase
{
    private readonly IAircraftManufacturerRepository _repository;

    public DeleteAircraftManufacturerUseCase(IAircraftManufacturerRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(AircraftManufacturerId id)
    {
        await _repository.DeleteAsync(id);
    }
}