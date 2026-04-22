using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Repositories;

public class BaggageRepository : IBaggageRepository
{
    private readonly AppDbContext _context;

    public BaggageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(BaggageItem baggage)
    {
        var entity = new BaggageEntity
        {
            CheckinId = baggage.CheckinId,
            BaggageTypeId = baggage.BaggageTypeId,
            WeightKg = baggage.WeightKg.Value,
            ChargedPrice = baggage.ChargedPrice.Value
        };

        await _context.Baggages.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<BaggageItem?> GetByIdAsync(int id)
    {
        var entity = await _context.Baggages.FirstOrDefaultAsync(b => b.Id == id);
        if (entity is null)
            return null;

        var now = DateTime.UtcNow;
        return BaggageItem.Reconstitute(
            entity.Id,
            entity.CheckinId,
            entity.BaggageTypeId,
            entity.WeightKg,
            entity.ChargedPrice,
            now,
            now);
    }

    public async Task<List<BaggageItem>> GetAllAsync()
    {
        var entities = await _context.Baggages.ToListAsync();
        return entities.Select(e =>
        {
            var now = DateTime.UtcNow;
            return BaggageItem.Reconstitute(
                e.Id,
                e.CheckinId,
                e.BaggageTypeId,
                e.WeightKg,
                e.ChargedPrice,
                now,
                now);
        }).ToList();
    }

    public async Task UpdateAsync(BaggageItem baggage)
    {
        var entity = await _context.Baggages.FirstOrDefaultAsync(b => b.Id == baggage.Id.Value);
        if (entity is null)
            return;

        entity.BaggageTypeId = baggage.BaggageTypeId;
        entity.WeightKg = baggage.WeightKg.Value;
        entity.ChargedPrice = baggage.ChargedPrice.Value;

        _context.Baggages.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Baggages.FirstOrDefaultAsync(b => b.Id == id);
        if (entity is null)
            return;

        _context.Baggages.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
