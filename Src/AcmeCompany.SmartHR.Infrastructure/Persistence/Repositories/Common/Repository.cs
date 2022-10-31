using AcmeCompany.SmartHR.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace AcmeCompany.SmartHR.Infrastructure.Persistence.Repositories.Common;

internal abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class, IAggregateRoot
{
    protected DbSet<TEntity> _entities = null!;

    public Repository(DbContext context)
    {
        if (context is null)
            throw new ArgumentNullException(nameof(context));

        _entities = context.Set<TEntity>();
    }

    public async ValueTask CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await _entities.AddAsync(entity, cancellationToken);

    public async ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _entities.Remove(entity);

        await Task.CompletedTask;
    }

    public async ValueTask<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
        => await _entities.FindAsync(new object?[] { id }, cancellationToken);

    public async ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _entities.Update(entity);

        await Task.CompletedTask;
    }
}
