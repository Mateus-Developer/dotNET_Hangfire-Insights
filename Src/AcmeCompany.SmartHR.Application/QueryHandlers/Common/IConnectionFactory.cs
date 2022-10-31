using System.Data;

namespace AcmeCompany.SmartHR.Application.QueryHandlers.Common;

public interface IConnectionFactory : IDisposable
{
    ValueTask<IDbConnection> GetConnection(CancellationToken cancellationToken = default);
}
