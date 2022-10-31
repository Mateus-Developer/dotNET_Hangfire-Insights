namespace AcmeCompany.SmartHR.Domain.SeedWork;

public interface IRepository<TEntity, TId>
    where TEntity : class, IAggregateRoot
{
    ValueTask CreateAsync(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    ValueTask<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
}