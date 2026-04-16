using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Application.UseCases;

public class CreateAircraftManufacturerUseCase
{
    private readonly IAircraftManufacturerRepository _repository;

    public CreateAircraftManufacturerUseCase(IAircraftManufacturerRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        AircraftManufacturerId id,
        AircraftManufacturerName name,
        Country country)
    {
        var aircraftManufacturer = AircraftManufacturer.Create(id, name, country);
        await _repository.AddAsync(aircraftManufacturer);
    }
}