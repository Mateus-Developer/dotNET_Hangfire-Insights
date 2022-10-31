using AcmeCompany.SmartHR.Domain.Aggregates.EmployeeAggregate;
using AcmeCompany.SmartHR.Infrastructure.Persistence.Repositories.Common;

namespace AcmeCompany.SmartHR.Infrastructure.Persistence.Repositories;

internal sealed class EmployeeRepository : Repository<Employee, Guid>, IEmployeeRepository
{
    public EmployeeRepository(SmartHRContext context)
        : base(context)
    {
    }
}
