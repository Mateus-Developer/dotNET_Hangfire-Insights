using AcmeCompany.SmartHR.Application.QueryHandlers.Common;
using AcmeCompany.SmartHR.Application.ViewModels;
using Dapper;
using System.Text;

namespace AcmeCompany.SmartHR.Application.QueryHandlers.EmployeeQuerys;

internal sealed class EmployeeQueryHandler : IEmployeeQueryHandler
{
    private readonly IConnectionFactory _connectionFactory;

    public EmployeeQueryHandler(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public async ValueTask<EmployeeDisplay?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var connection = await _connectionFactory.GetConnection(cancellationToken);
        var query = new StringBuilder()
            .Append("SELECT [Id],[CreatedOn],[UpdatedOn],[FullName],[Email],[HireDate],[JobTitle],[Salary] FROM [SmartHR].[HumanResources].[Employees] ")
            .Append("WHERE Id = @Id");

        return await connection.QueryFirstOrDefaultAsync<EmployeeDisplay>(
            sql: query.ToString(),
            param: new { Id = id },
            commandType: System.Data.CommandType.Text);
    }

    public async ValueTask<IEnumerable<EmployeeDisplay>> ListWithPaginationAsync(int pageNumber, int rowsOfPage, CancellationToken cancellationToken = default)
    {
        using var connection = await _connectionFactory.GetConnection(cancellationToken);
        var query = new StringBuilder()
            .Append("SELECT [Id],[CreatedOn],[UpdatedOn],[FullName],[Email],[HireDate],[JobTitle],[Salary] FROM [SmartHR].[HumanResources].[Employees] ")
            .Append("ORDER BY CreatedOn " )
            .Append("OFFSET (@PageNumber - 1) * @RowsOfPage ROWS ")
            .Append("FETCH NEXT @RowsOfPage ROWS ONLY");

        return await connection.QueryAsync<EmployeeDisplay>(
            sql: query.ToString(),
            param: new
            {
                PageNumber = pageNumber,
                RowsOfPage = rowsOfPage
            },
            commandType: System.Data.CommandType.Text);
    }
}
