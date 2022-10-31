using AcmeCompany.SmartHR.Domain.SeedWork;

namespace AcmeCompany.SmartHR.Domain.Aggregates.EmployeeAggregate;

public interface IEmployeeRepository : IRepository<Employee, Guid>
{

}