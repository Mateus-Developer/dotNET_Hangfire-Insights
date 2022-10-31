namespace AcmeCompany.SmartHR.Domain.SeedWork;

public abstract class Entity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedOn { get; protected set; }
}
