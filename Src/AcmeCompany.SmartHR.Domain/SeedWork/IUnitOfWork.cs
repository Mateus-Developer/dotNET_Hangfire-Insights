namespace AcmeCompany.SmartHR.Domain.SeedWork;

public interface IUnitOfWork
{
    ValueTask CommitAsync(CancellationToken cancellationToken = default);
}