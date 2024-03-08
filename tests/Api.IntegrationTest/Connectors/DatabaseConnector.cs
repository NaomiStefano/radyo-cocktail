using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.IntegrationTest.Connectors;

public abstract class DatabaseConnector<TDbContext>(bool dropAndCreateDatabase = true)
    : IDatabaseConnector
    where TDbContext : DbContext
{
    private IServiceProvider _serviceProvider;
    private TDbContext _context;

    public abstract void Configure(IServiceCollection serviceCollection, IConfiguration configuration);

    public void Use(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _context = _serviceProvider.GetRequiredService<TDbContext>();
        if (dropAndCreateDatabase)
        {
            EnsureDbCreated();
        }
    }
    
    protected virtual void ReplaceDbContext(IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction = null)
    {
        // Remove the app DbContext registration.
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<TDbContext>));

        if (descriptor != null)
        {
            services.Remove(descriptor);
        }

        services.AddDbContext<TDbContext>(optionsAction);
    }

    private void EnsureDbCreated()
    {
        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public virtual void Dispose()
    {
        GC.SuppressFinalize(this);
        if (dropAndCreateDatabase)
        {
            _context.Database.EnsureDeleted();
        }
    }
}