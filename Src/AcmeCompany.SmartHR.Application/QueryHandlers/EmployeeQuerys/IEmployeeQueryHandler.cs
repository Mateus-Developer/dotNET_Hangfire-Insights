using AcmeCompany.SmartHR.Application.ViewModels;

namespace AcmeCompany.SmartHR.Application.QueryHandlers.EmployeeQuerys;

public interface IEmployeeQueryHandler
{
    ValueTask<EmployeeDisplay?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<EmployeeDisplay>> ListWithPaginationAsync(int pageNumber, int rowsOfPage, CancellationToken cancellationToken = default);
}
