using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.IntegrationTest.Connectors;

public interface IDatabaseConnector : IDisposable
{
    void Use(IServiceProvider serviceProvider);
    void Configure(IServiceCollection serviceCollection, IConfiguration configuration);
}
