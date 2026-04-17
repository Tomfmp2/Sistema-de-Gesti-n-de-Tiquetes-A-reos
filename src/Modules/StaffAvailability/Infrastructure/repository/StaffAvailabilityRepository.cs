using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.repository;

public class StaffAvailabilityRepository : IStaffAvailabilityRepository
{
    private readonly AppDbContext _context;

    public StaffAvailabilityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StaffAvailabilityRecord?> GetByIdAsync(StaffAvailabilityId id)
    {
        var entity = await _context.StaffAvailabilities.FindAsync(id.Value);
        if (entity == null) return null;

        return StaffAvailabilityRecord.Reconstitute(
            StaffAvailabilityId.Reconstitute(entity.Id),
            StaffId.Reconstitute(entity.StaffId),
            AvailabilityStatusId.Reconstitute(entity.AvailabilityStatusId),
            StartDate.Reconstitute(entity.StartDate),
            EndDate.Reconstitute(entity.EndDate),
            entity.Observation != null ? Observation.Reconstitute(entity.Observation) : null
        );
    }

    public async Task<IEnumerable<StaffAvailabilityRecord>> GetAllAsync()
    {
        var entities = await _context.StaffAvailabilities.ToListAsync();
        return entities.Select(entity => StaffAvailabilityRecord.Reconstitute(
            StaffAvailabilityId.Reconstitute(entity.Id),
            StaffId.Reconstitute(entity.StaffId),
            AvailabilityStatusId.Reconstitute(entity.AvailabilityStatusId),
            StartDate.Reconstitute(entity.StartDate),
            EndDate.Reconstitute(entity.EndDate),
            entity.Observation != null ? Observation.Reconstitute(entity.Observation) : null
        ));
    }

    public async Task AddAsync(StaffAvailabilityRecord staffAvailability)
    {
        var entity = new StaffAvailabilityEntity
        {
            Id = staffAvailability.Id.Value,
            StaffId = staffAvailability.StaffId.Value,
            AvailabilityStatusId = staffAvailability.AvailabilityStatusId.Value,
            StartDate = staffAvailability.StartDate.Value,
            EndDate = staffAvailability.EndDate.Value,
            Observation = staffAvailability.Observation?.Value
        };

        await _context.StaffAvailabilities.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(StaffAvailabilityRecord staffAvailability)
    {
        var entity = await _context.StaffAvailabilities.FindAsync(staffAvailability.Id.Value);
        if (entity == null) return;

        entity.StaffId = staffAvailability.StaffId.Value;
        entity.AvailabilityStatusId = staffAvailability.AvailabilityStatusId.Value;
        entity.StartDate = staffAvailability.StartDate.Value;
        entity.EndDate = staffAvailability.EndDate.Value;
        entity.Observation = staffAvailability.Observation?.Value;

        _context.StaffAvailabilities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(StaffAvailabilityId id)
    {
        var entity = await _context.StaffAvailabilities.FindAsync(id.Value);
        if (entity == null) return;

        _context.StaffAvailabilities.Remove(entity);
        await _context.SaveChangesAsync();
    }
}