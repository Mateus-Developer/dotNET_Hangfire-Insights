using AcmeCompany.SmartHR.Domain.Aggregates.EmployeeAggregate;
using AcmeCompany.SmartHR.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace AcmeCompany.SmartHR.Infrastructure.Persistence;

internal sealed class SmartHRContext : DbContext, IUnitOfWork
{
    public DbSet<Employee> Employees => Set<Employee>();

    public SmartHRContext(DbContextOptions<SmartHRContext> options)
        : base(options)
    {
    }

    public async ValueTask CommitAsync(CancellationToken cancellationToken = default)
    {
        var strategy = Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().AreUnicode(false);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("HumanResources");

        modelBuilder.Entity<Employee>(builder =>
        {
            builder.ToTable(nameof(Employees));
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasColumnOrder(1);
            builder.Property(o => o.CreatedOn).HasColumnOrder(2).IsRequired();
            builder.Property(o => o.UpdatedOn).HasColumnOrder(3).ValueGeneratedOnUpdate();
            builder.Property(o => o.FullName).HasMaxLength(350).IsRequired();
            builder.Property(o => o.Email).HasMaxLength(350).IsRequired();
            builder.Property(o => o.HireDate).IsRequired();
            builder.Property(o => o.JobTitle).HasMaxLength(150).IsRequired();
            builder.Property(o => o.Salary).HasPrecision(10, 2).IsRequired();
        });
    }
}
