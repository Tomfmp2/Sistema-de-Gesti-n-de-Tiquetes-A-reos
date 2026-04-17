using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Application.UseCases;

public class UpdateAircraftManufacturerUseCase
{
    private readonly IAircraftManufacturerRepository _repository;

    public UpdateAircraftManufacturerUseCase(IAircraftManufacturerRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        AircraftManufacturerId id,
        AircraftManufacturerName name,
        Country country)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            throw new InvalidOperationException("Aircraft manufacturer not found.");
        }

        var updated = AircraftManufacturer.Create(id, name, country);
        await _repository.UpdateAsync(updated);
    }
}