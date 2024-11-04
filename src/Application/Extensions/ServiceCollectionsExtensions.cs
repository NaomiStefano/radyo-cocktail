using System.Reflection;
using Cocktail.Application.Behaviors;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cocktail.Application.Extensions;

public static class ServiceCollectionsExtensions
{
    public static IServiceCollection AddApplication( this IServiceCollection services, IConfiguration configuration )
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where( p => !p.IsDynamic && p.Location.Contains( nameof(Domain.Aggregates.Cocktail) ) ).ToArray();
        return services
              .AddMapping(Assembly.GetExecutingAssembly())
              .AddMediatR(  cfg => cfg.RegisterServicesFromAssemblies( assemblies ) )
              .AddTransient( typeof( IPipelineBehavior< , > ), typeof( RequestValidationBehavior< , > ) )
              .AddValidatorsFromAssemblies( assemblies );
    }

    private static IServiceCollection AddMapping(this IServiceCollection services, Assembly assembly, int? maxDeep = 3)
    {
        TypeAdapterConfig.GlobalSettings.Default.MaxDepth(maxDeep);

        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(Mappers.IMapFrom<>)))
            .ToList();

        var config = TypeAdapterConfig.GlobalSettings;
        
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod("Mapping")
                             ?? type.GetInterface("IMapFrom`1")!.GetMethod("Mapping");

            methodInfo?.Invoke(instance, new object[] { config });
        }
        return services;
    }
}