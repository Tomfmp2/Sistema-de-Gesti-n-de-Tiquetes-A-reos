namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using Domain.Aggregate;
using Domain.Repositories;
using Domain.ValueObjects;
using Entity;
using Shared.Context;

public class BaggageTypeRepository : IBaggageTypeRepository
{
    private readonly AppDbContext _context;

    public BaggageTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BaggageType> CreateAsync(BaggageType baggageType)
    {
        var entity = new BaggageTypeEntity
        {
            Name = baggageType.Name.Value,
            MaxWeightKg = baggageType.MaxWeightKg.Value,
            BasePrice = baggageType.BasePrice.Value
        };

        _context.BaggageTypes.Add(entity);
        await _context.SaveChangesAsync();

        return BaggageType.Reconstitute(
            BaggageTypeId.Reconstitute(entity.Id),
            BaggageTypeName.Reconstitute(entity.Name),
            MaxWeightKg.Reconstitute(entity.MaxWeightKg),
            BasePrice.Reconstitute(entity.BasePrice),
            DateTime.Now,
            DateTime.Now
        );
    }

    public async Task<BaggageType?> GetByIdAsync(int id)
    {
        var entity = await _context.BaggageTypes.FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            return null;

        return BaggageType.Reconstitute(
            BaggageTypeId.Reconstitute(entity.Id),
            BaggageTypeName.Reconstitute(entity.Name),
            MaxWeightKg.Reconstitute(entity.MaxWeightKg),
            BasePrice.Reconstitute(entity.BasePrice),
            DateTime.Now,
            DateTime.Now
        );
    }

    public async Task<List<BaggageType>> GetAllAsync()
    {
        var entities = await _context.BaggageTypes.ToListAsync();

        return entities.Select(entity => BaggageType.Reconstitute(
            BaggageTypeId.Reconstitute(entity.Id),
            BaggageTypeName.Reconstitute(entity.Name),
            MaxWeightKg.Reconstitute(entity.MaxWeightKg),
            BasePrice.Reconstitute(entity.BasePrice),
            DateTime.Now,
            DateTime.Now
        )).ToList();
    }

    public async Task<BaggageType> UpdateAsync(BaggageType baggageType)
    {
        var entity = await _context.BaggageTypes.FirstOrDefaultAsync(x => x.Id == baggageType.Id.Value);

        if (entity == null)
            throw new KeyNotFoundException($"BaggageType with ID {baggageType.Id.Value} not found.");

        entity.Name = baggageType.Name.Value;
        entity.MaxWeightKg = baggageType.MaxWeightKg.Value;
        entity.BasePrice = baggageType.BasePrice.Value;

        _context.BaggageTypes.Update(entity);
        await _context.SaveChangesAsync();

        return baggageType;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.BaggageTypes.FirstOrDefaultAsync(x => x.Id == id);

        if (entity == null)
            throw new KeyNotFoundException($"BaggageType with ID {id} not found.");

        _context.BaggageTypes.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
