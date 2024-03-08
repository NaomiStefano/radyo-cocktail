using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.IntegrationTest.Connectors;

public class SqliteConnector<T>() : DatabaseConnector<T> where T : DbContext
{
    private SqliteConnection? _dbConnection;
    private string? _connectionString;

    public override void Configure(IServiceCollection services, IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString(typeof(T).Name);
        _dbConnection = SetupConnection(services) as SqliteConnection;
    }

    private DbConnection? GetConnection() => _dbConnection;

    protected virtual DbConnection SetupConnection(IServiceCollection services)
    {
        // In-memory database only exists while the connection is open
        var connection = new SqliteConnection(_connectionString);
        connection.Open();

        ReplaceDbContext(services, options =>
        {
            options.UseSqlite(connection);
            options.ConfigureWarnings(warningsConfigurationBuilderAction => warningsConfigurationBuilderAction.Log((RelationalEventId.AmbientTransactionWarning, LogLevel.Debug)));
        });

        return connection;
    }

    public override void Dispose()
    {
        base.Dispose();
        var connection = GetConnection();
        connection?.Close();
        connection?.Dispose();
        GC.SuppressFinalize(this);
    }
}