using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace AcmeCompany.SmartHR.Application.QueryHandlers.Common;

internal sealed class ConnectionFactory : IConnectionFactory
{
    private readonly string _connectionString = null!;

    public ConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Default");
    }


    public async ValueTask<IDbConnection> GetConnection(CancellationToken cancellationToken = default)
    {
        var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        return connection;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
