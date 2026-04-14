using MyInventory2026.src.shared.contracts;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace MyInventory2026.src.shared.context;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}
