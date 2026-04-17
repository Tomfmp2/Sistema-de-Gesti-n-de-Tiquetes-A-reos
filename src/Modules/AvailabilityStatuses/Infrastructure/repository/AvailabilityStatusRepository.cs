using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Repository;

public class AvailabilityStatusRepository : IAvailabilityStatusRepository
{
    private readonly AppDbContext _context;

    public AvailabilityStatusRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AvailabilityStatus?> GetByIdAsync(AvailabilityStatusId id)
    {
        var entity = await _context.AvailabilityStatuses.FindAsync(id.Value);
        if (entity == null) return null;
        return AvailabilityStatus.Reconstitute(
            AvailabilityStatusId.Reconstitute(entity.Id),
            AvailabilityStatusName.Reconstitute(entity.Name)
        );
    }

    public async Task<IEnumerable<AvailabilityStatus>> GetAllAsync()
    {
        var entities = await _context.AvailabilityStatuses.ToListAsync();
        return entities.Select(e => AvailabilityStatus.Reconstitute(
            AvailabilityStatusId.Reconstitute(e.Id),
            AvailabilityStatusName.Reconstitute(e.Name)
        ));
    }

    public async Task AddAsync(AvailabilityStatus availabilityStatus)
    {
        var entity = new AvailabilityStatusEntity
        {
            Id = availabilityStatus.Id.Value,
            Name = availabilityStatus.Name.Value
        };
        await _context.AvailabilityStatuses.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AvailabilityStatus availabilityStatus)
    {
        var entity = await _context.AvailabilityStatuses.FindAsync(availabilityStatus.Id.Value);
        if (entity != null)
        {
            entity.Name = availabilityStatus.Name.Value;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(AvailabilityStatusId id)
    {
        var entity = await _context.AvailabilityStatuses.FindAsync(id.Value);
        if (entity != null)
        {
            _context.AvailabilityStatuses.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}